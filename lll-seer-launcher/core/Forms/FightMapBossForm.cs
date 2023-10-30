using System;
using System.Windows.Forms;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;

namespace lll_seer_launcher.core.Forms
{
    public partial class FightMapBossForm : Form
    {
        public FightMapBossForm()
        {
            InitializeComponent();
        }

        private void FightMapBossForm_Load(object sender, EventArgs e)
        {

        }
        private void FightMapBossForm_closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void bossNumTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void bossNumTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.fightBossButton_Click(sender, e);
            }
            else if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                e.Handled = true;
            }
        }

        private void fightBossButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariable.isLogin)
            {
                int bossId = Convert.ToInt32(this.bossNumTextBox.Text);
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(this.fightMapBossRadioButton.Checked ? CmdId.CHALLENGE_BOSS : CmdId.MIBAO_FIGHT, 
                    this.fightMapBossRadioButton.Checked ? new int[2] { bossId , 0 } : new int[1] {bossId});
            }
            else
            {
                MessageBox.Show("亲爱的小赛尔~没登录游戏的话，不能挑战强大的钢牙鲨哟~");
            }
        }
    }
}
