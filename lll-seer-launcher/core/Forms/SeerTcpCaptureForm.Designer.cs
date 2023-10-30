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
            this.startCaptureCheckBox = new System.Windows.Forms.CheckBox();
            this.dataTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tcpDataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
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
            this.tcpDataGridView.Size = new System.Drawing.Size(807, 467);
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
            this.sendDataToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
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
            // startCaptureCheckBox
            // 
            this.startCaptureCheckBox.AutoSize = true;
            this.startCaptureCheckBox.Checked = true;
            this.startCaptureCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startCaptureCheckBox.Location = new System.Drawing.Point(721, 476);
            this.startCaptureCheckBox.Name = "startCaptureCheckBox";
            this.startCaptureCheckBox.Size = new System.Drawing.Size(72, 16);
            this.startCaptureCheckBox.TabIndex = 1;
            this.startCaptureCheckBox.Text = "开始捕获";
            this.startCaptureCheckBox.UseVisualStyleBackColor = true;
            // 
            // dataTextBox
            // 
            this.dataTextBox.Location = new System.Drawing.Point(6, 474);
            this.dataTextBox.Multiline = true;
            this.dataTextBox.Name = "dataTextBox";
            this.dataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataTextBox.Size = new System.Drawing.Size(709, 126);
            this.dataTextBox.TabIndex = 2;
            // 
            // SeerTcpCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 605);
            this.Controls.Add(this.dataTextBox);
            this.Controls.Add(this.startCaptureCheckBox);
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
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}