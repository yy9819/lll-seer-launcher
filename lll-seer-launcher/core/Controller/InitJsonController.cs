using System;
using lll_seer_launcher.core.Servise;

namespace lll_seer_launcher.core.Controller
{
    class InitJsonController
    {
        public bool InitJson()
        {
            return InitAchieveTitleDictionary();
        }
        private bool InitAchieveTitleDictionary()
        {
            return InitJsonServise.InitAchieveTitleDictionary() && InitJsonServise.InitSuitDictionary() && InitJsonServise.InitGlassesDictionary();
        }
    }
}
