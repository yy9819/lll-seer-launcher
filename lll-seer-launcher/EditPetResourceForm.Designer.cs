namespace lll_seer_launcher
{
    partial class EditPetResourceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPetResourceForm));
            this.petSwfPreviewWebBrowser = new System.Windows.Forms.WebBrowser();
            this.planGroupBox = new System.Windows.Forms.GroupBox();
            this.planDataGridView = new System.Windows.Forms.DataGridView();
            this.petId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.petName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.petSkinsId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.petSkinsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.searchIdRadioButton = new System.Windows.Forms.RadioButton();
            this.searchNameRadioButton = new System.Windows.Forms.RadioButton();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.previewDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchPetRadioButton = new System.Windows.Forms.RadioButton();
            this.searchSkinsRadioButton = new System.Windows.Forms.RadioButton();
            this.addPlanButton = new System.Windows.Forms.Button();
            this.updatePlanButton = new System.Windows.Forms.Button();
            this.deletePlanButton = new System.Windows.Forms.Button();
            this.setPetButton = new System.Windows.Forms.Button();
            this.setSkinsButton = new System.Windows.Forms.Button();
            this.searchPetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchPetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planDataGridView)).BeginInit();
            this.searchGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // petSwfPreviewWebBrowser
            // 
            this.petSwfPreviewWebBrowser.Location = new System.Drawing.Point(6, 275);
            this.petSwfPreviewWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.petSwfPreviewWebBrowser.Name = "petSwfPreviewWebBrowser";
            this.petSwfPreviewWebBrowser.ScrollBarsEnabled = false;
            this.petSwfPreviewWebBrowser.Size = new System.Drawing.Size(288, 180);
            this.petSwfPreviewWebBrowser.TabIndex = 0;
            this.petSwfPreviewWebBrowser.Url = new System.Uri("https://seer.61.com/resource/fightResource/pet/swf/5000.swf", System.UriKind.Absolute);
            // 
            // planGroupBox
            // 
            this.planGroupBox.Controls.Add(this.planDataGridView);
            this.planGroupBox.Location = new System.Drawing.Point(9, 8);
            this.planGroupBox.Name = "planGroupBox";
            this.planGroupBox.Size = new System.Drawing.Size(375, 458);
            this.planGroupBox.TabIndex = 2;
            this.planGroupBox.TabStop = false;
            this.planGroupBox.Text = "替换方案表";
            // 
            // planDataGridView
            // 
            this.planDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.planDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.petId,
            this.petName,
            this.petSkinsId,
            this.petSkinsName});
            this.planDataGridView.Location = new System.Drawing.Point(6, 20);
            this.planDataGridView.MultiSelect = false;
            this.planDataGridView.Name = "planDataGridView";
            this.planDataGridView.RowTemplate.Height = 23;
            this.planDataGridView.Size = new System.Drawing.Size(362, 432);
            this.planDataGridView.TabIndex = 0;
            this.planDataGridView.Click += new System.EventHandler(this.planDataGridView_Click);
            // 
            // petId
            // 
            this.petId.Frozen = true;
            this.petId.HeaderText = "精灵Id";
            this.petId.Name = "petId";
            this.petId.ReadOnly = true;
            this.petId.Width = 40;
            // 
            // petName
            // 
            this.petName.Frozen = true;
            this.petName.HeaderText = "精灵名";
            this.petName.Name = "petName";
            this.petName.ReadOnly = true;
            this.petName.Width = 120;
            // 
            // petSkinsId
            // 
            this.petSkinsId.Frozen = true;
            this.petSkinsId.HeaderText = "替换Id";
            this.petSkinsId.Name = "petSkinsId";
            this.petSkinsId.ReadOnly = true;
            this.petSkinsId.Width = 40;
            // 
            // petSkinsName
            // 
            this.petSkinsName.Frozen = true;
            this.petSkinsName.HeaderText = "替换精灵/皮肤名";
            this.petSkinsName.Name = "petSkinsName";
            this.petSkinsName.ReadOnly = true;
            this.petSkinsName.Width = 120;
            // 
            // searchGroupBox
            // 
            this.searchGroupBox.Controls.Add(this.groupBox2);
            this.searchGroupBox.Controls.Add(this.searchButton);
            this.searchGroupBox.Controls.Add(this.searchTextBox);
            this.searchGroupBox.Controls.Add(this.previewDataGridView);
            this.searchGroupBox.Controls.Add(this.petSwfPreviewWebBrowser);
            this.searchGroupBox.Controls.Add(this.groupBox1);
            this.searchGroupBox.Location = new System.Drawing.Point(460, 9);
            this.searchGroupBox.Name = "searchGroupBox";
            this.searchGroupBox.Size = new System.Drawing.Size(301, 457);
            this.searchGroupBox.TabIndex = 3;
            this.searchGroupBox.TabStop = false;
            this.searchGroupBox.Text = "精灵・皮肤预览(右击可放大)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.searchIdRadioButton);
            this.groupBox2.Controls.Add(this.searchNameRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(95, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(87, 55);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // searchIdRadioButton
            // 
            this.searchIdRadioButton.AutoSize = true;
            this.searchIdRadioButton.Location = new System.Drawing.Point(6, 13);
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
            this.searchNameRadioButton.Location = new System.Drawing.Point(6, 32);
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
            this.searchButton.Location = new System.Drawing.Point(219, 47);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 5;
            this.searchButton.Text = "搜索";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.InitPreviewDataGridView);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(188, 23);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(106, 21);
            this.searchTextBox.TabIndex = 4;
            this.searchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchTextBox_KeyPress);
            // 
            // previewDataGridView
            // 
            this.previewDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.previewDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.searchPetId,
            this.searchPetName});
            this.previewDataGridView.Location = new System.Drawing.Point(4, 72);
            this.previewDataGridView.MultiSelect = false;
            this.previewDataGridView.Name = "previewDataGridView";
            this.previewDataGridView.RowHeadersVisible = false;
            this.previewDataGridView.RowTemplate.Height = 23;
            this.previewDataGridView.Size = new System.Drawing.Size(290, 197);
            this.previewDataGridView.TabIndex = 1;
            this.previewDataGridView.Click += new System.EventHandler(this.ChangePreviewSwf);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchPetRadioButton);
            this.groupBox1.Controls.Add(this.searchSkinsRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(83, 55);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // searchPetRadioButton
            // 
            this.searchPetRadioButton.AutoSize = true;
            this.searchPetRadioButton.Checked = true;
            this.searchPetRadioButton.Location = new System.Drawing.Point(6, 13);
            this.searchPetRadioButton.Name = "searchPetRadioButton";
            this.searchPetRadioButton.Size = new System.Drawing.Size(71, 16);
            this.searchPetRadioButton.TabIndex = 2;
            this.searchPetRadioButton.TabStop = true;
            this.searchPetRadioButton.Text = "搜索精灵";
            this.searchPetRadioButton.UseVisualStyleBackColor = true;
            this.searchPetRadioButton.Click += new System.EventHandler(this.ClearTextBoxText);
            // 
            // searchSkinsRadioButton
            // 
            this.searchSkinsRadioButton.AutoSize = true;
            this.searchSkinsRadioButton.Location = new System.Drawing.Point(6, 32);
            this.searchSkinsRadioButton.Name = "searchSkinsRadioButton";
            this.searchSkinsRadioButton.Size = new System.Drawing.Size(71, 16);
            this.searchSkinsRadioButton.TabIndex = 3;
            this.searchSkinsRadioButton.Text = "搜索皮肤";
            this.searchSkinsRadioButton.UseVisualStyleBackColor = true;
            this.searchSkinsRadioButton.Click += new System.EventHandler(this.ClearTextBoxText);
            // 
            // addPlanButton
            // 
            this.addPlanButton.Location = new System.Drawing.Point(390, 262);
            this.addPlanButton.Name = "addPlanButton";
            this.addPlanButton.Size = new System.Drawing.Size(64, 42);
            this.addPlanButton.TabIndex = 4;
            this.addPlanButton.Text = "添加";
            this.addPlanButton.UseVisualStyleBackColor = true;
            this.addPlanButton.Click += new System.EventHandler(this.addPlanButton_Click);
            // 
            // updatePlanButton
            // 
            this.updatePlanButton.Location = new System.Drawing.Point(390, 310);
            this.updatePlanButton.Name = "updatePlanButton";
            this.updatePlanButton.Size = new System.Drawing.Size(64, 42);
            this.updatePlanButton.TabIndex = 5;
            this.updatePlanButton.Text = "保存";
            this.updatePlanButton.UseVisualStyleBackColor = true;
            this.updatePlanButton.Click += new System.EventHandler(this.updatePlanButton_Click);
            // 
            // deletePlanButton
            // 
            this.deletePlanButton.Location = new System.Drawing.Point(390, 358);
            this.deletePlanButton.Name = "deletePlanButton";
            this.deletePlanButton.Size = new System.Drawing.Size(64, 42);
            this.deletePlanButton.TabIndex = 6;
            this.deletePlanButton.Text = "删除";
            this.deletePlanButton.UseVisualStyleBackColor = true;
            this.deletePlanButton.Click += new System.EventHandler(this.deletePlanButton_Click);
            // 
            // setPetButton
            // 
            this.setPetButton.Location = new System.Drawing.Point(390, 105);
            this.setPetButton.Name = "setPetButton";
            this.setPetButton.Size = new System.Drawing.Size(64, 42);
            this.setPetButton.TabIndex = 7;
            this.setPetButton.Text = "设为被替换精灵";
            this.setPetButton.UseVisualStyleBackColor = true;
            this.setPetButton.Click += new System.EventHandler(this.setPetButton_Click);
            // 
            // setSkinsButton
            // 
            this.setSkinsButton.Location = new System.Drawing.Point(390, 153);
            this.setSkinsButton.Name = "setSkinsButton";
            this.setSkinsButton.Size = new System.Drawing.Size(64, 42);
            this.setSkinsButton.TabIndex = 8;
            this.setSkinsButton.Text = "替换为此皮肤";
            this.setSkinsButton.UseVisualStyleBackColor = true;
            this.setSkinsButton.Click += new System.EventHandler(this.setSkinsButton_Click);
            // 
            // searchPetId
            // 
            this.searchPetId.Frozen = true;
            this.searchPetId.HeaderText = "id";
            this.searchPetId.Name = "searchPetId";
            this.searchPetId.ReadOnly = true;
            // 
            // searchPetName
            // 
            this.searchPetName.Frozen = true;
            this.searchPetName.HeaderText = "精灵・皮肤名字";
            this.searchPetName.Name = "searchPetName";
            this.searchPetName.ReadOnly = true;
            this.searchPetName.Width = 200;
            // 
            // EditPetResourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 478);
            this.Controls.Add(this.setSkinsButton);
            this.Controls.Add(this.setPetButton);
            this.Controls.Add(this.deletePlanButton);
            this.Controls.Add(this.updatePlanButton);
            this.Controls.Add(this.addPlanButton);
            this.Controls.Add(this.searchGroupBox);
            this.Controls.Add(this.planGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EditPetResourceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "精灵皮肤替换";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditPetResourceForm_FormClosing);
            this.Load += new System.EventHandler(this.EditPetResourceForm_Load);
            this.planGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.planDataGridView)).EndInit();
            this.searchGroupBox.ResumeLayout(false);
            this.searchGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser petSwfPreviewWebBrowser;
        private System.Windows.Forms.GroupBox planGroupBox;
        private System.Windows.Forms.DataGridView planDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn petId;
        private System.Windows.Forms.DataGridViewTextBoxColumn petName;
        private System.Windows.Forms.DataGridViewTextBoxColumn petSkinsId;
        private System.Windows.Forms.DataGridViewTextBoxColumn petSkinsName;
        private System.Windows.Forms.GroupBox searchGroupBox;
        private System.Windows.Forms.Button addPlanButton;
        private System.Windows.Forms.Button updatePlanButton;
        private System.Windows.Forms.Button deletePlanButton;
        private System.Windows.Forms.Button setPetButton;
        private System.Windows.Forms.Button setSkinsButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.RadioButton searchSkinsRadioButton;
        private System.Windows.Forms.RadioButton searchPetRadioButton;
        private System.Windows.Forms.DataGridView previewDataGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton searchIdRadioButton;
        private System.Windows.Forms.RadioButton searchNameRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn searchPetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn searchPetName;
    }
}