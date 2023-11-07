using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto;

namespace lll_seer_launcher.core.Service
{
    class AnalyzeRecvDataService
    {
        /// <summary>
        /// 加载当前账号所持有的称号
        /// </summary>
        /// <param name="recvDataHeadInfo">称号封包</param>
        public static void AnalyzeAchieveTitleList(HeadInfo recvDataHeadInfo)
        {
            int achieveTitlesLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 0,4));
            ArrayList achieveTitles = new ArrayList();
            int achieveTitleId;
            for (int i = 1; i <= achieveTitlesLen; i++)
            {
                achieveTitleId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, i*4, 4));
                if(achieveTitleId > 150 ) achieveTitles.Add(achieveTitleId);
            }
            if(GlobalVariable.userAchieveTitleDictionary.ContainsKey(GlobalVariable.loginUserInfo.userId))
            {
                GlobalVariable.userAchieveTitleDictionary[GlobalVariable.loginUserInfo.userId] = achieveTitles;
            }
            else
            {
                GlobalVariable.userAchieveTitleDictionary.Add(GlobalVariable.loginUserInfo.userId, achieveTitles);
            }
            //GlobalVariable.userAchieveTitleDictionary.TryGetValue(GlobalVariable.userId, out achieveTitles);
            GlobalVariable.initSuitGroupBoxsCompleteFlg[0] = true;
        }

        /// <summary>
        /// 加载当前账号所持有的装备
        /// </summary>
        /// <param name="recvDataHeadInfo">装备封包</param>
        public static void AnalyzeSuit(HeadInfo recvDataHeadInfo)
        {
            int clothLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 0, 4));
            Dictionary<int, int> clothDic = new Dictionary<int, int>();
            int clothId;
            int index = 4;
            for (int i = 0; i < clothLen; i++)
            {
                clothId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
                if(ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index + 4 , 4)) == 1)clothDic.Add(clothId, clothId);
                index += 20;
            }
            if (GlobalVariable.userSuitClothDictionary.ContainsKey(GlobalVariable.loginUserInfo.userId))
            {
                GlobalVariable.userSuitClothDictionary[GlobalVariable.loginUserInfo.userId] = clothDic;
            }
            else
            {
                GlobalVariable.userSuitClothDictionary.Add(GlobalVariable.loginUserInfo.userId, clothDic);
            }
            GlobalVariable.initSuitGroupBoxsCompleteFlg[1] = true;
            //Console.WriteLine(BitConverter.ToString(recvDataHeadInfo.decryptData));
        }

        public static void AnalyzeSimpeInfo(HeadInfo recvDataHeadInfo)
        {
            int index = 0;
            int userId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
            if (userId != GlobalVariable.loginUserInfo.userId)
            {
                index += 73;
                //获取当前装备的cloth数量并计算火焰对应的位置
                index += ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4)) * 8 + 8;
                //获取当前玩家的火焰类型
                int fireBuff = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index++, 1));
                int loginTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
                index += 4;
                int lastOfflineTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
                //如果当前正在进行借绿火，且判断当前玩家是否为需要借火的目标
                if (GlobalVariable.fireBuffCopyObj.copyGreenBuff[0] && GlobalVariable.fireBuffCopyObj.copyGreenBuffUserId == userId)
                {
                    //如果装备的是绿火，则设置当前的借火flag里的第三个flag（是否为绿火为真）
                    //且判断是否在线
                    if (loginTime > lastOfflineTime && (fireBuff == 5 || fireBuff == 6))
                    {
                        GlobalVariable.fireBuffCopyObj.copyGreenBuff[2] = true;
                        Logger.Log("copyFire", "快速借火--绿火");
                    }
                    //将借火flag里的第二个flag设为false，告诉借火循环查询目标用户信息完毕
                    GlobalVariable.fireBuffCopyObj.copyGreenBuff[1] = false;
                }
                //如果未在借绿火且此玩家装备火焰效果为绿火则加入绿火玩家dic里
                else if (!GlobalVariable.fireBuffCopyObj.greenFireBuffDic.ContainsKey(userId) && (fireBuff == 5 || fireBuff == 6))
                {
                    GlobalVariable.fireBuffCopyObj.greenFireBuffDic.Add(userId, fireBuff);
                }
            }
        }

        public static void AnalyzeOnCopyFire(HeadInfo recvDataHeadInfo)
        {
            //获取火焰类型
            int type = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 4, 1));
            //获取当前包的用户id
            int userId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 0, 4));
            //如果是当前登录用户，则进行倒计时
            if (userId == GlobalVariable.loginUserInfo.userId)
            {
                //判断当前是否有正在倒计时的火焰
                if (GlobalVariable.fireCountThread != null) GlobalVariable.fireCountThread.Abort();
                if (type == 0) return;
                //启动一个线程进行火焰倒计时
                GlobalVariable.fireCountThread = new Thread(() =>
                {
                    GlobalVariable.mainForm.StartCountFireBuff(type > 5 ? 30 : 10);
                });
                GlobalVariable.fireCountThread.Start();
            }
        }

        public static void AnalyzeOnEnterMap(HeadInfo recvDataHeadInfo)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.SetUserInfoForFireType(0, recvDataHeadInfo.decryptData);
            if (userInfo.userId == GlobalVariable.loginUserInfo.userId) return;
            //绿火→放入字典存储
            if (userInfo.fireBuffType > 0 && (userInfo.fireBuffType == 5 || userInfo.fireBuffType == 6))
            {
                if (GlobalVariable.fireBuffCopyObj.greenFireBuffDic.ContainsKey(userInfo.userId))
                {
                    GlobalVariable.fireBuffCopyObj.greenFireBuffDic[userInfo.userId] = userInfo.fireBuffType;
                }
                else
                {
                    GlobalVariable.fireBuffCopyObj.greenFireBuffDic.Add(userInfo.userId, userInfo.fireBuffType);
                }
            }
        }

        public static void AnalyzeMapPlayerList(HeadInfo recvDataHeadInfo)
        {
            try
            {
                //获取当前地图内玩家总数
                int playerCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 0, 4));
                if (playerCount <= 0) return;
                //对当前地图内的玩家进行火焰统计
                int index = 4;
                for (int i = 0; i < playerCount; i++)
                {
                    UserInfo userInfo = new UserInfo();
                    index = userInfo.SetUserInfoForFireType(index, recvDataHeadInfo.decryptData);
                    if (userInfo.userId == GlobalVariable.loginUserInfo.userId) continue;
                    //绿火→放入字典存储
                    if (userInfo.fireBuffType > 0 && (userInfo.fireBuffType == 5 || userInfo.fireBuffType == 6))
                    {
                        if (GlobalVariable.fireBuffCopyObj.greenFireBuffDic.ContainsKey(userInfo.userId))
                        {
                            GlobalVariable.fireBuffCopyObj.greenFireBuffDic[userInfo.userId] = userInfo.fireBuffType;
                        }
                        else
                        {
                            GlobalVariable.fireBuffCopyObj.greenFireBuffDic.Add(userInfo.userId, userInfo.fireBuffType);
                        }
                    }
                    //其他火焰，如当前按下了借火菜单按钮，根据需要借的火类型进行借火判断
                    else if (userInfo.fireBuffType > 0 && userInfo.fireBuffType == GlobalVariable.fireBuffCopyObj.copyFireBuffType)
                    {
                        GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.FIRE_ACT_COPY, new int[1] { userInfo.userId });
                        GlobalVariable.fireBuffCopyObj.copyFireBuffType = 0;
                        //GlobalVariable.fireBuffCopyObj.mins = fireBuff == 5 ? 10 : 30;
                        Logger.Log("copyFire", "快速借火--蓝火/金火/紫火");
                    }
                    //Console.WriteLine($"userId:{userId}  fireBuff:{fireBuff}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (GlobalVariable.fireBuffCopyObj.copyFireBuffType != 0)
                {
                    MessageBox.Show("亲爱的小赛尔，" +
                        "\n当前地图暂无对应火可借，" +
                        "\n请尝试前往人多的地图进行借火!");
                    GlobalVariable.fireBuffCopyObj.copyFireBuffType = 0;
                }
            }
        }
    }
}
