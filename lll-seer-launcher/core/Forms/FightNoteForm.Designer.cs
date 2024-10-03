using System.Windows.Forms;
using System.Drawing;

namespace lll_seer_launcher.core.Forms
{
    partial class FightNoteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FightNoteForm));
            this.fightPreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.otherPlayerPetStatusLabel = new System.Windows.Forms.Label();
            this.loginPlayerPetStatusLabel = new System.Windows.Forms.Label();
            this.otherPlayerPetAbilityLabel = new System.Windows.Forms.Label();
            this.loginPlayerPetAbilityLabel = new System.Windows.Forms.Label();
            this.otherPlayerHPLabel = new System.Windows.Forms.Label();
            this.loginPlayerHPLabel = new System.Windows.Forms.Label();
            this.otherPlayerPetHP = new System.Windows.Forms.ProgressBar();
            this.loginPlayerPetHP = new System.Windows.Forms.ProgressBar();
            this.otherPlayerPetInfo = new System.Windows.Forms.Label();
            this.loginPlayerPetInfo = new System.Windows.Forms.Label();
            this.otherPlayerPetHead = new System.Windows.Forms.PictureBox();
            this.loginPlayerPetHead = new System.Windows.Forms.PictureBox();
            this.petBag = new System.Windows.Forms.GroupBox();
            this.notUseSkillButton = new System.Windows.Forms.Button();
            this.hideFightModuleCheckBox = new System.Windows.Forms.CheckBox();
            this.autoAddPPCheckBox = new System.Windows.Forms.CheckBox();
            this.loopUseSkillCheckBox = new System.Windows.Forms.CheckBox();
            this.curePetButton = new System.Windows.Forms.Button();
            this.lowerHPButton = new System.Windows.Forms.Button();
            this.escFightButton = new System.Windows.Forms.Button();
            this.useStatusItem = new System.Windows.Forms.Button();
            this.statusItemComboBox = new System.Windows.Forms.ComboBox();
            this.skillListBox = new System.Windows.Forms.ListBox();
            this.useCatchItem = new System.Windows.Forms.Button();
            this.usePPItem = new System.Windows.Forms.Button();
            this.useHPItem = new System.Windows.Forms.Button();
            this.catchItemComboBox = new System.Windows.Forms.ComboBox();
            this.PPComboBox = new System.Windows.Forms.ComboBox();
            this.HPComboBox = new System.Windows.Forms.ComboBox();
            this.petList = new System.Windows.Forms.ListBox();
            this.fightNoteTextBox = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.fightPreviewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.otherPlayerPetHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginPlayerPetHead)).BeginInit();
            this.petBag.SuspendLayout();
            this.SuspendLayout();
            // 
            // fightPreviewGroupBox
            // 
            this.fightPreviewGroupBox.Controls.Add(this.otherPlayerPetStatusLabel);
            this.fightPreviewGroupBox.Controls.Add(this.loginPlayerPetStatusLabel);
            this.fightPreviewGroupBox.Controls.Add(this.otherPlayerPetAbilityLabel);
            this.fightPreviewGroupBox.Controls.Add(this.loginPlayerPetAbilityLabel);
            this.fightPreviewGroupBox.Controls.Add(this.otherPlayerHPLabel);
            this.fightPreviewGroupBox.Controls.Add(this.loginPlayerHPLabel);
            this.fightPreviewGroupBox.Controls.Add(this.otherPlayerPetHP);
            this.fightPreviewGroupBox.Controls.Add(this.loginPlayerPetHP);
            this.fightPreviewGroupBox.Controls.Add(this.otherPlayerPetInfo);
            this.fightPreviewGroupBox.Controls.Add(this.loginPlayerPetInfo);
            this.fightPreviewGroupBox.Controls.Add(this.otherPlayerPetHead);
            this.fightPreviewGroupBox.Controls.Add(this.loginPlayerPetHead);
            this.fightPreviewGroupBox.Location = new System.Drawing.Point(4, 6);
            this.fightPreviewGroupBox.Name = "fightPreviewGroupBox";
            this.fightPreviewGroupBox.Size = new System.Drawing.Size(309, 175);
            this.fightPreviewGroupBox.TabIndex = 0;
            this.fightPreviewGroupBox.TabStop = false;
            // 
            // otherPlayerPetStatusLabel
            // 
            this.otherPlayerPetStatusLabel.Location = new System.Drawing.Point(160, 124);
            this.otherPlayerPetStatusLabel.MinimumSize = new System.Drawing.Size(142, 45);
            this.otherPlayerPetStatusLabel.Name = "otherPlayerPetStatusLabel";
            this.otherPlayerPetStatusLabel.Size = new System.Drawing.Size(142, 45);
            this.otherPlayerPetStatusLabel.TabIndex = 15;
            this.otherPlayerPetStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // loginPlayerPetStatusLabel
            // 
            this.loginPlayerPetStatusLabel.Location = new System.Drawing.Point(9, 124);
            this.loginPlayerPetStatusLabel.MinimumSize = new System.Drawing.Size(142, 45);
            this.loginPlayerPetStatusLabel.Name = "loginPlayerPetStatusLabel";
            this.loginPlayerPetStatusLabel.Size = new System.Drawing.Size(142, 45);
            this.loginPlayerPetStatusLabel.TabIndex = 14;
            // 
            // otherPlayerPetAbilityLabel
            // 
            this.otherPlayerPetAbilityLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.otherPlayerPetAbilityLabel.Location = new System.Drawing.Point(159, 94);
            this.otherPlayerPetAbilityLabel.MinimumSize = new System.Drawing.Size(125, 30);
            this.otherPlayerPetAbilityLabel.Name = "otherPlayerPetAbilityLabel";
            this.otherPlayerPetAbilityLabel.Size = new System.Drawing.Size(142, 30);
            this.otherPlayerPetAbilityLabel.TabIndex = 13;
            this.otherPlayerPetAbilityLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // loginPlayerPetAbilityLabel
            // 
            this.loginPlayerPetAbilityLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginPlayerPetAbilityLabel.Location = new System.Drawing.Point(9, 94);
            this.loginPlayerPetAbilityLabel.MinimumSize = new System.Drawing.Size(125, 30);
            this.loginPlayerPetAbilityLabel.Name = "loginPlayerPetAbilityLabel";
            this.loginPlayerPetAbilityLabel.Size = new System.Drawing.Size(142, 30);
            this.loginPlayerPetAbilityLabel.TabIndex = 12;
            // 
            // otherPlayerHPLabel
            // 
            this.otherPlayerHPLabel.Location = new System.Drawing.Point(163, 55);
            this.otherPlayerHPLabel.Name = "otherPlayerHPLabel";
            this.otherPlayerHPLabel.Size = new System.Drawing.Size(81, 18);
            this.otherPlayerHPLabel.TabIndex = 11;
            this.otherPlayerHPLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // loginPlayerHPLabel
            // 
            this.loginPlayerHPLabel.Location = new System.Drawing.Point(70, 55);
            this.loginPlayerHPLabel.Name = "loginPlayerHPLabel";
            this.loginPlayerHPLabel.Size = new System.Drawing.Size(78, 18);
            this.loginPlayerHPLabel.TabIndex = 10;
            this.loginPlayerHPLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // otherPlayerPetHP
            // 
            this.otherPlayerPetHP.ForeColor = System.Drawing.Color.Red;
            this.otherPlayerPetHP.Location = new System.Drawing.Point(161, 76);
            this.otherPlayerPetHP.Name = "otherPlayerPetHP";
            this.otherPlayerPetHP.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.otherPlayerPetHP.RightToLeftLayout = true;
            this.otherPlayerPetHP.Size = new System.Drawing.Size(140, 15);
            this.otherPlayerPetHP.TabIndex = 8;
            // 
            // loginPlayerPetHP
            // 
            this.loginPlayerPetHP.ForeColor = System.Drawing.Color.Red;
            this.loginPlayerPetHP.Location = new System.Drawing.Point(10, 76);
            this.loginPlayerPetHP.Name = "loginPlayerPetHP";
            this.loginPlayerPetHP.Size = new System.Drawing.Size(140, 15);
            this.loginPlayerPetHP.TabIndex = 6;
            // 
            // otherPlayerPetInfo
            // 
            this.otherPlayerPetInfo.Location = new System.Drawing.Point(163, 17);
            this.otherPlayerPetInfo.Name = "otherPlayerPetInfo";
            this.otherPlayerPetInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.otherPlayerPetInfo.Size = new System.Drawing.Size(79, 38);
            this.otherPlayerPetInfo.TabIndex = 3;
            this.otherPlayerPetInfo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // loginPlayerPetInfo
            // 
            this.loginPlayerPetInfo.Location = new System.Drawing.Point(70, 17);
            this.loginPlayerPetInfo.Name = "loginPlayerPetInfo";
            this.loginPlayerPetInfo.Size = new System.Drawing.Size(80, 38);
            this.loginPlayerPetInfo.TabIndex = 2;
            this.loginPlayerPetInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // otherPlayerPetHead
            // 
            this.otherPlayerPetHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.otherPlayerPetHead.Location = new System.Drawing.Point(245, 17);
            this.otherPlayerPetHead.Name = "otherPlayerPetHead";
            this.otherPlayerPetHead.Size = new System.Drawing.Size(56, 56);
            this.otherPlayerPetHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.otherPlayerPetHead.TabIndex = 1;
            this.otherPlayerPetHead.TabStop = false;
            // 
            // loginPlayerPetHead
            // 
            this.loginPlayerPetHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loginPlayerPetHead.Location = new System.Drawing.Point(10, 17);
            this.loginPlayerPetHead.Name = "loginPlayerPetHead";
            this.loginPlayerPetHead.Size = new System.Drawing.Size(56, 56);
            this.loginPlayerPetHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loginPlayerPetHead.TabIndex = 0;
            this.loginPlayerPetHead.TabStop = false;
            // 
            // petBag
            // 
            this.petBag.Controls.Add(this.notUseSkillButton);
            this.petBag.Controls.Add(this.hideFightModuleCheckBox);
            this.petBag.Controls.Add(this.autoAddPPCheckBox);
            this.petBag.Controls.Add(this.loopUseSkillCheckBox);
            this.petBag.Controls.Add(this.curePetButton);
            this.petBag.Controls.Add(this.lowerHPButton);
            this.petBag.Controls.Add(this.escFightButton);
            this.petBag.Controls.Add(this.useStatusItem);
            this.petBag.Controls.Add(this.statusItemComboBox);
            this.petBag.Controls.Add(this.skillListBox);
            this.petBag.Controls.Add(this.useCatchItem);
            this.petBag.Controls.Add(this.usePPItem);
            this.petBag.Controls.Add(this.useHPItem);
            this.petBag.Controls.Add(this.catchItemComboBox);
            this.petBag.Controls.Add(this.PPComboBox);
            this.petBag.Controls.Add(this.HPComboBox);
            this.petBag.Controls.Add(this.petList);
            this.petBag.Location = new System.Drawing.Point(4, 406);
            this.petBag.Name = "petBag";
            this.petBag.Size = new System.Drawing.Size(307, 211);
            this.petBag.TabIndex = 1;
            this.petBag.TabStop = false;
            this.petBag.Text = "快捷操作(双击可快速切换精灵，使用技能)";
            // 
            // notUseSkillButton
            // 
            this.notUseSkillButton.Location = new System.Drawing.Point(81, 142);
            this.notUseSkillButton.Name = "notUseSkillButton";
            this.notUseSkillButton.Size = new System.Drawing.Size(70, 30);
            this.notUseSkillButton.TabIndex = 16;
            this.notUseSkillButton.Text = "空过";
            this.notUseSkillButton.UseVisualStyleBackColor = true;
            this.notUseSkillButton.Click += new System.EventHandler(this.notUseSkillButton_Click);
            // 
            // hideFightModuleCheckBox
            // 
            this.hideFightModuleCheckBox.AutoSize = true;
            this.hideFightModuleCheckBox.Location = new System.Drawing.Point(96, 124);
            this.hideFightModuleCheckBox.Name = "hideFightModuleCheckBox";
            this.hideFightModuleCheckBox.Size = new System.Drawing.Size(96, 16);
            this.hideFightModuleCheckBox.TabIndex = 15;
            this.hideFightModuleCheckBox.Text = "屏蔽对战界面";
            this.hideFightModuleCheckBox.UseVisualStyleBackColor = true;
            this.hideFightModuleCheckBox.CheckedChanged += new System.EventHandler(this.hideFightModuleCheckBox_CheckedChanged);
            // 
            // autoAddPPCheckBox
            // 
            this.autoAddPPCheckBox.AutoSize = true;
            this.autoAddPPCheckBox.Location = new System.Drawing.Point(197, 190);
            this.autoAddPPCheckBox.Name = "autoAddPPCheckBox";
            this.autoAddPPCheckBox.Size = new System.Drawing.Size(72, 16);
            this.autoAddPPCheckBox.TabIndex = 14;
            this.autoAddPPCheckBox.Text = "自动补PP";
            this.autoAddPPCheckBox.UseVisualStyleBackColor = true;
            this.autoAddPPCheckBox.CheckedChanged += new System.EventHandler(this.autoAddPPCheckBox_CheckedChanged);
            // 
            // loopUseSkillCheckBox
            // 
            this.loopUseSkillCheckBox.AutoSize = true;
            this.loopUseSkillCheckBox.Location = new System.Drawing.Point(197, 168);
            this.loopUseSkillCheckBox.Name = "loopUseSkillCheckBox";
            this.loopUseSkillCheckBox.Size = new System.Drawing.Size(108, 16);
            this.loopUseSkillCheckBox.TabIndex = 13;
            this.loopUseSkillCheckBox.Text = "自动使用该技能";
            this.loopUseSkillCheckBox.UseVisualStyleBackColor = true;
            this.loopUseSkillCheckBox.CheckedChanged += new System.EventHandler(this.loopUseSkillCheckBox_CheckedChanged);
            // 
            // curePetButton
            // 
            this.curePetButton.Location = new System.Drawing.Point(80, 178);
            this.curePetButton.Name = "curePetButton";
            this.curePetButton.Size = new System.Drawing.Size(70, 30);
            this.curePetButton.TabIndex = 12;
            this.curePetButton.Text = "回血";
            this.curePetButton.UseVisualStyleBackColor = true;
            this.curePetButton.Click += new System.EventHandler(this.curePetButton_Click);
            // 
            // lowerHPButton
            // 
            this.lowerHPButton.Location = new System.Drawing.Point(8, 178);
            this.lowerHPButton.Name = "lowerHPButton";
            this.lowerHPButton.Size = new System.Drawing.Size(70, 30);
            this.lowerHPButton.TabIndex = 11;
            this.lowerHPButton.Text = "压血";
            this.lowerHPButton.UseVisualStyleBackColor = true;
            this.lowerHPButton.Click += new System.EventHandler(this.lowerHPButton_Click);
            // 
            // escFightButton
            // 
            this.escFightButton.Location = new System.Drawing.Point(8, 142);
            this.escFightButton.Name = "escFightButton";
            this.escFightButton.Size = new System.Drawing.Size(70, 30);
            this.escFightButton.TabIndex = 10;
            this.escFightButton.Text = "逃跑";
            this.escFightButton.UseVisualStyleBackColor = true;
            this.escFightButton.Click += new System.EventHandler(this.escFightButton_Click);
            // 
            // useStatusItem
            // 
            this.useStatusItem.Location = new System.Drawing.Point(150, 97);
            this.useStatusItem.Name = "useStatusItem";
            this.useStatusItem.Size = new System.Drawing.Size(42, 20);
            this.useStatusItem.TabIndex = 9;
            this.useStatusItem.Text = "使用";
            this.useStatusItem.UseVisualStyleBackColor = true;
            this.useStatusItem.Click += new System.EventHandler(this.useStatusItem_Click);
            // 
            // statusItemComboBox
            // 
            this.statusItemComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusItemComboBox.FormattingEnabled = true;
            this.statusItemComboBox.Items.AddRange(new object[] {
            "状态解除药剂-害怕",
            "状态解除药剂-疲惫",
            "状态解除药剂-睡眠",
            "状态解除药剂-麻痹",
            "状态解除药剂-烧伤",
            "状态解除药剂-冻伤",
            "状态解除药剂-中毒",
            "状态解除药剂-冰封",
            "状态解除药剂-焚烬",
            "状态解除药剂-瘫痪",
            "状态解除药剂-感染",
            "状态解除药剂-石化",
            "状态解除药剂-诅咒",
            "完全净化药剂"});
            this.statusItemComboBox.Location = new System.Drawing.Point(8, 98);
            this.statusItemComboBox.Name = "statusItemComboBox";
            this.statusItemComboBox.Size = new System.Drawing.Size(140, 20);
            this.statusItemComboBox.TabIndex = 8;
            // 
            // skillListBox
            // 
            this.skillListBox.FormattingEnabled = true;
            this.skillListBox.ItemHeight = 12;
            this.skillListBox.Location = new System.Drawing.Point(197, 98);
            this.skillListBox.Name = "skillListBox";
            this.skillListBox.Size = new System.Drawing.Size(104, 64);
            this.skillListBox.TabIndex = 7;
            this.skillListBox.SelectedIndexChanged += new System.EventHandler(this.skillListBox_SelectedIndexChanged);
            this.skillListBox.DoubleClick += new System.EventHandler(this.skillListBox_DoubleClick);
            // 
            // useCatchItem
            // 
            this.useCatchItem.Location = new System.Drawing.Point(150, 72);
            this.useCatchItem.Name = "useCatchItem";
            this.useCatchItem.Size = new System.Drawing.Size(42, 20);
            this.useCatchItem.TabIndex = 6;
            this.useCatchItem.Text = "使用";
            this.useCatchItem.UseVisualStyleBackColor = true;
            this.useCatchItem.Click += new System.EventHandler(this.useCatchItem_Click);
            // 
            // usePPItem
            // 
            this.usePPItem.Location = new System.Drawing.Point(150, 46);
            this.usePPItem.Name = "usePPItem";
            this.usePPItem.Size = new System.Drawing.Size(42, 20);
            this.usePPItem.TabIndex = 5;
            this.usePPItem.Text = "使用";
            this.usePPItem.UseVisualStyleBackColor = true;
            this.usePPItem.Click += new System.EventHandler(this.usePPItem_Click);
            // 
            // useHPItem
            // 
            this.useHPItem.Location = new System.Drawing.Point(150, 20);
            this.useHPItem.Name = "useHPItem";
            this.useHPItem.Size = new System.Drawing.Size(42, 20);
            this.useHPItem.TabIndex = 4;
            this.useHPItem.Text = "使用";
            this.useHPItem.UseVisualStyleBackColor = true;
            this.useHPItem.Click += new System.EventHandler(this.useHPItem_Click);
            // 
            // catchItemComboBox
            // 
            this.catchItemComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.catchItemComboBox.FormattingEnabled = true;
            this.catchItemComboBox.Items.AddRange(new object[] {
            "无敌精灵胶囊Ω",
            "无敌精灵胶囊",
            "超能胶囊",
            "初级精灵捕捉胶囊(必中)",
            "普通精灵胶囊",
            "中级精灵胶囊",
            "高级精灵胶囊",
            "超级精灵胶囊",
            "特级精灵胶囊"});
            this.catchItemComboBox.Location = new System.Drawing.Point(8, 72);
            this.catchItemComboBox.Name = "catchItemComboBox";
            this.catchItemComboBox.Size = new System.Drawing.Size(140, 20);
            this.catchItemComboBox.TabIndex = 3;
            // 
            // PPComboBox
            // 
            this.PPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PPComboBox.FormattingEnabled = true;
            this.PPComboBox.Items.AddRange(new object[] {
            "初级活力药剂(5PP)",
            "中级活力药剂(10PP)",
            "高级活力药剂(20PP)",
            "巅峰活力药剂(10PP)",
            "极限活力药剂(20PP)"});
            this.PPComboBox.Location = new System.Drawing.Point(8, 46);
            this.PPComboBox.Name = "PPComboBox";
            this.PPComboBox.Size = new System.Drawing.Size(140, 20);
            this.PPComboBox.TabIndex = 2;
            // 
            // HPComboBox
            // 
            this.HPComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HPComboBox.FormattingEnabled = true;
            this.HPComboBox.Items.AddRange(new object[] {
            "上等体力药剂(200HP)",
            "永寒体力药剂(250HP)",
            "全能恢复药剂(150HP,3PP)",
            "高级体力药剂(100HP)",
            "中级体力药剂(50HP)",
            "初级体力药剂(20HP)"});
            this.HPComboBox.Location = new System.Drawing.Point(8, 20);
            this.HPComboBox.Name = "HPComboBox";
            this.HPComboBox.Size = new System.Drawing.Size(140, 20);
            this.HPComboBox.TabIndex = 1;
            // 
            // petList
            // 
            this.petList.FormattingEnabled = true;
            this.petList.ItemHeight = 12;
            this.petList.Location = new System.Drawing.Point(197, 20);
            this.petList.Name = "petList";
            this.petList.Size = new System.Drawing.Size(104, 76);
            this.petList.TabIndex = 0;
            this.petList.DoubleClick += new System.EventHandler(this.petList_DoubleClick);
            // 
            // fightNoteTextBox
            // 
            this.fightNoteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fightNoteTextBox.Location = new System.Drawing.Point(4, 182);
            this.fightNoteTextBox.Multiline = true;
            this.fightNoteTextBox.Name = "fightNoteTextBox";
            this.fightNoteTextBox.ReadOnly = true;
            this.fightNoteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.fightNoteTextBox.Size = new System.Drawing.Size(309, 218);
            this.fightNoteTextBox.TabIndex = 2;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(241, 375);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(54, 24);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "清空";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // FightNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 619);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.fightNoteTextBox);
            this.Controls.Add(this.petBag);
            this.Controls.Add(this.fightPreviewGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FightNoteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "对战记录助手";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FightNoteForm_Closing);
            this.Load += new System.EventHandler(this.FightNoteForm_Load);
            this.fightPreviewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.otherPlayerPetHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginPlayerPetHead)).EndInit();
            this.petBag.ResumeLayout(false);
            this.petBag.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       
        #endregion

        private System.Windows.Forms.GroupBox fightPreviewGroupBox;
        private System.Windows.Forms.PictureBox otherPlayerPetHead;
        private System.Windows.Forms.PictureBox loginPlayerPetHead;
        private System.Windows.Forms.Label otherPlayerPetInfo;
        private System.Windows.Forms.Label loginPlayerPetInfo;
        private System.Windows.Forms.ProgressBar otherPlayerPetHP;
        private System.Windows.Forms.ProgressBar loginPlayerPetHP;
        private Label otherPlayerHPLabel;
        private Label loginPlayerHPLabel;
        private GroupBox petBag;
        private TextBox fightNoteTextBox;
        private Label otherPlayerPetAbilityLabel;
        private Label loginPlayerPetAbilityLabel;
        private Label otherPlayerPetStatusLabel;
        private Label loginPlayerPetStatusLabel;
        private Button clearButton;
        private ComboBox catchItemComboBox;
        private ComboBox PPComboBox;
        private ComboBox HPComboBox;
        private ListBox petList;
        private Button useCatchItem;
        private Button usePPItem;
        private Button useHPItem;
        private ListBox skillListBox;
        private Button useStatusItem;
        private ComboBox statusItemComboBox;
        private Button lowerHPButton;
        private Button escFightButton;
        private Button curePetButton;
        private CheckBox autoAddPPCheckBox;
        private CheckBox loopUseSkillCheckBox;
        private CheckBox hideFightModuleCheckBox;
        private Button notUseSkillButton;
    }
}