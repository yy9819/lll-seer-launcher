using System;
using lll_seer_launcher.core.Service;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;

namespace lll_seer_launcher.core.Controller
{
    class InitJsonController
    {
        public void InitJson(VersionConfig versionConfig)
        {
            InitJsonService.LoadConfigJsonFile(versionConfig);
        }
        public bool InitTaomeeJson()
        {
            return InitJsonService.LoadTaomeeJson();
        }

        public bool InitAchieveTitleDictionary()
        {
            return InitJsonService.InitAchieveTitleDictionary();
        }
        public bool InitSuitDictionary()
        {
            return InitJsonService.InitSuitDictionary();
        }
        public bool InitGlassesDictionary()
        {
            return InitJsonService.InitGlassesDictionary();
        }
        public bool InitPetDB()
        {
            return InitJsonService.InitPetDB(GlobalVariable.jsonPathDic["pet"]);
        }
       
        public bool InitSkillDB()
        {
            return InitJsonService.InitSkillDB(GlobalVariable.jsonPathDic["skill"]);
        }
    }
}
