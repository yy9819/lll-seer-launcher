using System;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;
using Microsoft.VisualBasic;

namespace lll_seer_launcher
{
    public partial class ChangeSuitForm : Form
    {
        #region
        private SendDataController sendDataController = new SendDataController();
        private System.Timers.Timer initGroupBoxsTimer = new System.Timers.Timer(1000);
        private int initGroupBoxsTimerRetryTimes = 0;
        private int selectUserId = 0;
        private Dictionary<int, UserSuitAndAchieveTitleInfo> userCanUseClothDic = new Dictionary<int,UserSuitAndAchieveTitleInfo>();
        private List<AchieveTitleInfo> canUseAchieveList = new List<AchieveTitleInfo>();
        private List<SuitInfo> canUseSuitList = new List<SuitInfo>();
        private List<GlassesInfo> canUseGlassesList = new List<GlassesInfo>();
        private Dictionary<int, SuitAchieveTitlePlan> userPlan;
        #endregion

        #region
        private delegate void initAchieveListGroupBoxsCallback();
        private delegate void initSuitListGroupBoxsCallback();
        private delegate void initGlassesListGroupBoxsCallback();
        private delegate void InitUserListComboBoxCallback();
        private delegate void initPlanDataGridViewCallback();
        #endregion

        public ChangeSuitForm()
        {
            InitializeComponent();
        }
        private void ChangeSuitForm_Load(object sender, EventArgs e)
        {
            this.InitUserListComboBox();
        }

        private void InitUserListComboBox()
        {
            InitUserListComboBoxCallback callback = delegate ()
            {
                this.userListComboBox.Items.Clear();
                this.userCanUseClothDic = DBController.SuitAndAchieveTitleDbController.UserTableSelectDataGetUserClothDic();
                if (this.userCanUseClothDic != null && this.userCanUseClothDic.Count > 0)
                {
                    int index = 0;
                    foreach (int userId in this.userCanUseClothDic.Keys)
                    {
                        this.userListComboBox.Items.Add(userId);
                        if(userId == GlobalVariable.userId)
                        {
                            this.userListComboBox.SelectedIndex = index;
                        }
                        index++;
                    }
                    if (this.userListComboBox.SelectedIndex == -1) this.userListComboBox.SelectedIndex = 0;
                }
            };
            this.Invoke(callback);
        }

