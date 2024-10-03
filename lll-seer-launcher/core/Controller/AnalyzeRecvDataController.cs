using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using lll_seer_launcher.core.Service;
using lll_seer_launcher.core.Service.PetService;
using lll_seer_launcher.core.Service.AutoFightService;
using lll_seer_launcher.core.Service.FightNoteService;
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
                { CmdId.GET_PET_INFO , (param) =>  AnlyzeGetPetInfo(param)},
                { CmdId.GET_PET_LIST , (param) =>  AnlyzeGetPetList(param)},
                { CmdId.GET_LOVE_PET_LIST , (param) =>  AnlyzeGetLovePetList(param)},
                /*==========================================战斗相关解析============================================*/
                //{ CmdId.MIBAO_FIGHT , (param) =>  AnlyzeMibaoFight(param)},
                { CmdId.NOTE_READY_TO_FIGHT , (param) =>  AnlyzeNoteReadyToFight(param)},
                { CmdId.READY_TO_FIGHT , (param) =>  AnlyzeReadyToFight(param)},
                { CmdId.NOTE_START_FIGHT, (param) =>  AnlyzeNoteStartFight(param)},
                { CmdId.NOTE_USE_SKILL , (param) =>  AnlyzeNoteUseSkill(param)},
                { CmdId.USE_PET_ITEM , (param) =>  AnlyzeUsePetItem(param)},
                { CmdId.CHANGE_PET , (param) =>  AnlyzeChangePet(param)},
                { CmdId.FIGHT_OVER , (param) =>  AnlyzeOnFightOver(param)},
                { CmdId.PET_CURE_FREE , (param) =>  AnlyzeOnPetCureFree(param)},
                //{ CmdId. , (param) =>  AnlyzeReadyToFight(param)},
            };
        }

        /// <summary>
        /// 封包解析controller
        /// 根据headInfo内的cmdId选择调用相应的解析controller
        /// </summary>
        /// <param name="recvDataHeadInfo">封包信息</param>
        public void RunAnalyzeRecvDataMethod(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.isRunningScript)
            {
                GlobalVariable.gameConfigFlag.disableRecv = recvDataHeadInfo.cmdId != CmdId.SYSTEM_TIME;
            }
            else
            {
                GlobalVariable.gameConfigFlag.disableRecv = recvDataHeadInfo.cmdId == CmdId.NOTE_READY_TO_FIGHT
                   && (GlobalVariable.gameConfigFlag.shouldDisableRecv || GlobalVariable.gameConfigFlag.lowerHpFlag);
            }
            if (methodDictionary.TryGetValue(recvDataHeadInfo.cmdId, out AnalyzeRecvDataMethod method))
            {
                Thread methodThread = new Thread(() => { method(recvDataHeadInfo); });
                methodThread.Start();
            }
        }
        /// <summary>
        /// 称号封包解析controller
        /// </summary>
        /// <param name="recvDataHeadInfo">带有称号信息的封包</param>
        private void AnalyzeAchieveTitleList(HeadInfo recvDataHeadInfo)
        {

            AnalyzeRecvDataService.AnalyzeAchieveTitleList(recvDataHeadInfo);
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
                AnalyzeRecvDataService.AnalyzeSuit(recvDataHeadInfo);
            }
        }

        /// <summary>
        /// 解析当前地图上角色消息
        /// -1 当前地图角色火焰持有情况
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        private void AnlyzeMapPlayerList(HeadInfo recvDataHeadInfo)
        {
            AnalyzeRecvDataService.AnalyzeMapPlayerList(recvDataHeadInfo);
        }
        /// <summary>
        /// 当用户进入地图时的解析
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        private void AnlyzeEnterMap(HeadInfo recvDataHeadInfo)
        {
            AnalyzeRecvDataService.AnalyzeOnEnterMap(recvDataHeadInfo);
        }
        /// <summary>
        /// 对火焰获得信息进行解析
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        private void AnlyzeCopyFireNOTICE(HeadInfo recvDataHeadInfo)
        {
            AnalyzeRecvDataService.AnalyzeOnCopyFire(recvDataHeadInfo);
        }
        /// <summary>
        /// 解析指定玩家的角色信息
        /// -1 解析携带的火焰
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        private void AnlyzeSimpleInfo(HeadInfo recvDataHeadInfo)
        {
            AnalyzeRecvDataService.AnalyzeSimpeInfo(recvDataHeadInfo);
        }

        /// <summary>
        /// 解析当前背包内的精灵
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        private void AnlyzeGetPetInfoByOnce(HeadInfo recvDataHeadInfo)
        {
            lock (GlobalVariable.lockObjs["petBag"])
            {
                GetPetInfoService.OnGetPetInfoByOnce(recvDataHeadInfo);
                if (GlobalVariable.gameConfigFlag.lowerHpFlag)
                {
                    LowerHPService.OnGetPetInfoByOnce();
                }
            }
        }

        private void AnlyzeGetPetInfo(HeadInfo recvDataHeadInfo)
        {
            lock (GlobalVariable.lockObjs["petInfoDic"])
            {
                GetPetInfoService.OnGetPetInfo(recvDataHeadInfo);
            }
        }

        private void AnlyzeGetPetList(HeadInfo recvDataHeadInfo)
        {
            lock (GlobalVariable.lockObjs["petList"])
            {
                GetPetListService.OnGetPetList(recvDataHeadInfo);
            }
        }
        private void AnlyzeGetLovePetList(HeadInfo recvDataHeadInfo)
        {
            lock (GlobalVariable.lockObjs["petList"])
            {
                GetPetListService.OnGetLovePetList(recvDataHeadInfo);
            }
        }
        private void AnlyzeMibaoFight(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                LowerHPService.OnMibaoFight();
            }
        }
        private void AnlyzeNoteReadyToFight(HeadInfo recvDataHeadInfo)
        {
            GlobalVariable.gameConfigFlag.inFight = true;
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                LowerHPService.OnMibaoFight();
            }
            else if (GlobalVariable.gameConfigFlag.shouldDisableRecv)
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.READY_TO_FIGHT, new int[0]);
            }
        }
        private void AnlyzeReadyToFight(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                LowerHPService.OnReadyToFight();
            }
        }
        private void AnlyzeNoteStartFight(HeadInfo recvDataHeadInfo)
        {
            GlobalVariable.fightTurn = 0;
            Dictionary<string,FightPetInfo> fightPlayers = FightNoteService.OnNoteStartFight(recvDataHeadInfo);
            GlobalVariable.mainForm.SetPetFightNote(fightPlayers);
        }
        private void AnlyzeChangePet(HeadInfo recvDataHeadInfo)
        {
            if(recvDataHeadInfo.decryptData.Length > 4)
            {
                ChangePetInfo changePetInfo = FightNoteService.OnChangePet(recvDataHeadInfo);
                GlobalVariable.mainForm.OnChangePet(changePetInfo);
                if (GlobalVariable.gameConfigFlag.autoUseSkillFlg) AutoUseSkillService.OnChangePet(changePetInfo);
            }
        }
        private void AnlyzeNoteUseSkill(HeadInfo recvDataHeadInfo)
        {
            GlobalVariable.fightTurn++;
            Dictionary<string, AttackValueInfo> players = FightNoteService.OnNoteUseSkill(recvDataHeadInfo);
            GlobalVariable.mainForm.OnUseSkill(players);
            if (GlobalVariable.gameConfigFlag.lowerHpFlag) LowerHPService.OnNoteUseSkill(players);
            if (GlobalVariable.gameConfigFlag.autoUseSkillFlg && players["otherPlayer"].remainHP != 0) AutoUseSkillService.OnUseSkill(players["loginPlayer"]);
            //Console.WriteLine($"fristPlayerInfo:{fristPlayerInfo.userId}  secondPlayerInfo:{secondPlayerInfo.userId}");
        }
        private void AnlyzeUsePetItem(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.autoUseSkillFlg && recvDataHeadInfo.decryptData.Length == 0)
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.ITEM_BUY, new int[2] 
                    { 
                        300016,
                        999
                    });
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM, new int[3]
                    {
                        GlobalVariable.gameConfigFlag.autoUseSkillPetCatchTime,300017,0
                    });
            }
            //Console.WriteLine($"fristPlayerInfo:{fristPlayerInfo.userId}  secondPlayerInfo:{secondPlayerInfo.userId}");
        }
        private void AnlyzeOnFightOver(HeadInfo recvDataHeadInfo)
        {
            GlobalVariable.gameConfigFlag.inFight = false;
            if (GlobalVariable.gameConfigFlag.lowerHpFlag)
            {
                LowerHPService.OnFightOver();
            }else if ((GlobalVariable.gameConfigFlag.autoChargeFlag && GlobalVariable.loginUserInfo.vipLevel == 0)||
                    (GlobalVariable.gameConfigFlag.shouldDisableRecv && 
                    (GlobalVariable.gameConfigFlag.autoChargeFlag || !GlobalVariable.gameConfigFlag.disableVipAutoChargeFlag)))
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_CURE_FREE, new int[0]);
            }

            if (GlobalVariable.gameConfigFlag.autoUseSkillAddPPFlg)
            {
                GlobalVariable.mainForm.StopLoopUseSkill();
            }

            GlobalVariable.mainForm.InitFightNote();
        }

        private void AnlyzeOnPetCureFree(HeadInfo recvDataHeadInfo)
        {
            if (GlobalVariable.gameConfigFlag.shouldDisableRecv)
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.GET_PET_INFO_BY_ONCE,new int[0]);
            }
        }
    }
}
