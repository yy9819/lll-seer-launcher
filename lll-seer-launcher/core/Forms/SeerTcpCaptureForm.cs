using System;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Controller;
using System.Collections.Generic;

namespace lll_seer_launcher.core.Forms
{
    public partial class SeerTcpCaptureForm : Form
    {
        private delegate void InsertDataCallback();
        private List<int> blackList = new List<int>()
        {
            //CmdId.FIRE_ACT_COPY,
            CmdId.PEOPLE_WALK,
            CmdId.LOAD_PERCENT,
            CmdId.DAILY_TIMERS,
            CmdId.ACTIVEACHIEVE,
            CmdId.ALARM_CHECK,
            CmdId.ALARM_INFORM,
            CmdId.JAMES_ARMOR_QUERY_ABIBLITY,
            CmdId.NONO_FOLLOW_OR_HOOM,
            CmdId.USER_FOREVER_VALUE,
            CmdId.SYSTEM_TIME_CHECK
        };
        public SeerTcpCaptureForm()
        {
            InitializeComponent();
        }

        private void SeerTcpCaptureForm_Load(object sender, EventArgs e)
        {
            foreach(int cmdId in blackList)
            {
                this.disableCmdIdTextBox.Text += $"{(this.disableCmdIdTextBox.Text == "" ? "" : ",")}{cmdId}";
            }
        }

        private void SeerTcpCaptureForm_Closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        public void InsertData(HeadInfo headInfo,bool isSend)
        {
            if (!this.Visible || this.IsDisposed) return;
            InsertDataCallback callback = delegate ()
            {
                if(this.startCaptureCheckBox.Checked && !blackList.Contains(headInfo.cmdId))
                {
                    CmdDic.cmdDic.TryGetValue(headInfo.cmdId,out string cmd);
                    this.tcpDataGridView.Rows.Add(isSend ? "Send" : "Recv",
                        headInfo.cmdId,
                        cmd,
                        BitConverter.ToString(headInfo.decryptData));
                }
            };
            this.Invoke(callback);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tcpDataGridView.Rows.Clear();
        }
        private int GetSelectIndex()
        {
            int selectIndex = 0;
            if (this.tcpDataGridView.SelectedRows.Count > 0)
            {
                selectIndex = this.tcpDataGridView.SelectedRows[0].Index;
            }
            else if (this.tcpDataGridView.SelectedCells.Count > 0)
            {
                selectIndex = this.tcpDataGridView.SelectedCells[0].RowIndex;
            }
            return selectIndex;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = GetSelectIndex();
            try
            {
                Clipboard.SetText($"{Convert.ToString(this.tcpDataGridView.Rows[index].Cells["type"].Value)}|" +
                    $"{Convert.ToString(this.tcpDataGridView.Rows[index].Cells["cmdId"].Value)}|" +
                    $"{Convert.ToString(this.tcpDataGridView.Rows[index].Cells["data"].Value)}");
            }
            catch { }
        }

        private void tcpDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            new Thread(() =>
            {
                if(this.tcpDataGridView.Rows.Count > 0)
                {
                    int index = GetSelectIndex();
                    try
                    {
                        if (!this.IsDisposed)
                        {
                            InsertDataCallback callback = delegate ()
                            {
                                this.dataTextBox.Text = $"{Convert.ToString(this.tcpDataGridView.Rows[index].Cells["type"].Value)}|" +
                                    $"{Convert.ToString(this.tcpDataGridView.Rows[index].Cells["cmdId"].Value)}|" +
                                    $"{Convert.ToString(this.tcpDataGridView.Rows[index].Cells["data"].Value)}";
                            };
                            this.Invoke(callback);
                        }
                    }
                    catch
                    {
                        this.dataTextBox.Text = "";
                    }
                }
            }).Start();
        }

        private void sendDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = this.GetSelectIndex();
            try
            {
                string type = Convert.ToString(this.tcpDataGridView.Rows[index].Cells["type"].Value);
                if(type == "Send")
                {
                    int cmdId = Convert.ToInt32(this.tcpDataGridView.Rows[index].Cells["cmdId"].Value);
                    string body = Convert.ToString(this.tcpDataGridView.Rows[index].Cells["data"].Value).Replace("-","");
                    GlobalVariable.sendDataController.SendDataByCmdIdAndHexString(cmdId, body);
            }
                else
                {
                    MessageBox.Show("亲爱的小赛尔~接受包不可以发送哟~");
                }
            }catch 
            {
                //MessageBox.Show("");
            }
        }

        

        private void disableCmdIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete && e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void setDisableCmdIdButton_Click(object sender, EventArgs e)
        {
            string[] cmdIdList = this.disableCmdIdTextBox.Text.Split(',');
            this.blackList.Clear();
            foreach (string cmdId in cmdIdList)
            {
                if(cmdId != "")
                {
                    if(!this.blackList.Contains(Convert.ToInt32(cmdId)))this.blackList.Add(Convert.ToInt32(cmdId));
                }
            }
        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = GetSelectIndex();
            try
            {
                int cmdId = Convert.ToInt32(this.tcpDataGridView.Rows[index].Cells["cmdId"].Value);
                if (!this.blackList.Contains(Convert.ToInt32(cmdId)))
                {
                    this.blackList.Add(Convert.ToInt32(cmdId));
                    this.disableCmdIdTextBox.Text += $",{cmdId}";
                }
            }
            catch { }
        }

        private void sendDataButton_Click(object sender, EventArgs e)
        {
            string[] dataList = this.sendDataTextBox.Text.Split('\n');
            foreach (string data in dataList)
            {
                string[] tmp = data.Split('|');
                if(tmp.Length >= 3 && tmp[0].ToLower() == "send")
                {
                    try
                    {
                        GlobalVariable.sendDataController.SendDataByCmdIdAndHexString(Convert.ToInt32(tmp[1]),
                            tmp[2].Replace("-",""));
                    }
                    catch { }
                }
            }
        }

        private void addDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = this.GetSelectIndex();
            try
            {
                string type = Convert.ToString(this.tcpDataGridView.Rows[index].Cells["type"].Value);
                if (type == "Send")
                {
                    int cmdId = Convert.ToInt32(this.tcpDataGridView.Rows[index].Cells["cmdId"].Value);
                    string body = Convert.ToString(this.tcpDataGridView.Rows[index].Cells["data"].Value);
                    this.sendDataTextBox.AppendText($"{type}|{cmdId}|{body}\r\n");
                    MessageBox.Show("添加成功!");
                }
                else
                {
                    MessageBox.Show("亲爱的小赛尔~接受包不可以发送哟~");
                }
            }
            catch
            {
                //MessageBox.Show("");
            }
        }
    }
}
