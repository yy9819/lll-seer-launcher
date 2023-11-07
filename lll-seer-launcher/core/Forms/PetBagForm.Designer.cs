namespace lll_seer_launcher.core.Forms
{
    partial class PetBagForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PetBagForm));
            this.petInfoPreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.skillLabel = new System.Windows.Forms.Label();
            this.effectIcon = new System.Windows.Forms.PictureBox();
            this.singleInfoLabel = new System.Windows.Forms.Label();
            this.abilityLabel = new System.Windows.Forms.Label();
            this.petHead = new System.Windows.Forms.PictureBox();
            this.reloadButton = new System.Windows.Forms.Button();
            this.toFristButton = new System.Windows.Forms.Button();
            this.takeInStorageButton = new System.Windows.Forms.Button();
            this.petBagPlanGroupBox = new System.Windows.Forms.GroupBox();
            this.openStorageButton = new System.Windows.Forms.Button();
            this.searchPlanButton = new System.Windows.Forms.Button();
            this.planNameTextBox = new System.Windows.Forms.TextBox();
            this.changePlanButton = new System.Windows.Forms.Button();
            this.deletePlanbutton = new System.Windows.Forms.Button();
            this.updatePlanNameButton = new System.Windows.Forms.Button();
            this.saveNewPlanButton = new System.Windows.Forms.Button();
            this.inputPlanButton = new System.Windows.Forms.Button();
            this.planDataGridView = new System.Windows.Forms.DataGridView();
            this.planName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fightPetNameList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fightPetCatchTiemList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.awaitPetNameList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.awaitPetCatchTimeList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.petBagGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.awaitPetToFristButton = new System.Windows.Forms.Button();
            this.takeAwaitPetInStorageButton = new System.Windows.Forms.Button();
            this.awaitPetListView = new System.Windows.Forms.ListView();
            this.fightPetsGroupBox = new System.Windows.Forms.GroupBox();
            this.fightPetListView = new System.Windows.Forms.ListView();
            this.effectTip = new System.Windows.Forms.ToolTip(this.components);
            this.curePetButton = new System.Windows.Forms.Button();
            this.lowerHPButton = new System.Windows.Forms.Button();
            this.petInfoPreviewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.effectIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.petHead)).BeginInit();
            this.petBagPlanGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planDataGridView)).BeginInit();
            this.petBagGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.fightPetsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // petInfoPreviewGroupBox
            // 
            this.petInfoPreviewGroupBox.Controls.Add(this.skillLabel);
            this.petInfoPreviewGroupBox.Controls.Add(this.effectIcon);
            this.petInfoPreviewGroupBox.Controls.Add(this.singleInfoLabel);
            this.petInfoPreviewGroupBox.Controls.Add(this.abilityLabel);
            this.petInfoPreviewGroupBox.Controls.Add(this.petHead);
            this.petInfoPreviewGroupBox.Location = new System.Drawing.Point(499, 12);
            this.petInfoPreviewGroupBox.Name = "petInfoPreviewGroupBox";
            this.petInfoPreviewGroupBox.Size = new System.Drawing.Size(274, 297);
            this.petInfoPreviewGroupBox.TabIndex = 4;
            this.petInfoPreviewGroupBox.TabStop = false;
            this.petInfoPreviewGroupBox.Text = "精灵信息(能力值为无任何加成的初始能力值)";
            // 
            // skillLabel
            // 
            this.skillLabel.Location = new System.Drawing.Point(141, 107);
            this.skillLabel.Name = "skillLabel";
            this.skillLabel.Size = new System.Drawing.Size(127, 148);
            this.skillLabel.TabIndex = 4;
            // 
            // effectIcon
            // 
            this.effectIcon.Location = new System.Drawing.Point(170, 57);
            this.effectIcon.Name = "effectIcon";
            this.effectIcon.Size = new System.Drawing.Size(26, 26);
            this.effectIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.effectIcon.TabIndex = 3;
            this.effectIcon.TabStop = false;
            // 
            // singleInfoLabel
            // 
            this.singleInfoLabel.Location = new System.Drawing.Point(6, 17);
            this.singleInfoLabel.Name = "singleInfoLabel";
            this.singleInfoLabel.Size = new System.Drawing.Size(175, 69);
            this.singleInfoLabel.TabIndex = 2;
            // 
            // abilityLabel
            // 
            this.abilityLabel.Location = new System.Drawing.Point(6, 86);
            this.abilityLabel.Name = "abilityLabel";
            this.abilityLabel.Size = new System.Drawing.Size(247, 208);
            this.abilityLabel.TabIndex = 1;
            // 
            // petHead
            // 
            this.petHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.petHead.Location = new System.Drawing.Point(202, 17);
            this.petHead.Name = "petHead";
            this.petHead.Size = new System.Drawing.Size(66, 66);
            this.petHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.petHead.TabIndex = 0;
            this.petHead.TabStop = false;
            // 
            // reloadButton
            // 
            this.reloadButton.Location = new System.Drawing.Point(410, 100);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(64, 25);
            this.reloadButton.TabIndex = 7;
            this.reloadButton.Text = "刷新背包";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // toFristButton
            // 
            this.toFristButton.Location = new System.Drawing.Point(6, 102);
            this.toFristButton.Name = "toFristButton";
            this.toFristButton.Size = new System.Drawing.Size(64, 25);
            this.toFristButton.TabIndex = 6;
            this.toFristButton.Text = "首发";
            this.toFristButton.UseVisualStyleBackColor = true;
            this.toFristButton.Click += new System.EventHandler(this.toFristButton_Click);
            // 
            // takeInStorageButton
            // 
            this.takeInStorageButton.Location = new System.Drawing.Point(76, 102);
            this.takeInStorageButton.Name = "takeInStorageButton";
            this.takeInStorageButton.Size = new System.Drawing.Size(64, 25);
            this.takeInStorageButton.TabIndex = 5;
            this.takeInStorageButton.Text = "入库";
            this.takeInStorageButton.UseVisualStyleBackColor = true;
            this.takeInStorageButton.Click += new System.EventHandler(this.takeInStorageButton_Click);
            // 
            // petBagPlanGroupBox
            // 
            this.petBagPlanGroupBox.Controls.Add(this.openStorageButton);
            this.petBagPlanGroupBox.Controls.Add(this.searchPlanButton);
            this.petBagPlanGroupBox.Controls.Add(this.planNameTextBox);
            this.petBagPlanGroupBox.Controls.Add(this.changePlanButton);
            this.petBagPlanGroupBox.Controls.Add(this.deletePlanbutton);
            this.petBagPlanGroupBox.Controls.Add(this.updatePlanNameButton);
            this.petBagPlanGroupBox.Controls.Add(this.saveNewPlanButton);
            this.petBagPlanGroupBox.Controls.Add(this.inputPlanButton);
            this.petBagPlanGroupBox.Controls.Add(this.planDataGridView);
            this.petBagPlanGroupBox.Location = new System.Drawing.Point(4, 318);
            this.petBagPlanGroupBox.Name = "petBagPlanGroupBox";
            this.petBagPlanGroupBox.Size = new System.Drawing.Size(769, 282);
            this.petBagPlanGroupBox.TabIndex = 5;
            this.petBagPlanGroupBox.TabStop = false;
            this.petBagPlanGroupBox.Text = "精灵背包方案";
            // 
            // openStorageButton
            // 
            this.openStorageButton.Location = new System.Drawing.Point(575, 110);
            this.openStorageButton.Name = "openStorageButton";
            this.openStorageButton.Size = new System.Drawing.Size(91, 36);
            this.openStorageButton.TabIndex = 9;
            this.openStorageButton.Text = "精灵仓库";
            this.openStorageButton.UseVisualStyleBackColor = true;
            this.openStorageButton.Click += new System.EventHandler(this.openStorageButton_Click);
            // 
            // searchPlanButton
            // 
            this.searchPlanButton.Location = new System.Drawing.Point(672, 50);
            this.searchPlanButton.Name = "searchPlanButton";
            this.searchPlanButton.Size = new System.Drawing.Size(91, 33);
            this.searchPlanButton.TabIndex = 16;
            this.searchPlanButton.Text = "搜索";
            this.searchPlanButton.UseVisualStyleBackColor = true;
            this.searchPlanButton.Click += new System.EventHandler(this.searchPlanButton_Click);
            // 
            // planNameTextBox
            // 
            this.planNameTextBox.Location = new System.Drawing.Point(575, 23);
            this.planNameTextBox.Name = "planNameTextBox";
            this.planNameTextBox.Size = new System.Drawing.Size(188, 21);
            this.planNameTextBox.TabIndex = 15;
            // 
            // changePlanButton
            // 
            this.changePlanButton.Enabled = false;
            this.changePlanButton.Location = new System.Drawing.Point(575, 152);
            this.changePlanButton.Name = "changePlanButton";
            this.changePlanButton.Size = new System.Drawing.Size(91, 36);
            this.changePlanButton.TabIndex = 14;
            this.changePlanButton.Text = "切换至此方案";
            this.changePlanButton.UseVisualStyleBackColor = true;
            this.changePlanButton.Click += new System.EventHandler(this.changePlanButton_Click);
            // 
            // deletePlanbutton
            // 
            this.deletePlanbutton.Enabled = false;
            this.deletePlanbutton.Location = new System.Drawing.Point(672, 236);
            this.deletePlanbutton.Name = "deletePlanbutton";
            this.deletePlanbutton.Size = new System.Drawing.Size(91, 36);
            this.deletePlanbutton.TabIndex = 13;
            this.deletePlanbutton.Text = "删除方案";
            this.deletePlanbutton.UseVisualStyleBackColor = true;
            this.deletePlanbutton.Click += new System.EventHandler(this.deletePlanbutton_Click);
            // 
            // updatePlanNameButton
            // 
            this.updatePlanNameButton.Enabled = false;
            this.updatePlanNameButton.Location = new System.Drawing.Point(575, 236);
            this.updatePlanNameButton.Name = "updatePlanNameButton";
            this.updatePlanNameButton.Size = new System.Drawing.Size(91, 36);
            this.updatePlanNameButton.TabIndex = 12;
            this.updatePlanNameButton.Text = "保存方案名";
            this.updatePlanNameButton.UseVisualStyleBackColor = true;
            this.updatePlanNameButton.Click += new System.EventHandler(this.updatePlanNameButton_Click);
            // 
            // saveNewPlanButton
            // 
            this.saveNewPlanButton.Location = new System.Drawing.Point(672, 194);
            this.saveNewPlanButton.Name = "saveNewPlanButton";
            this.saveNewPlanButton.Size = new System.Drawing.Size(91, 36);
            this.saveNewPlanButton.TabIndex = 11;
            this.saveNewPlanButton.Text = "新建方案";
            this.saveNewPlanButton.UseVisualStyleBackColor = true;
            this.saveNewPlanButton.Click += new System.EventHandler(this.saveNewPlanButton_Click);
            // 
            // inputPlanButton
            // 
            this.inputPlanButton.Location = new System.Drawing.Point(575, 194);
            this.inputPlanButton.Name = "inputPlanButton";
            this.inputPlanButton.Size = new System.Drawing.Size(91, 36);
            this.inputPlanButton.TabIndex = 10;
            this.inputPlanButton.Text = "导入当前背包";
            this.inputPlanButton.UseVisualStyleBackColor = true;
            this.inputPlanButton.Click += new System.EventHandler(this.inputPlanButton_Click);
            // 
            // planDataGridView
            // 
            this.planDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.planDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.planName,
            this.planId,
            this.fightPetNameList,
            this.fightPetCatchTiemList,
            this.awaitPetNameList,
            this.awaitPetCatchTimeList,
            this.userId});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.planDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.planDataGridView.Location = new System.Drawing.Point(9, 23);
            this.planDataGridView.MultiSelect = false;
            this.planDataGridView.Name = "planDataGridView";
            this.planDataGridView.RowHeadersVisible = false;
            this.planDataGridView.RowTemplate.Height = 40;
            this.planDataGridView.Size = new System.Drawing.Size(560, 249);
            this.planDataGridView.TabIndex = 0;
            this.planDataGridView.Click += new System.EventHandler(this.planDataGridView_Click);
            // 
            // planName
            // 
            this.planName.HeaderText = "方案名";
            this.planName.Name = "planName";
            this.planName.Width = 80;
            // 
            // planId
            // 
            this.planId.HeaderText = "方案Id";
            this.planId.Name = "planId";
            this.planId.ReadOnly = true;
            this.planId.Visible = false;
            // 
            // fightPetNameList
            // 
            this.fightPetNameList.HeaderText = "出战精灵";
            this.fightPetNameList.Name = "fightPetNameList";
            this.fightPetNameList.ReadOnly = true;
            this.fightPetNameList.Width = 240;
            // 
            // fightPetCatchTiemList
            // 
            this.fightPetCatchTiemList.HeaderText = "出战精灵时间码";
            this.fightPetCatchTiemList.Name = "fightPetCatchTiemList";
            this.fightPetCatchTiemList.ReadOnly = true;
            this.fightPetCatchTiemList.Visible = false;
            // 
            // awaitPetNameList
            // 
            this.awaitPetNameList.HeaderText = "备战精灵";
            this.awaitPetNameList.Name = "awaitPetNameList";
            this.awaitPetNameList.ReadOnly = true;
            this.awaitPetNameList.Width = 240;
            // 
            // awaitPetCatchTimeList
            // 
            this.awaitPetCatchTimeList.HeaderText = "备战精灵时间码";
            this.awaitPetCatchTimeList.Name = "awaitPetCatchTimeList";
            this.awaitPetCatchTimeList.ReadOnly = true;
            this.awaitPetCatchTimeList.Visible = false;
            // 
            // userId
            // 
            this.userId.HeaderText = "账号";
            this.userId.Name = "userId";
            this.userId.ReadOnly = true;
            this.userId.Visible = false;
            // 
            // petBagGroupBox
            // 
            this.petBagGroupBox.Controls.Add(this.groupBox1);
            this.petBagGroupBox.Controls.Add(this.fightPetsGroupBox);
            this.petBagGroupBox.Location = new System.Drawing.Point(3, 12);
            this.petBagGroupBox.Name = "petBagGroupBox";
            this.petBagGroupBox.Size = new System.Drawing.Size(490, 296);
            this.petBagGroupBox.TabIndex = 6;
            this.petBagGroupBox.TabStop = false;
            this.petBagGroupBox.Text = "精灵背包";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.awaitPetToFristButton);
            this.groupBox1.Controls.Add(this.reloadButton);
            this.groupBox1.Controls.Add(this.takeAwaitPetInStorageButton);
            this.groupBox1.Controls.Add(this.awaitPetListView);
            this.groupBox1.Location = new System.Drawing.Point(4, 160);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 130);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "备战";
            // 
            // awaitPetToFristButton
            // 
            this.awaitPetToFristButton.Location = new System.Drawing.Point(6, 100);
            this.awaitPetToFristButton.Name = "awaitPetToFristButton";
            this.awaitPetToFristButton.Size = new System.Drawing.Size(64, 25);
            this.awaitPetToFristButton.TabIndex = 8;
            this.awaitPetToFristButton.Text = "首发";
            this.awaitPetToFristButton.UseVisualStyleBackColor = true;
            this.awaitPetToFristButton.Click += new System.EventHandler(this.awaitPetToFristButton_Click);
            // 
            // takeAwaitPetInStorageButton
            // 
            this.takeAwaitPetInStorageButton.Location = new System.Drawing.Point(76, 100);
            this.takeAwaitPetInStorageButton.Name = "takeAwaitPetInStorageButton";
            this.takeAwaitPetInStorageButton.Size = new System.Drawing.Size(64, 25);
            this.takeAwaitPetInStorageButton.TabIndex = 7;
            this.takeAwaitPetInStorageButton.Text = "入库";
            this.takeAwaitPetInStorageButton.UseVisualStyleBackColor = true;
            this.takeAwaitPetInStorageButton.Click += new System.EventHandler(this.takeAwaitPetInStorageButton_Click);
            // 
            // awaitPetListView
            // 
            this.awaitPetListView.HideSelection = false;
            this.awaitPetListView.Location = new System.Drawing.Point(5, 13);
            this.awaitPetListView.Name = "awaitPetListView";
            this.awaitPetListView.Size = new System.Drawing.Size(469, 86);
            this.awaitPetListView.TabIndex = 1;
            this.awaitPetListView.UseCompatibleStateImageBehavior = false;
            // 
            // fightPetsGroupBox
            // 
            this.fightPetsGroupBox.Controls.Add(this.curePetButton);
            this.fightPetsGroupBox.Controls.Add(this.lowerHPButton);
            this.fightPetsGroupBox.Controls.Add(this.fightPetListView);
            this.fightPetsGroupBox.Controls.Add(this.toFristButton);
            this.fightPetsGroupBox.Controls.Add(this.takeInStorageButton);
            this.fightPetsGroupBox.Location = new System.Drawing.Point(6, 20);
            this.fightPetsGroupBox.Name = "fightPetsGroupBox";
            this.fightPetsGroupBox.Size = new System.Drawing.Size(480, 130);
            this.fightPetsGroupBox.TabIndex = 8;
            this.fightPetsGroupBox.TabStop = false;
            this.fightPetsGroupBox.Text = "出战";
            // 
            // fightPetListView
            // 
            this.fightPetListView.HideSelection = false;
            this.fightPetListView.Location = new System.Drawing.Point(6, 15);
            this.fightPetListView.Name = "fightPetListView";
            this.fightPetListView.Size = new System.Drawing.Size(468, 87);
            this.fightPetListView.TabIndex = 0;
            this.fightPetListView.UseCompatibleStateImageBehavior = false;
            // 
            // curePetButton
            // 
            this.curePetButton.Location = new System.Drawing.Point(340, 102);
            this.curePetButton.Name = "curePetButton";
            this.curePetButton.Size = new System.Drawing.Size(64, 25);
            this.curePetButton.TabIndex = 9;
            this.curePetButton.Text = "回血";
            this.curePetButton.UseVisualStyleBackColor = true;
            this.curePetButton.Click += new System.EventHandler(this.curePetButton_Click);
            // 
            // lowerHPButton
            // 
            this.lowerHPButton.Location = new System.Drawing.Point(410, 102);
            this.lowerHPButton.Name = "lowerHPButton";
            this.lowerHPButton.Size = new System.Drawing.Size(64, 25);
            this.lowerHPButton.TabIndex = 10;
            this.lowerHPButton.Text = "压血";
            this.lowerHPButton.UseVisualStyleBackColor = true;
            this.lowerHPButton.Click += new System.EventHandler(this.lowerHPButton_Click);
            // 
            // PetBagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 604);
            this.Controls.Add(this.petBagGroupBox);
            this.Controls.Add(this.petBagPlanGroupBox);
            this.Controls.Add(this.petInfoPreviewGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PetBagForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "精灵背包(战斗中时部分功能无法使用)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PetBagForm_Closing);
            this.Load += new System.EventHandler(this.PetBagForm_Load);
            this.petInfoPreviewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.effectIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.petHead)).EndInit();
            this.petBagPlanGroupBox.ResumeLayout(false);
            this.petBagPlanGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planDataGridView)).EndInit();
            this.petBagGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.fightPetsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox petInfoPreviewGroupBox;
        private System.Windows.Forms.Label skillLabel;
        private System.Windows.Forms.PictureBox effectIcon;
        private System.Windows.Forms.Label singleInfoLabel;
        private System.Windows.Forms.Label abilityLabel;
        private System.Windows.Forms.PictureBox petHead;
        private System.Windows.Forms.GroupBox petBagPlanGroupBox;
        private System.Windows.Forms.GroupBox petBagGroupBox;
        private System.Windows.Forms.DataGridView planDataGridView;
        private System.Windows.Forms.Button toFristButton;
        private System.Windows.Forms.Button takeInStorageButton;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.ListView awaitPetListView;
        private System.Windows.Forms.ListView fightPetListView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox fightPetsGroupBox;
        private System.Windows.Forms.Button awaitPetToFristButton;
        private System.Windows.Forms.Button takeAwaitPetInStorageButton;
        private System.Windows.Forms.ToolTip effectTip;
        private System.Windows.Forms.Button changePlanButton;
        private System.Windows.Forms.Button deletePlanbutton;
        private System.Windows.Forms.Button updatePlanNameButton;
        private System.Windows.Forms.Button saveNewPlanButton;
        private System.Windows.Forms.Button inputPlanButton;
        private System.Windows.Forms.Button searchPlanButton;
        private System.Windows.Forms.TextBox planNameTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn planName;
        private System.Windows.Forms.DataGridViewTextBoxColumn planId;
        private System.Windows.Forms.DataGridViewTextBoxColumn fightPetNameList;
        private System.Windows.Forms.DataGridViewTextBoxColumn fightPetCatchTiemList;
        private System.Windows.Forms.DataGridViewTextBoxColumn awaitPetNameList;
        private System.Windows.Forms.DataGridViewTextBoxColumn awaitPetCatchTimeList;
        private System.Windows.Forms.DataGridViewTextBoxColumn userId;
        private System.Windows.Forms.Button openStorageButton;
        private System.Windows.Forms.Button curePetButton;
        private System.Windows.Forms.Button lowerHPButton;
    }
}