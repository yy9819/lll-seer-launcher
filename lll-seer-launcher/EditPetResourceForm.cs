using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;

namespace lll_seer_launcher
{
    public partial class EditPetResourceForm : Form
    {
        private string ieCachePath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
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
            this.petSwfPreviewWebBrowser.Url = new System.Uri("https://seer.61.com/resource/fightResource/pet/swf/5000.swf", System.UriKind.Absolute);
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
                    this.DeleteCacheFile(new int[1] { petId });
                    this.InitPlanGridView(sender, e);
                    Logger.Log("addPlan",$"新建皮肤替换方案{petId}-{petName}-->{petSkinsId}-{petSkinsName}");
                }
                else
                {
                    Logger.Error( $"皮肤替换方案{petId}-{petName}-->{petSkinsId}-{petSkinsName}新建失败...");
                    MessageBox.Show("亲爱的小赛尔，新建方案失败！查看一下是否对相同精灵进行了替换~");
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
                    this.DeleteCacheFile(new int[1] { petId });
                    this.InitPlanGridView(sender, e);
                    Logger.Log("updatePlan", $"修改皮肤替换方案{petId}-{petName}-->{skinsId}-{petSkinsName}");
                }
                else
                {
                    MessageBox.Show("亲爱的小赛尔，修改方案失败！查看一下是否对相同精灵进行了替换~");
                }
            }
        }
        private void DeleteCacheFile(int[] petId)
        {
            string command = "";
            foreach (int id in petId)
            {
                command +=$"dir {this.ieCachePath}\\*{id}[*].swf /b /s & ";
            }
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // 创建一个Process对象并启动
            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();

                // 获取Process的输入流
                StreamWriter streamWriter = process.StandardInput;

                // 写入要执行的命令
                streamWriter.WriteLine(command);

                // 关闭输入流
                streamWriter.Close();

                // 等待命令执行完毕
                process.WaitForExit();

                // 读取输出流中的结果
                string output = process.StandardOutput.ReadToEnd();
                string[] filePathList = output.Split('\n');
                foreach(string filePath in filePathList)
                {
                    try
                    {
                        if (File.Exists(filePath)) File.Delete(filePath);
                    }
                    catch { }
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
                    this.DeleteCacheFile(new int[1] { petId });
                    this.InitPlanGridView(sender, e);
                    Logger.Log("deletePlan", $"删除精灵ID:{petId}的皮肤替换方案");
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

        private void deletePetCacheButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否删除皮肤方案内的被替换精灵文件缓存?", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                List<PetSkinsReplacePlan> plans = DBController.PetDBController.SearchPlan("");
                if (plans != null)
                {
                    List<int> petId = new List<int>();
                    foreach (PetSkinsReplacePlan plan in plans)
                    {
                        petId.Add(plan.petId);
                    }
                    this.DeleteCacheFile(petId.ToArray());
                    Logger.Log("deleteCache", $"手动清理精灵缓存文件");
                }
            }
        }
    }
}
