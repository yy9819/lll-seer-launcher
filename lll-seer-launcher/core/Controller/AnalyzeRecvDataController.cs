using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using lll_seer_launcher.core.Servise;
using lll_seer_launcher.core.Dto;
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
                { CmdId.ACHIEVETITLELIST, (param) => AnalyzeAchieveTitleList(param) },
                { CmdId.ITEM_LIST, (param) =>  AnalyzeItemList(param) },
                { CmdId.LIST_MAP_PLAYER , (param) =>  AnlyzeMapPlayerList(param)},
                { CmdId.ENTER_MAP , (param) =>  AnlyzeEnterMap(param)},
                { CmdId.GET_SIM_USERINFO , (param) =>  AnlyzeSimpleInfo(param)},
                { CmdId.FIRE_ACT_NOTICE , (param) =>  AnlyzeCopyFireNOTICE(param)},
                { CmdId.GET_PET_INFO_BY_ONCE , (param) =>  AnlyzeGetPetInfoByOnce(param)},
                { CmdId.FIGHT_OVER , (param) =>  AnlyzeOnFightOver(param)},
                { CmdId.MIBAO_FIGHT , (param) =>  AnlyzeMibaoFight(param)},
                { CmdId.READY_TO_FIGHT , (param) =>  AnlyzeReadyToFight(param)},
                { CmdId.NOTE_USE_SKILL , (param) =>  AnlyzeNoteUseSkill(param)},
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
                GlobalVariable.pets.Clear();
                List<PetInfo> tmpPetBags = new List<PetInfo>();
                int index = 0;
                int petCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
                index += 4;
                for (int i = 0; i < petCount; i++)
                {
                    PetInfo petInfo = new PetInfo();
                    index = petInfo.SetPetInfo(index, recvDataHeadInfo.decryptData);
                    GlobalVariable.pets.Add(petInfo);
                }
                if (GlobalVariable.gameConfigFlag.lowerHpFlag)
                {
                    if(GlobalVariable.pets.Count > 0)
                    {
                        GlobalVariable.mainForm.SetLowerHpStatus("精灵列表读取成功，开始压血！");
                        GlobalVariable.gameConfigFlag.lowerHpPetLen = 0;
                        while (GlobalVariable.pets.Count > GlobalVariable.gameConfigFlag.lowerHpPetLen)
                        {
                            if (GlobalVariable.pets[GlobalVariable.gameConfigFlag.lowerHpPetLen].hp > 0) break;
                            GlobalVariable.gameConfigFlag.lowerHpPetLen++;
                        }
                        if(GlobalVariable.pets.Count > GlobalVariable.gameConfigFlag.lowerHpPetLen)
                        {
                            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.MIBAO_FIGHT, new int[1] { 8692 });
                        }
                        else
                        {
                            new Task(() => {
                                GlobalVariable.mainForm.SetLowerHpStatus("战斗结束，开始回血");
                                GlobalVariable.gameConfigFlag.lowerHpFlag = false;
                                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.ITEM_BUY, new int[2] { 300016, GlobalVariable.pets.Count });
                                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.ITEM_BUY, new int[2] { 300011, GlobalVariable.pets.Count });
                                foreach (var pet in GlobalVariable.pets)
                                {
                                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM_OUT_OF_FIGHT,
                                        new int[2] { pet.catchTime, 300011 });
                                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM_OUT_OF_FIGHT,
                                        new int[2] { pet.catchTime, 300016 });
                                }
                                GlobalVariable.mainForm.SetLowerHpStatus("压血完成!");
                                Thread.Sleep(2000);
                                GlobalVariable.mainForm.SetLowerHpStatus("");
                            }).Start();
                        }

                    }
                    else
                    {
                        GlobalVariable.mainForm.SetLowerHpStatus("");
                        MessageBox.Show("亲爱的小赛尔，\n你的背包里没有精灵，不能进行压血哟~");
                    }
                }
                //foreach (PetInfo petinfo in GlobalVariable.pets)
                //{
                //    Console.WriteLine($"id:{petinfo.petId} name:{petinfo.petName} catchTime:{petinfo.catchTime}");
                //}
                int awaitPetCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
                GlobalVariable.awaitPets.Clear();
                if (awaitPetCount > 0)
                {
                    index += 4;
                    for (int i = 0; i < awaitPetCount; i++)
                    {
                        PetInfo petInfo = new PetInfo();
                        index = petInfo.SetPetInfo(index, recvDataHeadInfo.decryptData);
                        GlobalVariable.awaitPets.Add(petInfo);
                    }
                    //foreach (PetInfo petinfo in GlobalVariable.awaitPets)
                    //{
                    //    Console.WriteLine($"id:{petinfo.petId} name:{petinfo.petName} catchTime:{petinfo.catchTime}");
                    //}
                }
            }
        }
        private void AnlyzeMibaoFight(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                new Task(() => {
                    GlobalVariable.mainForm.SetLowerHpStatus("进入战斗!");
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.READY_TO_FIGHT, new int[0]);
                }).Start();
            }
        }
        private void AnlyzeReadyToFight(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                new Task(() => {
                    GlobalVariable.mainForm.SetLowerHpStatus($"正在进行第{GlobalVariable.gameConfigFlag.lowerHpPetLen + 1}只精灵的压血");
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL, new int[1] { GlobalVariable.pets[GlobalVariable.gameConfigFlag.lowerHpPetLen].skillArray[0] });
                    //for(int i = 1; i < GlobalVariable.pets.Count; i++)
                    //{
                    //    GlobalVariable.mainForm.SetLowerHpStatus($"正在进行第{i+1}只精灵的压血");
                    //    if(GlobalVariable.pets[i].hp > 0)
                    //    {
                    //        Thread.Sleep(500);
                    //        GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.CHANGE_PET, new int[1] { GlobalVariable.pets[i].catchTime });
                    //        Thread.Sleep(500);
                    //        GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL, new int[1] { GlobalVariable.pets[i].skillArray[0] });
                    //    }
                    //}
                }).Start();
            }
        }

        private void AnlyzeNoteUseSkill(HeadInfo recvDataHeadInfo)
        {
            AttackValueInfo fristPlayerInfo = new AttackValueInfo();
            AttackValueInfo secondPlayerInfo = new AttackValueInfo();
            int index = 0;
            index = fristPlayerInfo.SetAttackValueInfo(index, recvDataHeadInfo.decryptData);
            int hp;
            if(fristPlayerInfo.userId  == GlobalVariable.loginUserInfo.userId)
            {
                hp = fristPlayerInfo.remainHP;
            }
            else
            {
                secondPlayerInfo.SetAttackValueInfo(index, recvDataHeadInfo.decryptData);
                hp = secondPlayerInfo.remainHP;
            }
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                if(hp > 0)
                {
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL, 
                        new int[1] { GlobalVariable.pets[GlobalVariable.gameConfigFlag.lowerHpPetLen].skillArray[0] });
                }
                else if (GlobalVariable.pets.Count > ++GlobalVariable.gameConfigFlag.lowerHpPetLen )
                {
                    while (GlobalVariable.pets.Count > GlobalVariable.gameConfigFlag.lowerHpPetLen)
                    {
                        if (GlobalVariable.pets[GlobalVariable.gameConfigFlag.lowerHpPetLen].hp > 0) break;
                        GlobalVariable.gameConfigFlag.lowerHpPetLen++;
                    }
                    if (GlobalVariable.pets.Count > GlobalVariable.gameConfigFlag.lowerHpPetLen)
                    {
                        GlobalVariable.mainForm.SetLowerHpStatus($"正在进行第{GlobalVariable.gameConfigFlag.lowerHpPetLen + 1}只精灵的压血");
                        GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.CHANGE_PET,
                        new int[1] { GlobalVariable.pets[GlobalVariable.gameConfigFlag.lowerHpPetLen].catchTime });
                        Thread.Sleep(100);
                        GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL,
                            new int[1] { GlobalVariable.pets[GlobalVariable.gameConfigFlag.lowerHpPetLen].skillArray[0] });
                    }
                }
                else
                {
                    GlobalVariable.gameConfigFlag.lowerHpPetLen = 0;
                }
            }
            //Console.WriteLine($"fristPlayerInfo:{fristPlayerInfo.userId}  secondPlayerInfo:{secondPlayerInfo.userId}");
        }
        private void AnlyzeOnFightOver(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                new Task(()=>{
                    GlobalVariable.mainForm.SetLowerHpStatus("战斗结束，开始回血");
                    GlobalVariable.gameConfigFlag.lowerHpFlag = false;
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.ITEM_BUY, new int[2] { 300016, GlobalVariable.pets.Count });
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.ITEM_BUY, new int[2] { 300011, GlobalVariable.pets.Count });
                    foreach(var pet in GlobalVariable.pets)
                    {
                        GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM_OUT_OF_FIGHT,
                            new int[2] { pet.catchTime, 300011 });
                        GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM_OUT_OF_FIGHT,
                            new int[2] { pet.catchTime, 300016 });
                    }
                    GlobalVariable.mainForm.SetLowerHpStatus("压血完成!");
                    Thread.Sleep(2000);
                    GlobalVariable.mainForm.SetLowerHpStatus("");
                }).Start();
            }else if (GlobalVariable.gameConfigFlag.autoChargeFlag  && GlobalVariable.loginUserInfo.vipLevel == 0
                 && GlobalVariable.gameConfigFlag.autoChargeFlag)
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_CURE_FREE, new int[0]);
            }

        }
    }
}
