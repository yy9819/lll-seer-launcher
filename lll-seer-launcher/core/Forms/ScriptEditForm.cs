using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.JSON;
using System.Text.RegularExpressions;

namespace lll_seer_launcher.core.Forms
{
    public partial class ScriptEditForm : Form
    {
        private string scriptPath { get; set; }
        private ScriptDecryptEncryptUtil scriptDecryptEncryptUtil = new ScriptDecryptEncryptUtil();
        private ScriptJsonDto scriptDto = null;
        public ScriptEditForm()
        {
            InitializeComponent();
            this.scriptPath =  Directory.GetCurrentDirectory() + "\\bin\\script";
            if (!Directory.Exists(this.scriptPath)) Directory.CreateDirectory(this.scriptPath);
            this.scriptsDataGridView.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void exportScriptButton_Click(object sender, EventArgs e)
        {
            if(this.scriptTitleTextBox.Text == "")
            {
                MessageBox.Show("必须给脚本取一个标题哦!");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = $"{this.scriptTitleTextBox.Text}.script";
            saveFileDialog.Filter = "脚本文件(*.script)|*.script";
            saveFileDialog.InitialDirectory = this.scriptPath;
            DialogResult result = saveFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                ScriptJsonDto tmpDto = new ScriptJsonDto();
                tmpDto.securityCode = GlobalVariable.securityCode;
                tmpDto.title = this.scriptTitleTextBox.Text;
                tmpDto.pwd = this.scriptPwdTextBox.Text;
                tmpDto.description = this.scriptDescTextBox.Text;
                tmpDto.type = 1;
                for(int i = 0 ; i < this.scriptsDataGridView.Rows.Count ; i++)
                {
                    ScriptDto tmp = new ScriptDto();
                    tmp.CmdId = Convert.ToInt32(this.scriptsDataGridView.Rows[i].Cells[0].Value);
                    tmp.Body = Convert.ToString(this.scriptsDataGridView.Rows[i].Cells[1].Value);
                    tmp.times = Convert.ToInt32(this.scriptsDataGridView.Rows[i].Cells[2].Value);
                    tmpDto.scripts.Add(tmp);
                }
                string encryptScriptString = this.scriptDecryptEncryptUtil.ScriptEncrypt(tmpDto);
                File.WriteAllText(saveFileDialog.FileName, encryptScriptString);
                Logger.Log("CrateLog",$"生成自定义脚本{tmpDto.title },保存路径为:{saveFileDialog.FileName}");
            }
        }

        private void ScriptEditForm_closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void importScriptButton_Click(object sender, EventArgs e)
        {
            if(this.scriptDto != null)
            {
                DialogResult result = MessageBox.Show("当前正在编辑脚本，是否打开其他脚本?", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result != DialogResult.OK) return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "脚本文件(*.script)|*.script";
            openFileDialog.InitialDirectory = this.scriptPath;
            DialogResult openFileResult = openFileDialog.ShowDialog();
            if(openFileResult == DialogResult.OK)
            {
                string scriptString = File.ReadAllText(openFileDialog.FileName);
                ScriptJsonDto scriptJsonDto = this.scriptDecryptEncryptUtil.ScriptDecrypt(scriptString);
                if(scriptJsonDto.securityCode != GlobalVariable.securityCode || scriptJsonDto.type != 1)
                {
                    MessageBox.Show("脚本导入失败！\n脚本已损坏或导入了非脚本文件！");
                    return;
                }
                if(scriptJsonDto.pwd != "")
                {
                    string pwd = Interaction.InputBox("请输入密码！", "输入密码", "", -1, -1);
                    if (pwd == "") return;
                    if(pwd != scriptJsonDto.pwd)
                    {
                        MessageBox.Show("密码错误！");
                        return ;
                    }
                }
                this.EnbaleEditForm(scriptJsonDto);
                this.scriptTitleLabel.Text = openFileDialog.FileName;
            }
        }

        private void EnbaleEditForm(ScriptJsonDto script)
        {
            this.scriptDto = script;
            this.editGroupBox.Enabled = this.exportScriptButton.Enabled = this.scriptDescGroupBox.Enabled = true;
            this.scriptsDataGridView.Rows.Clear();
            foreach(ScriptDto scriptDto in this.scriptDto.scripts)
            {
                this.scriptsDataGridView.Rows.Add(
                    scriptDto.CmdId,
                    scriptDto.Body,
                    scriptDto.times);
            }
            this.scriptTitleTextBox.Text = this.scriptDto.title;
            this.scriptDescTextBox.Text = this.scriptDto.description;
        }

        private void CheckInputedKeyIsDigit(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete) e.Handled = true;
        }
        private void DeleteOnly(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete) e.Handled = true;
        }

