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
            this.loginUserIdToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fireBuffTimeCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lowerHpStatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.gameReloadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.muteGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batteryDormantSwitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTcpCaptureFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seerUtilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changSuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fightMapBossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFightNoteFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.petSkinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideSkillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hidePetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEditFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSeerFiddlerWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenBuffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buleBuffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buleBuffPlusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purpleBuffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purpleBuffPlusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goldBuffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goldBuffPlusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowerHpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoChargeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableVipAutoChargeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batteryStatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.loginUserIdToolStripStatusLabel,
            this.fireBuffTimeCountToolStripStatusLabel,
            this.lowerHpStatusToolStripStatusLabel,
            this.batteryStatusToolStripStatusLabel});
            this.mainFormStatusStrip.Location = new System.Drawing.Point(0, 585);
            this.mainFormStatusStrip.Name = "mainFormStatusStrip";
            this.mainFormStatusStrip.Size = new System.Drawing.Size(960, 26);
            this.mainFormStatusStrip.TabIndex = 1;
            this.mainFormStatusStrip.Text = "statusStrip1";
            // 
            // usedRamToolStripStatusLabel
            // 
            this.usedRamToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.usedRamToolStripStatusLabel.Name = "usedRamToolStripStatusLabel";
            this.usedRamToolStripStatusLabel.Size = new System.Drawing.Size(72, 21);
            this.usedRamToolStripStatusLabel.Text = "运行内存：";
            // 
            // loginUserIdToolStripStatusLabel
            // 
            this.loginUserIdToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.loginUserIdToolStripStatusLabel.Name = "loginUserIdToolStripStatusLabel";
            this.loginUserIdToolStripStatusLabel.Size = new System.Drawing.Size(63, 21);
            this.loginUserIdToolStripStatusLabel.Text = "登录账号:";
            // 
            // fireBuffTimeCountToolStripStatusLabel
            // 
            this.fireBuffTimeCountToolStripStatusLabel.Name = "fireBuffTimeCountToolStripStatusLabel";
            this.fireBuffTimeCountToolStripStatusLabel.Size = new System.Drawing.Size(0, 21);
            // 
            // lowerHpStatusToolStripStatusLabel
            // 
            this.lowerHpStatusToolStripStatusLabel.Name = "lowerHpStatusToolStripStatusLabel";
            this.lowerHpStatusToolStripStatusLabel.Size = new System.Drawing.Size(0, 21);
            // 
            // mainFormMenuStrip
            // 
            this.mainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSetting,
            this.seerUtilsToolStripMenuItem,
            this.petSkinToolStripMenuItem,
            this.fightToolStripMenuItem});
            this.mainFormMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainFormMenuStrip.Name = "mainFormMenuStrip";
            this.mainFormMenuStrip.Size = new System.Drawing.Size(960, 25);
            this.mainFormMenuStrip.TabIndex = 2;
            this.mainFormMenuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemSetting
            // 
            this.toolStripMenuItemSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameReloadMenu,
            this.muteGameToolStripMenuItem,
            this.batteryDormantSwitchToolStripMenuItem,
            this.openTcpCaptureFormToolStripMenuItem});
            this.toolStripMenuItemSetting.Name = "toolStripMenuItemSetting";
            this.toolStripMenuItemSetting.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItemSetting.Text = "设置";
            // 
            // gameReloadMenu
            // 
            this.gameReloadMenu.Name = "gameReloadMenu";
            this.gameReloadMenu.Size = new System.Drawing.Size(242, 22);
            this.gameReloadMenu.Text = "刷新";
            this.gameReloadMenu.Click += new System.EventHandler(this.gameReloadMenu_Click);
            // 
            // muteGameToolStripMenuItem
            // 
            this.muteGameToolStripMenuItem.Name = "muteGameToolStripMenuItem";
            this.muteGameToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.muteGameToolStripMenuItem.Text = "静音";
            this.muteGameToolStripMenuItem.Click += new System.EventHandler(this.muteGameToolStripMenuItem_Click);
            // 
            // batteryDormantSwitchToolStripMenuItem
            // 
            this.batteryDormantSwitchToolStripMenuItem.Name = "batteryDormantSwitchToolStripMenuItem";
            this.batteryDormantSwitchToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.batteryDormantSwitchToolStripMenuItem.Text = "关闭电池";
            this.batteryDormantSwitchToolStripMenuItem.Click += new System.EventHandler(this.batteryDormantSwitchToolStripMenuItem_Click);
            // 
            // openTcpCaptureFormToolStripMenuItem
            // 
            this.openTcpCaptureFormToolStripMenuItem.Name = "openTcpCaptureFormToolStripMenuItem";
            this.openTcpCaptureFormToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.openTcpCaptureFormToolStripMenuItem.Text = "打开赛尔号command捕获窗口";
            this.openTcpCaptureFormToolStripMenuItem.Click += new System.EventHandler(this.openTcpCaptureFormToolStripMenuItem_Click);
            // 
            // seerUtilsToolStripMenuItem
            // 
            this.seerUtilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changSuitToolStripMenuItem,
            this.fightMapBossToolStripMenuItem,
            this.showFightNoteFormToolStripMenuItem});
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
            // fightMapBossToolStripMenuItem
            // 
            this.fightMapBossToolStripMenuItem.Name = "fightMapBossToolStripMenuItem";
            this.fightMapBossToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fightMapBossToolStripMenuItem.Text = "对战地图怪";
            this.fightMapBossToolStripMenuItem.Click += new System.EventHandler(this.fightMapBossToolStripMenuItem_Click);
            // 
            // showFightNoteFormToolStripMenuItem
            // 
            this.showFightNoteFormToolStripMenuItem.Name = "showFightNoteFormToolStripMenuItem";
            this.showFightNoteFormToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showFightNoteFormToolStripMenuItem.Text = "对战助手";
            this.showFightNoteFormToolStripMenuItem.Click += new System.EventHandler(this.showFightNoteFormToolStripMenuItem_Click);
            // 
            // petSkinToolStripMenuItem
            // 
            this.petSkinToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideSkillToolStripMenuItem,
            this.hidePetToolStripMenuItem,
            this.showEditFormToolStripMenuItem,
            this.openSeerFiddlerWindowToolStripMenuItem,
            this.reloadToolStripMenuItem});
            this.petSkinToolStripMenuItem.Name = "petSkinToolStripMenuItem";
            this.petSkinToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.petSkinToolStripMenuItem.Text = "精灵换肤";
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
            // openSeerFiddlerWindowToolStripMenuItem
            // 
            this.openSeerFiddlerWindowToolStripMenuItem.Name = "openSeerFiddlerWindowToolStripMenuItem";
            this.openSeerFiddlerWindowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openSeerFiddlerWindowToolStripMenuItem.Text = "资源链接捕获窗口";
            this.openSeerFiddlerWindowToolStripMenuItem.Click += new System.EventHandler(this.openSeerFiddlerWindowToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reloadToolStripMenuItem.Text = "修复登录器网络";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // fightToolStripMenuItem
            // 
            this.fightToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyFireToolStripMenuItem,
            this.lowerHpToolStripMenuItem,
            this.autoChargeToolStripMenuItem,
            this.disableVipAutoChargeToolStripMenuItem});
            this.fightToolStripMenuItem.Name = "fightToolStripMenuItem";
            this.fightToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fightToolStripMenuItem.Text = "战斗";
            // 
            // copyFireToolStripMenuItem
            // 
            this.copyFireToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.greenBuffToolStripMenuItem,
            this.buleBuffToolStripMenuItem,
            this.buleBuffPlusToolStripMenuItem,
            this.purpleBuffToolStripMenuItem,
            this.purpleBuffPlusToolStripMenuItem,
            this.goldBuffToolStripMenuItem,
            this.goldBuffPlusToolStripMenuItem});
            this.copyFireToolStripMenuItem.Name = "copyFireToolStripMenuItem";
            this.copyFireToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyFireToolStripMenuItem.Text = "借火";
            // 
            // greenBuffToolStripMenuItem
            // 
            this.greenBuffToolStripMenuItem.Name = "greenBuffToolStripMenuItem";
            this.greenBuffToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.greenBuffToolStripMenuItem.Text = "绿火";
            this.greenBuffToolStripMenuItem.Click += new System.EventHandler(this.greenBuffToolStripMenuItem_Click);
            // 
            // buleBuffToolStripMenuItem
            // 
            this.buleBuffToolStripMenuItem.Name = "buleBuffToolStripMenuItem";
            this.buleBuffToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.buleBuffToolStripMenuItem.Text = "蓝火";
            this.buleBuffToolStripMenuItem.Click += new System.EventHandler(this.buleBuffToolStripMenuItem_Click);
            // 
            // buleBuffPlusToolStripMenuItem
            // 
            this.buleBuffPlusToolStripMenuItem.Name = "buleBuffPlusToolStripMenuItem";
            this.buleBuffPlusToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.buleBuffPlusToolStripMenuItem.Text = "蓝火加强型";
            this.buleBuffPlusToolStripMenuItem.Click += new System.EventHandler(this.buleBuffPlusToolStripMenuItem_Click);
            // 
            // purpleBuffToolStripMenuItem
            // 
            this.purpleBuffToolStripMenuItem.Name = "purpleBuffToolStripMenuItem";
            this.purpleBuffToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.purpleBuffToolStripMenuItem.Text = "紫火";
            this.purpleBuffToolStripMenuItem.Click += new System.EventHandler(this.purpleBuffToolStripMenuItem_Click);
            // 
            // purpleBuffPlusToolStripMenuItem
            // 
            this.purpleBuffPlusToolStripMenuItem.Name = "purpleBuffPlusToolStripMenuItem";
            this.purpleBuffPlusToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.purpleBuffPlusToolStripMenuItem.Text = "紫火加强型";
            this.purpleBuffPlusToolStripMenuItem.Click += new System.EventHandler(this.purpleBuffPlusToolStripMenuItem_Click);
            // 
            // goldBuffToolStripMenuItem
            // 
            this.goldBuffToolStripMenuItem.Name = "goldBuffToolStripMenuItem";
            this.goldBuffToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.goldBuffToolStripMenuItem.Text = "金火";
            this.goldBuffToolStripMenuItem.Click += new System.EventHandler(this.goldBuffToolStripMenuItem_Click);
            // 
            // goldBuffPlusToolStripMenuItem
            // 
            this.goldBuffPlusToolStripMenuItem.Name = "goldBuffPlusToolStripMenuItem";
            this.goldBuffPlusToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.goldBuffPlusToolStripMenuItem.Text = "金火加强型";
            this.goldBuffPlusToolStripMenuItem.Click += new System.EventHandler(this.goldBuffPlusToolStripMenuItem_Click);
            // 
            // lowerHpToolStripMenuItem
            // 
            this.lowerHpToolStripMenuItem.Name = "lowerHpToolStripMenuItem";
            this.lowerHpToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lowerHpToolStripMenuItem.Text = "压血";
            this.lowerHpToolStripMenuItem.Click += new System.EventHandler(this.lowerHpToolStripMenuItem_Click);
            // 
            // autoChargeToolStripMenuItem
            // 
            this.autoChargeToolStripMenuItem.Name = "autoChargeToolStripMenuItem";
            this.autoChargeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.autoChargeToolStripMenuItem.Text = "自动回血(非VIP)";
            this.autoChargeToolStripMenuItem.Click += new System.EventHandler(this.autoChargeToolStripMenuItem_Click);
            // 
            // disableVipAutoChargeToolStripMenuItem
            // 
            this.disableVipAutoChargeToolStripMenuItem.Name = "disableVipAutoChargeToolStripMenuItem";
            this.disableVipAutoChargeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.disableVipAutoChargeToolStripMenuItem.Text = "关闭VIP自动回血";
            this.disableVipAutoChargeToolStripMenuItem.Click += new System.EventHandler(this.disableVipAutoChargeToolStripMenuItem_Click);
            // 
            // batteryStatusToolStripStatusLabel
            // 
            this.batteryStatusToolStripStatusLabel.Name = "batteryStatusToolStripStatusLabel";
            this.batteryStatusToolStripStatusLabel.Size = new System.Drawing.Size(0, 21);
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
        private delegate void StartCountFireBuffCallBack();

        private void GetUsedMemorySize()
        {
            Process currentProcess = Process.GetCurrentProcess();
            while (!GlobalVariable.stopThread)
            {
                currentProcess.Refresh();
                try
                {
                    if (!this.IsDisposed)
                    {
                        GetUsedMemoryCallBack callBack = delegate ()
                        {
                            this.usedRamToolStripStatusLabel.Text = $"运行内存：{currentProcess.PrivateMemorySize64 / 1024 / 1024}MB";
                            this.loginUserIdToolStripStatusLabel.Text = $"当前登录账号：{GlobalVariable.loginUserInfo.userId}";
                        };
                        this.Invoke(callBack);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                }
                Thread.Sleep(1000);
            }

        }

        public void StartCountFireBuff(int mins)
        {
            int totalTime = mins * 60;
            StartCountFireBuffCallBack callBack;
            try
            {
                while (totalTime > 0)
                {
                    if (GlobalVariable.stopThread) break;
                    if (!this.IsDisposed)
                    {
                        callBack = delegate ()
                        {
                            this.fireBuffTimeCountToolStripStatusLabel.Text = $"火焰剩余：{ totalTime / 60 }分{ totalTime % 60 }秒";
                        };
                        this.Invoke((callBack));
                    }
                    Thread.Sleep(1000);
                    totalTime--;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
            finally
            {
                if (!this.IsDisposed)
                {
                    callBack = delegate ()
                    {
                        this.fireBuffTimeCountToolStripStatusLabel.Text = "";
                    };
                    this.Invoke((callBack));
                    GlobalVariable.fireCountThread = null;
                }
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
        private System.Windows.Forms.ToolStripMenuItem petSkinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSeerFiddlerWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideSkillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hidePetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showEditFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel loginUserIdToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem muteGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fightMapBossToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batteryDormantSwitchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTcpCaptureFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenBuffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buleBuffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purpleBuffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goldBuffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buleBuffPlusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purpleBuffPlusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goldBuffPlusToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel fireBuffTimeCountToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem lowerHpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoChargeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableVipAutoChargeToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lowerHpStatusToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem showFightNoteFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel batteryStatusToolStripStatusLabel;
    }
}

