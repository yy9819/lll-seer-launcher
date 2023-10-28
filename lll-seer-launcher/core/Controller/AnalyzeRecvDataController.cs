using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using lll_seer_launcher.core.Servise;
using lll_seer_launcher.core.Servise.AutoFightService;
using lll_seer_launcher.core.Servise.FightNoteServise;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Controller
{
    class AnalyzeRecvDataController
    {
        /// <summary>
        /// 对需在字典里调用的方法设置一个通用格式委托方法
        /// </summary>
        /// <param name="recvDataHeadInfo">封包</param>
        private delegate void AnalyzeRecvDataMethod(HeadInfo recvDataHeadInfo);

        /// <summary>
        /// key:cmdId
        /// value:需要执行的封包controller
        /// </summary>
        private Dictionary<int, AnalyzeRecvDataMethod> methodDictionary;
        public AnalyzeRecvDataController()
        {
            methodDictionary = new Dictionary<int, AnalyzeRecvDataMethod>
            {
                /*==========================================玩家・地图相关解析============================================*/
                { CmdId.ACHIEVETITLELIST, (param) => AnalyzeAchieveTitleList(param) },
                { CmdId.ITEM_LIST, (param) =>  AnalyzeItemList(param) },
                { CmdId.LIST_MAP_PLAYER , (param) =>  AnlyzeMapPlayerList(param)},
                { CmdId.ENTER_MAP , (param) =>  AnlyzeEnterMap(param)},
                { CmdId.GET_SIM_USERINFO , (param) =>  AnlyzeSimpleInfo(param)},
                { CmdId.FIRE_ACT_NOTICE , (param) =>  AnlyzeCopyFireNOTICE(param)},
                /*==========================================精灵相关解析============================================*/
                { CmdId.GET_PET_INFO_BY_ONCE , (param) =>  AnlyzeGetPetInfoByOnce(param)},
                /*==========================================战斗相关解析============================================*/
                { CmdId.MIBAO_FIGHT , (param) =>  AnlyzeMibaoFight(param)},
                { CmdId.READY_TO_FIGHT , (param) =>  AnlyzeReadyToFight(param)},
                { CmdId.NOTE_START_FIGHT, (param) =>  AnlyzeNoteStartFight(param)},
                { CmdId.NOTE_USE_SKILL , (param) =>  AnlyzeNoteUseSkill(param)},
                { CmdId.CHANGE_PET , (param) =>  AnlyzeChangePet(param)},
                { CmdId.FIGHT_OVER , (param) =>  AnlyzeOnFightOver(param)},
                //{ CmdId. , (param) =>  AnlyzeReadyToFight(param)},
            };
        }

        private static readonly object lockObject = new object();

        /// <summary>
        /// 封包解析controller
        /// 根据headInfo内的cmdId选择调用相应的解析controller
        /// </summary>
        /// <param name="recvDataHeadInfo">封包信息</param>
        public void RunAnalyzeRecvDataMethod(HeadInfo recvDataHeadInfo)
        {
            GlobalVariable.gameConfigFlag.disableRecv = recvDataHeadInfo.cmdId == CmdId.NOTE_READY_TO_FIGHT 
                && (GlobalVariable.gameConfigFlag.shouldDisableRecv || GlobalVariable.gameConfigFlag.lowerHpFlag);
            if (methodDictionary.TryGetValue(recvDataHeadInfo.cmdId, out AnalyzeRecvDataMethod method))
            {
                Thread methodThread = new Thread(() => { method(recvDataHeadInfo); });
                methodThread.Start();
                //Console.WriteLine($"Start method for value {recvDataHeadInfo.cmdId}");
            }
            else
            {
                //Console.WriteLine($"No method defined for value {recvDataHeadInfo.cmdId}");
            }
        }

        /// <summary>
        /// 称号封包解析controller
        /// </summary>
        /// <param name="recvDataHeadInfo">带有称号信息的封包</param>
        private void AnalyzeAchieveTitleList(HeadInfo recvDataHeadInfo)
        {

            AnalyzeRecvDataServise.AnalyzeAchieveTitleList(recvDataHeadInfo);
        }

        /// <summary>
        /// 物品查询封包解析controller
        /// 根据传入的封包判断执行的解析程序
        /// </summary>
        /// <param name="recvDataHeadInfo">带有物品list的封包</param>
        private void AnalyzeItemList(HeadInfo recvDataHeadInfo)
        {
            if (!GlobalVariable.userSuitClothDictionary.ContainsKey(GlobalVariable.loginUserInfo.userId))
            {
                GlobalVariable.userSuitClothDictionary.Add(GlobalVariable.loginUserInfo.userId, new Dictionary<int, int>());
            }
            if (recvDataHeadInfo.decryptData.Length <= 4) return;
            int fristItemId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 4, 4));
            if (fristItemId >= 1300083 && fristItemId < 1400000)
            {
                AnalyzeRecvDataServise.AnalyzeSuit(recvDataHeadInfo);
            }
        }

        /// <summary>
        /// 解析当前地图上角色消息
        /// -1 当前地图角色火焰持有情况
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        private void AnlyzeMapPlayerList(HeadInfo recvDataHeadInfo)
        {
            try
            {
                //获取当前地图内玩家总数
                int playerCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 0, 4));
                if (playerCount > 0)
                {
                    //对当前地图内的玩家进行火焰统计
                    int index = 4;
                    for (int i = 0; i < playerCount; i++)
                    {
                        UserInfo userInfo = new UserInfo();
                        index = userInfo.SetUserInfoForFireType(index, recvDataHeadInfo.decryptData);
                        if (userInfo.userId != GlobalVariable.loginUserInfo.userId)
                        {
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
                        }
                        //Console.WriteLine($"userId:{userId}  fireBuff:{fireBuff}");
                    }
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
        private void AnlyzeEnterMap(HeadInfo recvDataHeadInfo)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.SetUserInfoForFireType(0, recvDataHeadInfo.decryptData);
            if (userInfo.userId != GlobalVariable.loginUserInfo.userId)
            {
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
        }
        /// <summary>
        /// 对火焰获得信息进行解析
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        private void AnlyzeCopyFireNOTICE(HeadInfo recvDataHeadInfo)
        {
            //获取火焰类型
            int type = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 4, 1));
            //获取当前包的用户id
            int userId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 0, 4));
            //如果是当前登录用户，则进行倒计时
            if(userId == GlobalVariable.loginUserInfo.userId)
            {
                //判断当前是否有正在倒计时的火焰
                if(GlobalVariable.fireCountThread != null) GlobalVariable.fireCountThread.Abort();
                //启动一个线程进行火焰倒计时
                GlobalVariable.fireCountThread = new Thread(() =>
                {
                    GlobalVariable.mainForm.StartCountFireBuff(type > 5 ? 30 : 10);
                });
                GlobalVariable.fireCountThread.Start();
                
            }
        }
        /// <summary>
        /// 解析指定玩家的角色信息
        /// -1 解析携带的火焰
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        private void AnlyzeSimpleInfo(HeadInfo recvDataHeadInfo)
        {
            int index = 0;
            int userId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
            if (userId != GlobalVariable.loginUserInfo.userId)
            {
                index += 73;
                //获取当前装备的cloth数量并计算火焰对应的位置
                index += ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4)) * 8 + 8;
                //获取当前玩家的火焰类型
                int fireBuff = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 1));
                //如果当前正在进行借绿火，且判断当前玩家是否为需要借火的目标
                if (GlobalVariable.fireBuffCopyObj.copyGreenBuff[0] && GlobalVariable.fireBuffCopyObj.copyGreenBuffUserId == userId)
                {
                    //如果装备的是绿火，则设置当前的借火flag里的第三个flag（是否为绿火为真）
                    if(fireBuff == 5 || fireBuff == 6)
                    {
                        GlobalVariable.fireBuffCopyObj.copyGreenBuff[2] = true;
                        Logger.Log("copyFire","快速借火--绿火");
                    }
                    //将借火flag里的第二个flag设为false，告诉借火循环查询目标用户信息完毕
                    GlobalVariable.fireBuffCopyObj.copyGreenBuff[1] = false;
                }
                //如果未在借绿火且此玩家装备火焰效果为绿火则加入绿火玩家dic里
                else if(!GlobalVariable.fireBuffCopyObj.greenFireBuffDic.ContainsKey(userId) && (fireBuff == 5 || fireBuff == 6))
                {
                    GlobalVariable.fireBuffCopyObj.greenFireBuffDic.Add(userId, fireBuff);
                }
            }
            
        }


        /// <summary>
        /// 解析当前背包内的精灵
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        private void AnlyzeGetPetInfoByOnce(HeadInfo recvDataHeadInfo)
        {
            lock (lockObject)
            {
                GetPetInfoService.OnGetPetInfoByOnce(recvDataHeadInfo);
                if (GlobalVariable.gameConfigFlag.lowerHpFlag)
                {
                    LowerHPServise.OnGetPetInfoByOnce();
                }
            }
        }
        private void AnlyzeMibaoFight(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                LowerHPServise.OnMibaoFight();
            }
        }
        private void AnlyzeReadyToFight(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                LowerHPServise.OnReadyToFight();
            }
        }
        private void AnlyzeNoteStartFight(HeadInfo recvDataHeadInfo)
        {
            GlobalVariable.fightTurn = 0;
            Dictionary<string,FightPetInfo> fightPlayers = FightNoteServise.OnNoteStartFight(recvDataHeadInfo);
            GlobalVariable.mainForm.SetPetFightNote(fightPlayers);
            //foreach(var item in fightPlayers["loginPlayer"].petBagMarkArr.Keys)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine($"loginPlayer:{fightPlayers["loginPlayer"].userId}-{fightPlayers["loginPlayer"].petName}" +
            //    $" otherPlayer:{fightPlayers["otherPlayer"].userId}-{fightPlayers["otherPlayer"].petName}");
        }
        private void AnlyzeChangePet(HeadInfo recvDataHeadInfo)
        {
            if(recvDataHeadInfo.decryptData.Length > 4)
            {
                ChangePetInfo changePetInfo = FightNoteServise.OnChangePet(recvDataHeadInfo);
                GlobalVariable.mainForm.OnChangePet(changePetInfo);
                if (GlobalVariable.gameConfigFlag.autoUseSkillFlg) AutoUseSkillServise.OnChangePet(changePetInfo);
                //Console.WriteLine($"loginPlayer:{changePetInfo.userId}-{changePetInfo.petName}");
                //    $" otherPlayer:{fightPlayers["otherPlayer"].userId}-{fightPlayers["otherPlayer"].petName}");
            }
        }
        private void AnlyzeNoteUseSkill(HeadInfo recvDataHeadInfo)
        {
            GlobalVariable.fightTurn++;
            Dictionary<string, AttackValueInfo> players = FightNoteServise.OnNoteUseSkill(recvDataHeadInfo);
            GlobalVariable.mainForm.OnUseSkill(players);
            if (GlobalVariable.gameConfigFlag.lowerHpFlag) LowerHPServise.OnNoteUseSkill(players);
            if (GlobalVariable.gameConfigFlag.autoUseSkillFlg && players["otherPlayer"].remainHP != 0) AutoUseSkillServise.OnUseSkill(players["loginPlayer"]);
            //Console.WriteLine($"fristPlayerInfo:{fristPlayerInfo.userId}  secondPlayerInfo:{secondPlayerInfo.userId}");
        }
        private void AnlyzeOnFightOver(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                LowerHPServise.OnFightOver();
            }else if (GlobalVariable.gameConfigFlag.autoChargeFlag  && GlobalVariable.loginUserInfo.vipLevel == 0
                 && GlobalVariable.gameConfigFlag.autoChargeFlag)
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_CURE_FREE, new int[0]);
            }

            if (GlobalVariable.gameConfigFlag.autoUseSkillAddPPFlg)
            {
                GlobalVariable.mainForm.StopLoopUseSkill();
            }

            GlobalVariable.mainForm.InitFightNote();
        }
    }
}
