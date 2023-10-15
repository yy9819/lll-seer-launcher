using System;
using System.Threading;
using System.Diagnostics;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(seerMainWindow));
            this.seerWebBrowser = new System.Windows.Forms.WebBrowser();
            this.mainFormStatusStrip = new System.Windows.Forms.StatusStrip();
            this.usedRamToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.gameReloadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.seerUtilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changSuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainFormStatusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // seerWebBrowser
            // 
            this.seerWebBrowser.Location = new System.Drawing.Point(0, 25);
            this.seerWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.seerWebBrowser.Name = "seerWebBrowser";
            this.seerWebBrowser.ScrollBarsEnabled = false;
            this.seerWebBrowser.Size = new System.Drawing.Size(960, 611);
            this.seerWebBrowser.TabIndex = 0;
            this.seerWebBrowser.Url = new System.Uri("https://seer.61.com/play.shtml", System.UriKind.Absolute);
            this.seerWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.SeerWebBrowser_DocumentCompleted);
            this.seerWebBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.SeerWebBrowser_Navigated);
            this.seerWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.SeerWebBrowser_Navigating);
            this.seerWebBrowser.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.SeerWebBrowser_KeyDown);
            // 
            // mainFormStatusStrip
            // 
            this.mainFormStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usedRamToolStripStatusLabel});
            this.mainFormStatusStrip.Location = new System.Drawing.Point(0, 589);
            this.mainFormStatusStrip.Name = "mainFormStatusStrip";
            this.mainFormStatusStrip.Size = new System.Drawing.Size(960, 22);
            this.mainFormStatusStrip.TabIndex = 1;
            this.mainFormStatusStrip.Text = "statusStrip1";
            // 
            // usedRamToolStripStatusLabel
            // 
            this.usedRamToolStripStatusLabel.Name = "usedRamToolStripStatusLabel";
            this.usedRamToolStripStatusLabel.Size = new System.Drawing.Size(68, 17);
            this.usedRamToolStripStatusLabel.Text = "运行内存：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSetting,
            this.seerUtilsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemSetting
            // 
            this.toolStripMenuItemSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameReloadMenu});
            this.toolStripMenuItemSetting.Name = "toolStripMenuItemSetting";
            this.toolStripMenuItemSetting.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItemSetting.Text = "设置";
            // 
            // gameReloadMenu
            // 
            this.gameReloadMenu.Name = "gameReloadMenu";
            this.gameReloadMenu.Size = new System.Drawing.Size(180, 22);
            this.gameReloadMenu.Text = "刷新";
            this.gameReloadMenu.Click += new System.EventHandler(this.gameReloadMenu_Click);
            // 
            // seerUtilsToolStripMenuItem
            // 
            this.seerUtilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changSuitToolStripMenuItem});
            this.seerUtilsToolStripMenuItem.Name = "seerUtilsToolStripMenuItem";
            this.seerUtilsToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.seerUtilsToolStripMenuItem.Text = "小助手";
            // 
            // changSuitToolStripMenuItem
            // 
            this.changSuitToolStripMenuItem.Name = "changSuitToolStripMenuItem";
            this.changSuitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.changSuitToolStripMenuItem.Text = "更换装备与称号";
            this.changSuitToolStripMenuItem.Click += new System.EventHandler(this.changSuitToolStripMenuItem_Click);
            // 
            // seerMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 611);
            this.Controls.Add(this.mainFormStatusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.seerWebBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "seerMainWindow";
            this.Text = "啦啦啦赛尔号登录器";
            this.Load += new System.EventHandler(this.SeerMainWindow_Load);
            this.HandleDestroyed += new System.EventHandler(this.SeerMainWindow_Destroyed);
            this.mainFormStatusStrip.ResumeLayout(false);
            this.mainFormStatusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private void GetUsedMemorySize()
        {
            Process currentProcess = Process.GetCurrentProcess();
            while (!GlobalVariable.stopThread)
            {
                currentProcess.Refresh();
                try
                {
                    if (GlobalVariable.gameReloadFlg)
                    {
                        GlobalVariable.gameReloadFlg = false;
                        this.seerWebBrowser.Refresh();
                    }
                    this.usedRamToolStripStatusLabel.Text = $"运行内存：{currentProcess.PrivateMemorySize64 / 1024 / 1024}MB";
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                }

                Thread.Sleep(1000);
            }
        }
        private System.Windows.Forms.WebBrowser seerWebBrowser;
        private System.Windows.Forms.StatusStrip mainFormStatusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSetting;
        private System.Windows.Forms.ToolStripMenuItem gameReloadMenu;
        private System.Windows.Forms.ToolStripStatusLabel usedRamToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem seerUtilsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changSuitToolStripMenuItem;
    }
}

