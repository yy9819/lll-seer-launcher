namespace lll_seer_launcher.core.Forms
{
    partial class ScriptEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditForm));
            this.scriptsDataGridView = new System.Windows.Forms.DataGridView();
            this.cmdId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataBody = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.times = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scriptDescTextBox = new System.Windows.Forms.TextBox();
            this.scriptDescGroupBox = new System.Windows.Forms.GroupBox();
            this.scriptTitleTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.scriptPwdTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.importExportGroupBox = new System.Windows.Forms.GroupBox();
            this.scriptTitleLabel = new System.Windows.Forms.Label();
            this.createNewScriptButton = new System.Windows.Forms.Button();
            this.exportScriptButton = new System.Windows.Forms.Button();
            this.importScriptButton = new System.Windows.Forms.Button();
            this.editGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.timesEditbutton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.timesTextBox = new System.Windows.Forms.TextBox();
            this.modeTabControl = new System.Windows.Forms.TabControl();
            this.defaultTabPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.defaultImportNutton = new System.Windows.Forms.Button();
            this.defaultImportTextBox = new System.Windows.Forms.TextBox();
            this.intListTabPage = new System.Windows.Forms.TabPage();
            this.replaceButton = new System.Windows.Forms.Button();
            this.insertNumListDataButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.numDataTextBox = new System.Windows.Forms.TextBox();
            this.generateDataButton = new System.Windows.Forms.Button();
            this.clearArgButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.argListTextBox = new System.Windows.Forms.TextBox();
            this.addArgButton = new System.Windows.Forms.Button();
            this.argTextBox = new System.Windows.Forms.TextBox();
            this.cmdIdTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scriptsDataGridView)).BeginInit();
            this.scriptDescGroupBox.SuspendLayout();
            this.importExportGroupBox.SuspendLayout();
            this.editGroupBox.SuspendLayout();
            this.modeTabControl.SuspendLayout();
            this.defaultTabPage.SuspendLayout();
            this.intListTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // scriptsDataGridView
            // 
            this.scriptsDataGridView.AllowUserToAddRows = false;
            this.scriptsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scriptsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cmdId,
            this.dataBody,
            this.times});
            this.scriptsDataGridView.Location = new System.Drawing.Point(405, 254);
            this.scriptsDataGridView.MultiSelect = false;
            this.scriptsDataGridView.Name = "scriptsDataGridView";
            this.scriptsDataGridView.ReadOnly = true;
            this.scriptsDataGridView.RowHeadersVisible = false;
            this.scriptsDataGridView.RowTemplate.Height = 23;
            this.scriptsDataGridView.Size = new System.Drawing.Size(344, 246);
            this.scriptsDataGridView.TabIndex = 1;
            // 
            // cmdId
            // 
            this.cmdId.HeaderText = "命令";
            this.cmdId.Name = "cmdId";
            this.cmdId.ReadOnly = true;
            this.cmdId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cmdId.Width = 60;
            // 
            // dataBody
            // 
            this.dataBody.HeaderText = "包体";
            this.dataBody.Name = "dataBody";
            this.dataBody.ReadOnly = true;
            this.dataBody.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataBody.Width = 220;
            // 
            // times
            // 
            this.times.HeaderText = "次数";
            this.times.Name = "times";
            this.times.ReadOnly = true;
            this.times.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.times.Width = 60;
            // 
            // scriptDescTextBox
            // 
            this.scriptDescTextBox.Location = new System.Drawing.Point(6, 77);
            this.scriptDescTextBox.Multiline = true;
            this.scriptDescTextBox.Name = "scriptDescTextBox";
            this.scriptDescTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scriptDescTextBox.Size = new System.Drawing.Size(331, 131);
            this.scriptDescTextBox.TabIndex = 2;
            this.scriptDescTextBox.Text = "脚本说明：\r\n\r\n作者：";
            // 
            // scriptDescGroupBox
            // 
            this.scriptDescGroupBox.Controls.Add(this.scriptTitleTextBox);
            this.scriptDescGroupBox.Controls.Add(this.label3);
            this.scriptDescGroupBox.Controls.Add(this.scriptPwdTextBox);
            this.scriptDescGroupBox.Controls.Add(this.label2);
            this.scriptDescGroupBox.Controls.Add(this.label1);
            this.scriptDescGroupBox.Controls.Add(this.scriptDescTextBox);
            this.scriptDescGroupBox.Enabled = false;
            this.scriptDescGroupBox.Location = new System.Drawing.Point(406, 6);
            this.scriptDescGroupBox.Name = "scriptDescGroupBox";
            this.scriptDescGroupBox.Size = new System.Drawing.Size(343, 242);
            this.scriptDescGroupBox.TabIndex = 3;
            this.scriptDescGroupBox.TabStop = false;
            // 
            // scriptTitleTextBox
            // 
            this.scriptTitleTextBox.Location = new System.Drawing.Point(6, 36);
            this.scriptTitleTextBox.Name = "scriptTitleTextBox";
            this.scriptTitleTextBox.Size = new System.Drawing.Size(331, 21);
            this.scriptTitleTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "脚本标题：";
            // 
            // scriptPwdTextBox
            // 
            this.scriptPwdTextBox.Location = new System.Drawing.Point(101, 214);
            this.scriptPwdTextBox.Name = "scriptPwdTextBox";
            this.scriptPwdTextBox.Size = new System.Drawing.Size(236, 21);
            this.scriptPwdTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "脚本编辑密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "描述：";
            // 
            // importExportGroupBox
            // 
            this.importExportGroupBox.Controls.Add(this.scriptTitleLabel);
            this.importExportGroupBox.Controls.Add(this.createNewScriptButton);
            this.importExportGroupBox.Controls.Add(this.exportScriptButton);
            this.importExportGroupBox.Controls.Add(this.importScriptButton);
            this.importExportGroupBox.Location = new System.Drawing.Point(7, 7);
            this.importExportGroupBox.Name = "importExportGroupBox";
            this.importExportGroupBox.Size = new System.Drawing.Size(393, 93);
            this.importExportGroupBox.TabIndex = 4;
            this.importExportGroupBox.TabStop = false;
            // 
            // scriptTitleLabel
            // 
            this.scriptTitleLabel.Location = new System.Drawing.Point(6, 16);
            this.scriptTitleLabel.Name = "scriptTitleLabel";
            this.scriptTitleLabel.Size = new System.Drawing.Size(259, 69);
            this.scriptTitleLabel.TabIndex = 3;
            // 
            // createNewScriptButton
            // 
            this.createNewScriptButton.Location = new System.Drawing.Point(334, 14);
            this.createNewScriptButton.Name = "createNewScriptButton";
            this.createNewScriptButton.Size = new System.Drawing.Size(52, 35);
            this.createNewScriptButton.TabIndex = 2;
            this.createNewScriptButton.Text = "新建";
            this.createNewScriptButton.UseVisualStyleBackColor = true;
            this.createNewScriptButton.Click += new System.EventHandler(this.createNewScriptButton_Click);
            // 
            // exportScriptButton
            // 
            this.exportScriptButton.Enabled = false;
            this.exportScriptButton.Location = new System.Drawing.Point(276, 51);
            this.exportScriptButton.Name = "exportScriptButton";
            this.exportScriptButton.Size = new System.Drawing.Size(110, 35);
            this.exportScriptButton.TabIndex = 1;
            this.exportScriptButton.Text = "导出（保存）";
            this.exportScriptButton.UseVisualStyleBackColor = true;
            this.exportScriptButton.Click += new System.EventHandler(this.exportScriptButton_Click);
            // 
            // importScriptButton
            // 
            this.importScriptButton.Location = new System.Drawing.Point(276, 14);
            this.importScriptButton.Name = "importScriptButton";
            this.importScriptButton.Size = new System.Drawing.Size(52, 35);
            this.importScriptButton.TabIndex = 0;
            this.importScriptButton.Text = "导入";
            this.importScriptButton.UseVisualStyleBackColor = true;
            this.importScriptButton.Click += new System.EventHandler(this.importScriptButton_Click);
            // 
            // editGroupBox
            // 
            this.editGroupBox.Controls.Add(this.deleteButton);
            this.editGroupBox.Controls.Add(this.timesEditbutton);
            this.editGroupBox.Controls.Add(this.label9);
            this.editGroupBox.Controls.Add(this.timesTextBox);
            this.editGroupBox.Controls.Add(this.modeTabControl);
            this.editGroupBox.Enabled = false;
            this.editGroupBox.Location = new System.Drawing.Point(7, 106);
            this.editGroupBox.Name = "editGroupBox";
            this.editGroupBox.Size = new System.Drawing.Size(393, 394);
            this.editGroupBox.TabIndex = 5;
            this.editGroupBox.TabStop = false;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(304, 353);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(74, 35);
            this.deleteButton.TabIndex = 13;
            this.deleteButton.Text = "删除选中";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // timesEditbutton
            // 
            this.timesEditbutton.Location = new System.Drawing.Point(110, 354);
            this.timesEditbutton.Name = "timesEditbutton";
            this.timesEditbutton.Size = new System.Drawing.Size(88, 35);
            this.timesEditbutton.TabIndex = 12;
            this.timesEditbutton.Text = "修改为此次数";
            this.timesEditbutton.UseVisualStyleBackColor = true;
            this.timesEditbutton.Click += new System.EventHandler(this.timesEditbutton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 351);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "发包次数:";
            // 
            // timesTextBox
            // 
            this.timesTextBox.Location = new System.Drawing.Point(16, 367);
            this.timesTextBox.Name = "timesTextBox";
            this.timesTextBox.Size = new System.Drawing.Size(88, 21);
            this.timesTextBox.TabIndex = 10;
            this.timesTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.timesTextBox_KeyDown);
            this.timesTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckInputedKeyIsDigit);
            // 
            // modeTabControl
            // 
            this.modeTabControl.Controls.Add(this.defaultTabPage);
            this.modeTabControl.Controls.Add(this.intListTabPage);
            this.modeTabControl.Location = new System.Drawing.Point(7, 17);
            this.modeTabControl.Name = "modeTabControl";
            this.modeTabControl.SelectedIndex = 0;
            this.modeTabControl.Size = new System.Drawing.Size(379, 331);
            this.modeTabControl.TabIndex = 0;
            // 
            // defaultTabPage
            // 
            this.defaultTabPage.Controls.Add(this.label4);
            this.defaultTabPage.Controls.Add(this.defaultImportNutton);
            this.defaultTabPage.Controls.Add(this.defaultImportTextBox);
            this.defaultTabPage.Location = new System.Drawing.Point(4, 22);
            this.defaultTabPage.Name = "defaultTabPage";
            this.defaultTabPage.Size = new System.Drawing.Size(371, 305);
            this.defaultTabPage.TabIndex = 2;
            this.defaultTabPage.Text = "快速导入";
            this.defaultTabPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "一键导入捕获到的脚本，每条命令使用换行符分割";
            // 
            // defaultImportNutton
            // 
            this.defaultImportNutton.Location = new System.Drawing.Point(317, 277);
            this.defaultImportNutton.Name = "defaultImportNutton";
            this.defaultImportNutton.Size = new System.Drawing.Size(50, 25);
            this.defaultImportNutton.TabIndex = 1;
            this.defaultImportNutton.Text = "导入";
            this.defaultImportNutton.UseVisualStyleBackColor = true;
            this.defaultImportNutton.Click += new System.EventHandler(this.defaultImportNutton_Click);
            // 
            // defaultImportTextBox
            // 
            this.defaultImportTextBox.Location = new System.Drawing.Point(5, 24);
            this.defaultImportTextBox.Multiline = true;
            this.defaultImportTextBox.Name = "defaultImportTextBox";
            this.defaultImportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.defaultImportTextBox.Size = new System.Drawing.Size(307, 278);
            this.defaultImportTextBox.TabIndex = 0;
            // 
            // intListTabPage
            // 
            this.intListTabPage.Controls.Add(this.replaceButton);
            this.intListTabPage.Controls.Add(this.insertNumListDataButton);
            this.intListTabPage.Controls.Add(this.label8);
            this.intListTabPage.Controls.Add(this.numDataTextBox);
            this.intListTabPage.Controls.Add(this.generateDataButton);
            this.intListTabPage.Controls.Add(this.clearArgButton);
            this.intListTabPage.Controls.Add(this.label7);
            this.intListTabPage.Controls.Add(this.argListTextBox);
            this.intListTabPage.Controls.Add(this.addArgButton);
            this.intListTabPage.Controls.Add(this.argTextBox);
            this.intListTabPage.Controls.Add(this.cmdIdTextBox);
            this.intListTabPage.Controls.Add(this.label6);
            this.intListTabPage.Controls.Add(this.label5);
            this.intListTabPage.Location = new System.Drawing.Point(4, 22);
            this.intListTabPage.Name = "intListTabPage";
            this.intListTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.intListTabPage.Size = new System.Drawing.Size(371, 305);
            this.intListTabPage.TabIndex = 1;
            this.intListTabPage.Text = "自定义";
            this.intListTabPage.UseVisualStyleBackColor = true;
            // 
            // replaceButton
            // 
            this.replaceButton.Location = new System.Drawing.Point(293, 221);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(74, 35);
            this.replaceButton.TabIndex = 18;
            this.replaceButton.Text = "替换为此包";
            this.replaceButton.UseVisualStyleBackColor = true;
            this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
            // 
            // insertNumListDataButton
            // 
            this.insertNumListDataButton.Location = new System.Drawing.Point(293, 262);
            this.insertNumListDataButton.Name = "insertNumListDataButton";
            this.insertNumListDataButton.Size = new System.Drawing.Size(74, 35);
            this.insertNumListDataButton.TabIndex = 17;
            this.insertNumListDataButton.Text = "插入此包";
            this.insertNumListDataButton.UseVisualStyleBackColor = true;
            this.insertNumListDataButton.Click += new System.EventHandler(this.insertNumListDataButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "欲添加包:";
            // 
            // numDataTextBox
            // 
            this.numDataTextBox.Location = new System.Drawing.Point(8, 134);
            this.numDataTextBox.Multiline = true;
            this.numDataTextBox.Name = "numDataTextBox";
            this.numDataTextBox.ReadOnly = true;
            this.numDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.numDataTextBox.Size = new System.Drawing.Size(279, 163);
            this.numDataTextBox.TabIndex = 15;
            // 
            // generateDataButton
            // 
            this.generateDataButton.Location = new System.Drawing.Point(293, 82);
            this.generateDataButton.Name = "generateDataButton";
            this.generateDataButton.Size = new System.Drawing.Size(74, 35);
            this.generateDataButton.TabIndex = 14;
            this.generateDataButton.Text = "生成封包";
            this.generateDataButton.UseVisualStyleBackColor = true;
            this.generateDataButton.Click += new System.EventHandler(this.generateDataButton_Click);
            // 
            // clearArgButton
            // 
            this.clearArgButton.Location = new System.Drawing.Point(293, 47);
            this.clearArgButton.Name = "clearArgButton";
            this.clearArgButton.Size = new System.Drawing.Size(74, 33);
            this.clearArgButton.TabIndex = 13;
            this.clearArgButton.Text = "清空参数";
            this.clearArgButton.UseVisualStyleBackColor = true;
            this.clearArgButton.Click += new System.EventHandler(this.clearArgButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "参数:";
            // 
            // argListTextBox
            // 
            this.argListTextBox.Location = new System.Drawing.Point(6, 48);
            this.argListTextBox.Multiline = true;
            this.argListTextBox.Name = "argListTextBox";
            this.argListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.argListTextBox.Size = new System.Drawing.Size(281, 68);
            this.argListTextBox.TabIndex = 11;
            this.argListTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DeleteOnly);
            // 
            // addArgButton
            // 
            this.addArgButton.Location = new System.Drawing.Point(293, 6);
            this.addArgButton.Name = "addArgButton";
            this.addArgButton.Size = new System.Drawing.Size(74, 21);
            this.addArgButton.TabIndex = 10;
            this.addArgButton.Text = "添加参数";
            this.addArgButton.UseVisualStyleBackColor = true;
            this.addArgButton.Click += new System.EventHandler(this.addArgButton_Click);
            // 
            // argTextBox
            // 
            this.argTextBox.Location = new System.Drawing.Point(199, 6);
            this.argTextBox.Name = "argTextBox";
            this.argTextBox.Size = new System.Drawing.Size(88, 21);
            this.argTextBox.TabIndex = 9;
            this.argTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.argTextBox_KeyDown);
            this.argTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckInputedKeyIsDigit);
            // 
            // cmdIdTextBox
            // 
            this.cmdIdTextBox.Location = new System.Drawing.Point(44, 6);
            this.cmdIdTextBox.Name = "cmdIdTextBox";
            this.cmdIdTextBox.Size = new System.Drawing.Size(74, 21);
            this.cmdIdTextBox.TabIndex = 8;
            this.cmdIdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckInputedKeyIsDigit);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(144, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "包体参数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "CmdID:";
            // 
            // ScriptEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 509);
            this.Controls.Add(this.editGroupBox);
            this.Controls.Add(this.importExportGroupBox);
            this.Controls.Add(this.scriptDescGroupBox);
            this.Controls.Add(this.scriptsDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScriptEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "脚本编辑窗口";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptEditForm_closing);
            ((System.ComponentModel.ISupportInitialize)(this.scriptsDataGridView)).EndInit();
            this.scriptDescGroupBox.ResumeLayout(false);
            this.scriptDescGroupBox.PerformLayout();
            this.importExportGroupBox.ResumeLayout(false);
            this.editGroupBox.ResumeLayout(false);
            this.editGroupBox.PerformLayout();
            this.modeTabControl.ResumeLayout(false);
            this.defaultTabPage.ResumeLayout(false);
            this.defaultTabPage.PerformLayout();
            this.intListTabPage.ResumeLayout(false);
            this.intListTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView scriptsDataGridView;
        private System.Windows.Forms.TextBox scriptDescTextBox;
        private System.Windows.Forms.GroupBox scriptDescGroupBox;
        private System.Windows.Forms.TextBox scriptTitleTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox scriptPwdTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox importExportGroupBox;
        private System.Windows.Forms.GroupBox editGroupBox;
        private System.Windows.Forms.Button exportScriptButton;
        private System.Windows.Forms.Button importScriptButton;
        private System.Windows.Forms.Button createNewScriptButton;
        private System.Windows.Forms.Label scriptTitleLabel;
        private System.Windows.Forms.TabControl modeTabControl;
        private System.Windows.Forms.TabPage defaultTabPage;
        private System.Windows.Forms.TextBox defaultImportTextBox;
        private System.Windows.Forms.TabPage intListTabPage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button defaultImportNutton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox argTextBox;
        private System.Windows.Forms.TextBox cmdIdTextBox;
        private System.Windows.Forms.Button insertNumListDataButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox numDataTextBox;
        private System.Windows.Forms.Button generateDataButton;
        private System.Windows.Forms.Button clearArgButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox argListTextBox;
        private System.Windows.Forms.Button addArgButton;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.Button timesEditbutton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox timesTextBox;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataBody;
        private System.Windows.Forms.DataGridViewTextBoxColumn times;
    }
}