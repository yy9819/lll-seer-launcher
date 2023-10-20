using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher
{
    public partial class LoadingForm : Form
    {
        private string swfPath = Directory.GetCurrentDirectory() + "\\bin\\loading\\bibiloading.swf";
        public LoadingForm()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            FormController.SetWindowLong(this.Handle);
            TransparencyKey = System.Drawing.Color.White;
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            
        }
        public bool InitData()
        {
            InitJsonController initJsonControl = new InitJsonController();
            if (DBController.SuitAndAchieveTitleDbController.CheckAndInitDB())
            {
                initJsonControl.InitJson();
                if (GlobalVariable.shoudUpdateJsonDic["suit"])
                {
                    Logger.Log("updateData", "装备信息更新！更新装备信息中...");
                    initJsonControl.InitSuitDictionary();
                    DBController.SuitAndAchieveTitleDbController.InitSuitTable();
                    Logger.Log("updateData", "装备信息更新完成！");
                }
                else
                {
                    DBController.SuitAndAchieveTitleDbController.SetSuitTitleDic();
                }
                if (GlobalVariable.shoudUpdateJsonDic["glasses"])
                {
                    Logger.Log("updateData", "目镜信息更新！更新目镜信息中...");
                    initJsonControl.InitGlassesDictionary();
                    DBController.SuitAndAchieveTitleDbController.InitGlassesTable();
                    Logger.Log("updateData", "目镜信息更新完成！");
                }
                else
                {
                    DBController.SuitAndAchieveTitleDbController.SetGlassesTitleDic();
                }
                if (GlobalVariable.shoudUpdateJsonDic["achieveTitle"])
                {
                    Logger.Log("updateData", "称号信息更新！更新称号信息中...");
                    initJsonControl.InitAchieveTitleDictionary();
                    DBController.SuitAndAchieveTitleDbController.InitAchieveTitleTable();
                    Logger.Log("updateData", "称号信息更新完成！");
                }
                else
                {
                    DBController.SuitAndAchieveTitleDbController.SetAchieveTitleDic();
                }

            }
            return true;
        }
        public bool StartFiddler()
        {
            if(FormController.FindWindow(GlobalVariable.seerFiddlerTitle) == IntPtr.Zero)
            {
                Process.Start(Directory.GetCurrentDirectory() + "\\seer-fiddler.exe");
            }
            int count = 0;
            while (count > 100)
            {
                if (FormController.FindWindow(GlobalVariable.seerFiddlerTitle) != IntPtr.Zero)
                {
                    break;
                }
                count++;
                Thread.Sleep(100);
            }
            return count <= 100;
        }
    }
}
