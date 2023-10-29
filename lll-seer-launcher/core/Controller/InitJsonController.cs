using System;
using lll_seer_launcher.core.Servise;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;

namespace lll_seer_launcher.core.Controller
{
    class InitJsonController
    {
        public void InitJson(VersionConfig versionConfig)
        {
            InitJsonServise.LoadConfigJsonFile(versionConfig);
        }
        public bool InitTaomeeJson()
        {
            return InitJsonServise.LoadTaomeeJson();
        }

        public bool InitAchieveTitleDictionary()
        {
            return InitJsonServise.InitAchieveTitleDictionary();
        }
        public bool InitSuitDictionary()
        {
            return InitJsonServise.InitSuitDictionary();
        }
        public bool InitGlassesDictionary()
        {
            return InitJsonServise.InitGlassesDictionary();
        }
        public bool InitPetDB()
        {
            return InitJsonServise.InitPetDB(GlobalVariable.jsonPathDic["pet"]);
        }
       
        public bool InitSkillDB()
        {
            return InitJsonServise.InitSkillDB(GlobalVariable.jsonPathDic["skill"]);
        }
    }
}
