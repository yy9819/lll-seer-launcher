using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lll_seer_launcher
{
    internal static class Program
    {
        public static seerMainWindow MainFormInstance;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainFormInstance = new seerMainWindow();
            Application.Run(MainFormInstance);
        }


    }
}
