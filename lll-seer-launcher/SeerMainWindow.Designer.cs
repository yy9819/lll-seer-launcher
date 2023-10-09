namespace lll_seer_launcher
{
    partial class seerMainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.seerWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // seerWebBrowser
            // 
            this.seerWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seerWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.seerWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.seerWebBrowser.Name = "seerWebBrowser";
            this.seerWebBrowser.ScrollBarsEnabled = false;
            this.seerWebBrowser.Size = new System.Drawing.Size(960, 560);
            this.seerWebBrowser.TabIndex = 0;
            this.seerWebBrowser.Url = new System.Uri("https://seer.61.com/play.shtml", System.UriKind.Absolute);
            this.seerWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.seerWebBrowser_DocumentCompleted);
            this.seerWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.seerWebBrowser_Navigating);
            this.seerWebBrowser.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.seerWebBrowser_KeyDown);
            // 
            // seerMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 560);
            this.Controls.Add(this.seerWebBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "seerMainWindow";
            this.Text = "啦啦啦赛尔号登录器";
            this.Load += new System.EventHandler(this.SeerMainWindow_Load);
            this.HandleDestroyed += new System.EventHandler(this.SeerMainWindow_Destroyed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser seerWebBrowser;
    }
}

