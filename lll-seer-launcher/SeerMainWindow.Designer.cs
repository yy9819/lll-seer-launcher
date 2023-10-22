using System;
using System.Threading;
using System.Diagnostics;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

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
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.gameReloadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.seerUtilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changSuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.精灵换肤ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSeerFiddlerWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideSkillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hidePetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEditFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainFormStatusStrip.SuspendLayout();
            this.mainFormMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // seerWebBrowser
            // 
            this.seerWebBrowser.Location = new System.Drawing.Point(0, 25);
            this.seerWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.seerWebBrowser.Name = "seerWebBrowser";
            this.seerWebBrowser.ScriptErrorsSuppressed = true;
            this.seerWebBrowser.ScrollBarsEnabled = false;
            this.seerWebBrowser.Size = new System.Drawing.Size(960, 611);
            this.seerWebBrowser.TabIndex = 0;
            this.seerWebBrowser.Url = new System.Uri("https://seer.61.com/play.shtml?micro=1", System.UriKind.Absolute);
            this.seerWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.SeerWebBrowser_DocumentCompleted);
            this.seerWebBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.SeerWebBrowser_Navigated);
            this.seerWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.SeerWebBrowser_Navigating);
            this.seerWebBrowser.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.SeerWebBrowser_KeyDown);
            // 
            // mainFormStatusStrip
            // 
            this.mainFormStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usedRamToolStripStatusLabel,
            this.toolStripStatusLabel1});
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
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "登录账号:";
            // 
            // mainFormMenuStrip
            // 
            this.mainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSetting,
            this.seerUtilsToolStripMenuItem,
            this.精灵换肤ToolStripMenuItem});
            this.mainFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainFormMenuStrip.Name = "mainFormMenuStrip";
            this.mainFormMenuStrip.Size = new System.Drawing.Size(960, 25);
            this.mainFormMenuStrip.TabIndex = 2;
            this.mainFormMenuStrip.Text = "menuStrip1";
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
            this.gameReloadMenu.Size = new System.Drawing.Size(100, 22);
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
            // 精灵换肤ToolStripMenuItem
            // 
            this.精灵换肤ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSeerFiddlerWindowToolStripMenuItem,
            this.hideSkillToolStripMenuItem,
            this.hidePetToolStripMenuItem,
            this.showEditFormToolStripMenuItem,
            this.reloadToolStripMenuItem});
            this.精灵换肤ToolStripMenuItem.Name = "精灵换肤ToolStripMenuItem";
            this.精灵换肤ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.精灵换肤ToolStripMenuItem.Text = "精灵换肤";
            // 
            // openSeerFiddlerWindowToolStripMenuItem
            // 
            this.openSeerFiddlerWindowToolStripMenuItem.Name = "openSeerFiddlerWindowToolStripMenuItem";
            this.openSeerFiddlerWindowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openSeerFiddlerWindowToolStripMenuItem.Text = "资源链接捕获窗口";
            this.openSeerFiddlerWindowToolStripMenuItem.Click += new System.EventHandler(this.openSeerFiddlerWindowToolStripMenuItem_Click);
            // 
            // hideSkillToolStripMenuItem
            // 
            this.hideSkillToolStripMenuItem.Name = "hideSkillToolStripMenuItem";
            this.hideSkillToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hideSkillToolStripMenuItem.Text = "技能透明";
            this.hideSkillToolStripMenuItem.Click += new System.EventHandler(this.hideSkillToolStripMenuItem_Click);
            // 
            // hidePetToolStripMenuItem
            // 
            this.hidePetToolStripMenuItem.Name = "hidePetToolStripMenuItem";
            this.hidePetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hidePetToolStripMenuItem.Text = "精灵透明";
            this.hidePetToolStripMenuItem.Click += new System.EventHandler(this.hidePetToolStripMenuItem_Click);
            // 
            // showEditFormToolStripMenuItem
            // 
            this.showEditFormToolStripMenuItem.Name = "showEditFormToolStripMenuItem";
            this.showEditFormToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showEditFormToolStripMenuItem.Text = "精灵换肤编辑窗口";
            this.showEditFormToolStripMenuItem.Click += new System.EventHandler(this.showEditFormToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reloadToolStripMenuItem.Text = "修复并重启登录器";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // seerMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 611);
            this.Controls.Add(this.mainFormStatusStrip);
            this.Controls.Add(this.mainFormMenuStrip);
            this.Controls.Add(this.seerWebBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainFormMenuStrip;
            this.MaximizeBox = false;
            this.Name = "seerMainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "啦啦啦赛尔号登录器";
            this.Load += new System.EventHandler(this.SeerMainWindow_Load);
            this.HandleDestroyed += new System.EventHandler(this.SeerMainWindow_Destroyed);
            this.mainFormStatusStrip.ResumeLayout(false);
            this.mainFormStatusStrip.PerformLayout();
            this.mainFormMenuStrip.ResumeLayout(false);
            this.mainFormMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private delegate void GetUsedMemoryCallBack(); 

        private void GetUsedMemorySize()
        {
            Process currentProcess = Process.GetCurrentProcess();
            while (!GlobalVariable.stopThread)
            {
                currentProcess.Refresh();
                try
                {
                    GetUsedMemoryCallBack callBack = delegate ()
                    {
                        this.usedRamToolStripStatusLabel.Text = $"运行内存：{currentProcess.PrivateMemorySize64 / 1024 / 1024}MB";
                        this.toolStripStatusLabel1.Text = $"当前登录账号：{GlobalVariable.userId}";
                    };
                    this.Invoke(callBack);
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
        private System.Windows.Forms.MenuStrip mainFormMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSetting;
        private System.Windows.Forms.ToolStripMenuItem gameReloadMenu;
        private System.Windows.Forms.ToolStripStatusLabel usedRamToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem seerUtilsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changSuitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 精灵换肤ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSeerFiddlerWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideSkillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hidePetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showEditFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
    }
}

