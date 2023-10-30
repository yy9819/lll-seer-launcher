using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
using System.Windows.Forms;
using Fiddler;


namespace lll_seer_launcher
{
    internal static class Program
    {
        public static seerMainWindow MainFormInstance;
        private static LoadingForm loadingForm;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            loadingForm = new LoadingForm();
            Application.Run(loadingForm);
            if (GlobalVariable.successfullyInit)
            {
                MainFormInstance = new seerMainWindow();
                if (GlobalUtil.StartFiddler())
                {
                    new Thread(() =>
                    {
                        Thread.Sleep(5000);
                        URLMonInterop.SetProxyInProcess("127.0.0.1:4201", "<-loopback>");
                    }).Start();
                    Application.Run(MainFormInstance);
                }
                else
                {
                    MessageBox.Show("赛尔号资源捕获器启动失败！");
                }
            }
        }


    }
}
