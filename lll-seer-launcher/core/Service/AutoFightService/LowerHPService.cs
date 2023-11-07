using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Service.AutoFightService
{
    public class LowerHPService
    {
        public static void OnGetPetInfoByOnce()
        {
            if (GlobalVariable.pets.Count > 0)
            {
                GlobalVariable.lowerHpPets.Clear();
                foreach (var pet in GlobalVariable.pets.Values)
                {
                    GlobalVariable.lowerHpPets.Add(pet);
                }
                GlobalVariable.mainForm.SetLowerHpStatus("精灵列表读取成功，开始压血！");
                GlobalVariable.gameConfigFlag.lowerHpPetLen = 0;
                while (GlobalVariable.lowerHpPets.Count > GlobalVariable.gameConfigFlag.lowerHpPetLen)
                {
                    if (GlobalVariable.lowerHpPets[GlobalVariable.gameConfigFlag.lowerHpPetLen].hp > 0) break;
                    GlobalVariable.gameConfigFlag.lowerHpPetLen++;
                }
                if (GlobalVariable.lowerHpPets.Count > GlobalVariable.gameConfigFlag.lowerHpPetLen)
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
                        foreach (var pet in GlobalVariable.pets.Values)
                        {
                            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM_OUT_OF_FIGHT,
                                new int[2] { pet.catchTime, 300011 });
                            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM_OUT_OF_FIGHT,
                                new int[2] { pet.catchTime, 300016 });
                        }
                        GlobalVariable.mainForm.SetLowerHpStatus("压血完成!");
                        Logger.Log("LowerHP", "压血结束(当前背包精灵均阵亡，不进行战斗直接回血)");
                        Thread.Sleep(2000);
                        GlobalVariable.mainForm.SetLowerHpStatus("");
                    }).Start();
                }

            }
            else
            {
                GlobalVariable.mainForm.SetLowerHpStatus("");
                Logger.Log("LowerHP", "用户当前背包无精灵！停止压血");
                MessageBox.Show("亲爱的小赛尔，\n你的背包里没有精灵，不能进行压血哟~");
            }
        }
        public static void OnMibaoFight()
        {
            new Task(() => {
                GlobalVariable.mainForm.SetLowerHpStatus("进入战斗!");
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.READY_TO_FIGHT, new int[0]);
            }).Start();
        }
        public static void OnReadyToFight()
        {
            new Task(() => {
                GlobalVariable.mainForm.SetLowerHpStatus($"正在进行第{GlobalVariable.gameConfigFlag.lowerHpPetLen + 1}只精灵的压血");
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL, new int[1] { 0 });
            }).Start();
        }
        public static void OnFightOver()
        {
            new Task(() => {
                GlobalVariable.mainForm.SetLowerHpStatus("战斗结束，开始回血");
                GlobalVariable.gameConfigFlag.lowerHpFlag = false;
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.ITEM_BUY, new int[2] { 300016, GlobalVariable.pets.Count });
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.ITEM_BUY, new int[2] { 300011, GlobalVariable.pets.Count });
                foreach (var pet in GlobalVariable.pets.Values)
                {
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM_OUT_OF_FIGHT,
                        new int[2] { pet.catchTime, 300011 });
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM_OUT_OF_FIGHT,
                        new int[2] { pet.catchTime, 300016 });
                }
                GlobalVariable.mainForm.SetLowerHpStatus("压血完成!");
                Thread.Sleep(2000);
                Logger.Log("LowerHP", "压血结束(当前背包精灵存在未阵亡精灵，战斗结束后回血)");
                GlobalVariable.mainForm.SetLowerHpStatus("");
            }).Start();
        } 
        public static void OnNoteUseSkill(Dictionary<string, AttackValueInfo> players)
        {
            //判断当前精灵剩余血量
            if (players["loginPlayer"].remainHP > 0)
            {
                //未死亡则继续使用技能
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL,
                    new int[1] { 0 });
            }
            // 如已阵亡，则判断是否存在未阵亡精灵
            else if (GlobalVariable.lowerHpPets.Count > ++GlobalVariable.gameConfigFlag.lowerHpPetLen)
            {
                while (GlobalVariable.lowerHpPets.Count > GlobalVariable.gameConfigFlag.lowerHpPetLen)
                {
                    if (GlobalVariable.lowerHpPets[GlobalVariable.gameConfigFlag.lowerHpPetLen].hp > 0) break;
                    GlobalVariable.gameConfigFlag.lowerHpPetLen++;
                }
                if (GlobalVariable.lowerHpPets.Count > GlobalVariable.gameConfigFlag.lowerHpPetLen)
                {
                    GlobalVariable.mainForm.SetLowerHpStatus($"正在进行第{GlobalVariable.gameConfigFlag.lowerHpPetLen + 1}只精灵的压血");
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.CHANGE_PET,
                    new int[1] { GlobalVariable.lowerHpPets[GlobalVariable.gameConfigFlag.lowerHpPetLen].catchTime });
                    Thread.Sleep(100);
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL,
                        new int[1] { 0 });
                }
            }
            // 如均阵亡，设置可出战精灵为0
            else
            {
                GlobalVariable.gameConfigFlag.lowerHpPetLen = 0;
            }
        }
    }
}
