namespace lll_seer_launcher.core.Forms
{
    partial class SeerTcpCaptureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeerTcpCaptureForm));
            this.tcpDataGridView = new System.Windows.Forms.DataGridView();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startCaptureCheckBox = new System.Windows.Forms.CheckBox();
            this.dataTextBox = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.preViewTabPage = new System.Windows.Forms.TabPage();
            this.disableTabPage = new System.Windows.Forms.TabPage();
            this.setDisableCmdIdButton = new System.Windows.Forms.Button();
            this.disableCmdIdTextBox = new System.Windows.Forms.TextBox();
            this.sendDataTabPage = new System.Windows.Forms.TabPage();
            this.sendDataButton = new System.Windows.Forms.Button();
            this.sendDataTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tcpDataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.preViewTabPage.SuspendLayout();
            this.disableTabPage.SuspendLayout();
            this.sendDataTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcpDataGridView
            // 
            this.tcpDataGridView.AllowUserToAddRows = false;
            this.tcpDataGridView.AllowUserToDeleteRows = false;
            this.tcpDataGridView.AllowUserToResizeRows = false;
            this.tcpDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tcpDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.type,
            this.cmdId,
            this.cmdName,
            this.data});
            this.tcpDataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.tcpDataGridView.Location = new System.Drawing.Point(6, 3);
            this.tcpDataGridView.MultiSelect = false;
            this.tcpDataGridView.Name = "tcpDataGridView";
            this.tcpDataGridView.RowHeadersVisible = false;
            this.tcpDataGridView.RowTemplate.Height = 23;
            this.tcpDataGridView.Size = new System.Drawing.Size(807, 450);
            this.tcpDataGridView.TabIndex = 0;
            this.tcpDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tcpDataGridView_CellContentClick);
            // 
            // type
            // 
            this.type.HeaderText = "类型";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 70;
            // 
            // cmdId
            // 
            this.cmdId.HeaderText = "cmdId";
            this.cmdId.Name = "cmdId";
            this.cmdId.ReadOnly = true;
            this.cmdId.Width = 60;
            // 
            // cmdName
            // 
            this.cmdName.HeaderText = "cmdName";
            this.cmdName.Name = "cmdName";
            this.cmdName.ReadOnly = true;
            // 
            // data
            // 
            this.data.HeaderText = "包体";
            this.data.Name = "data";
            this.data.ReadOnly = true;
            this.data.Width = 1500;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.sendDataToolStripMenuItem,
            this.addDataToolStripMenuItem,
            this.disableToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 114);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.clearToolStripMenuItem.Text = "清空";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.copyToolStripMenuItem.Text = "复制";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // sendDataToolStripMenuItem
            // 
            this.sendDataToolStripMenuItem.Name = "sendDataToolStripMenuItem";
            this.sendDataToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.sendDataToolStripMenuItem.Text = "发送";
            this.sendDataToolStripMenuItem.Click += new System.EventHandler(this.sendDataToolStripMenuItem_Click);
            // 
            // addDataToolStripMenuItem
            // 
            this.addDataToolStripMenuItem.Name = "addDataToolStripMenuItem";
            this.addDataToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.addDataToolStripMenuItem.Text = "添加";
            this.addDataToolStripMenuItem.Click += new System.EventHandler(this.addDataToolStripMenuItem_Click);
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.disableToolStripMenuItem.Text = "屏蔽";
            this.disableToolStripMenuItem.Click += new System.EventHandler(this.disableToolStripMenuItem_Click);
            // 
            // startCaptureCheckBox
            // 
            this.startCaptureCheckBox.AutoSize = true;
            this.startCaptureCheckBox.Checked = true;
            this.startCaptureCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startCaptureCheckBox.Location = new System.Drawing.Point(697, 6);
            this.startCaptureCheckBox.Name = "startCaptureCheckBox";
            this.startCaptureCheckBox.Size = new System.Drawing.Size(72, 16);
            this.startCaptureCheckBox.TabIndex = 1;
            this.startCaptureCheckBox.Text = "开始捕获";
            this.startCaptureCheckBox.UseVisualStyleBackColor = true;
            // 
            // dataTextBox
            // 
            this.dataTextBox.Location = new System.Drawing.Point(6, 7);
            this.dataTextBox.Multiline = true;
            this.dataTextBox.Name = "dataTextBox";
            this.dataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataTextBox.Size = new System.Drawing.Size(685, 142);
            this.dataTextBox.TabIndex = 2;
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl.Controls.Add(this.preViewTabPage);
            this.tabControl.Controls.Add(this.disableTabPage);
            this.tabControl.Controls.Add(this.sendDataTabPage);
            this.tabControl.Location = new System.Drawing.Point(6, 459);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(801, 163);
            this.tabControl.TabIndex = 3;
            // 
            // preViewTabPage
            // 
            this.preViewTabPage.Controls.Add(this.dataTextBox);
            this.preViewTabPage.Controls.Add(this.startCaptureCheckBox);
            this.preViewTabPage.Location = new System.Drawing.Point(22, 4);
            this.preViewTabPage.Name = "preViewTabPage";
            this.preViewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.preViewTabPage.Size = new System.Drawing.Size(775, 155);
            this.preViewTabPage.TabIndex = 0;
            this.preViewTabPage.Text = "预览";
            this.preViewTabPage.UseVisualStyleBackColor = true;
            // 
            // disableTabPage
            // 
            this.disableTabPage.Controls.Add(this.setDisableCmdIdButton);
            this.disableTabPage.Controls.Add(this.disableCmdIdTextBox);
            this.disableTabPage.Location = new System.Drawing.Point(22, 4);
            this.disableTabPage.Name = "disableTabPage";
            this.disableTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.disableTabPage.Size = new System.Drawing.Size(775, 155);
            this.disableTabPage.TabIndex = 1;
            this.disableTabPage.Text = "屏蔽";
            this.disableTabPage.UseVisualStyleBackColor = true;
            // 
            // setDisableCmdIdButton
            // 
            this.setDisableCmdIdButton.Location = new System.Drawing.Point(711, 126);
            this.setDisableCmdIdButton.Name = "setDisableCmdIdButton";
            this.setDisableCmdIdButton.Size = new System.Drawing.Size(61, 23);
            this.setDisableCmdIdButton.TabIndex = 1;
            this.setDisableCmdIdButton.Text = "确认";
            this.setDisableCmdIdButton.UseVisualStyleBackColor = true;
            this.setDisableCmdIdButton.Click += new System.EventHandler(this.setDisableCmdIdButton_Click);
            // 
            // disableCmdIdTextBox
            // 
            this.disableCmdIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.disableCmdIdTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.disableCmdIdTextBox.Location = new System.Drawing.Point(6, 6);
            this.disableCmdIdTextBox.Multiline = true;
            this.disableCmdIdTextBox.Name = "disableCmdIdTextBox";
            this.disableCmdIdTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.disableCmdIdTextBox.Size = new System.Drawing.Size(699, 143);
            this.disableCmdIdTextBox.TabIndex = 0;
            this.disableCmdIdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.disableCmdIdTextBox_KeyPress);
            // 
            // sendDataTabPage
            // 
            this.sendDataTabPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sendDataTabPage.Controls.Add(this.sendDataButton);
            this.sendDataTabPage.Controls.Add(this.sendDataTextBox);
            this.sendDataTabPage.Location = new System.Drawing.Point(22, 4);
            this.sendDataTabPage.Name = "sendDataTabPage";
            this.sendDataTabPage.Size = new System.Drawing.Size(775, 155);
            this.sendDataTabPage.TabIndex = 2;
            this.sendDataTabPage.Text = "发送";
            this.sendDataTabPage.UseVisualStyleBackColor = true;
            // 
            // sendDataButton
            // 
            this.sendDataButton.Location = new System.Drawing.Point(613, 46);
            this.sendDataButton.Name = "sendDataButton";
            this.sendDataButton.Size = new System.Drawing.Size(157, 103);
            this.sendDataButton.TabIndex = 3;
            this.sendDataButton.Text = "发送\r\n\r\n例:(回车分割)\r\nsend|1001|00-00-00-00\r\nsend|1002|00-00-00-00\r\n";
            this.sendDataButton.UseVisualStyleBackColor = true;
            this.sendDataButton.Click += new System.EventHandler(this.sendDataButton_Click);
            // 
            // sendDataTextBox
            // 
            this.sendDataTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sendDataTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sendDataTextBox.Location = new System.Drawing.Point(5, 6);
            this.sendDataTextBox.Multiline = true;
            this.sendDataTextBox.Name = "sendDataTextBox";
            this.sendDataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sendDataTextBox.Size = new System.Drawing.Size(603, 143);
            this.sendDataTextBox.TabIndex = 2;
            // 
            // SeerTcpCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 625);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.tcpDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SeerTcpCaptureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "command捕获窗口";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SeerTcpCaptureForm_Closing);
            this.Load += new System.EventHandler(this.SeerTcpCaptureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tcpDataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.preViewTabPage.ResumeLayout(false);
            this.preViewTabPage.PerformLayout();
            this.disableTabPage.ResumeLayout(false);
            this.disableTabPage.PerformLayout();
            this.sendDataTabPage.ResumeLayout(false);
            this.sendDataTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tcpDataGridView;
        private System.Windows.Forms.CheckBox startCaptureCheckBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdId;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.TextBox dataTextBox;
        private System.Windows.Forms.ToolStripMenuItem sendDataToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage preViewTabPage;
        private System.Windows.Forms.TabPage disableTabPage;
        private System.Windows.Forms.TextBox disableCmdIdTextBox;
        private System.Windows.Forms.Button setDisableCmdIdButton;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.TabPage sendDataTabPage;
        private System.Windows.Forms.Button sendDataButton;
        private System.Windows.Forms.TextBox sendDataTextBox;
        private System.Windows.Forms.ToolStripMenuItem addDataToolStripMenuItem;
    }
}