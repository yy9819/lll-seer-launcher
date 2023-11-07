namespace lll_seer_launcher.core.Forms
{
    partial class PetStorageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PetStorageForm));
            this.petListDataGridView = new System.Windows.Forms.DataGridView();
            this.petId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.petName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.petLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.petCatchTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.realCatchTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loadStoragePetButton = new System.Windows.Forms.Button();
            this.searchGroupBox = new System.Windows.Forms.GroupBox();
            this.searchIdRadioButton = new System.Windows.Forms.RadioButton();
            this.searchNameRadioButton = new System.Windows.Forms.RadioButton();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.petDataGridView = new System.Windows.Forms.DataGridView();
            this.targetPetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.targetPetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.petInfoPreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.skillLabel = new System.Windows.Forms.Label();
            this.effectIcon = new System.Windows.Forms.PictureBox();
            this.singleInfoLabel = new System.Windows.Forms.Label();
            this.abilityLabel = new System.Windows.Forms.Label();
            this.petHead = new System.Windows.Forms.PictureBox();
            this.effectTip = new System.Windows.Forms.ToolTip(this.components);
            this.takeInBagButton = new System.Windows.Forms.Button();
            this.openPetBagButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.petListDataGridView)).BeginInit();
            this.searchGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.petDataGridView)).BeginInit();
            this.petInfoPreviewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.effectIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.petHead)).BeginInit();
            this.SuspendLayout();
            // 
            // petListDataGridView
            // 
            this.petListDataGridView.AllowUserToAddRows = false;
            this.petListDataGridView.AllowUserToDeleteRows = false;
            this.petListDataGridView.AllowUserToResizeColumns = false;
            this.petListDataGridView.AllowUserToResizeRows = false;
            this.petListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.petListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.petId,
            this.petName,
            this.petLevel,
            this.petCatchTime,
            this.realCatchTime});
            this.petListDataGridView.Location = new System.Drawing.Point(12, 14);
            this.petListDataGridView.MultiSelect = false;
            this.petListDataGridView.Name = "petListDataGridView";
            this.petListDataGridView.ReadOnly = true;
            this.petListDataGridView.RowHeadersVisible = false;
            this.petListDataGridView.RowTemplate.Height = 23;
            this.petListDataGridView.Size = new System.Drawing.Size(413, 487);
            this.petListDataGridView.TabIndex = 0;
            this.petListDataGridView.Click += new System.EventHandler(this.petListDataGridView_Click);
            // 
            // petId
            // 
            this.petId.HeaderText = "精灵ID";
            this.petId.Name = "petId";
            this.petId.ReadOnly = true;
            this.petId.Width = 70;
            // 
            // petName
            // 
            this.petName.HeaderText = "精灵名";
            this.petName.Name = "petName";
            this.petName.ReadOnly = true;
            this.petName.Width = 125;
            // 
            // petLevel
            // 
            this.petLevel.HeaderText = "精灵等级";
            this.petLevel.Name = "petLevel";
            this.petLevel.ReadOnly = true;
            this.petLevel.Width = 80;
            // 
            // petCatchTime
            // 
            this.petCatchTime.HeaderText = "获得时间";
            this.petCatchTime.Name = "petCatchTime";
            this.petCatchTime.ReadOnly = true;
            this.petCatchTime.Width = 140;
            // 
            // realCatchTime
            // 
            this.realCatchTime.HeaderText = "获得时间";
            this.realCatchTime.Name = "realCatchTime";
            this.realCatchTime.ReadOnly = true;
            this.realCatchTime.Visible = false;
            // 
            // loadStoragePetButton
            // 
            this.loadStoragePetButton.Location = new System.Drawing.Point(355, 507);
            this.loadStoragePetButton.Name = "loadStoragePetButton";
            this.loadStoragePetButton.Size = new System.Drawing.Size(70, 42);
            this.loadStoragePetButton.TabIndex = 1;
            this.loadStoragePetButton.Text = "读取仓库精灵";
            this.loadStoragePetButton.UseVisualStyleBackColor = true;
            this.loadStoragePetButton.Click += new System.EventHandler(this.loadStoragePetButton_Click);
            // 
            // searchGroupBox
            // 
            this.searchGroupBox.Controls.Add(this.searchIdRadioButton);
            this.searchGroupBox.Controls.Add(this.searchNameRadioButton);
            this.searchGroupBox.Controls.Add(this.searchButton);
            this.searchGroupBox.Controls.Add(this.searchTextBox);
            this.searchGroupBox.Controls.Add(this.petDataGridView);
            this.searchGroupBox.Location = new System.Drawing.Point(432, 315);
            this.searchGroupBox.Name = "searchGroupBox";
            this.searchGroupBox.Size = new System.Drawing.Size(270, 236);
            this.searchGroupBox.TabIndex = 2;
            this.searchGroupBox.TabStop = false;
            this.searchGroupBox.Text = "搜索(点击表格查看所持有的对应精灵)";
            // 
            // searchIdRadioButton
            // 
            this.searchIdRadioButton.AutoSize = true;
            this.searchIdRadioButton.Location = new System.Drawing.Point(83, 20);
            this.searchIdRadioButton.Name = "searchIdRadioButton";
            this.searchIdRadioButton.Size = new System.Drawing.Size(59, 16);
            this.searchIdRadioButton.TabIndex = 2;
            this.searchIdRadioButton.Text = "搜索Id";
            this.searchIdRadioButton.UseVisualStyleBackColor = true;
            this.searchIdRadioButton.Click += new System.EventHandler(this.ClearTextBoxText);
            // 
            // searchNameRadioButton
            // 
            this.searchNameRadioButton.AutoSize = true;
            this.searchNameRadioButton.Checked = true;
            this.searchNameRadioButton.Location = new System.Drawing.Point(6, 20);
            this.searchNameRadioButton.Name = "searchNameRadioButton";
            this.searchNameRadioButton.Size = new System.Drawing.Size(71, 16);
            this.searchNameRadioButton.TabIndex = 3;
            this.searchNameRadioButton.TabStop = true;
            this.searchNameRadioButton.Text = "搜索名字";
            this.searchNameRadioButton.UseVisualStyleBackColor = true;
            this.searchNameRadioButton.Click += new System.EventHandler(this.ClearTextBoxText);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(186, 40);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 9;
            this.searchButton.Text = "搜索";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(6, 40);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(174, 21);
            this.searchTextBox.TabIndex = 8;
            this.searchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchTextBox_KeyPress);
            // 
            // petDataGridView
            // 
            this.petDataGridView.AllowUserToAddRows = false;
            this.petDataGridView.AllowUserToDeleteRows = false;
            this.petDataGridView.AllowUserToResizeColumns = false;
            this.petDataGridView.AllowUserToResizeRows = false;
            this.petDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.petDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.targetPetId,
            this.targetPetName});
            this.petDataGridView.Location = new System.Drawing.Point(6, 67);
            this.petDataGridView.MultiSelect = false;
            this.petDataGridView.Name = "petDataGridView";
            this.petDataGridView.ReadOnly = true;
            this.petDataGridView.RowHeadersVisible = false;
            this.petDataGridView.RowTemplate.Height = 23;
            this.petDataGridView.Size = new System.Drawing.Size(255, 163);
            this.petDataGridView.TabIndex = 1;
            this.petDataGridView.Click += new System.EventHandler(this.petDataGridView_Click);
            // 
            // targetPetId
            // 
            this.targetPetId.HeaderText = "精灵ID";
            this.targetPetId.Name = "targetPetId";
            this.targetPetId.ReadOnly = true;
            this.targetPetId.Width = 80;
            // 
            // targetPetName
            // 
            this.targetPetName.HeaderText = "精灵名";
            this.targetPetName.Name = "targetPetName";
            this.targetPetName.ReadOnly = true;
            this.targetPetName.Width = 160;
            // 
            // petInfoPreviewGroupBox
            // 
            this.petInfoPreviewGroupBox.Controls.Add(this.skillLabel);
            this.petInfoPreviewGroupBox.Controls.Add(this.effectIcon);
            this.petInfoPreviewGroupBox.Controls.Add(this.singleInfoLabel);
            this.petInfoPreviewGroupBox.Controls.Add(this.abilityLabel);
            this.petInfoPreviewGroupBox.Controls.Add(this.petHead);
            this.petInfoPreviewGroupBox.Location = new System.Drawing.Point(431, 12);
            this.petInfoPreviewGroupBox.Name = "petInfoPreviewGroupBox";
            this.petInfoPreviewGroupBox.Size = new System.Drawing.Size(271, 297);
            this.petInfoPreviewGroupBox.TabIndex = 3;
            this.petInfoPreviewGroupBox.TabStop = false;
            this.petInfoPreviewGroupBox.Text = "精灵信息(能力值为无任何加成的初始能力值)";
            // 
            // skillLabel
            // 
            this.skillLabel.Location = new System.Drawing.Point(132, 110);
            this.skillLabel.Name = "skillLabel";
            this.skillLabel.Size = new System.Drawing.Size(127, 148);
            this.skillLabel.TabIndex = 4;
            // 
            // effectIcon
            // 
            this.effectIcon.Location = new System.Drawing.Point(161, 57);
            this.effectIcon.Name = "effectIcon";
            this.effectIcon.Size = new System.Drawing.Size(26, 26);
            this.effectIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.effectIcon.TabIndex = 3;
            this.effectIcon.TabStop = false;
            // 
            // singleInfoLabel
            // 
            this.singleInfoLabel.Location = new System.Drawing.Point(12, 17);
            this.singleInfoLabel.Name = "singleInfoLabel";
            this.singleInfoLabel.Size = new System.Drawing.Size(175, 69);
            this.singleInfoLabel.TabIndex = 2;
            // 
            // abilityLabel
            // 
            this.abilityLabel.Location = new System.Drawing.Point(12, 86);
            this.abilityLabel.Name = "abilityLabel";
            this.abilityLabel.Size = new System.Drawing.Size(247, 208);
            this.abilityLabel.TabIndex = 1;
            // 
            // petHead
            // 
            this.petHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.petHead.Location = new System.Drawing.Point(193, 17);
            this.petHead.Name = "petHead";
            this.petHead.Size = new System.Drawing.Size(66, 66);
            this.petHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.petHead.TabIndex = 0;
            this.petHead.TabStop = false;
            // 
            // effectTip
            // 
            this.effectTip.AutoPopDelay = 60000;
            this.effectTip.InitialDelay = 500;
            this.effectTip.ReshowDelay = 100;
            // 
            // takeInBagButton
            // 
            this.takeInBagButton.Location = new System.Drawing.Point(12, 507);
            this.takeInBagButton.Name = "takeInBagButton";
            this.takeInBagButton.Size = new System.Drawing.Size(70, 42);
            this.takeInBagButton.TabIndex = 4;
            this.takeInBagButton.Text = "加入背包";
            this.takeInBagButton.UseVisualStyleBackColor = true;
            this.takeInBagButton.Click += new System.EventHandler(this.takeInBagButton_Click);
            // 
            // openPetBagButton
            // 
            this.openPetBagButton.Location = new System.Drawing.Point(188, 507);
            this.openPetBagButton.Name = "openPetBagButton";
            this.openPetBagButton.Size = new System.Drawing.Size(70, 42);
            this.openPetBagButton.TabIndex = 5;
            this.openPetBagButton.Text = "精灵背包";
            this.openPetBagButton.UseVisualStyleBackColor = true;
            this.openPetBagButton.Click += new System.EventHandler(this.openPetBagButton_Click);
            // 
            // PetStorageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 555);
            this.Controls.Add(this.openPetBagButton);
            this.Controls.Add(this.takeInBagButton);
            this.Controls.Add(this.petInfoPreviewGroupBox);
            this.Controls.Add(this.searchGroupBox);
            this.Controls.Add(this.loadStoragePetButton);
            this.Controls.Add(this.petListDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PetStorageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "精灵仓库(战斗中时无法使用)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PetStorageForm_Closing);
            this.Load += new System.EventHandler(this.PetStorageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.petListDataGridView)).EndInit();
            this.searchGroupBox.ResumeLayout(false);
            this.searchGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.petDataGridView)).EndInit();
            this.petInfoPreviewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.effectIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.petHead)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView petListDataGridView;
        private System.Windows.Forms.Button loadStoragePetButton;
        private System.Windows.Forms.GroupBox searchGroupBox;
        private System.Windows.Forms.GroupBox petInfoPreviewGroupBox;
        private System.Windows.Forms.PictureBox petHead;
        private System.Windows.Forms.DataGridView petDataGridView;
        private System.Windows.Forms.RadioButton searchIdRadioButton;
        private System.Windows.Forms.RadioButton searchNameRadioButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetPetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetPetName;
        private System.Windows.Forms.Label singleInfoLabel;
        private System.Windows.Forms.Label abilityLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn petId;
        private System.Windows.Forms.DataGridViewTextBoxColumn petName;
        private System.Windows.Forms.DataGridViewTextBoxColumn petLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn petCatchTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn realCatchTime;
        private System.Windows.Forms.PictureBox effectIcon;
        private System.Windows.Forms.ToolTip effectTip;
        private System.Windows.Forms.Label skillLabel;
        private System.Windows.Forms.Button takeInBagButton;
        private System.Windows.Forms.Button openPetBagButton;
    }
}