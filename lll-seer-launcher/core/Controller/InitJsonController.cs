using System;
using lll_seer_launcher.core.Servise;
using lll_seer_launcher.core.Dto;

namespace lll_seer_launcher.core.Controller
{
    class InitJsonController
    {
        public void InitJson()
        {
            InitJsonServise.LoadConfigJsonFile();
        }
        public bool InitAchieveTitleDictionary()
        {
            return InitJsonServise.InitAchieveTitleDictionary(GlobalVariable.jsonPathDic["achieveTitle"]);
        }
        public bool InitSuitDictionary()
        {
            return InitJsonServise.InitSuitDictionary(GlobalVariable.jsonPathDic["suit"]);
        }
        public bool InitGlassesDictionary()
        {
            return InitJsonServise.InitGlassesDictionary(GlobalVariable.jsonPathDic["glasses"]);
        }
    }
}
