using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using lll_seer_launcher.core.Dto;

namespace lll_seer_launcher.core.Controller
{
    public class FormController
    {
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPStr)] string text);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private const int WM_APPCOMMAND = 0x0319;
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int WM_SETTEXT = 0x000C;
        private const int WM_COPYDATA = 0x004A;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        public static void SetMute(IntPtr browserHandle)
        {
            //SendMessage(browserHandle, WM_APPCOMMAND, browserHandle.ToInt32(), APPCOMMAND_VOLUME_MUTE);
        }

        public static void ProgramInit()
        {

        }
        public static void SetWindowLong(IntPtr targetWindowHwnd)
        {
            SetWindowLong(targetWindowHwnd, GWL_EXSTYLE, GetWindowLong(targetWindowHwnd, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }
        public static IntPtr FindWindow(string lpWindowName)
        {
            IntPtr targetWindowHandle = FindWindow(null, lpWindowName);
            //Console.WriteLine(targetWindowHandle);
            return targetWindowHandle;
        } 
        public static IntPtr FindWindowEx(IntPtr hwndParent, string lpWindowName)
        {
            IntPtr childHwnd = FindWindowEx(hwndParent , IntPtr.Zero , null, lpWindowName);
            //Console.WriteLine(childHwnd);
            return childHwnd;
        }
        private static void SendMessage(IntPtr targetHwnd , string msg)
        {
            SendMessage(targetHwnd , WM_SETTEXT , 0 , msg);
        }

        public static void SendMessageToSeerFiddler(string msg)
        {
            IntPtr seerFiddlerHwnd = FormController.FindWindow(GlobalVariable.seerFiddlerTitle);
            if (seerFiddlerHwnd != IntPtr.Zero)
            {
                IntPtr childHwnd = FormController.FindWindowEx(seerFiddlerHwnd, "");
                if (childHwnd != IntPtr.Zero)
                {
                    FormController.SendMessage(childHwnd, msg);
                }
            }
        }
    }
}