        private void addArgButton_Click(object sender, EventArgs e)
        {
            if (this.argTextBox.Text == "") return;
            try
            {
                if (this.argTextBox.Text.IndexOf('.') > -1 || Convert.ToInt32(this.argTextBox.Text) < 0)
                {
                    MessageBox.Show("输入的参数不符合要求！");
                    return;
                }
            }
            catch
            { 
                MessageBox.Show("输入的参数不符合要求！");
                return;
            }
            this.argListTextBox.Text += $"{( this.argListTextBox.Text != "" ? "," : "")}{this.argTextBox.Text}";
            this.argTextBox.Text = "";
        }

        private void clearArgButton_Click(object sender, EventArgs e)
        {
            this.argListTextBox.Text = "";
        }

        private void generateDataButton_Click(object sender, EventArgs e)
        {
            if(this.cmdIdTextBox.Text == "")
            {
                MessageBox.Show("还没有输入CmdId哦!");
                return;
            }
            try
            {
                if (this.cmdIdTextBox.Text.IndexOf('.') > -1 || Convert.ToInt32(this.cmdIdTextBox.Text) <= 0)
                {
                    MessageBox.Show("输入的CmdId不符合要求！");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("输入的CmdId不符合要求！");
                return;
            }
            string hexString = "";
            if (this.argListTextBox.Text != "")
            {
                string[] argList = this.argListTextBox.Text.Split(',');
                for (int i = 0 ; i < argList.Length ; i++)
                {
                    if (argList[i] == "") continue;
                    hexString += $"{ByteConverter.DecimalToHex(Convert.ToInt32(argList[i]), 4)}";
                }
                char[] result = new char[hexString.Length + hexString.Length / 2];
                int index = 0;
                for (int i = 0; i < hexString.Length; i++)
                {
                    result[index++] = hexString[i];
                    if ((i + 1) % 2 == 0 && index < hexString.Length + hexString.Length / 2)
                    {
                        result[index++] = '-';
                    }
                }
                hexString  = new string(result);
            }
            this.numDataTextBox.Text = $"{this.cmdIdTextBox.Text}|{hexString}";
        }

        private void insertNumListDataButton_Click(object sender, EventArgs e)
        {
            if(this.numDataTextBox.Text == "") return;
            string[] tmpScript = this.numDataTextBox.Text.Split('|');
            this.scriptsDataGridView.Rows.Add(tmpScript[0],tmpScript[1],1);
        }

        private void timesEditbutton_Click(object sender, EventArgs e)
        {
            int index = this.GetIndex();
            if(index == -1) return;
            try
            {
                if(Convert.ToInt32(this.timesTextBox.Text) > 50 || Convert.ToInt32(this.timesTextBox.Text) <= 0)
                {
                    MessageBox.Show("输入的执行次数不符合要求！");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("输入的执行次数不符合要求！");
                return;
            }
            this.scriptsDataGridView.Rows[index].Cells[2].Value = this.timesTextBox.Text;
        }
        private int GetIndex()
        {
            int selectIndex = -1;
            if (this.scriptsDataGridView.SelectedRows.Count > 0)
            {
                selectIndex = this.scriptsDataGridView.SelectedRows[0].Index;
            }
            else if (this.scriptsDataGridView.SelectedCells.Count > 0)
            {
                selectIndex = this.scriptsDataGridView.SelectedCells[0].RowIndex;
            }
            return selectIndex;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int index = this.GetIndex();
            if (index == -1) return;
            this.scriptsDataGridView.Rows.RemoveAt(index);
        }

        private void defaultImportNutton_Click(object sender, EventArgs e)
        {
            string[] dataList = this.defaultImportTextBox.Text.Replace("\r","").Split('\n');

            string pattern = "^[0-9A-Fa-f-]+$";
            Regex regex = new Regex(pattern);
            foreach (string data in dataList)
            {
                string[] tmp = data.Split('|');
                if (tmp.Length >= 3 && tmp[0].ToLower() == "send")
                {
                    if(regex.IsMatch(tmp[2]) || tmp[2].Length == 0) this.scriptsDataGridView.Rows.Add(tmp[1], tmp[2], 1);
                }
            }
        }

        private void createNewScriptButton_Click(object sender, EventArgs e)
        {
            if (this.scriptDto != null)
            {
                DialogResult result = MessageBox.Show("当前正在编辑脚本，是否新建脚本?", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result != DialogResult.OK) return;
            }
            this.EnbaleEditForm(new ScriptJsonDto());
            this.scriptTitleLabel.Text = "新建脚本";
            this.scriptDescTextBox.Text = "脚本说明：\r\n\r\n作者：";
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            int index = this.GetIndex();
            if (index == -1) return;
            if (this.numDataTextBox.Text.Length <= 0) return;
            string[] tmpScript = this.numDataTextBox.Text.Split('|');
            this.scriptsDataGridView.Rows[index].Cells[0].Value = tmpScript[0];
            this.scriptsDataGridView.Rows[index].Cells[1].Value = tmpScript[1];
        }

        private void argTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.addArgButton_Click(sender, e);
            }
        }

        private void timesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                this.timesEditbutton_Click(sender, e);
            }
        }
    }
}
