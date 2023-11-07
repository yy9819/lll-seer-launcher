using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;

namespace lll_seer_launcher.core.Service.AutoFightService
{
    public class AutoUseSkillService
    {
        public static void OnUseSkill(AttackValueInfo attackValueInfo)
        {
            if(attackValueInfo.remainHP > 0 && attackValueInfo.skillArray.TryGetValue(GlobalVariable.gameConfigFlag.autoUseSkillId,out SkillInfo skill))
            {
                if(skill.skillPP < 1 && GlobalVariable.gameConfigFlag.autoUseSkillAddPPFlg)
                {
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_PET_ITEM, new int[3]
                    {
                        GlobalVariable.gameConfigFlag.autoUseSkillPetCatchTime,300017,0
                    });
                }
                else
                {
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL, new int[1] { skill.skillId});
                }
            }
            else
            {
                GlobalVariable.mainForm.StopLoopUseSkill();
            }
        }
        public static void OnChangePet(ChangePetInfo changePetInfo)
        {
            if(changePetInfo.userId == GlobalVariable.loginUserInfo.userId)
            {
                if (changePetInfo.catchTime != GlobalVariable.gameConfigFlag.autoUseSkillPetCatchTime) GlobalVariable.mainForm.StopLoopUseSkill();
            }
            else
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.USE_SKILL, new int[1] { GlobalVariable.gameConfigFlag.autoUseSkillId });
            }
        }
    }
}
