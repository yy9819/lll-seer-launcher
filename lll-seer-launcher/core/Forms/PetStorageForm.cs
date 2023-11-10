using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto.JSON;
using lll_seer_launcher.core.Dto.PetDto;

namespace lll_seer_launcher.core.Forms
{
    public partial class PetStorageForm : Form
    {
        private bool gettingPetInfo = false;
        public PetStorageForm()
        {
            InitializeComponent();
        }
        private PetHeadSetter petHeadSetter = new PetHeadSetter();
        private void PetStorageForm_Load(object sender, EventArgs e)
        {
            this.InitPetDataGridView(sender, e);
        }

        private void PetStorageForm_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void loadStoragePetButton_Click(object sender, EventArgs e)
        {
            new Task(() =>
            {
                this.SetPetList();
            }).Start();
        }
        private delegate void SetFormCallback();
        private void SetPetList(int targetId = 0)
        {
            if (!GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            long key = GlobalUtil.GetKey();
            GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.GET_PET_INFO_BY_ONCE, (HeadInfo headInfo) =>
            {
                new Thread(() =>
                {
                    GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.GET_PET_INFO_BY_ONCE, key);
                    Thread.Sleep(500);
                    SetFormCallback callback = delegate ()
                    {
                        lock (GlobalVariable.lockObjs["petList"])
                        {
                            if (!GlobalVariable.petList.ContainsKey(GlobalVariable.loginUserInfo.userId)) return;
                            this.petListDataGridView.Rows.Clear();
                            Dictionary<int, Dictionary<int, PetListInfo>> petList = GlobalVariable.petList[GlobalVariable.loginUserInfo.userId];
                            if (targetId == 0)
                            {
                                foreach (var petId in petList.Keys)
                                {
                                    string petName = DBController.PetDBController.SearchPetNameByPetId(petId);
                                    foreach (var pet in petList[petId].Values)
                                    {
                                        if (GlobalVariable.pets.ContainsKey(pet.catchTime) || GlobalVariable.awaitPets.ContainsKey(pet.catchTime) || pet.inBag)
                                        {
                                            GlobalVariable.petList[GlobalVariable.loginUserInfo.userId][petId][pet.catchTime].inBag = true;
                                            continue;
                                        }
                                        if (pet.level == 0 && GlobalVariable.petCatchTimeDic.ContainsKey(pet.catchTime))
                                        {
                                            pet.level = GlobalVariable.petCatchTimeDic[pet.catchTime].level;
                                        }
                                        this.petListDataGridView.Rows.Add(
                                            pet.petId,
                                            petName,
                                            pet.level,
                                            DateTimeOffset.FromUnixTimeSeconds(pet.catchTime).DateTime,
                                            pet.catchTime
                                            );
                                    }
                                }
                            }
                            else if (petList.ContainsKey(targetId))
                            {
                                string petName = DBController.PetDBController.SearchPetNameByPetId(targetId);
                                foreach (var pet in petList[targetId].Values)
                                {
                                    if (GlobalVariable.pets.ContainsKey(pet.catchTime) || GlobalVariable.awaitPets.ContainsKey(pet.catchTime) || pet.inBag)
                                    {
                                        GlobalVariable.petList[GlobalVariable.loginUserInfo.userId][targetId][pet.catchTime].inBag = true;
                                        continue;
                                    }
                                    if (pet.level == 0 && GlobalVariable.petCatchTimeDic.ContainsKey(pet.catchTime))
                                    {
                                        pet.level = GlobalVariable.petCatchTimeDic[pet.catchTime].level;
                                    }
                                    this.petListDataGridView.Rows.Add(
                                        pet.petId,
                                        petName,
                                        pet.level,
                                        DateTimeOffset.FromUnixTimeSeconds(pet.catchTime).DateTime,
                                        pet.catchTime
                                        );
                                }
                            }
                        }

                    };
                    this.Invoke(callback);
                }).Start();
            },key);
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.GET_PET_INFO_BY_ONCE, new int[0]);
        }
        private void ClearTextBoxText(object sender, EventArgs e)
        {
            this.searchTextBox.Text = "";
        }

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.InitPetDataGridView(sender, e);
            }
            else if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete && this.searchIdRadioButton.Checked)
            {
                e.Handled = true;
            }
        }

        private void InitPetDataGridView(object sender, EventArgs e)
        {
            List<Pet> petInfo = this.searchIdRadioButton.Checked ? DBController.PetDBController.LikeSearchPetByPetId
                    (this.searchTextBox.Text != "" ? Convert.ToInt16(this.searchTextBox.Text) : 0) :
                    DBController.PetDBController.LikeSearchPetByPetName(this.searchTextBox.Text);
            this.petDataGridView.Rows.Clear();
            //Console.WriteLine(petInfo.Count);
            if (petInfo != null)
            {
                foreach (Pet pet in petInfo)
                {
                    this.petDataGridView.Rows.Add(pet.id, pet.defName);
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.InitPetDataGridView(sender, e);
        }

        private void petDataGridView_Click(object sender, EventArgs e)
        {
            int index = this.GetPetDataGridViewSelectIndex();
            this.SetPetList(Convert.ToInt32(this.petDataGridView.Rows[index].Cells["targetPetId"].Value));
        }

        private int GetPetDataGridViewSelectIndex()
        {

            int selectIndex = 0;
            if (this.petDataGridView.SelectedRows.Count > 0)
            {
                selectIndex = this.petDataGridView.SelectedRows[0].Index;
            }
            else if (this.petDataGridView.SelectedCells.Count > 0)
            {
                selectIndex = this.petDataGridView.SelectedCells[0].RowIndex;
            }
            return selectIndex;
        }

        private int GetPetListDataGridViewSelectIndex()
        {

            int selectIndex = -1;
            if (this.petListDataGridView.SelectedRows.Count > 0)
            {
                selectIndex = this.petListDataGridView.SelectedRows[0].Index;
            }
            else if (this.petListDataGridView.SelectedCells.Count > 0)
            {
                selectIndex = this.petListDataGridView.SelectedCells[0].RowIndex;
            }
            return selectIndex;
        }

        private void petListDataGridView_Click(object sender, EventArgs e)
        {
            int selectIndex = this.GetPetListDataGridViewSelectIndex();
            if (selectIndex < 0 || this.gettingPetInfo || GlobalVariable.gameConfigFlag.inFight) return;
            this.gettingPetInfo = true;
            int petCatchTime = Convert.ToInt32(this.petListDataGridView.Rows[selectIndex].Cells["realCatchTime"].Value);
            long key = GlobalUtil.GetKey();
            GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.GET_PET_INFO, (HeadInfo headInfo) =>
            {
                int catchTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(headInfo.decryptData,148, 4));
                if (catchTime == petCatchTime)
                {
                    GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.GET_PET_INFO, key);
                    Thread.Sleep(100);
                    while (!GlobalVariable.petCatchTimeDic.ContainsKey(petCatchTime))
                    {
                        Thread.Sleep(100);
                    }
                    //Thread.Sleep(1000);
                    PetInfo petInfo = GlobalVariable.petCatchTimeDic[petCatchTime];
                    string headPath = this.petHeadSetter.GetHeadPath(petInfo.petId);

                    SetFormCallback callback = delegate ()
                    {
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

                        this.SetPetInfoPreview(petInfo);
                        this.gettingPetInfo = false;
                    };
                    if (this.Disposing || this.IsDisposed || !this.Visible) return;
                    this.Invoke(callback);
                }
            }, key);
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.GET_PET_INFO, new int[1] { petCatchTime });
        }
        private void SetPetInfoPreview(PetInfo petInfo)
        {
            this.singleInfoLabel.Text =
                $"精灵ID:{petInfo.petId}  属性:{petInfo.type}\n\n" +
                $"精灵名:{petInfo.petName}\n\n" +
                $"等级:{petInfo.level}  个体:{petInfo.dv}\n\n";

            this.abilityLabel.Text = 
                $"性格:{petInfo.nature}\n\n" +
                $"攻击:{petInfo.attack} \n\n" +
                $"防御:{petInfo.defence} \n\n" +
                $"特攻:{petInfo.spAttack} \n\n" +
                $"特防:{petInfo.spDefence} \n\n" +
                $"速度:{petInfo.speed} \n\n" +
                $"体力:{petInfo.hp}/{petInfo.maxHp} \n \n";

            this.skillLabel.Text = "技能:\n\n";
            foreach (var skill in petInfo.skillArray.Values)
            {
                if(skill.skillId != 0)
                {
                    this.skillLabel.Text += $"    {skill.skillName}\n\n";
                }
            }

            int effectId = 0;
            string effectTips = "";
            foreach (var eft in petInfo.petEffectInfos)
            {
                if(eft.status == 5)
                {
                    effectId = eft.effectId;
                    effectTips = DBController.EffectDBController.GetPetEffect(effectId);
                }
                else if(eft.status == 1)
                {
                    NewSeIdx info = DBController.EffectDBController.GetNewSeInfo(eft.effectId,eft.args);
                    if (info.starLevel >= 0)
                    {
                        this.abilityLabel.Text += $"特性:{info.desc} {info.starLevel}星 \n ({info.intro})";
                    }
                }
                
            }
            if(effectId == 0)
            {
                this.effectIcon.Image = null;
                this.effectTip.RemoveAll();
            }
            else
            {
                this.effectIcon.Image = Properties.Resources.effectIcon;
                this.effectTip.SetToolTip(this.effectIcon, effectTips.Replace("；", "；\n").Replace("|","\n"));
            }
        }

        private void takeInBagButton_Click(object sender, EventArgs e)
        {
            int selectIndex = this.GetPetListDataGridViewSelectIndex();
            if (!GlobalVariable.isLogin || selectIndex < 0) return;
            int petCatchTime = Convert.ToInt32(this.petListDataGridView.Rows[selectIndex].Cells["realCatchTime"].Value);
            long key = GlobalUtil.GetKey();
            GlobalVariable.analyzeRecvDataController.SetRecvEventListener(CmdId.GET_PET_INFO_BY_ONCE, (HeadInfo recvDataHeadInfo) =>
             {
                 GlobalVariable.analyzeRecvDataController.RemoveRecvEventListener(CmdId.GET_PET_INFO_BY_ONCE, key);
                 int index = 0;
                 // 获取当前背包内精灵总数
                 int petCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
                 index += 4;
                 // 获取每只精灵的详细信息
                 for (int i = 0; i < petCount; i++)
                 {
                     index = new PetInfo().GetNextIndex(index, recvDataHeadInfo.decryptData);
                 }
                 int awaitPetCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
                 if(awaitPetCount + petCount < GlobalVariable.loginUserInfo.petBagGreatestAmount)
                 {
                     GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.PET_RELEASE, new int[2]
                     {
                         petCatchTime, petCount < 6 ? 1 : 2
                     });
                     SetFormCallback callback = delegate ()
                     {
                         this.petListDataGridView.Rows.Remove(this.petListDataGridView.Rows[selectIndex]);
                     };
                     this.Invoke(callback);
                 }
                 else
                 {
                     MessageBox.Show("亲爱的小赛尔\n当前可携带精灵数已超过你的背包容量了哟~");
                 }
             },key);
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.GET_PET_INFO_BY_ONCE, new int[0]);
        }

        private void openPetBagButton_Click(object sender, EventArgs e)
        {
            GlobalVariable.mainForm.showPetBagToolStripMenuItem_Click(sender, e);
        }
    }
}
