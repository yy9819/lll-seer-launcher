using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace lll_seer_launcher.core.Controller
{
    public class MainFormController
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        const int WM_APPCOMMAND = 0x0319;
        const int APPCOMMAND_VOLUME_MUTE = 0x80000;

        public static void SetMute(IntPtr browserHandle)
        {
            SendMessage(browserHandle, WM_APPCOMMAND, browserHandle.ToInt32(), APPCOMMAND_VOLUME_MUTE);
        }

        public static void ProgramInit()
        {

        }

    }
}
