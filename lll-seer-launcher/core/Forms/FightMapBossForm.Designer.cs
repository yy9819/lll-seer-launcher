namespace lll_seer_launcher.core.Forms
{
    partial class FightMapBossForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FightMapBossForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fightMapBossRadioButton = new System.Windows.Forms.RadioButton();
            this.mibaoFightRadioButton = new System.Windows.Forms.RadioButton();
            this.bossNumTextBox = new System.Windows.Forms.TextBox();
            this.fightBossButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::lll_seer_launcher.Properties.Resources.gangyasha;
            this.pictureBox1.Location = new System.Drawing.Point(3, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // fightMapBossRadioButton
            // 
            this.fightMapBossRadioButton.AutoSize = true;
            this.fightMapBossRadioButton.Checked = true;
            this.fightMapBossRadioButton.Location = new System.Drawing.Point(217, 14);
            this.fightMapBossRadioButton.Name = "fightMapBossRadioButton";
            this.fightMapBossRadioButton.Size = new System.Drawing.Size(83, 16);
            this.fightMapBossRadioButton.TabIndex = 1;
            this.fightMapBossRadioButton.TabStop = true;
            this.fightMapBossRadioButton.Text = "对战地图怪";
            this.fightMapBossRadioButton.UseVisualStyleBackColor = true;
            // 
            // mibaoFightRadioButton
            // 
            this.mibaoFightRadioButton.AutoSize = true;
            this.mibaoFightRadioButton.Location = new System.Drawing.Point(317, 14);
            this.mibaoFightRadioButton.Name = "mibaoFightRadioButton";
            this.mibaoFightRadioButton.Size = new System.Drawing.Size(83, 16);
            this.mibaoFightRadioButton.TabIndex = 2;
            this.mibaoFightRadioButton.Text = "对战任务怪";
            this.mibaoFightRadioButton.UseVisualStyleBackColor = true;
            // 
            // bossNumTextBox
            // 
            this.bossNumTextBox.Location = new System.Drawing.Point(217, 89);
            this.bossNumTextBox.Name = "bossNumTextBox";
            this.bossNumTextBox.Size = new System.Drawing.Size(193, 21);
            this.bossNumTextBox.TabIndex = 3;
            this.bossNumTextBox.Text = "0";
            this.bossNumTextBox.TextChanged += new System.EventHandler(this.bossNumTextBox_TextChanged);
            this.bossNumTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bossNumTextBox_KeyPress);
            // 
            // fightBossButton
            // 
            this.fightBossButton.Location = new System.Drawing.Point(330, 114);
            this.fightBossButton.Name = "fightBossButton";
            this.fightBossButton.Size = new System.Drawing.Size(80, 33);
            this.fightBossButton.TabIndex = 4;
            this.fightBossButton.Text = "进入战斗！";
            this.fightBossButton.UseVisualStyleBackColor = true;
            this.fightBossButton.Click += new System.EventHandler(this.fightBossButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "输入欲挑战的BossId";
            // 
            // FightMapBossForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 159);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fightBossButton);
            this.Controls.Add(this.bossNumTextBox);
            this.Controls.Add(this.mibaoFightRadioButton);
            this.Controls.Add(this.fightMapBossRadioButton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FightMapBossForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "钢牙鲨非常强大，你确定要挑战它吗？";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FightMapBossForm_closing);
            this.Load += new System.EventHandler(this.FightMapBossForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton fightMapBossRadioButton;
        private System.Windows.Forms.RadioButton mibaoFightRadioButton;
        private System.Windows.Forms.TextBox bossNumTextBox;
        private System.Windows.Forms.Button fightBossButton;
        private System.Windows.Forms.Label label1;
    }
}