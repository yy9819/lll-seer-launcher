using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Service;

namespace lll_seer_launcher.core.Controller
{
    public class AutoClickScriptController
    {
        public static void Init()
        {
            AutoClickScriptService.Init();
        }
        public static void AutoClick()
        {
            AutoClickScriptService.AutoClick();
        }

        public static void GetCupture()
        {
            AutoClickScriptService.GetCupture();
        }
    }
}
