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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(seerMainWindow));
            this.seerWebBrowser = new System.Windows.Forms.WebBrowser();
            this.mainFormStatusStrip = new System.Windows.Forms.StatusStrip();
            this.launcherNameToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.usedRamToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.loginUserIdToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fireBuffTimeCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lowerHpStatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.batteryStatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainFormMenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.gameReloadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.muteGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batteryDormantSwitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTcpCaptureFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearIECacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seerUtilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changSuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fightMapBossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFightNoteFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPetBagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPetStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getScreenShotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.fightLoadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startFightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showScriptComponetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.framelessGroupBox = new System.Windows.Forms.GroupBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.stopScriptsButton = new System.Windows.Forms.Button();
            this.importScriptButton = new System.Windows.Forms.Button();
            this.reloadScriprtsButton = new System.Windows.Forms.Button();
            this.runScriptsButton = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.scriptDescTextBox = new System.Windows.Forms.TextBox();
            this.scriptTabControl = new System.Windows.Forms.TabControl();
            this.sendDataScriptTabPage = new System.Windows.Forms.TabPage();
            this.sendDataScriptCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.scriptContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainFormStatusStrip.SuspendLayout();
            this.mainFormMenuStrip.SuspendLayout();
            this.framelessGroupBox.SuspendLayout();
            this.scriptTabControl.SuspendLayout();
            this.sendDataScriptTabPage.SuspendLayout();
            this.scriptContextMenuStrip.SuspendLayout();
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
            this.seerWebBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.SeerWebBrowser_DocumentCompleted);
            this.seerWebBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.SeerWebBrowser_Navigated);
            this.seerWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.SeerWebBrowser_Navigating);
            this.seerWebBrowser.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.SeerWebBrowser_KeyDown);
            // 
            // mainFormStatusStrip
            // 
            this.mainFormStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launcherNameToolStripStatusLabel,
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
            // launcherNameToolStripStatusLabel
            // 
            this.launcherNameToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.launcherNameToolStripStatusLabel.Name = "launcherNameToolStripStatusLabel";
            this.launcherNameToolStripStatusLabel.Size = new System.Drawing.Size(114, 21);
            this.launcherNameToolStripStatusLabel.Text = "交流群:306605758";
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
            this.fireBuffTimeCountToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.fireBuffTimeCountToolStripStatusLabel.Name = "fireBuffTimeCountToolStripStatusLabel";
            this.fireBuffTimeCountToolStripStatusLabel.Size = new System.Drawing.Size(4, 21);
            // 
            // lowerHpStatusToolStripStatusLabel
            // 
            this.lowerHpStatusToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lowerHpStatusToolStripStatusLabel.Name = "lowerHpStatusToolStripStatusLabel";
            this.lowerHpStatusToolStripStatusLabel.Size = new System.Drawing.Size(4, 21);
            // 
            // batteryStatusToolStripStatusLabel
            // 
            this.batteryStatusToolStripStatusLabel.Name = "batteryStatusToolStripStatusLabel";
            this.batteryStatusToolStripStatusLabel.Size = new System.Drawing.Size(0, 21);
            // 
            // mainFormMenuStrip
            // 
            this.mainFormMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSetting,
            this.seerUtilsToolStripMenuItem,
            this.petSkinToolStripMenuItem,
            this.fightToolStripMenuItem,
            this.scriptToolStripMenuItem,
            this.aboutMeToolStripMenuItem});
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
            this.autoClickToolStripMenuItem,
            this.openTcpCaptureFormToolStripMenuItem,
            this.clearIECacheToolStripMenuItem,
            this.clearCacheToolStripMenuItem});
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
            // autoClickToolStripMenuItem
            // 
            this.autoClickToolStripMenuItem.Name = "autoClickToolStripMenuItem";
            this.autoClickToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.autoClickToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.autoClickToolStripMenuItem.Text = "自动确认";
            this.autoClickToolStripMenuItem.Click += new System.EventHandler(this.autoClickToolStripMenuItem_Click);
            // 
            // openTcpCaptureFormToolStripMenuItem
            // 
            this.openTcpCaptureFormToolStripMenuItem.Name = "openTcpCaptureFormToolStripMenuItem";
            this.openTcpCaptureFormToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.openTcpCaptureFormToolStripMenuItem.Text = "打开赛尔号command捕获窗口";
            this.openTcpCaptureFormToolStripMenuItem.Click += new System.EventHandler(this.openTcpCaptureFormToolStripMenuItem_Click);
            // 
            // clearIECacheToolStripMenuItem
            // 
            this.clearIECacheToolStripMenuItem.Name = "clearIECacheToolStripMenuItem";
            this.clearIECacheToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.clearIECacheToolStripMenuItem.Text = "清理IE缓存";
            this.clearIECacheToolStripMenuItem.Click += new System.EventHandler(this.clearIECacheToolStripMenuItem_Click);
            // 
            // clearCacheToolStripMenuItem
            // 
            this.clearCacheToolStripMenuItem.Name = "clearCacheToolStripMenuItem";
            this.clearCacheToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.clearCacheToolStripMenuItem.Text = "清理登录器缓存";
            this.clearCacheToolStripMenuItem.Click += new System.EventHandler(this.clearCacheToolStripMenuItem_Click);
            // 
            // seerUtilsToolStripMenuItem
            // 
            this.seerUtilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changSuitToolStripMenuItem,
            this.fightMapBossToolStripMenuItem,
            this.showFightNoteFormToolStripMenuItem,
            this.showPetBagToolStripMenuItem,
            this.openPetStorageToolStripMenuItem,
            this.getScreenShotToolStripMenuItem});
            this.seerUtilsToolStripMenuItem.Name = "seerUtilsToolStripMenuItem";
            this.seerUtilsToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.seerUtilsToolStripMenuItem.Text = "小助手";
            // 
            // changSuitToolStripMenuItem
            // 
            this.changSuitToolStripMenuItem.Name = "changSuitToolStripMenuItem";
            this.changSuitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.changSuitToolStripMenuItem.Text = "更换装备与称号";
            this.changSuitToolStripMenuItem.Click += new System.EventHandler(this.changSuitToolStripMenuItem_Click);
            // 
            // fightMapBossToolStripMenuItem
            // 
            this.fightMapBossToolStripMenuItem.Name = "fightMapBossToolStripMenuItem";
            this.fightMapBossToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.fightMapBossToolStripMenuItem.Text = "对战地图怪";
            this.fightMapBossToolStripMenuItem.Click += new System.EventHandler(this.fightMapBossToolStripMenuItem_Click);
            // 
            // showFightNoteFormToolStripMenuItem
            // 
            this.showFightNoteFormToolStripMenuItem.Name = "showFightNoteFormToolStripMenuItem";
            this.showFightNoteFormToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.showFightNoteFormToolStripMenuItem.Text = "对战助手";
            this.showFightNoteFormToolStripMenuItem.Click += new System.EventHandler(this.showFightNoteFormToolStripMenuItem_Click);
            // 
            // showPetBagToolStripMenuItem
            // 
            this.showPetBagToolStripMenuItem.Name = "showPetBagToolStripMenuItem";
            this.showPetBagToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.showPetBagToolStripMenuItem.Text = "精灵背包";
            this.showPetBagToolStripMenuItem.Click += new System.EventHandler(this.showPetBagToolStripMenuItem_Click);
            // 
            // openPetStorageToolStripMenuItem
            // 
            this.openPetStorageToolStripMenuItem.Name = "openPetStorageToolStripMenuItem";
            this.openPetStorageToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openPetStorageToolStripMenuItem.Text = "精灵仓库";
            this.openPetStorageToolStripMenuItem.Click += new System.EventHandler(this.openPetStorageToolStripMenuItem_Click);
            // 
            // getScreenShotToolStripMenuItem
            // 
            this.getScreenShotToolStripMenuItem.Name = "getScreenShotToolStripMenuItem";
            this.getScreenShotToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.getScreenShotToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.getScreenShotToolStripMenuItem.Text = "截图";
            this.getScreenShotToolStripMenuItem.Click += new System.EventHandler(this.getScreenShotToolStripMenuItem_Click);
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
            this.hideSkillToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.hideSkillToolStripMenuItem.Text = "技能透明";
            this.hideSkillToolStripMenuItem.Click += new System.EventHandler(this.hideSkillToolStripMenuItem_Click);
            // 
            // hidePetToolStripMenuItem
            // 
            this.hidePetToolStripMenuItem.Name = "hidePetToolStripMenuItem";
            this.hidePetToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.hidePetToolStripMenuItem.Text = "精灵透明";
            this.hidePetToolStripMenuItem.Click += new System.EventHandler(this.hidePetToolStripMenuItem_Click);
            // 
            // showEditFormToolStripMenuItem
            // 
            this.showEditFormToolStripMenuItem.Name = "showEditFormToolStripMenuItem";
            this.showEditFormToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showEditFormToolStripMenuItem.Text = "精灵换肤编辑窗口";
            this.showEditFormToolStripMenuItem.Click += new System.EventHandler(this.showEditFormToolStripMenuItem_Click);
            // 
            // openSeerFiddlerWindowToolStripMenuItem
            // 
            this.openSeerFiddlerWindowToolStripMenuItem.Name = "openSeerFiddlerWindowToolStripMenuItem";
            this.openSeerFiddlerWindowToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.openSeerFiddlerWindowToolStripMenuItem.Text = "资源链接捕获窗口";
            this.openSeerFiddlerWindowToolStripMenuItem.Click += new System.EventHandler(this.openSeerFiddlerWindowToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.reloadToolStripMenuItem.Text = "修复登录器网络";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // fightToolStripMenuItem
            // 
            this.fightToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyFireToolStripMenuItem,
            this.lowerHpToolStripMenuItem,
            this.autoChargeToolStripMenuItem,
            this.disableVipAutoChargeToolStripMenuItem,
            this.fightLoadingToolStripMenuItem});
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
            this.copyFireToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
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
            this.lowerHpToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.lowerHpToolStripMenuItem.Text = "压血";
            this.lowerHpToolStripMenuItem.Click += new System.EventHandler(this.lowerHpToolStripMenuItem_Click);
            // 
            // autoChargeToolStripMenuItem
            // 
            this.autoChargeToolStripMenuItem.Name = "autoChargeToolStripMenuItem";
            this.autoChargeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.autoChargeToolStripMenuItem.Text = "自动回血(非VIP)";
            this.autoChargeToolStripMenuItem.Click += new System.EventHandler(this.autoChargeToolStripMenuItem_Click);
            // 
            // disableVipAutoChargeToolStripMenuItem
            // 
            this.disableVipAutoChargeToolStripMenuItem.Name = "disableVipAutoChargeToolStripMenuItem";
            this.disableVipAutoChargeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.disableVipAutoChargeToolStripMenuItem.Text = "关闭VIP自动回血";
            this.disableVipAutoChargeToolStripMenuItem.Click += new System.EventHandler(this.disableVipAutoChargeToolStripMenuItem_Click);
            // 
            // fightLoadingToolStripMenuItem
            // 
            this.fightLoadingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startFightToolStripMenuItem});
            this.fightLoadingToolStripMenuItem.Name = "fightLoadingToolStripMenuItem";
            this.fightLoadingToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.fightLoadingToolStripMenuItem.Text = "战斗加载界面卡死";
            // 
            // startFightToolStripMenuItem
            // 
            this.startFightToolStripMenuItem.Name = "startFightToolStripMenuItem";
            this.startFightToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.startFightToolStripMenuItem.Text = "直接进入战斗(谨慎使用)";
            this.startFightToolStripMenuItem.Click += new System.EventHandler(this.startFightToolStripMenuItem_Click_1);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showScriptComponetToolStripMenuItem,
            this.editScriptToolStripMenuItem});
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            this.scriptToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.scriptToolStripMenuItem.Text = "脚本";
            // 
            // showScriptComponetToolStripMenuItem
            // 
            this.showScriptComponetToolStripMenuItem.Name = "showScriptComponetToolStripMenuItem";
            this.showScriptComponetToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.showScriptComponetToolStripMenuItem.Text = "运行脚本";
            this.showScriptComponetToolStripMenuItem.Click += new System.EventHandler(this.showScriptComponetToolStripMenuItem_Click);
            // 
            // editScriptToolStripMenuItem
            // 
            this.editScriptToolStripMenuItem.Name = "editScriptToolStripMenuItem";
            this.editScriptToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.editScriptToolStripMenuItem.Text = "编辑脚本";
            this.editScriptToolStripMenuItem.Click += new System.EventHandler(this.editScriptToolStripMenuItem_Click);
            // 
            // aboutMeToolStripMenuItem
            // 
            this.aboutMeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.releaseNotesToolStripMenuItem});
            this.aboutMeToolStripMenuItem.Name = "aboutMeToolStripMenuItem";
            this.aboutMeToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.aboutMeToolStripMenuItem.Text = "关于";
            // 
            // releaseNotesToolStripMenuItem
            // 
            this.releaseNotesToolStripMenuItem.Name = "releaseNotesToolStripMenuItem";
            this.releaseNotesToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.releaseNotesToolStripMenuItem.Text = "更新日志";
            this.releaseNotesToolStripMenuItem.Click += new System.EventHandler(this.releaseNotesToolStripMenuItem_Click);
            // 
            // framelessGroupBox
            // 
            this.framelessGroupBox.Controls.Add(this.clearButton);
            this.framelessGroupBox.Controls.Add(this.stopScriptsButton);
            this.framelessGroupBox.Controls.Add(this.importScriptButton);
            this.framelessGroupBox.Controls.Add(this.reloadScriprtsButton);
            this.framelessGroupBox.Controls.Add(this.runScriptsButton);
            this.framelessGroupBox.Controls.Add(this.logTextBox);
            this.framelessGroupBox.Controls.Add(this.scriptDescTextBox);
            this.framelessGroupBox.Controls.Add(this.scriptTabControl);
            this.framelessGroupBox.Location = new System.Drawing.Point(0, 25);
            this.framelessGroupBox.Name = "framelessGroupBox";
            this.framelessGroupBox.Size = new System.Drawing.Size(960, 560);
            this.framelessGroupBox.TabIndex = 3;
            this.framelessGroupBox.TabStop = false;
            this.framelessGroupBox.Text = "脚本";
            this.framelessGroupBox.Visible = false;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(878, 531);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(50, 21);
            this.clearButton.TabIndex = 22;
            this.clearButton.Text = "清空";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // stopScriptsButton
            // 
            this.stopScriptsButton.Enabled = false;
            this.stopScriptsButton.Location = new System.Drawing.Point(313, 520);
            this.stopScriptsButton.Name = "stopScriptsButton";
            this.stopScriptsButton.Size = new System.Drawing.Size(80, 35);
            this.stopScriptsButton.TabIndex = 21;
            this.stopScriptsButton.Text = "停止执行";
            this.stopScriptsButton.UseVisualStyleBackColor = true;
            this.stopScriptsButton.Click += new System.EventHandler(this.stopScriptsButton_Click);
            // 
            // importScriptButton
            // 
            this.importScriptButton.Location = new System.Drawing.Point(98, 519);
            this.importScriptButton.Name = "importScriptButton";
            this.importScriptButton.Size = new System.Drawing.Size(80, 35);
            this.importScriptButton.TabIndex = 20;
            this.importScriptButton.Text = "导入脚本";
            this.importScriptButton.UseVisualStyleBackColor = true;
            this.importScriptButton.Click += new System.EventHandler(this.importScriptButton_Click);
            // 
            // reloadScriprtsButton
            // 
            this.reloadScriprtsButton.Location = new System.Drawing.Point(12, 519);
            this.reloadScriprtsButton.Name = "reloadScriprtsButton";
            this.reloadScriprtsButton.Size = new System.Drawing.Size(80, 35);
            this.reloadScriprtsButton.TabIndex = 19;
            this.reloadScriprtsButton.Text = "刷新脚本";
            this.reloadScriprtsButton.UseVisualStyleBackColor = true;
            this.reloadScriprtsButton.Click += new System.EventHandler(this.reloadScriprtsButton_Click);
            // 
            // runScriptsButton
            // 
            this.runScriptsButton.Location = new System.Drawing.Point(399, 519);
            this.runScriptsButton.Name = "runScriptsButton";
            this.runScriptsButton.Size = new System.Drawing.Size(80, 35);
            this.runScriptsButton.TabIndex = 18;
            this.runScriptsButton.Text = "执行选中";
            this.runScriptsButton.UseVisualStyleBackColor = true;
            this.runScriptsButton.Click += new System.EventHandler(this.runScriptsButton_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(499, 274);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(449, 280);
            this.logTextBox.TabIndex = 17;
            // 
            // scriptDescTextBox
            // 
            this.scriptDescTextBox.Location = new System.Drawing.Point(499, 40);
            this.scriptDescTextBox.Multiline = true;
            this.scriptDescTextBox.Name = "scriptDescTextBox";
            this.scriptDescTextBox.ReadOnly = true;
            this.scriptDescTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scriptDescTextBox.Size = new System.Drawing.Size(449, 228);
            this.scriptDescTextBox.TabIndex = 16;
            // 
            // scriptTabControl
            // 
            this.scriptTabControl.Controls.Add(this.sendDataScriptTabPage);
            this.scriptTabControl.Location = new System.Drawing.Point(10, 18);
            this.scriptTabControl.Name = "scriptTabControl";
            this.scriptTabControl.SelectedIndex = 0;
            this.scriptTabControl.Size = new System.Drawing.Size(473, 502);
            this.scriptTabControl.TabIndex = 0;
            // 
            // sendDataScriptTabPage
            // 
            this.sendDataScriptTabPage.Controls.Add(this.sendDataScriptCheckedListBox);
            this.sendDataScriptTabPage.Location = new System.Drawing.Point(4, 22);
            this.sendDataScriptTabPage.Name = "sendDataScriptTabPage";
            this.sendDataScriptTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.sendDataScriptTabPage.Size = new System.Drawing.Size(465, 476);
            this.sendDataScriptTabPage.TabIndex = 0;
            this.sendDataScriptTabPage.Text = "自动发包脚本";
            this.sendDataScriptTabPage.UseVisualStyleBackColor = true;
            // 
            // sendDataScriptCheckedListBox
            // 
            this.sendDataScriptCheckedListBox.ContextMenuStrip = this.scriptContextMenuStrip;
            this.sendDataScriptCheckedListBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendDataScriptCheckedListBox.FormattingEnabled = true;
            this.sendDataScriptCheckedListBox.Location = new System.Drawing.Point(3, 6);
            this.sendDataScriptCheckedListBox.Name = "sendDataScriptCheckedListBox";
            this.sendDataScriptCheckedListBox.Size = new System.Drawing.Size(453, 468);
            this.sendDataScriptCheckedListBox.TabIndex = 0;
            this.sendDataScriptCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.sendDataScriptCheckedListBox_SelectedIndexChanged);
            // 
            // scriptContextMenuStrip
            // 
            this.scriptContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteScriptToolStripMenuItem,
            this.reloadScriptsToolStripMenuItem,
            this.importScriptToolStripMenuItem});
            this.scriptContextMenuStrip.Name = "scriptContextMenuStrip";
            this.scriptContextMenuStrip.Size = new System.Drawing.Size(181, 92);
            // 
            // deleteScriptToolStripMenuItem
            // 
            this.deleteScriptToolStripMenuItem.Name = "deleteScriptToolStripMenuItem";
            this.deleteScriptToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteScriptToolStripMenuItem.Text = "删除选中";
            this.deleteScriptToolStripMenuItem.Click += new System.EventHandler(this.deleteScriptToolStripMenuItem_Click);
            // 
            // reloadScriptsToolStripMenuItem
            // 
            this.reloadScriptsToolStripMenuItem.Name = "reloadScriptsToolStripMenuItem";
            this.reloadScriptsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reloadScriptsToolStripMenuItem.Text = "刷新脚本列表";
            this.reloadScriptsToolStripMenuItem.Click += new System.EventHandler(this.reloadScriptsToolStripMenuItem_Click);
            // 
            // importScriptToolStripMenuItem
            // 
            this.importScriptToolStripMenuItem.Name = "importScriptToolStripMenuItem";
            this.importScriptToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importScriptToolStripMenuItem.Text = "导入脚本";
            this.importScriptToolStripMenuItem.Click += new System.EventHandler(this.importScriptToolStripMenuItem_Click);
            // 
            // seerMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 611);
            this.Controls.Add(this.framelessGroupBox);
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
            this.framelessGroupBox.ResumeLayout(false);
            this.framelessGroupBox.PerformLayout();
            this.scriptTabControl.ResumeLayout(false);
            this.sendDataScriptTabPage.ResumeLayout(false);
            this.scriptContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private delegate void SetStatusCallBack();
        private void AutoClick()
        {
            
            while (!GlobalVariable.stopThread)
            {
                if (GlobalVariable.autoClick)
                {
                    IntPtr foregroundWindow = FormController.GetForegroundWindow();
                    if (foregroundWindow != IntPtr.Zero &&  FormController.GetWindow(foregroundWindow, FormController.GW_HWNDNEXT) != IntPtr.Zero)
                    {
                        AutoClickScriptController.AutoClick();
                    }
                }
                Thread.Sleep(100);
            }
        }

        private void GetUsedMemorySize()
        {
            SetStatusCallBack callBack;
            Process currentProcess = Process.GetCurrentProcess();
            while (!GlobalVariable.stopThread)
            {
                currentProcess.Refresh();
                try
                {
                    if (!this.IsDisposed && !this.Disposing)
                    {
                        callBack = delegate ()
                        {
                            this.usedRamToolStripStatusLabel.Text = $"运行内存：{currentProcess.PrivateMemorySize64 / 1024 / 1024}MB";
                            this.loginUserIdToolStripStatusLabel.Text = $"当前登录账号：{GlobalVariable.loginUserInfo.userId}";
                        };
                        this.Invoke(callBack);
                    }
                    if (GlobalVariable.isRunningScript && GlobalVariable.isLogin)
                    {
                        GlobalVariable.isLogin = GetPeerName.CheckSocket(GlobalVariable.gameSocket);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                }
                Thread.Sleep(1000);
            }

        }

        private void ShowNotices()
        {
            SetStatusCallBack callBack;
            while (!GlobalVariable.stopThread)
            {
                foreach(var Notice in GlobalVariable.notices)
                {
                    callBack = delegate ()
                    {
                        this.launcherNameToolStripStatusLabel.Text = Notice.ToString();
                    };
                    this.Invoke((callBack));
                    
                    Thread.Sleep(20000);
                }
            }
        }

        public void StartCountFireBuff(int mins)
        {
            int totalTime = mins * 60;
            SetStatusCallBack callBack;
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
                if (!this.IsDisposed && !this.Disposing)
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
        private System.Windows.Forms.ToolStripMenuItem clearIECacheToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCacheToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel launcherNameToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem openPetStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPetBagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoClickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getScreenShotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fightLoadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startFightToolStripMenuItem;
        private System.Windows.Forms.GroupBox framelessGroupBox;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showScriptComponetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editScriptToolStripMenuItem;
        private System.Windows.Forms.TabControl scriptTabControl;
        private System.Windows.Forms.TabPage sendDataScriptTabPage;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.TextBox scriptDescTextBox;
        private System.Windows.Forms.CheckedListBox sendDataScriptCheckedListBox;
        private System.Windows.Forms.Button importScriptButton;
        private System.Windows.Forms.Button reloadScriprtsButton;
        private System.Windows.Forms.Button runScriptsButton;
        private System.Windows.Forms.Button stopScriptsButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ContextMenuStrip scriptContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadScriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importScriptToolStripMenuItem;
    }
}

