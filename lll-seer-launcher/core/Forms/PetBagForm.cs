using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;
using lll_seer_launcher.core.Dto.PetDto;
using lll_seer_launcher.core.Utils;


namespace lll_seer_launcher.core.Forms
{
    public partial class PetBagForm : Form
    {
        ImageList fightPetHeadList = new ImageList();
        ImageList awaitPetHeadList = new ImageList();
        private PetHeadSetter petHeadSetter = new PetHeadSetter();

        Dictionary<int, List<PetInfo>> fightPets = new Dictionary<int, List<PetInfo>>();
        Dictionary<int, List<PetInfo>> awaitPets = new Dictionary<int, List<PetInfo>>();
        List<PetBagPlan> plan = new List<PetBagPlan>();

        private delegate void SetFormCallback();
        public PetBagForm()
        {
            InitializeComponent();
        }

        private void PetBagForm_Load(object sender, EventArgs e)
        {
            this.awaitPetListView.SelectedIndexChanged += this.OnAwaitPetSelected;
            this.fightPetListView.SelectedIndexChanged += this.OnFightPetSelected;
        }
        private void PetBagForm_Closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void CheckDic()
        {
            if (!GlobalVariable.isLogin) return;
            if (!this.fightPets.ContainsKey(GlobalVariable.loginUserInfo.userId)) this.fightPets.Add(GlobalVariable.loginUserInfo.userId, new List<PetInfo>());
            if (!this.awaitPets.ContainsKey(GlobalVariable.loginUserInfo.userId)) this.awaitPets.Add(GlobalVariable.loginUserInfo.userId, new List<PetInfo>());
        }
        private void SetPetBag()
        {
            //等待内置监听设置精灵背包完毕
            while (GlobalVariable.gameConfigFlag.getPetBag && GlobalVariable.isLogin)
            {
                Thread.Sleep(100);
            }
            if (!GlobalVariable.isLogin) return;
            //提取精灵背包内的精灵
            lock (GlobalVariable.lockObjs["petBag"])
            {
                this.CheckDic();
                this.fightPets[GlobalVariable.loginUserInfo.userId].Clear();
                this.awaitPets[GlobalVariable.loginUserInfo.userId].Clear();
                foreach (var petInfo in GlobalVariable.pets.Values)
                {
                    this.fightPets[GlobalVariable.loginUserInfo.userId].Add(petInfo);
                }
                foreach (var petInfo in GlobalVariable.awaitPets.Values)
                {
                    this.awaitPets[GlobalVariable.loginUserInfo.userId].Add(petInfo);
                }
            }
        }
        private void reloadButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            //防止连续点击
            this.reloadButton.Enabled = false;
            long key = GlobalUtil.GetKey();
            //设置获取精灵背包封包监听方法
            GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.GET_PET_INFO_BY_ONCE, (HeadInfo recvData) =>
            {
                //移除监听方法
                GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.GET_PET_INFO_BY_ONCE, key);
                this.SetPetBag();
                //设置精灵背包
                SetFormCallback callback = delegate ()
                {
                    if (!GlobalVariable.isLogin)
                    {
                        this.reloadButton.Enabled = true;
                        return;
                    }
                    //将当前出战信息清空
                    this.fightPetHeadList.Images.Clear();
                    this.fightPetHeadList.ImageSize = new Size(32, 32);
                    this.fightPetListView.Items.Clear();
                    //设置当前背包内的出战精灵头像
                    foreach (var petInfo in fightPets[GlobalVariable.loginUserInfo.userId])
                    {
                        string headPath = this.petHeadSetter.GetHeadPath(petInfo.petId);
                        Image loginPlayerPetImg;
                        try
                        {
                            loginPlayerPetImg = Image.FromFile(headPath);
                        }
                        catch
                        {
                            loginPlayerPetImg = Properties.Resources._214;
                        }
                        this.fightPetHeadList.Images.Add(loginPlayerPetImg);
                    }
                    this.fightPetListView.LargeImageList = fightPetHeadList;
                    //将当前备战精灵信息清空
                    this.awaitPetHeadList.Images.Clear();
                    this.awaitPetHeadList.ImageSize = new Size(32, 32);
                    this.awaitPetListView.Items.Clear();
                    //设置当前备战精灵的头像
                    foreach (var petInfo in awaitPets[GlobalVariable.loginUserInfo.userId])
                    {
                        string headPath = this.petHeadSetter.GetHeadPath(petInfo.petId);
                        Image loginPlayerPetImg;
                        try
                        {
                            loginPlayerPetImg = Image.FromFile(headPath);
                        }
                        catch
                        {
                            loginPlayerPetImg = Properties.Resources._214;
                        }
                        this.awaitPetHeadList.Images.Add(loginPlayerPetImg);
                    }
                    this.awaitPetListView.LargeImageList = awaitPetHeadList;
                    //设置出战精灵信息
                    int i = 0;
                    foreach (var petInfo in fightPets[GlobalVariable.loginUserInfo.userId])
                    {
                        ListViewItem listViewItem = new ListViewItem();
                        listViewItem.ImageIndex = i++;
                        listViewItem.Text = petInfo.petName;
                        this.fightPetListView.Items.Add(listViewItem);
                    }
                    //设置备战精灵信息
                    i = 0;
                    foreach (var petInfo in awaitPets[GlobalVariable.loginUserInfo.userId])
                    {
                        ListViewItem listViewItem = new ListViewItem();
                        listViewItem.ImageIndex = i++;
                        listViewItem.Text = petInfo.petName;
                        this.awaitPetListView.Items.Add(listViewItem);
                    }
                    //设置完毕，恢复加载按钮的可操作性
                    this.reloadButton.Enabled = true;
                };
                this.Invoke(callback);

            },key);
            //发送获取精灵背包封包
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.GET_PET_INFO_BY_ONCE, new int[0]);
            //设置当前正在查询精灵背包中
            GlobalVariable.gameConfigFlag.getPetBag = true;
        }
        private void OnAwaitPetSelected(object sender,EventArgs e)
        {
            if (this.awaitPetListView.SelectedItems.Count <= 0 || !GlobalVariable.isLogin) return;
            //获取所选择的备战精灵信息
            PetInfo targetPet = this.awaitPets[GlobalVariable.loginUserInfo.userId][this.awaitPetListView.SelectedIndices[0]];
            //设置精灵信息
            this.SetPetInfoPreview(targetPet);

        }
        private void OnFightPetSelected(object sender,EventArgs e)
        {
            if (this.fightPetListView.SelectedItems.Count <= 0 || !GlobalVariable.isLogin) return;
            //获取所选择的出战精灵信息
            PetInfo targetPet = this.fightPets[GlobalVariable.loginUserInfo.userId][this.fightPetListView.SelectedIndices[0]];
            //设置精灵信息
            this.SetPetInfoPreview(targetPet);

        }
        private void SetPetInfoPreview(PetInfo petInfo)
        {
            //获取头像路径
            string headPath = this.petHeadSetter.GetHeadPath(petInfo.petId);
            //设置头像图片
            Image loginPlayerPetImg;
            try
            {
                loginPlayerPetImg = Image.FromFile(headPath);
            }
            catch
            {
                loginPlayerPetImg = Properties.Resources._214;
            }
            this.petHead.Image = loginPlayerPetImg;
            //设置精灵信息
            this.singleInfoLabel.Text =
                $"精灵ID:{petInfo.petId}  属性:{petInfo.type}\n\n" +
                $"精灵名:{petInfo.petName}\n\n" +
                $"等级:{petInfo.level}  个体:{petInfo.dv}\n\n";

            this.abilityLabel.Text =
                $"性格:{petInfo.nature}\n\n" +
                $"攻击:{petInfo.attack}{(petInfo.evAttack > 0 ? $"({petInfo.evAttack})" : "")} \n\n" +
                $"防御:{petInfo.defence}{(petInfo.evDefence > 0 ? $"({petInfo.evDefence})" : "")} \n\n" +
                $"特攻:{petInfo.spAttack}{(petInfo.evSpAttack > 0 ? $"({petInfo.evSpAttack})" : "")} \n\n" +
                $"特防:{petInfo.spDefence}{(petInfo.evSpDefence > 0 ? $"({petInfo.evSpDefence})" : "")} \n\n" +
                $"速度:{petInfo.speed}{(petInfo.evSpeed > 0 ? $"({petInfo.evSpeed})" : "")} \n\n" +
                $"体力:{petInfo.hp}/{petInfo.maxHp} {(petInfo.evHp > 0 ? $"({petInfo.evHp})" : "")}\n \n";
            //设置精灵技能
            this.skillLabel.Text = "技能:\n\n";
            foreach (var skill in petInfo.skillArray.Values)
            {
                if (skill.skillId != 0)
                {
                    this.skillLabel.Text += $"    {skill.skillName}\n\n";
                }
            }
            //获取并设置精灵特性，获取魂印信息
            int effectId = 0;
            string effectTips = "";
            foreach (var eft in petInfo.petEffectInfos)
            {
                if (eft.status == 5)
                {
                    effectId = eft.effectId;
                    effectTips = DBController.EffectDBController.GetPetEffect(effectId);
                }
                else if (eft.status == 1)
                {
                    NewSeIdx info = DBController.EffectDBController.GetNewSeInfo(eft.effectId, eft.args);
                    if (info.starLevel >= 0)
                    {
                        this.abilityLabel.Text += $"特性:{info.desc} {info.starLevel}星 \n ({info.intro})";
                    }
                }

            }
            //设置魂印信息
            if (effectId == 0)
            {
                this.effectIcon.Image = null;
                this.effectTip.RemoveAll();
            }
            else
            {
                this.effectIcon.Image = Properties.Resources.effectIcon;
                this.effectTip.SetToolTip(this.effectIcon, effectTips.Replace("；", "；\n").Replace("|", "\n"));
            }
        }
        /// <summary>
        /// 备战精灵入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takeAwaitPetInStorageButton_Click(object sender, EventArgs e)
        {
            if (this.awaitPetListView.SelectedItems.Count <= 0 || !GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            long key = GlobalUtil.GetKey() - new Random().Next(200, 1000);
            //获取需要入库的备战精灵索引
            int index = this.awaitPetListView.SelectedIndices[0];
            //获取目标精灵信息
            PetInfo targetPet = this.awaitPets[GlobalVariable.loginUserInfo.userId][index];
            //设置监听器
            GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.PET_RELEASE, (HeadInfo recvData) =>
             {
                 //移除监听器
                 GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.PET_RELEASE, key);
                 //查看是否入库成功
                 if (recvData.decryptData.Length > 0)
                 {
                     SetFormCallback callback = delegate ()
                     {
                         this.awaitPetListView.Items.RemoveAt(index);
                     };
                     this.Invoke(callback);
                     this.awaitPets[GlobalVariable.loginUserInfo.userId].RemoveAt(index);
                 }
             },key);
            //发送入库封包
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_RELEASE, new int[2]
            {
                targetPet.catchTime,3
            });
        }
        /// <summary>
        /// 出战精灵入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takeInStorageButton_Click(object sender, EventArgs e)
        {
            if (this.fightPetListView.SelectedItems.Count <= 0 || !GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            long key = GlobalUtil.GetKey() - new Random().Next(200, 1000);
            //获取出战精灵索引
            int index = this.fightPetListView.SelectedIndices[0];
            //获取目标精灵信息
            PetInfo targetPet = this.fightPets[GlobalVariable.loginUserInfo.userId][index];
            //设置入库封包监听器
            GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.PET_RELEASE, (HeadInfo recvData) =>
            {
                //移除监听器
                GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.PET_RELEASE, key);
                //判断是否入库成功
                if (recvData.decryptData.Length > 0)
                {
                    SetFormCallback callback = delegate ()
                    {
                        this.fightPetListView.Items.RemoveAt(index);
                    };
                    this.Invoke(callback);
                    this.fightPets[GlobalVariable.loginUserInfo.userId].RemoveAt(index);
                }
            }, key);
            //发送入库封包
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_RELEASE, new int[2]
            {
                targetPet.catchTime,0
            });
        }
        /// <summary>
        /// 备战精灵首发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void awaitPetToFristButton_Click(object sender, EventArgs e)
        {
            if (this.awaitPetListView.SelectedItems.Count <= 0 || !GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            long key = GlobalUtil.GetKey();
            //获取目标精灵索引
            int index = this.awaitPetListView.SelectedIndices[0];
            //获取目标精灵信息
            PetInfo targetPet = this.awaitPets[GlobalVariable.loginUserInfo.userId][index];
            //设置监听器
            GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.PET_DEFAULT, (HeadInfo recvData) =>
            {
                //移除监听器
                GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.PET_DEFAULT, key);
                SetFormCallback callback = delegate ()
                {
                    this.reloadButton_Click(sender,e);
                };
                this.Invoke(callback);
            }, key);
            //发送首发封包
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_DEFAULT, new int[1]
            {
                targetPet.catchTime
            });
        }
        /// <summary>
        /// 出战精灵首发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toFristButton_Click(object sender, EventArgs e)
        {
            if (this.fightPetListView.SelectedItems.Count <= 0 || !GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            //获取出战精灵索引
            int index = this.fightPetListView.SelectedIndices[0];
            //如果已经是首发精灵则不进行后续操作
            if(index < 1) return;
            //获取目标精灵信息
            PetInfo targetPet = this.fightPets[GlobalVariable.loginUserInfo.userId][index];
            long key = GlobalUtil.GetKey();
            //设置监听器
            GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.PET_DEFAULT, (HeadInfo recvData) =>
            {
                //移除监听器
                GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.PET_DEFAULT, key);
                SetFormCallback callback = delegate ()
                {
                    this.reloadButton_Click(sender, e);
                };
                this.Invoke(callback);
            }, key);
            //发送首发封包
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_DEFAULT, new int[1]
            {
                targetPet.catchTime
            });
        }
        /// <summary>
        /// 搜索精灵背包方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchPlanButton_Click(object sender, EventArgs e)
        {
            //获取搜索结果
            this.plan = DBController.PetDBController.SearchPetPlan(this.planNameTextBox.Text);
            //设置方案表
            this.SetPlanDataGridView(this.plan);
        }
        /// <summary>
        /// 设置方案一览
        /// </summary>
        /// <param name="plan"></param>
        private void SetPlanDataGridView(List<PetBagPlan> plan)
        {
            //清空方案
            this.planDataGridView.Rows.Clear();
            //设置方案一览
            foreach(PetBagPlan planItem in plan)
            {
                this.planDataGridView.Rows.Add(
                    planItem.planName,
                    planItem.planId,
                    planItem.fightPetsName,
                    planItem.fightPetsCatchTime,
                    planItem.awaitPetsName,
                    planItem.awaitPetsCatchTime,
                    planItem.userId);
            }
            this.planDataGridView_Click(new object(), new DataGridViewCellEventArgs(0, 0));
        }
        /// <summary>
        /// 获取所选方案的索引
        /// </summary>
        /// <returns></returns>
        private int GetSelectPlanIndex()
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
        /// 更新方案的名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updatePlanNameButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin) return;
            this.CheckDic();
            //获取目标索引
            int planIndex = this.GetSelectPlanIndex();
            int userId = Convert.ToInt32(this.planDataGridView.Rows[planIndex].Cells["userId"].Value);
            if(userId != GlobalVariable.loginUserInfo.userId)
            {
                this.searchPlanButton_Click(sender, e);
                MessageBox.Show("亲爱的小赛尔\n所选方案不是当前登录账号的方案哟~");
                return;
            }
            //获取目标的方案信息
            PetBagPlan plan = new PetBagPlan();
            plan.planId = Convert.ToInt32(this.planDataGridView.Rows[planIndex].Cells["planId"].Value);
            plan.planName = Convert.ToString(this.planDataGridView.Rows[planIndex].Cells["planName"].Value);
            //执行更新操作
            int result = DBController.PetDBController.UpdatePetBagPlan(plan);
            if (result != 1)
            {
                MessageBox.Show("方案保存失败...");
            }
            else
            {
                //更新方案列表
                this.searchPlanButton_Click(sender, e);
                Logger.Log("EditPetBagPlan", $"user:{GlobalVariable.loginUserInfo.userId} 修改方案ID:[{plan.planId}]背包方案名为[{plan.planName}].");
                MessageBox.Show("方案保存成功！");
            }
        }
        /// <summary>
        /// 当所选方案发生改变时，设置方案各按钮的可操作状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void planDataGridView_Click(object sender, EventArgs e)
        {
            int selectIndex = this.GetSelectPlanIndex();
            if (this.plan == null || this.plan.Count <= 0 || selectIndex >= this.plan.Count)
            {
                this.updatePlanNameButton.Enabled = false;
                this.deletePlanbutton.Enabled = false;
                this.changePlanButton.Enabled = false;
                return;
            }
            this.updatePlanNameButton.Enabled = true;
            this.deletePlanbutton.Enabled = true;
            this.changePlanButton.Enabled = true;
        }
        /// <summary>
        /// 导入当前背包内的精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputPlanButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin) return;
            this.CheckDic();
            //确保出战精灵至少为1
            if (this.fightPets[GlobalVariable.loginUserInfo.userId].Count < 1)
            {
                MessageBox.Show("亲爱的小赛尔\n需要至少设置一只出战精灵哟~");
                return;
            }
            //设置编辑栏索引
            int index = this.plan.Count;
            //新建方案
            PetBagPlan plan = new PetBagPlan();
            //设置方案的出战精灵信息
            foreach(var petInfo in this.fightPets[GlobalVariable.loginUserInfo.userId])
            {
                plan.fightPetsName += $"{petInfo.petName}|";
                plan.fightPetsCatchTime += $"{petInfo.catchTime}|";
            }
            //设置备战精灵信息
            foreach(var petInfo in awaitPets[GlobalVariable.loginUserInfo.userId])
            {
                plan.awaitPetsName += $"{petInfo.petName}|";
                plan.awaitPetsCatchTime += $"{petInfo.catchTime}|";
            }
            //将方案信息设置到表格的编辑栏
            this.planDataGridView.Rows[index].Cells["fightPetNameList"].Value = plan.fightPetsName;
            this.planDataGridView.Rows[index].Cells["fightPetCatchTiemList"].Value = plan.fightPetsCatchTime;
            this.planDataGridView.Rows[index].Cells["awaitPetNameList"].Value = plan.awaitPetsName;
            this.planDataGridView.Rows[index].Cells["awaitPetCatchTimeList"].Value = plan.awaitPetsCatchTime;
            this.planDataGridView.Rows[index].Cells["userId"].Value = GlobalVariable.loginUserInfo.userId;
        }
        /// <summary>
        /// 保存新方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveNewPlanButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin) return;
            this.CheckDic();
            //设置新方案的索引
            int index = this.plan.Count;
            //获取对应索引的方案信息
            PetBagPlan plan = new PetBagPlan();
            plan.fightPetsName = Convert.ToString(this.planDataGridView.Rows[index].Cells["fightPetNameList"].Value);
            //如出战精灵未设置，则报错
            if(plan.fightPetsName == "")
            {
                MessageBox.Show("亲爱的小赛尔\n需要至少设置一只出战精灵哟~");
                return;
            }
            int userId = Convert.ToInt32(this.planDataGridView.Rows[index].Cells["userId"].Value);
            if (userId != GlobalVariable.loginUserInfo.userId)
            {
                MessageBox.Show("亲爱的小赛尔\n当前方案不属于你的账号哟~");
                return;
            }
            //设置方案详细
            plan.planName = Convert.ToString(this.planDataGridView.Rows[index].Cells["planName"].Value);
            plan.fightPetsCatchTime = Convert.ToString(this.planDataGridView.Rows[index].Cells["fightPetCatchTiemList"].Value);
            plan.awaitPetsName = Convert.ToString(this.planDataGridView.Rows[index].Cells["awaitPetNameList"].Value);
            plan.awaitPetsCatchTime = Convert.ToString(this.planDataGridView.Rows[index].Cells["awaitPetCatchTimeList"].Value);
            //向数据库插入方案
            int result = DBController.PetDBController.AddPetBagPlan(plan);
            //判断方案是否新建成功
            if(result > 0)
            {
                this.searchPlanButton_Click(sender, e);
                Logger.Log("AddPetBagPlan",$"user:{GlobalVariable.loginUserInfo.userId} 新建背包方案:[{plan.planName}].");
                MessageBox.Show("方案新建成功!");
            }
            else
            {
                MessageBox.Show("方案创建失败...");
            }
        }
        /// <summary>
        /// 删除方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletePlanbutton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin) return;
            this.CheckDic();
            //获取目标方案索引
            int planIndex = this.GetSelectPlanIndex();
            int userId = Convert.ToInt32(this.planDataGridView.Rows[planIndex].Cells["userId"].Value);
            if (userId != GlobalVariable.loginUserInfo.userId)
            {
                this.searchPlanButton_Click(sender, e);
                MessageBox.Show("亲爱的小赛尔\n所选方案不是当前登录账号的方案哟~");
                return;
            }
            //获取方案id
            int planId = Convert.ToInt32(this.planDataGridView.Rows[planIndex].Cells["planId"].Value);
            string planName = Convert.ToString(this.planDataGridView.Rows[planIndex].Cells["planName"].Value);
            //执行删除操作
            int result = DBController.PetDBController.DeletePetBagPlan(planId);
            //判断是否删除成功
            if (result > 0)
            {
                this.searchPlanButton_Click(sender, e);
                Logger.Log("DeletePetBagPlan", $"user:{GlobalVariable.loginUserInfo.userId} 删除背包方案:[{planName}].");
                MessageBox.Show("方案删除成功!");
            }
            else
            {
                MessageBox.Show("方案删除失败...");
            }
        }

        private void changePlanButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            this.CheckDic();
            int planIndex = this.GetSelectPlanIndex();
            int userId = Convert.ToInt32(this.planDataGridView.Rows[planIndex].Cells["userId"].Value);
            if (userId != GlobalVariable.loginUserInfo.userId)
            {
                this.searchPlanButton_Click(sender, e);
                MessageBox.Show("亲爱的小赛尔\n所选方案不是当前登录账号的方案哟~");
                return;
            }
            this.changePlanButton.Enabled = false;
            string[] fightPetCatchTime = Convert.ToString(this.planDataGridView.Rows[planIndex].Cells["fightPetCatchTiemList"].Value).Split('|');
            string[] awaitPetCatchTime = Convert.ToString(this.planDataGridView.Rows[planIndex].Cells["awaitPetCatchTimeList"].Value).Split('|');
            long key = GlobalUtil.GetKey();
            GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.GET_PET_INFO_BY_ONCE, (HeadInfo recvData) =>
             {
                 //移除监听方法
                 GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.GET_PET_INFO_BY_ONCE, key);
                 this.SetPetBag();
                 SetFormCallback callback = delegate ()
                 {
                     if (!GlobalVariable.isLogin)
                     {
                         this.changePlanButton.Enabled = true;
                         return;
                     }

                     foreach (var petInfo in this.fightPets[GlobalVariable.loginUserInfo.userId])
                     {
                         GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_RELEASE, new int[2]
                         {
                         petInfo.catchTime,0
                         });
                     }
                     foreach (var petInfo in this.awaitPets[GlobalVariable.loginUserInfo.userId])
                     {
                         GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_RELEASE, new int[2]
                         {
                         petInfo.catchTime,3
                         });
                     }

                     foreach (var catchTime in fightPetCatchTime)
                     {
                         if (catchTime == "") continue;
                         GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_RELEASE, new int[2]
                         {
                         Convert.ToInt32(catchTime),1
                         });
                     }
                     foreach (var catchTime in awaitPetCatchTime)
                     {
                         if (catchTime == "") continue;
                         GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_RELEASE, new int[2]
                         {
                         Convert.ToInt32(catchTime),2
                         });
                     }
                     this.changePlanButton.Enabled = true;
                     this.reloadButton_Click(sender, e);
                 };
                 this.Invoke(callback);
             }, key);
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.GET_PET_INFO_BY_ONCE, new int[0]);
            //设置当前正在查询精灵背包中
            GlobalVariable.gameConfigFlag.getPetBag = true;
        }

        private void openStorageButton_Click(object sender, EventArgs e)
        {
            GlobalVariable.mainForm.openPetStorageToolStripMenuItem_Click(sender, e);
        }

        private void lowerHPButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            GlobalVariable.mainForm.lowerHpToolStripMenuItem_Click(sender, e);
        }

        private void curePetButton_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_CURE_FREE, new int[0]);
        }
    }
}
