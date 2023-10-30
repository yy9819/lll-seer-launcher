using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;

namespace lll_seer_launcher
{
    public partial class EditPetResourceForm : Form
    {
        public EditPetResourceForm()
        {
            InitializeComponent();
        }

        private void EditPetResourceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void ChangePreviewSwf(object sender, EventArgs e)
        {
            int index = this.GetPreviewDataGridViewSelectIndex();
            int petId = Convert.ToInt32(this.previewDataGridView.Rows[index].Cells["searchPetId"].Value);
            petId = petId < 1400000 ? DBController.PetDBController.GetPetRealId(petId) : DBController.PetDBController.GetPetSkinsRealId(petId);
            if (petId > 0)
            {
                this.petSwfPreviewWebBrowser.Navigate($"https://seer.61.com/resource/fightResource/pet/swf/{petId}.swf");

            }
        }
        private int GetPreviewDataGridViewSelectIndex()
        {

            int selectIndex = 0;
            if (this.previewDataGridView.SelectedRows.Count > 0)
            {
                selectIndex = this.previewDataGridView.SelectedRows[0].Index;
            }
            else if (this.previewDataGridView.SelectedCells.Count > 0)
            {
                selectIndex = this.previewDataGridView.SelectedCells[0].RowIndex;
            }
            return selectIndex;
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

        private void EditPetResourceForm_Load(object sender, EventArgs e)
        {
            this.InitPreviewDataGridView(sender, e);
            this.InitPlanGridView(sender, e);
            this.planDataGridView_Click(sender, e);
        }
        private void InitPreviewDataGridView(object sender, EventArgs e)
        {
            if (this.searchPetRadioButton.Checked)
            {
                List<Pet> petInfo = this.searchIdRadioButton.Checked ? DBController.PetDBController.LikeSearchPetByPetId
                    (this.searchTextBox.Text != "" ? Convert.ToInt16(this.searchTextBox.Text) : 0) :
                    DBController.PetDBController.LikeSearchPetByPetName(this.searchTextBox.Text);
                this.previewDataGridView.Rows.Clear();
                //Console.WriteLine(petInfo.Count);
                if (petInfo != null)
                {
                    foreach(Pet pet in petInfo)
                    {
                        this.previewDataGridView.Rows.Add(pet.id,pet.defName);
                    }
                }
            }
            else
            {
                List<PetSkins> petInfo = this.searchIdRadioButton.Checked ? DBController.PetDBController.LikeSearchPetSkinsByPetId
                    (this.searchTextBox.Text != "" ? Convert.ToInt16(this.searchTextBox.Text) : 0) :
                    DBController.PetDBController.LikeSearchPetSkinsByPetName(this.searchTextBox.Text);
                this.previewDataGridView.Rows.Clear();
                //Console.WriteLine(petInfo.Count);
                if (petInfo != null)
                {
                    foreach (PetSkins pet in petInfo)
                    {
                        this.previewDataGridView.Rows.Add(pet.id, pet.name);
                    }
                }
            }
        }
        private void ClearTextBoxText(object sender, EventArgs e)
        {
            this.searchTextBox.Text = "";
            this.setPetButton.Enabled = this.searchPetRadioButton.Checked;
        }

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                this.InitPreviewDataGridView(sender, e);
            }
            else if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete && this.searchIdRadioButton.Checked)
            {
                e.Handled = true; 
            }
        }

        private void setPetButton_Click(object sender, EventArgs e)
        {
            int index = this.GetPreviewDataGridViewSelectIndex();
            int petId = Convert.ToInt32(this.previewDataGridView.Rows[index].Cells["searchPetId"].Value);
            if (petId > 0)
            {
                int planIndex = this.GetPlanDataGridViewSelectIndex();
                int targetIndex = this.planDataGridView.Rows.Count - 1 != planIndex ? planIndex : this.planDataGridView.Rows.Count - 1;
                this.planDataGridView.Rows[targetIndex].Cells["petId"].Value = petId;
                this.planDataGridView.Rows[targetIndex].Cells["petName"].Value = this.previewDataGridView.Rows[index].Cells["searchPetName"].Value;
            }
        }

        private void setSkinsButton_Click(object sender, EventArgs e)
        {
            int index = this.GetPreviewDataGridViewSelectIndex();
            int petId = Convert.ToInt32(this.previewDataGridView.Rows[index].Cells["searchPetId"].Value);
            if (petId > 0)
            {
                int planIndex = this.GetPlanDataGridViewSelectIndex();
                int targetIndex = this.planDataGridView.Rows.Count - 1 != planIndex ? planIndex : this.planDataGridView.Rows.Count - 1;
                this.planDataGridView.Rows[targetIndex].Cells["petSkinsId"].Value = petId;
                this.planDataGridView.Rows[targetIndex].Cells["petSkinsName"].Value = this.previewDataGridView.Rows[index].Cells["searchPetName"].Value;
            }
        }

        private void InitPlanGridView(object sender, EventArgs e)
        {
            this.planDataGridView.Rows.Clear();
            List<PetSkinsReplacePlan> plans = DBController.PetDBController.SearchPlan("");
            if(plans != null)
            {
                foreach(PetSkinsReplacePlan plan in plans)
                {
                    this.planDataGridView.Rows.Add(plan.petId,plan.petName,plan.skinsId,plan.skinsName);
                }
                FormController.SendMessageToSeerFiddler("3");
            }
        }

        private void addPlanButton_Click(object sender, EventArgs e)
        {
            int petId = Convert.ToInt32(this.planDataGridView.Rows[this.planDataGridView.Rows.Count - 1].Cells["petId"].Value);
            int skinsId = Convert.ToInt32(this.planDataGridView.Rows[this.planDataGridView.Rows.Count - 1].Cells["petSkinsId"].Value);
            if(petId > 0 && skinsId > 0)
            {
                string petName = Convert.ToString(this.planDataGridView.Rows[this.planDataGridView.Rows.Count - 1].Cells["petName"].Value);
                string petSkinsName = Convert.ToString(this.planDataGridView.Rows[this.planDataGridView.Rows.Count - 1].Cells["petSkinsName"].Value);
                PetSkinsReplacePlan plan = new PetSkinsReplacePlan(petId, petName, skinsId, petSkinsName);
                int result = DBController.PetDBController.AddPlan(plan);
                if(result != 0)
                {
                    this.InitPlanGridView(sender, e);
                }
                else
                {
                    //MessageBox.Show("亲爱的小赛尔，新建方案失败！查看一下是否对相同精灵进行了替换~");
                }
            }
            else
            {
                MessageBox.Show("亲爱的小赛尔，还有项目没有设置哟~");
            }
        }

        private void updatePlanButton_Click(object sender, EventArgs e)
        {
            int planIndex = this.GetPlanDataGridViewSelectIndex();
            if(planIndex != this.planDataGridView.Rows.Count - 1)
            {
                int petId = Convert.ToInt32(this.planDataGridView.Rows[planIndex].Cells["petId"].Value);
                int skinsId = Convert.ToInt32(this.planDataGridView.Rows[planIndex].Cells["petSkinsId"].Value);
                string petName = Convert.ToString(this.planDataGridView.Rows[planIndex].Cells["petName"].Value);
                string petSkinsName = Convert.ToString(this.planDataGridView.Rows[planIndex].Cells["petSkinsName"].Value);
                PetSkinsReplacePlan plan = new PetSkinsReplacePlan(petId, petName, skinsId, petSkinsName);
                int result = DBController.PetDBController.UpdatePlan(plan);
                if (result != 0)
                {
                    this.InitPlanGridView(sender, e);
                }
                else
                {
                    //MessageBox.Show("亲爱的小赛尔，新建方案失败！查看一下是否对相同精灵进行了替换~");
                }
            }
        }

        private void deletePlanButton_Click(object sender, EventArgs e)
        {
            int planIndex = this.GetPlanDataGridViewSelectIndex();
            if (planIndex != this.planDataGridView.Rows.Count - 1)
            {
                int petId = Convert.ToInt32(this.planDataGridView.Rows[planIndex].Cells["petId"].Value);
                int result = DBController.PetDBController.DeletePlan(petId);
                if (result != 0)
                {
                    this.InitPlanGridView(sender, e);
                }
                else
                {
                    MessageBox.Show("删除失败...");
                }
            }
            else
            {
                MessageBox.Show("亲爱的小赛尔，不可以删除还未保存的方案哦~");
            }
        }

        private void planDataGridView_Click(object sender, EventArgs e)
        {
            int index = this.GetPlanDataGridViewSelectIndex();
            this.updatePlanButton.Enabled = index != this.planDataGridView.Rows.Count - 1;
            this.deletePlanButton.Enabled = index != this.planDataGridView.Rows.Count - 1;
            this.addPlanButton.Enabled = index == this.planDataGridView.Rows.Count - 1;
            this.setPetButton.Enabled = index == this.planDataGridView.Rows.Count - 1;
        }
    }
}