        private void ChangeSuitForm_closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void getUserSuitGlassTittleInfoButton_Click(object sender, EventArgs e)
        {
            this.InitGroupBoxs();
        }
        #region
        /*========================================通过封包形式获取当前登录账号最新装备信息================================================*/
        public void InitGroupBoxs()
        {
            this.getUserSuitGlassesTittleInfoButton.Enabled = false;
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < GlobalVariable.initSuitGroupBoxsCompleteFlg.Length; i++)
                {
                    GlobalVariable.initSuitGroupBoxsCompleteFlg[i] = false;
                }
                if (!GlobalVariable.isLogin) return;
                this.sendDataController.SendDataByCmdIdAndHexString(CmdId.ACHIEVETITLELIST, "");
                this.sendDataController.SendDataByCmdIdAndIntList(CmdId.ITEM_LIST, new int[3] { 1300083, 1300998, 2 });
            });
            thread.Start();
            this.initGroupBoxsTimer.Elapsed += this.InitGroupBoxsTimer;
            this.initGroupBoxsTimerRetryTimes = 0;
            this.initGroupBoxsTimer.Start();
        }

        private void InitGroupBoxsTimer(object sender, ElapsedEventArgs e)
        {
            this.initGroupBoxsTimerRetryTimes += 1;
            if ((GlobalVariable.initSuitGroupBoxsCompleteFlg[0] & GlobalVariable.initSuitGroupBoxsCompleteFlg[1]) | this.initGroupBoxsTimerRetryTimes >= 5 | !GlobalVariable.isLogin)
            {
                this.initGroupBoxsTimer.Elapsed -= this.InitGroupBoxsTimer;
                this.initGroupBoxsTimer.Stop();
                if (!this.userCanUseClothDic.ContainsKey(GlobalVariable.userId)) this.userCanUseClothDic.Add(GlobalVariable.userId, new UserSuitAndAchieveTitleInfo());
                this.InitAchieveListBox();
                this.InitSuitListBox();
                this.InitGlassesListBox();
                if (GlobalVariable.isLogin)
                {
                    List<int> achieveTitleList = new List<int>();
                    for (int i = 0; i < this.canUseAchieveList.Count; i++)
                    {
                        achieveTitleList.Add(this.canUseAchieveList[i].id);
                    }
                    List<int> glassesList = new List<int>();
                    for (int i = 0; i < canUseGlassesList.Count; i++)
                    {
                        glassesList.Add(this.canUseGlassesList[i].glassesId);
                    }
                    List<int> suitList = new List<int>();
                    for (int i = 0; i < this.canUseSuitList.Count; i++)
                    {
                        suitList.Add(this.canUseSuitList[i].suitId);
                    }
                    UserSuitAndAchieveTitleInfo info = new UserSuitAndAchieveTitleInfo(GlobalVariable.userId, suitList, glassesList, achieveTitleList);
                    int result = DBController.SuitAndAchieveTitleDbController.UserTableInsertData(info);
                    if (result != 1) DBController.SuitAndAchieveTitleDbController.UserTableUpadateData(info);
                    this.InitUserListComboBox();
                    this.InitPlanDataGridView();
                }
                else
                {
                    this.userListComboBox_SelectedIndexChanged(sender, e);
                }
            }
        }

        private void InitAchieveListBox()
        {
            initAchieveListGroupBoxsCallback callback = delegate ()
            {
                this.achieveTitleListBox.Items.Clear();
                this.canUseAchieveList.Clear();
                if (GlobalVariable.userAchieveTitleDictionary.TryGetValue(GlobalVariable.userId, out ArrayList userAchieveTitles))
                {
                    this.achieveTitleListBox.Items.Add("---不佩戴称号---");
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
                    if (GlobalVariable.isLogin)
                    {

                    }
                }
                else if (!GlobalVariable.isLogin)
                {
                    this.achieveTitleListBox.Items.Add("未登录游戏！");
                    this.canUseAchieveList.Add(new AchieveTitleInfo());
                }
                else
                {
                    this.achieveTitleListBox.Items.Add("请尝试重新读取！");
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
                    this.suitListBox.Items.Add("---不穿戴任何套装---");
                    this.canUseSuitList.Add(new SuitInfo());
                    foreach (int key in GlobalVariable.suitDictionary.Keys)
                    {
                        SuitInfo suitInfo = GlobalVariable.suitDictionary[key];
                        if (userSuits.ContainsKey(suitInfo.clothIdList[0]))
                        {
                            int lastClothId = suitInfo.clothIdList[suitInfo.clothIdList.Count - 1];
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
                    this.suitListBox.Items.Add("请尝试重新读取！");
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
                    this.glassesListBox.Items.Add("---不佩戴独立目镜---");
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
                    this.glassesListBox.Items.Add("请尝试重新读取！");
                    this.canUseGlassesList.Add(new GlassesInfo());
                }
                this.getUserSuitGlassesTittleInfoButton.Enabled = true;
            };
            this.Invoke(callback);
        }
        #endregion
        #region
        /*========================================当装备/目镜/称号被选中时，展示相应效果================================================*/
        private void achieveTitleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.achieveTitleListBox.SelectedIndex < 0) return;
            this.achieveTittleTextBox.Text = this.canUseAchieveList[this.achieveTitleListBox.SelectedIndex].abtext;
            this.planDataGridView.Rows[GetPlanDataGridViewSelectIndex()].Cells["achieveTitle"].Value = this.canUseAchieveList[this.achieveTitleListBox.SelectedIndex].title;
        }

        private void glassesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.glassesListBox.SelectedIndex < 0) return;
            this.glassesTextBox.Text = this.canUseGlassesList[this.glassesListBox.SelectedIndex].desc;
            this.planDataGridView.Rows[GetPlanDataGridViewSelectIndex()].Cells["glasses"].Value = this.glassesTextBox.Enabled ? 
                this.canUseGlassesList[this.glassesListBox.SelectedIndex].name : "无";
        }
        private void suitListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.suitListBox.SelectedIndex < 0)
            {
                return;
            }
            else if (this.suitListBox.SelectedIndex == 0)
            {
                this.suitTextBox.Text = "";
                this.planDataGridView.Rows[GetPlanDataGridViewSelectIndex()].Cells["suit"].Value = "无";
            }
            else
            {
                this.planDataGridView.Rows[GetPlanDataGridViewSelectIndex()].Cells["suit"].Value = this.canUseSuitList[this.suitListBox.SelectedIndex].name;
                this.suitTextBox.Text = this.canUseSuitList[this.suitListBox.SelectedIndex].desc;
                if (this.canUseSuitList[this.suitListBox.SelectedIndex].clothIdList.Count > 4)
                {
                    this.glassesListBox.Enabled = false;
                    this.glassesTextBox.Text = "当前所选套装含有目镜，不可选择独立目镜！";
                }
                else
                {
                    this.glassesListBox.Enabled = true;
                    this.glassesTextBox.Text = this.glassesListBox.SelectedIndex > -1 ? this.canUseGlassesList[this.glassesListBox.SelectedIndex].desc : "";
                }
            }

        }
        #endregion
        #region
        /*========================================从数据库获取当前所有保存账号的装备信息================================================*/
        private void userListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.suitTextBox.Text = this.glassesTextBox.Text = this.achieveTittleTextBox.Text = "";
            this.suitTextBox.Enabled = this.glassesTextBox.Enabled = this.achieveTittleTextBox.Enabled = true;
            this.selectUserId = Convert.ToInt32(this.userListComboBox.SelectedItem.ToString());
            if(this.userCanUseClothDic.TryGetValue(this.selectUserId, out var clothDic))
            {
                this.suitListBox.Items.Clear();
                this.canUseSuitList.Clear();
                this.suitListBox.Items.Add("---不穿戴任何套装---");
                this.canUseSuitList.Add(new SuitInfo());
                foreach (int key in clothDic.suitIdList)
                {
                    if (GlobalVariable.suitDictionary.ContainsKey(key))
                    {
                        SuitInfo suitInfo = GlobalVariable.suitDictionary[key];
                        this.suitListBox.Items.Add(suitInfo.name);
                        this.canUseSuitList.Add(suitInfo);
                    }
                }
                this.glassesListBox.Items.Clear();
                this.canUseGlassesList.Clear();
                this.glassesListBox.Items.Add("---不佩戴独立目镜---");
                this.canUseGlassesList.Add(new GlassesInfo());
                foreach (int key in clothDic.glassesIdList)
                {
                    if (GlobalVariable.glassesDictionary.ContainsKey(key))
                    {
                        GlassesInfo glassesInfo = GlobalVariable.glassesDictionary[key];
                        this.glassesListBox.Items.Add(glassesInfo.name);
                        this.canUseGlassesList.Add(glassesInfo);
                    }
                }
                this.achieveTitleListBox.Items.Clear();
                this.canUseAchieveList.Clear();
                this.achieveTitleListBox.Items.Add("---不佩戴称号---");
                this.canUseAchieveList.Add(new AchieveTitleInfo());
                foreach (int key in clothDic.achieveTitleIdList)
                {
                    if (GlobalVariable.achieveTitleDictionary.ContainsKey(key))
                    {
                        AchieveTitleInfo achievetitleInfo = GlobalVariable.achieveTitleDictionary[key];
                        this.achieveTitleListBox.Items.Add(achievetitleInfo.title);
                        this.canUseAchieveList.Add(achievetitleInfo);
                    }
                }
                this.InitPlanDataGridView();
            }
        }
        #endregion

        public void InitPlanDataGridView()
        {
            initPlanDataGridViewCallback callback = delegate ()
            {
                this.planDataGridView.Rows.Clear();
                this.userPlan = DBController.SuitAndAchieveTitleDbController.GetUserPlan(this.selectUserId);
                if(this.userPlan != null)
                {
                    foreach(int key in this.userPlan.Keys)
                    {
                        SuitAchieveTitlePlan info = this.userPlan[key];
                        this.planDataGridView.Rows.Add(info.name,
                            GlobalVariable.suitDictionary.ContainsKey(info.suitId) ? GlobalVariable.suitDictionary[info.suitId].name : "无",
                            GlobalVariable.glassesDictionary.ContainsKey(info.glassesId) ? GlobalVariable.glassesDictionary[info.glassesId].name : "无",
                            GlobalVariable.achieveTitleDictionary.ContainsKey(info.achieveTitleId) ? GlobalVariable.achieveTitleDictionary[info.achieveTitleId].title : "无",
                            info.id);
                    }
                }
                this.planDataGridView_CellClick(new object(), new DataGridViewCellEventArgs(0,0));
            };
            this.Invoke(callback);
        }

        private int GetPlanDataGridViewSelectIndex()
        {

            int selectIndex = 0;
            if (this.planDataGridView.SelectedRows.Count > 0)
            {
                selectIndex = this.planDataGridView.SelectedRows[0].Index;
            }
            else if (this.planDataGridView.SelectedCells.Count > 0)
            {
                selectIndex = this.planDataGridView.SelectedCells[0].RowIndex;
            }
            return selectIndex;
        }

        private void planDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectIndex = this.GetPlanDataGridViewSelectIndex();
            if (this.userPlan == null || this.userPlan.Count <= 0 || selectIndex >= this.userPlan.Count)
            {
                this.updatePlanButton.Enabled = false;
                this.deletePlanButton.Enabled = false;
                this.changePlanButton.Enabled = false;
                return;
            }
            this.updatePlanButton.Enabled = true;
            this.deletePlanButton.Enabled = true;
            this.changePlanButton.Enabled = true;
            this.achieveTitleListBox.SelectedIndex = 0;
            this.suitListBox.SelectedIndex = 0;
            this.glassesListBox.SelectedIndex = 0;
            DataGridViewRow row = this.planDataGridView.Rows[selectIndex];
            int planId = Convert.ToInt32(row.Cells["id"].Value);
            int index = 0;
            foreach(var info in this.canUseAchieveList)
            {
                if(info.id == this.userPlan[planId].achieveTitleId)
                {
                    this.achieveTitleListBox.SelectedIndex = index;
                    break;
                }
                index++;
            }
            index = 0;
            foreach (var info in this.canUseSuitList)
            {
                if (info.suitId == this.userPlan[planId].suitId)
                {
                    this.suitListBox.SelectedIndex = index;
                    break;
                }
                index++;
            }
            index = 0;
            foreach (var info in this.canUseGlassesList)
            {
                if (info.glassesId == this.userPlan[planId].glassesId)
                {
                    this.glassesListBox.SelectedIndex = index;
                    break;
                }
                index++;
            }
        }

        private void addPlanButton_Click(object sender, EventArgs e)
        {

            string inputText = Convert.ToString(this.planDataGridView.Rows[this.userPlan == null ? 0 : this.userPlan.Count].Cells["name"].Value);
            SuitAchieveTitlePlan insertPlan = new SuitAchieveTitlePlan();
            insertPlan.suitId = this.suitListBox.SelectedIndex > -1 ? this.canUseSuitList[this.suitListBox.SelectedIndex].suitId : 0;
            insertPlan.glassesId = this.glassesListBox.SelectedIndex > -1 && this.glassesListBox.Enabled ? this.canUseGlassesList[this.glassesListBox.SelectedIndex].glassesId : 0;
            insertPlan.achieveTitleId = this.achieveTitleListBox.SelectedIndex > -1 ? this.canUseAchieveList[this.achieveTitleListBox.SelectedIndex].id : 0;
            insertPlan.userId = this.selectUserId;
            insertPlan.name = inputText;
            int result = DBController.SuitAndAchieveTitleDbController.InsertPlan(insertPlan);
            if (result != 1)
            {
                MessageBox.Show("方案新建失败...");
            }
            else
            {
                MessageBox.Show("方案新建成功！");
                this.InitPlanDataGridView();
            }
        }

        private void deletePlanButton_Click(object sender, EventArgs e)
        {
            int planId = Convert.ToInt32(this.planDataGridView.Rows[this.GetPlanDataGridViewSelectIndex()].Cells["id"].Value);
            int result = DBController.SuitAndAchieveTitleDbController.DeletePlan(planId);
            if (result != 1)
            {
                MessageBox.Show("方案删除失败...");
            }
            else
            {
                MessageBox.Show("方案删除成功！");
                this.InitPlanDataGridView();
            }
        }

        private void updatePlanButton_Click(object sender, EventArgs e)
        {
            int selectIndex = this.GetPlanDataGridViewSelectIndex();
            SuitAchieveTitlePlan plan = new SuitAchieveTitlePlan();
            plan.id = Convert.ToInt32(this.planDataGridView.Rows[selectIndex].Cells["id"].Value);
            plan.name = Convert.ToString(this.planDataGridView.Rows[selectIndex].Cells["name"].Value);
            plan.suitId = this.suitListBox.Enabled ? this.canUseSuitList[this.suitListBox.SelectedIndex].suitId : 0;
            plan.glassesId = this.glassesListBox.Enabled ? this.canUseGlassesList[this.glassesListBox.SelectedIndex].glassesId : 0;
            plan.achieveTitleId = this.achieveTitleListBox.Enabled ? this.canUseAchieveList[this.achieveTitleListBox.SelectedIndex].id : 0;
            int result = DBController.SuitAndAchieveTitleDbController.UpdatePlan(plan);
            if (result != 1)
            {
                MessageBox.Show("方案保存失败...");
            }
            else
            {
                MessageBox.Show("方案保存成功！");
                this.InitPlanDataGridView();
            }
        }
    }
}
