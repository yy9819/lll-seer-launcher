using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using lll_seer_launcher.core.Dto;
using WindowsInput;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Service
{
    public class AutoClickScriptService
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);


        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, out Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        private static InputSimulator simulator = new InputSimulator();
        private static List<Image<Bgr, byte>> bmps = new List<Image<Bgr, byte>>();
        private static IntPtr mainFormhWnd;
        public static void GetCupture()
        {

            GetWindowRect(mainFormhWnd, out Rect windowRect);
            
            using (Bitmap bmp = new Bitmap(windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top))
            {
                using (Graphics gfxBmp = Graphics.FromImage(bmp))
                {
                    IntPtr hdcBitmap = gfxBmp.GetHdc();

                    // 将窗口内容绘制到Bitmap上
                    PrintWindow(mainFormhWnd, hdcBitmap, 0);

                    gfxBmp.ReleaseHdc(hdcBitmap);
                }
                //bmp.Save($"cache\\windowScreenshot_{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}.bmp");
                try
                {
                    Clipboard.SetImage(bmp);
                    Logger.Log("getScreenShot",$"用户使用登录器截图成功!");
                }
                catch
                {
                    MessageBox.Show("截图失败...");
                }
            }
        }
        public static void Init()
        {
            string path = Directory.GetCurrentDirectory() + "\\bin\\img\\autoclick";
            if (!Directory.Exists(path) || Directory.GetFiles(path, "*.bmp", SearchOption.TopDirectoryOnly).Length < 4)
            {
                Directory.CreateDirectory(path);
                Properties.Resources._1.Save($"{path}\\1.bmp");
                Properties.Resources._2.Save($"{path}\\2.bmp");
                Properties.Resources._3.Save($"{path}\\3.bmp");
                Properties.Resources._4.Save($"{path}\\4.bmp");
            }
            string[] bmpPath = Directory.GetFiles(path,"*.bmp",SearchOption.TopDirectoryOnly);
            mainFormhWnd = GlobalVariable.mainForm.Handle;
            if (bmps.Count >= bmpPath.Length) return;
            foreach(string bmp in bmpPath)
            {
                Image<Bgr, byte> template = new Image<Bgr, byte>(bmp); // Image A
                bmps.Add(template);
                if (bmps.Count >= 6) break;
            }
        }

        public static void AutoClick()
        {
            GetWindowRect(mainFormhWnd, out Rect windowRect);
            using (Bitmap bmp = new Bitmap(windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top))
            {
                using (Graphics gfxBmp = Graphics.FromImage(bmp))
                {
                    IntPtr hdcBitmap = gfxBmp.GetHdc();

                    // 将窗口内容绘制到Bitmap上
                    PrintWindow(mainFormhWnd, hdcBitmap, 0);

                    gfxBmp.ReleaseHdc(hdcBitmap);
                }
                using (Image<Bgr, byte> screenshotImage = new Image<Bgr, byte>(bmp))
                {
                    foreach (var template in bmps)
                    {
                        using (Image<Gray, float> result = screenshotImage.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
                        {
                            double[] minValues, maxValues;
                            Point[] minLocations, maxLocations;
                            result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                            //Console.WriteLine(maxValues[0]);
                            //Console.WriteLine(maxLocations[0]);
                            if (maxValues[0] > 0.95)
                            {
                                LeftClick(maxLocations[0].X, maxLocations[0].Y);
                                break;
                            }
                        }
                    }
                }
            }
        }
        private static void LeftClick(int left,int top)
        {
            Rect windowRect;
            GetWindowRect(mainFormhWnd, out windowRect);
            Point cursorPosition = Cursor.Position;
            SetCursorPos(windowRect.Left + 25 + left, windowRect.Top + 10 + top);
            simulator.Mouse.LeftButtonClick();
            SetCursorPos(cursorPosition.X, cursorPosition.Y);
        }
    }
}
