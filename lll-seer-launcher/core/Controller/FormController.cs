using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Controller
{
    public class FormController
    {
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPStr)] string text);
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("winmm.dll")]
        private static extern int waveOutGetVolume(IntPtr hVolume , out uint dwVolume);
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hVolume, uint dwVolume);

        [DllImport("shell32.dll")]
        private static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);

        private const int WM_APPCOMMAND = 0x0319;
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int WM_SETTEXT = 0x000C;
        private const int WM_COPYDATA = 0x004A;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const uint WM_LBUTTONDOWN = 0x201;
        private const uint WM_LBUTTONUP = 0x202;

        private enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        public static void ProgramInit()
        {

        }
        /// <summary>
        /// 设置指定窗口不出现在alt tab里
        /// </summary>
        /// <param name="targetWindowHwnd"></param>
        public static void SetWindowLong(IntPtr targetWindowHwnd)
        {
            SetWindowLong(targetWindowHwnd, GWL_EXSTYLE, GetWindowLong(targetWindowHwnd, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }
        /// <summary>
        /// 查找指定标题的窗口
        /// </summary>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        public static IntPtr FindWindow(string lpWindowName)
        {
            IntPtr targetWindowHandle = FindWindow(null, lpWindowName);
            //Console.WriteLine(targetWindowHandle);
            return targetWindowHandle;
        } 

        /// <summary>
        /// 查找指定窗口里的第一个textbox
        /// </summary>
        /// <param name="hwndParent"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        public static IntPtr FindWindowEx(IntPtr hwndParent, string lpWindowName)
        {
            IntPtr childHwnd = FindWindowEx(hwndParent , IntPtr.Zero , null, lpWindowName);
            //Console.WriteLine(childHwnd);
            return childHwnd;
        }

        /// <summary>
        /// 给指定窗口的textBox设置值
        /// </summary>
        /// <param name="targetHwnd"></param>
        /// <param name="msg"></param>
        private static void SendMessage(IntPtr targetHwnd , string msg)
        {
            SendMessage(targetHwnd , WM_SETTEXT , 0 , msg);
        }

        /// <summary>
        /// 向fiddler窗口发送消息
        /// </summary>
        /// <param name="msg"></param>
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

        public static int GetVolume()
        {
            uint CurrVol = 0;
            waveOutGetVolume(IntPtr.Zero , out CurrVol);
            ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
            return CalcVol / (ushort.MaxValue / 10);
        }

        /// <summary>
        /// 设置本程序的音量
        /// </summary>
        /// <param name="volume"></param>
        public static void SetVolume(int volume)
        {
            int newVolume = ((ushort.MaxValue / 10) * volume);
            uint newVolumeAllChannels = (((uint)newVolume & 0x0000ffff)| ((uint)newVolume << 16));
            waveOutSetVolume(IntPtr.Zero , newVolumeAllChannels);
        }

        public static void SimulateClick(IntPtr hWnd, int x, int y)
        {
            if (hWnd != IntPtr.Zero)
            {
                GetClientRect(hWnd, out RECT clientRect);
                int width = clientRect.right - clientRect.left;
                int height = clientRect.bottom - clientRect.top;
                //Console.WriteLine(width);
                //Console.WriteLine(height);

                int lParam = (y * 65536 / height) << 16 | (x * 65536 / width);

                SendMessage(hWnd, WM_LBUTTONDOWN, 0, lParam);
                SendMessage(hWnd, WM_LBUTTONUP, 0, lParam);
            }
        }

        

        public static void ClearIECache()
        {
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 8", "", ShowCommands.SW_HIDE);
        }
    }
}
