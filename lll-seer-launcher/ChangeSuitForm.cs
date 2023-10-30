using System;
using System.Windows.Forms;
using System.Timers;
using System.Threading.Tasks;
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
        private delegate void InitUserClothCallback();
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
                this.userCanUseClothDic = DBController.SuitAndAchieveTitleDBController.UserTableSelectDataGetUserClothDic();
                if (this.userCanUseClothDic != null && this.userCanUseClothDic.Count > 0)
                {
                    int index = 0;
                    foreach (int userId in this.userCanUseClothDic.Keys)
                    {
                        this.userListComboBox.Items.Add(userId);
                        if(userId == GlobalVariable.loginUserInfo.userId)
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
        /// <summary>
        /// 初始化当前登录账号所持装备信息
        /// -1发送装备查询封包
        /// -2启动Timer，监测服务器返回的数据
        /// </summary>
        public void InitGroupBoxs()
        {
            this.getUserSuitGlassesTittleInfoButton.Enabled = false;
            new Task(() =>
            {
                for (int i = 0; i < GlobalVariable.initSuitGroupBoxsCompleteFlg.Length; i++)
                {
                    GlobalVariable.initSuitGroupBoxsCompleteFlg[i] = false;
                }
                if (!GlobalVariable.isLogin) return;
                GlobalVariable.sendDataController.SendDataByCmdIdAndHexString(CmdId.ACHIEVETITLELIST, "");
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.ITEM_LIST, new int[3] { 1300083, 1310000, 2 });
            }).Start();
            this.initGroupBoxsTimer.Elapsed += this.InitGroupBoxsTimer;
            this.initGroupBoxsTimerRetryTimes = 0;
            this.initGroupBoxsTimer.Start();
        }
        /// <summary>
        /// 封包查询当前账号所持装备Timer
        /// -1 称号和装备信息均处理完毕时立即执行信息初始化
        /// -2 5s内(initGroupBoxsTimerRetryTimes >= 5) 即使称号和装备信息有任意一个还未收到recv包，也立即进行初始化
        /// -3 未登录游戏时，尝试从数据库内查询已保存的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitGroupBoxsTimer(object sender, ElapsedEventArgs e)
        {
            this.initGroupBoxsTimerRetryTimes += 1;
            if ((GlobalVariable.initSuitGroupBoxsCompleteFlg[0] && GlobalVariable.initSuitGroupBoxsCompleteFlg[1]) || this.initGroupBoxsTimerRetryTimes >= 5 || !GlobalVariable.isLogin)
            {
                this.initGroupBoxsTimer.Elapsed -= this.InitGroupBoxsTimer;
                this.initGroupBoxsTimer.Stop();
                //

                if (GlobalVariable.isLogin && (GlobalVariable.initSuitGroupBoxsCompleteFlg[0] || GlobalVariable.initSuitGroupBoxsCompleteFlg[1]))
                {
                    if (!this.userCanUseClothDic.ContainsKey(GlobalVariable.loginUserInfo.userId)) this.userCanUseClothDic.Add(GlobalVariable.loginUserInfo.userId, new UserSuitAndAchieveTitleInfo());
                    List<int> achieveTitleList = new List<int>();
                    List<int> glassesList = new List<int>();
                    List<int> suitList = new List<int>();
                    //有称号封包返回时生成称号持有状况List，并更新选择框
                    if (GlobalVariable.initSuitGroupBoxsCompleteFlg[0])
                    {
                        this.InitAchieveListBox();
                        for (int i = 0; i < this.canUseAchieveList.Count; i++)
                        {
                            achieveTitleList.Add(this.canUseAchieveList[i].id);
                        }
                    }
                    //有装备封包返回时生成装备持有状况List，并更新选择框
                    if (GlobalVariable.initSuitGroupBoxsCompleteFlg[1])
                    {
                        this.InitSuitListBox();
                        this.InitGlassesListBox();
                        for (int i = 0; i < canUseGlassesList.Count; i++)
                        {
                            glassesList.Add(this.canUseGlassesList[i].glassesId);
                        }
                        for (int i = 0; i < this.canUseSuitList.Count; i++)
                        {
                            suitList.Add(this.canUseSuitList[i].suitId);
                        }
                    }

                    //尝试向数据库插入当前账号的装备持有状况
                    if(GlobalVariable.loginUserInfo.userId != 0)
                    {
                        UserSuitAndAchieveTitleInfo info = new UserSuitAndAchieveTitleInfo(GlobalVariable.loginUserInfo.userId, suitList, glassesList, achieveTitleList);
                        int result = DBController.SuitAndAchieveTitleDBController.UserTableInsertData(info);
                        //插入失败时，更新对应用户的信息
                        if (result != 1)
                        {
                            //有称号封包返回时更新称号持有状况
                            if (GlobalVariable.initSuitGroupBoxsCompleteFlg[0]) DBController.SuitAndAchieveTitleDBController.UserTableUpadateAchieveTitleData(info);
                            //有装备封包返回时更新装备持有状况
                            if (GlobalVariable.initSuitGroupBoxsCompleteFlg[1]) DBController.SuitAndAchieveTitleDBController.UserTableUpadateClothData(info);
                        }
                    }
                    this.InitUserListComboBox();
                    this.InitPlanDataGridView();
                }
                //如果有称号/装备其中一个封包未返回 或者还未登录，尝试从数据库获取当前账号的装备持有情况
                if (this.initGroupBoxsTimerRetryTimes >= 5 || !GlobalVariable.isLogin)
                {
                    this.userListComboBox_SelectedIndexChanged(sender, e);
                }
            }
        }
        /// <summary>
        /// 初始化当前所选择账号的称号信息一览
        /// </summary>
        private void InitAchieveListBox()
        {
            initAchieveListGroupBoxsCallback callback = delegate ()
            {
                this.achieveTitleListBox.Items.Clear();
                this.canUseAchieveList.Clear();
                if (GlobalVariable.userAchieveTitleDictionary.TryGetValue(GlobalVariable.loginUserInfo.userId, out ArrayList userAchieveTitles))
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
        /// <summary>
        /// 初始化当前所选账号的套装所持状况
        /// </summary>
        private void InitSuitListBox()
        {
            initSuitListGroupBoxsCallback callback = delegate ()
            {
                this.suitListBox.Items.Clear();
                this.canUseSuitList.Clear();
                if (GlobalVariable.userSuitClothDictionary.TryGetValue(GlobalVariable.loginUserInfo.userId, out Dictionary<int,int> userSuits))
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
        /// <summary>
        /// 初始化当前所选账号，目镜所持情况
        /// </summary>
        private void InitGlassesListBox()
        {
            initGlassesListGroupBoxsCallback callback = delegate ()
            {
                this.glassesListBox.Items.Clear();
                this.canUseGlassesList.Clear();
                if (GlobalVariable.userSuitClothDictionary.TryGetValue(GlobalVariable.loginUserInfo.userId, out Dictionary<int, int> userCloths))
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
        /// <summary>
        /// 当称号被选中时，展示对应效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void achieveTitleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //未被选中时，不执行操作
            if(this.achieveTitleListBox.SelectedIndex < 0) return;
            //当某项被选中时，将称号效果展示
            this.achieveTittleTextBox.Text = this.canUseAchieveList[this.achieveTitleListBox.SelectedIndex].abtext;
            //将所选称号名添加到方案表当前所选列中
            this.planDataGridView.Rows[GetPlanDataGridViewSelectIndex()].Cells["achieveTitle"].Value = this.canUseAchieveList[this.achieveTitleListBox.SelectedIndex].title;
        }

        /// <summary>
        /// 当目镜被选中时，展示对应效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glassesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.glassesListBox.SelectedIndex < 0) return;
            this.glassesTextBox.Text = this.canUseGlassesList[this.glassesListBox.SelectedIndex].desc;
            //将所选目镜名添加到方案表当前所选列中
            //如当前所选套装为5件套，将对应行数的方案内的目镜设为无
            this.planDataGridView.Rows[GetPlanDataGridViewSelectIndex()].Cells["glasses"].Value = this.glassesTextBox.Enabled ? 
                this.canUseGlassesList[this.glassesListBox.SelectedIndex].name : "无";
        }

        /// <summary>
        /// 当套装选中时，展示对应效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                // 根据当前所选套装的零件数判断，是否可单独配搭目镜
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
            InitUserClothCallback callback = delegate ()
            {
                if (!this.getUserSuitGlassesTittleInfoButton.Enabled) this.getUserSuitGlassesTittleInfoButton.Enabled =true;
                this.suitTextBox.Text = this.glassesTextBox.Text = this.achieveTittleTextBox.Text = "";
                this.suitTextBox.Enabled = this.glassesTextBox.Enabled = this.achieveTittleTextBox.Enabled = true;
                if (this.userListComboBox.SelectedItem != null)
                {
                    this.selectUserId = Convert.ToInt32(this.userListComboBox.SelectedItem.ToString());
                    if (this.userCanUseClothDic.TryGetValue(this.selectUserId, out var clothDic))
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
            };
            this.Invoke(callback);
        }
        #endregion
        /// <summary>
        /// 初始化方案表格
        /// </summary>
        public void InitPlanDataGridView()
        {
            initPlanDataGridViewCallback callback = delegate ()
            {
                this.planDataGridView.Rows.Clear();
                // 从数据库拉取所保存方案
                this.userPlan = DBController.SuitAndAchieveTitleDBController.GetUserPlan(this.selectUserId);
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

        /// <summary>
        /// 获取当前所选方案的index
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 当方案表格被点击时，
        /// -1 设置「新建」「修改」「删除」按钮是否可按
        /// -2 根据所选方案，将套装，目镜，称号的index设为对应方案的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 新建方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPlanButton_Click(object sender, EventArgs e)
        {

            string inputText = Convert.ToString(this.planDataGridView.Rows[this.userPlan == null ? 0 : this.userPlan.Count].Cells["name"].Value);
            SuitAchieveTitlePlan insertPlan = new SuitAchieveTitlePlan();
            insertPlan.suitId = this.suitListBox.SelectedIndex > -1 ? this.canUseSuitList[this.suitListBox.SelectedIndex].suitId : 0;
            insertPlan.glassesId = this.glassesListBox.SelectedIndex > -1 && this.glassesListBox.Enabled ? this.canUseGlassesList[this.glassesListBox.SelectedIndex].glassesId : 0;
            insertPlan.achieveTitleId = this.achieveTitleListBox.SelectedIndex > -1 ? this.canUseAchieveList[this.achieveTitleListBox.SelectedIndex].id : 0;
            insertPlan.userId = this.selectUserId;
            insertPlan.name = inputText;
            if(insertPlan.userId != 0)
            {
                int result = DBController.SuitAndAchieveTitleDBController.InsertPlan(insertPlan);
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
        }

        /// <summary>
        /// 删除方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletePlanButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否确认执行此操作?", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                int planId = Convert.ToInt32(this.planDataGridView.Rows[this.GetPlanDataGridViewSelectIndex()].Cells["id"].Value);
                int deleteResult = DBController.SuitAndAchieveTitleDBController.DeletePlan(planId);
                if (deleteResult != 1)
                {
                    MessageBox.Show("方案删除失败...");
                }
                else
                {
                    MessageBox.Show("方案删除成功！");
                    this.InitPlanDataGridView();
                }
            }

        }

        /// <summary>
        /// 修改方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updatePlanButton_Click(object sender, EventArgs e)
        {
            int selectIndex = this.GetPlanDataGridViewSelectIndex();
            SuitAchieveTitlePlan plan = new SuitAchieveTitlePlan();
            plan.id = Convert.ToInt32(this.planDataGridView.Rows[selectIndex].Cells["id"].Value);
            plan.name = Convert.ToString(this.planDataGridView.Rows[selectIndex].Cells["name"].Value);
            plan.suitId = this.suitListBox.Enabled ? this.canUseSuitList[this.suitListBox.SelectedIndex].suitId : 0;
            plan.glassesId = this.glassesListBox.Enabled ? this.canUseGlassesList[this.glassesListBox.SelectedIndex].glassesId : 0;
            plan.achieveTitleId = this.achieveTitleListBox.Enabled ? this.canUseAchieveList[this.achieveTitleListBox.SelectedIndex].id : 0;
            int result = DBController.SuitAndAchieveTitleDBController.UpdatePlan(plan);
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

        /// <summary>
        /// 切换要编辑的账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changePlanButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin)
            {
                MessageBox.Show("当前还未登录游戏！");
                return;
            }
            else if (this.selectUserId != GlobalVariable.loginUserInfo.userId)
            {
                MessageBox.Show("当前方案非登录账号方案！");
                return;
            }
            int selectIndex = this.GetPlanDataGridViewSelectIndex();
            int planId = Convert.ToInt32(this.planDataGridView.Rows[selectIndex].Cells["id"].Value);
            if(this.userPlan.TryGetValue(planId,out SuitAchieveTitlePlan plan))
            {

                if(plan.achieveTitleId > 0) GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.SETTITLE, new int[1] { plan.achieveTitleId});
                List<int> suitList = new List<int>();
                int glassesId = plan.glassesId;
                if (GlobalVariable.suitDictionary.ContainsKey(plan.suitId)) suitList = GlobalVariable.suitDictionary[plan.suitId].clothIdList;
                if (suitList.Count < 5 && glassesId > 0)
                {
                    suitList.Add(glassesId);
                }
                if(suitList.Count > 0)
                {
                    int[] sendData = new int[suitList.Count + 1];
                    sendData[0] = suitList.Count;
                    int index = 1;
                    foreach (int i in suitList)
                    {
                        sendData[index++] = i;
                    }
                    GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.CHANGE_CLOTH, sendData);
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// 搜索方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void placnSearchButton_Click(object sender, EventArgs e)
        {
            this.userPlan = DBController.SuitAndAchieveTitleDBController.SearchUserPlan(this.selectUserId,this.planSearchTextBox.Text);
            this.planDataGridView.Rows.Clear();
            if (this.userPlan != null)
            {
                foreach (int key in this.userPlan.Keys)
                {
                    SuitAchieveTitlePlan info = this.userPlan[key];
                    this.planDataGridView.Rows.Add(info.name,
                        GlobalVariable.suitDictionary.ContainsKey(info.suitId) ? GlobalVariable.suitDictionary[info.suitId].name : "无",
                        GlobalVariable.glassesDictionary.ContainsKey(info.glassesId) ? GlobalVariable.glassesDictionary[info.glassesId].name : "无",
                        GlobalVariable.achieveTitleDictionary.ContainsKey(info.achieveTitleId) ? GlobalVariable.achieveTitleDictionary[info.achieveTitleId].title : "无",
                        info.id);
                }
            }
            this.planDataGridView_CellClick(new object(), new DataGridViewCellEventArgs(0, 0));
        }

        /// <summary>
        /// 删除用户的方案及所持装备信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteUserButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否确认执行此操作?", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(result == DialogResult.OK)
            {
                DBController.SuitAndAchieveTitleDBController.DeletePlanByuserId(this.selectUserId);
                DBController.SuitAndAchieveTitleDBController.UserTableDeleteUser(this.selectUserId);
                //this.userPlan = new Dictionary<int, SuitAchieveTitlePlan>();
                //this.planDataGridView.Rows.Clear();
                //this.planDataGridView_CellClick(new object(), new DataGridViewCellEventArgs(0, 0));
                this.InitUserListComboBox();
            }
        }

        private void planSearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                this.placnSearchButton_Click(sender, e);
            }
        }
    }
}
