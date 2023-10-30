using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;
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
            this.UpdateInitState("检测最新版本中...");
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version version = assembly.GetName().Version;
            string serverJson = GlobalUtil.GetJsonString("http://52.68.134.105/version/version.json");
            VersionConfig versionConfig = JsonConvert.DeserializeObject<VersionConfig>(serverJson);
            if (serverJson == "")
            {
                LoadingFormCallBack callBack = delegate ()
                {
                    this.Hide();
                };
                this.Invoke(callBack);
                MessageBox.Show("获取版本信息失败！");
                GlobalVariable.successfullyInit = false;
                return true;
            }else if (string.Compare(versionConfig.appversion, version.ToString()) != 0)
            {
                LoadingFormCallBack callBack = delegate ()
                {
                    this.Hide();
                };
                this.Invoke(callBack);
                MessageBox.Show("检测到新版本！\n" +
                    $"最新版本为:{versionConfig.appversion}");
                try
                {
                    Process.Start(versionConfig.downloadurl);
                }
                catch { }
                GlobalVariable.successfullyInit = false;
                return true;
            }
            else
            {
                InitJsonController initJsonControl = new InitJsonController();
                this.UpdateInitState("检查装备称号是否有更新中");
                initJsonControl.InitJson(versionConfig);
                if (DBController.SuitAndAchieveTitleDBController.CheckAndInitDB())
                {
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
                if (initJsonControl.InitTaomeeJson())
                {
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
                    }

                    this.UpdateInitState("检查技能信息是否有更新");
                    if (DBController.SkillDBController.CheckAndInitDB())
                    {
                        if (GlobalVariable.shoudUpdateJsonDic["skill"])
                        {
                            this.UpdateInitState("加载技能信息中");
                            Logger.Log("updateData", "技能信息更新！更新技能信息中...");
                            initJsonControl.InitSkillDB();
                            Logger.Log("updateData", "技能信息更新完成！");
                            this.UpdateInitState("技能信息更新完成");
                        }
                    }
                }
                else
                {
                    LoadingFormCallBack callBack = delegate ()
                    {
                        this.Hide();
                    };
                    this.Invoke(callBack);
                    MessageBox.Show("获取精灵最新信息失败！部分功能可能无法使用...");
                }
                
                this.UpdateInitState("登录器启动中");
                GlobalVariable.loadingComplate = true;
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
