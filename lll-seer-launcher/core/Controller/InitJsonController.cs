using System;
using lll_seer_launcher.core.Service;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;

namespace lll_seer_launcher.core.Controller
{
    class InitJsonController
    {
        public bool InitTaomeeJson()
        {
            return InitJsonService.LoadTaomeeJson();
        }

        public bool InitAchieveTitleDictionary()
        {
            return InitJsonService.InitAchieveTitleDictionary(GlobalVariable.jsonPathDic["achievements"]);
        }
        public bool InitEquipAndSuit()
        {
            return InitJsonService.InitEquipAndSuit(GlobalVariable.jsonPathDic["equip"], GlobalVariable.jsonPathDic["suit"]);
        }
        public bool InitPetDB()
        {
            return InitJsonService.InitPetDB(GlobalVariable.jsonPathDic["pet"]);
        }
        public bool InitPetEffectDB()
        {
            return InitJsonService.InitPetEffectDB(GlobalVariable.jsonPathDic["effectIcon"]);
        }
        public bool InitNewSeDB()
        {
            return InitJsonService.InitNewSeDB(GlobalVariable.jsonPathDic["newSe"]);
        }
        public bool InitSkillDB()
        {
            return InitJsonService.InitSkillDB(GlobalVariable.jsonPathDic["skill"]);
        }
        public bool InitSkillDBOfMoveStones()
        {
            return InitJsonService.InitMoveStones(GlobalVariable.jsonPathDic["move_stones"]);
        }
        public bool InitTypeDB()
        {
            return InitJsonService.InitTypeDB(GlobalVariable.jsonPathDic["skillType"]);
        }
    }
}
