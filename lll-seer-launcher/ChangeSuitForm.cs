using System;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher
{
    public partial class ChangeSuitForm : Form
    {
        #region
        private SendDataController sendDataController = new SendDataController();
        private System.Timers.Timer initGroupBoxsTimer = new System.Timers.Timer(1000);
        private int initGroupBoxsTimerRetryTimes = 0;
        private List<AchieveTitleInfo> canUseAchieveList = new List<AchieveTitleInfo>();
        private List<SuitInfo> canUseSuitList = new List<SuitInfo>();
        private List<GlassesInfo> canUseGlassesList = new List<GlassesInfo>();
        #endregion

        #region
        private delegate void initAchieveListGroupBoxsCallback();
        private delegate void initSuitListGroupBoxsCallback();
        private delegate void initGlassesListGroupBoxsCallback();
        #endregion

        public ChangeSuitForm()
        {
            InitializeComponent();
        }


        private void suitListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.suitListBox.SelectedIndex < 0) return;
            this.suitTextBox.Text = this.canUseSuitList[this.suitListBox.SelectedIndex].desc;
        }

        private void ChangeSuitForm_Load(object sender, EventArgs e)
        {
        }

        private void ChangeSuitForm_closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }


        public void InitGroupBoxs()
        {
            this.getUserSuitGlassesTittleInfoButton.Enabled = false;
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < GlobalVariable.initSuitGroupBoxsCompleteFlg.Length; i++)
                {
                    GlobalVariable.initSuitGroupBoxsCompleteFlg[i] = false;
                }
                this.sendDataController.SendDataByCmdIdAndHexString(3403, "");
                this.sendDataController.SendDataByCmdIdAndIntList(4475, new int[3] { 1300083, 1300998, 2 });
            });
            thread.Start();
            this.initGroupBoxsTimer.Elapsed += this.InitGroupBoxsTimer;
            this.initGroupBoxsTimerRetryTimes = 0;
            this.initGroupBoxsTimer.Start();
        }

        private void InitAchieveListBox()
        {
            initAchieveListGroupBoxsCallback callback = delegate ()
            {
                this.achieveTitleListBox.Items.Clear();
                this.canUseAchieveList.Clear();
                if (GlobalVariable.userAchieveTitleDictionary.TryGetValue(GlobalVariable.userId, out ArrayList userAchieveTitles))
                {
                    this.achieveTitleListBox.Items.Add("不佩戴称号。");
                    //GlobalUtils.ArrayListToString(userAchieveTitles);
                    this.canUseAchieveList.Add(new AchieveTitleInfo());
                    foreach (int key in userAchieveTitles)
                    {
                        if (GlobalVariable.achieveTitleDictionary.TryGetValue(key, out AchieveTitleInfo value))
                        {
                            this.achieveTitleListBox.Items.Add(value.title);
                            this.canUseAchieveList.Add(value);
                        }
                    }
                }
                else if (!GlobalVariable.isLogin)
                {
                    this.achieveTitleListBox.Items.Add("未登录游戏！");
                    this.canUseAchieveList.Add(new AchieveTitleInfo());
                }
                else
                {
                    this.achieveTitleListBox.Items.Add("请尝试重新读取。");
                    this.canUseAchieveList.Add(new AchieveTitleInfo());
                }
                this.getUserSuitGlassesTittleInfoButton.Enabled = true;
            };
            this.Invoke(callback);
        }

        private void InitSuitListBox()
        {
            initSuitListGroupBoxsCallback callback = delegate ()
            {
                this.suitListBox.Items.Clear();
                this.canUseSuitList.Clear();
                if (GlobalVariable.userSuitClothDictionary.TryGetValue(GlobalVariable.userId, out Dictionary<int,int> userSuits))
                {
                    this.suitListBox.Items.Add("不穿戴任何套装。");
                    this.canUseSuitList.Add(new SuitInfo());
                    foreach (int key in GlobalVariable.suitDictionary.Keys)
                    {
                        SuitInfo suitInfo = GlobalVariable.suitDictionary[key];
                        if (userSuits.ContainsKey(suitInfo.clothIdList[0]))
                        {
                            int lastClothId = suitInfo.clothIdList[suitInfo.clothIdList.Length - 1];
                            foreach (int clothId in suitInfo.clothIdList)
                            {
                                if (!userSuits.ContainsKey(clothId)) break;
                                if (clothId == lastClothId)
                                {
                                    this.suitListBox.Items.Add(suitInfo.name);
                                    this.canUseSuitList.Add(suitInfo);
                                }
                            }
                        }
                    }
                }
                else if (!GlobalVariable.isLogin)
                {
                    this.suitListBox.Items.Add("未登录游戏！");
                    this.canUseSuitList.Add(new SuitInfo());
                }
                else
                {
                    this.suitListBox.Items.Add("请尝试重新读取。");
                    this.canUseSuitList.Add(new SuitInfo());
                }
                this.getUserSuitGlassesTittleInfoButton.Enabled = true;
            };
            this.Invoke(callback);
        }

        private void InitGlassesListBox()
        {
            initGlassesListGroupBoxsCallback callback = delegate ()
            {
                this.glassesListBox.Items.Clear();
                this.canUseGlassesList.Clear();
                if (GlobalVariable.userSuitClothDictionary.TryGetValue(GlobalVariable.userId, out Dictionary<int, int> userCloths))
                {
                    this.glassesListBox.Items.Add("不佩戴独立目镜。");
                    this.canUseGlassesList.Add(new GlassesInfo());
                    foreach (int key in GlobalVariable.glassesDictionary.Keys)
                    {
                        if (userCloths.ContainsKey(key))
                        {
                            GlassesInfo glassesInfo = GlobalVariable.glassesDictionary[key];
                            this.glassesListBox.Items.Add(glassesInfo.name);
                            this.canUseGlassesList.Add(glassesInfo);
                        }
                    }
                }
                else if (!GlobalVariable.isLogin)
                {
                    this.glassesListBox.Items.Add("未登录游戏！");
                    this.canUseGlassesList.Add(new GlassesInfo());
                }
                else
                {
                    this.glassesListBox.Items.Add("请尝试重新读取。");
                    this.canUseGlassesList.Add(new GlassesInfo());
                }
                this.getUserSuitGlassesTittleInfoButton.Enabled = true;
            };
            this.Invoke(callback);
        }

        private void achieveTitleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.achieveTitleListBox.SelectedIndex < 0) return;
            this.achieveTittleTextBox.Text = this.canUseAchieveList[this.achieveTitleListBox.SelectedIndex].abtext;
        }

        private void InitGroupBoxsTimer(object sender, ElapsedEventArgs e)
        {
            this.initGroupBoxsTimerRetryTimes += 1;
            if ((GlobalVariable.initSuitGroupBoxsCompleteFlg[0] & GlobalVariable.initSuitGroupBoxsCompleteFlg[1]) | this.initGroupBoxsTimerRetryTimes >= 5)
            {
                this.initGroupBoxsTimer.Elapsed -= this.InitGroupBoxsTimer;
                this.initGroupBoxsTimer.Stop();
                this.InitAchieveListBox();
                this.InitSuitListBox();
                this.InitGlassesListBox();
            }
        }

        private void getUserSuitGlassTittleInfoButton_Click(object sender, EventArgs e)
        {
            this.InitGroupBoxs();
        }

        private void glassesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.glassesListBox.SelectedIndex < 0) return;
            this.glassesTextBox.Text = this.canUseGlassesList[this.glassesListBox.SelectedIndex].desc;
        }
    }
}
