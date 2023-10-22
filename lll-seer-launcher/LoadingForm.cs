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
        private delegate void LoadingFormCallBack();
        public LoadingForm()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            FormController.SetWindowLong(this.Handle);
            TransparencyKey = System.Drawing.Color.White;
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            new Thread(() => 
            {
                bool result = InitData();
                LoadingFormCallBack callBack = delegate ()
                {
                    if (result) this.Close();
                };
                this.Invoke(callBack);
            }).Start();
        }
        public bool InitData()
        {
            InitJsonController initJsonControl = new InitJsonController();
            this.UpdateInitState("检查装备称号是否有更新中");
            if (DBController.SuitAndAchieveTitleDBController.CheckAndInitDB())
            {
                initJsonControl.InitJson();
                if (GlobalVariable.shoudUpdateJsonDic["suit"])
                {
                    this.UpdateInitState("加载装备信息中");
                    Logger.Log("updateData", "装备信息更新！更新装备信息中...");
                    initJsonControl.InitSuitDictionary();
                    DBController.SuitAndAchieveTitleDBController.InitSuitTable();
                    Logger.Log("updateData", "装备信息更新完成！");
                }
                else
                {
                    DBController.SuitAndAchieveTitleDBController.SetSuitTitleDic();
                }
                if (GlobalVariable.shoudUpdateJsonDic["glasses"])
                {
                    this.UpdateInitState("加载目镜信息中");
                    Logger.Log("updateData", "目镜信息更新！更新目镜信息中...");
                    initJsonControl.InitGlassesDictionary();
                    DBController.SuitAndAchieveTitleDBController.InitGlassesTable();
                    Logger.Log("updateData", "目镜信息更新完成！");
                }
                else
                {
                    DBController.SuitAndAchieveTitleDBController.SetGlassesTitleDic();
                }
                if (GlobalVariable.shoudUpdateJsonDic["achieveTitle"])
                {
                    this.UpdateInitState("加载称号信息中");
                    Logger.Log("updateData", "称号信息更新！更新称号信息中...");
                    initJsonControl.InitAchieveTitleDictionary();
                    DBController.SuitAndAchieveTitleDBController.InitAchieveTitleTable();
                    Logger.Log("updateData", "称号信息更新完成！");
                }
                else
                {
                    DBController.SuitAndAchieveTitleDBController.SetAchieveTitleDic();
                }

            }
            this.UpdateInitState("检查精灵信息是否有更新");
            if (DBController.PetDBController.CheckAndInitDB())
            {
                if (GlobalVariable.shoudUpdateJsonDic["pet"])
                {
                    this.UpdateInitState("加载精灵信息中");
                    Logger.Log("updateData", "精灵信息更新！更新精灵信息中...");
                    initJsonControl.InitPetDB();
                    Logger.Log("updateData", "精灵信息更新完成！");
                    this.UpdateInitState("精灵信息更新完成");
                }
                if (GlobalVariable.shoudUpdateJsonDic["petSkins"])
                {
                    this.UpdateInitState("加载精灵皮肤信息中");
                    Logger.Log("updateData", "精灵皮肤信息更新！更新精灵皮肤信息中...");
                    initJsonControl.InitPetSkinsDB();
                    Logger.Log("updateData", "精灵皮肤信息更新完成！");
                    this.UpdateInitState("精灵皮肤信息更新完成");
                }
            }
            this.UpdateInitState("登录器启动中");
            GlobalVariable.loadingComplate = true;
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
        private void UpdateInitState(string msg)
        {
            LoadingFormCallBack callBack = delegate ()
            {
                this.loadingLabel.Text = msg ;
            };
            this.Invoke(callBack);
        }
    }
}
