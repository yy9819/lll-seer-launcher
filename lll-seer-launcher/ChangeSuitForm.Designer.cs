namespace lll_seer_launcher
{
    partial class ChangeSuitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeSuitForm));
            this.suitGroupBox = new System.Windows.Forms.GroupBox();
            this.suitAbTextGroupBox = new System.Windows.Forms.GroupBox();
            this.suitTextBox = new System.Windows.Forms.TextBox();
            this.suitListBox = new System.Windows.Forms.ListBox();
            this.glassesGroupBox = new System.Windows.Forms.GroupBox();
            this.glassesAbTextGroupBox = new System.Windows.Forms.GroupBox();
            this.glassesTextBox = new System.Windows.Forms.TextBox();
            this.glassesListBox = new System.Windows.Forms.ListBox();
            this.achieveTitleGroupBox = new System.Windows.Forms.GroupBox();
            this.titelAbTextGroupBox = new System.Windows.Forms.GroupBox();
            this.achieveTittleTextBox = new System.Windows.Forms.TextBox();
            this.achieveTitleListBox = new System.Windows.Forms.ListBox();
            this.totalGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.updatePlanButton = new System.Windows.Forms.Button();
            this.changePlanButton = new System.Windows.Forms.Button();
            this.deletePlanButton = new System.Windows.Forms.Button();
            this.addPlanButton = new System.Windows.Forms.Button();
            this.userListComboBox = new System.Windows.Forms.ComboBox();
            this.planDataGridView = new System.Windows.Forms.DataGridView();
            this.getUserSuitGlassesTittleInfoButton = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.glasses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.achieveTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suitGroupBox.SuspendLayout();
            this.suitAbTextGroupBox.SuspendLayout();
            this.glassesGroupBox.SuspendLayout();
            this.glassesAbTextGroupBox.SuspendLayout();
            this.achieveTitleGroupBox.SuspendLayout();
            this.titelAbTextGroupBox.SuspendLayout();
            this.totalGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.planDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // suitGroupBox
            // 
            this.suitGroupBox.Controls.Add(this.suitAbTextGroupBox);
            this.suitGroupBox.Controls.Add(this.suitListBox);
            resources.ApplyResources(this.suitGroupBox, "suitGroupBox");
            this.suitGroupBox.Name = "suitGroupBox";
            this.suitGroupBox.TabStop = false;
            // 
            // suitAbTextGroupBox
            // 
            this.suitAbTextGroupBox.Controls.Add(this.suitTextBox);
            resources.ApplyResources(this.suitAbTextGroupBox, "suitAbTextGroupBox");
            this.suitAbTextGroupBox.Name = "suitAbTextGroupBox";
            this.suitAbTextGroupBox.TabStop = false;
            // 
            // suitTextBox
            // 
            this.suitTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.suitTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.suitTextBox, "suitTextBox");
            this.suitTextBox.Name = "suitTextBox";
            this.suitTextBox.ReadOnly = true;
            // 
            // suitListBox
            // 
            this.suitListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.suitListBox, "suitListBox");
            this.suitListBox.ForeColor = System.Drawing.Color.Black;
            this.suitListBox.FormattingEnabled = true;
            this.suitListBox.Name = "suitListBox";
            this.suitListBox.SelectedIndexChanged += new System.EventHandler(this.suitListBox_SelectedIndexChanged);
            // 
            // glassesGroupBox
            // 
            this.glassesGroupBox.Controls.Add(this.glassesAbTextGroupBox);
            this.glassesGroupBox.Controls.Add(this.glassesListBox);
            resources.ApplyResources(this.glassesGroupBox, "glassesGroupBox");
            this.glassesGroupBox.Name = "glassesGroupBox";
            this.glassesGroupBox.TabStop = false;
            // 
            // glassesAbTextGroupBox
            // 
            this.glassesAbTextGroupBox.Controls.Add(this.glassesTextBox);
            resources.ApplyResources(this.glassesAbTextGroupBox, "glassesAbTextGroupBox");
            this.glassesAbTextGroupBox.Name = "glassesAbTextGroupBox";
            this.glassesAbTextGroupBox.TabStop = false;
            // 
            // glassesTextBox
            // 
            this.glassesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.glassesTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.glassesTextBox, "glassesTextBox");
            this.glassesTextBox.Name = "glassesTextBox";
            this.glassesTextBox.ReadOnly = true;
            // 
            // glassesListBox
            // 
            this.glassesListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.glassesListBox, "glassesListBox");
            this.glassesListBox.ForeColor = System.Drawing.Color.Black;
            this.glassesListBox.FormattingEnabled = true;
            this.glassesListBox.Name = "glassesListBox";
            this.glassesListBox.SelectedIndexChanged += new System.EventHandler(this.glassesListBox_SelectedIndexChanged);
            // 
            // achieveTitleGroupBox
            // 
            this.achieveTitleGroupBox.Controls.Add(this.titelAbTextGroupBox);
            this.achieveTitleGroupBox.Controls.Add(this.achieveTitleListBox);
            resources.ApplyResources(this.achieveTitleGroupBox, "achieveTitleGroupBox");
            this.achieveTitleGroupBox.Name = "achieveTitleGroupBox";
            this.achieveTitleGroupBox.TabStop = false;
            // 
            // titelAbTextGroupBox
            // 
            this.titelAbTextGroupBox.Controls.Add(this.achieveTittleTextBox);
            resources.ApplyResources(this.titelAbTextGroupBox, "titelAbTextGroupBox");
            this.titelAbTextGroupBox.Name = "titelAbTextGroupBox";
            this.titelAbTextGroupBox.TabStop = false;
            // 
            // achieveTittleTextBox
            // 
            this.achieveTittleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.achieveTittleTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.achieveTittleTextBox, "achieveTittleTextBox");
            this.achieveTittleTextBox.Name = "achieveTittleTextBox";
            this.achieveTittleTextBox.ReadOnly = true;
            // 
            // achieveTitleListBox
            // 
            this.achieveTitleListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.achieveTitleListBox, "achieveTitleListBox");
            this.achieveTitleListBox.ForeColor = System.Drawing.Color.Black;
            this.achieveTitleListBox.FormattingEnabled = true;
            this.achieveTitleListBox.Name = "achieveTitleListBox";
            this.achieveTitleListBox.SelectedIndexChanged += new System.EventHandler(this.achieveTitleListBox_SelectedIndexChanged);
            // 
            // totalGroupBox
            // 
            this.totalGroupBox.Controls.Add(this.button1);
            this.totalGroupBox.Controls.Add(this.updatePlanButton);
            this.totalGroupBox.Controls.Add(this.changePlanButton);
            this.totalGroupBox.Controls.Add(this.deletePlanButton);
            this.totalGroupBox.Controls.Add(this.addPlanButton);
            this.totalGroupBox.Controls.Add(this.userListComboBox);
            this.totalGroupBox.Controls.Add(this.planDataGridView);
            this.totalGroupBox.Controls.Add(this.getUserSuitGlassesTittleInfoButton);
            resources.ApplyResources(this.totalGroupBox, "totalGroupBox");
            this.totalGroupBox.Name = "totalGroupBox";
            this.totalGroupBox.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // updatePlanButton
            // 
            resources.ApplyResources(this.updatePlanButton, "updatePlanButton");
            this.updatePlanButton.Name = "updatePlanButton";
            this.updatePlanButton.UseVisualStyleBackColor = true;
            this.updatePlanButton.Click += new System.EventHandler(this.updatePlanButton_Click);
            // 
            // changePlanButton
            // 
            resources.ApplyResources(this.changePlanButton, "changePlanButton");
            this.changePlanButton.Name = "changePlanButton";
            this.changePlanButton.UseVisualStyleBackColor = true;
            // 
            // deletePlanButton
            // 
            resources.ApplyResources(this.deletePlanButton, "deletePlanButton");
            this.deletePlanButton.Name = "deletePlanButton";
            this.deletePlanButton.UseVisualStyleBackColor = true;
            this.deletePlanButton.Click += new System.EventHandler(this.deletePlanButton_Click);
            // 
            // addPlanButton
            // 
            resources.ApplyResources(this.addPlanButton, "addPlanButton");
            this.addPlanButton.Name = "addPlanButton";
            this.addPlanButton.UseVisualStyleBackColor = true;
            this.addPlanButton.Click += new System.EventHandler(this.addPlanButton_Click);
            // 
            // userListComboBox
            // 
            this.userListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userListComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.userListComboBox, "userListComboBox");
            this.userListComboBox.Name = "userListComboBox";
            this.userListComboBox.SelectedIndexChanged += new System.EventHandler(this.userListComboBox_SelectedIndexChanged);
            // 
            // planDataGridView
            // 
            this.planDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.planDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.suit,
            this.glasses,
            this.achieveTitle,
            this.id});
            resources.ApplyResources(this.planDataGridView, "planDataGridView");
            this.planDataGridView.MultiSelect = false;
            this.planDataGridView.Name = "planDataGridView";
            this.planDataGridView.RowTemplate.Height = 23;
            this.planDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.planDataGridView_CellClick);
            // 
            // getUserSuitGlassesTittleInfoButton
            // 
            resources.ApplyResources(this.getUserSuitGlassesTittleInfoButton, "getUserSuitGlassesTittleInfoButton");
            this.getUserSuitGlassesTittleInfoButton.Name = "getUserSuitGlassesTittleInfoButton";
            this.getUserSuitGlassesTittleInfoButton.UseVisualStyleBackColor = true;
            this.getUserSuitGlassesTittleInfoButton.Click += new System.EventHandler(this.getUserSuitGlassTittleInfoButton_Click);
            // 
            // name
            // 
            resources.ApplyResources(this.name, "name");
            this.name.Name = "name";
            // 
            // suit
            // 
            resources.ApplyResources(this.suit, "suit");
            this.suit.Name = "suit";
            this.suit.ReadOnly = true;
            // 
            // glasses
            // 
            resources.ApplyResources(this.glasses, "glasses");
            this.glasses.Name = "glasses";
            this.glasses.ReadOnly = true;
            // 
            // achieveTitle
            // 
            resources.ApplyResources(this.achieveTitle, "achieveTitle");
            this.achieveTitle.Name = "achieveTitle";
            this.achieveTitle.ReadOnly = true;
            // 
            // id
            // 
            resources.ApplyResources(this.id, "id");
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // ChangeSuitForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.totalGroupBox);
            this.Controls.Add(this.achieveTitleGroupBox);
            this.Controls.Add(this.glassesGroupBox);
            this.Controls.Add(this.suitGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChangeSuitForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangeSuitForm_closing);
            this.Load += new System.EventHandler(this.ChangeSuitForm_Load);
            this.suitGroupBox.ResumeLayout(false);
            this.suitAbTextGroupBox.ResumeLayout(false);
            this.suitAbTextGroupBox.PerformLayout();
            this.glassesGroupBox.ResumeLayout(false);
            this.glassesAbTextGroupBox.ResumeLayout(false);
            this.glassesAbTextGroupBox.PerformLayout();
            this.achieveTitleGroupBox.ResumeLayout(false);
            this.titelAbTextGroupBox.ResumeLayout(false);
            this.titelAbTextGroupBox.PerformLayout();
            this.totalGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.planDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox suitGroupBox;
        private System.Windows.Forms.GroupBox glassesGroupBox;
        private System.Windows.Forms.GroupBox achieveTitleGroupBox;
        private System.Windows.Forms.GroupBox totalGroupBox;
        private System.Windows.Forms.ListBox suitListBox;
        private System.Windows.Forms.ListBox glassesListBox;
        private System.Windows.Forms.ListBox achieveTitleListBox;
        private System.Windows.Forms.GroupBox titelAbTextGroupBox;
        private System.Windows.Forms.GroupBox suitAbTextGroupBox;
        private System.Windows.Forms.GroupBox glassesAbTextGroupBox;
        private System.Windows.Forms.TextBox suitTextBox;
        private System.Windows.Forms.TextBox glassesTextBox;
        private System.Windows.Forms.TextBox achieveTittleTextBox;
        private System.Windows.Forms.Button getUserSuitGlassesTittleInfoButton;
        private System.Windows.Forms.ComboBox userListComboBox;
        private System.Windows.Forms.Button updatePlanButton;
        private System.Windows.Forms.Button changePlanButton;
        private System.Windows.Forms.Button deletePlanButton;
        private System.Windows.Forms.Button addPlanButton;
        private System.Windows.Forms.DataGridView planDataGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn suit;
        private System.Windows.Forms.DataGridViewTextBoxColumn glasses;
        private System.Windows.Forms.DataGridViewTextBoxColumn achieveTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}