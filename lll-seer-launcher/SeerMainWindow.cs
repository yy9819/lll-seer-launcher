using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using lll_seer_launcher.core;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Forms;
using mshtml;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Fiddler;

namespace lll_seer_launcher
{
    public partial class seerMainWindow : Form
    {
        private HookControl[] hook;
        private IntPtr sendFunctionPtr;
        private IntPtr recvFunctionPtr;
        private SendHandleDelegate sendFunction;
        private SendHandleDelegate recvFunction;
        private MessageEncryptDecryptController messageEncryptControl;
        private HeadInfo sendPkgInfo;
        private ChangeSuitForm changeSuitForm = new ChangeSuitForm();
        private EditPetResourceForm editPetResourceForm = new EditPetResourceForm();
        private FightMapBossForm fightMapBossForm = new FightMapBossForm();
        private SeerTcpCaptureForm seerTcpCaptureForm = new SeerTcpCaptureForm();
        private FightNoteForm fightNoteForm = new FightNoteForm();
        private PetBagForm petBagForm = new PetBagForm();
        private PetStorageForm petStorageForm = new PetStorageForm();
        private ReleaseNotesForm releaseNotesForm = new ReleaseNotesForm();
        private string iniFilePath = Directory.GetCurrentDirectory() + "\\bin\\ini\\";
        private List<Thread> threads = new List<Thread>();
        //private FiddlerController fiddlerController = new FiddlerController();
        //private Process fiddlerProcess;
        public seerMainWindow()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            GlobalVariable.mainForm = this;
            AutoClickScriptController.Init();
        }

        private void SeerMainWindow_Load(object sender, EventArgs e)
        {
            this.InitIniFile();
            this.threads.Add(new Thread(ShowNotices));
            this.threads.Add(new Thread(GetUsedMemorySize));
            foreach (var thread in threads) thread.Start();
            this.messageEncryptControl = new MessageEncryptDecryptController();
            this.sendFunction = SendHandle;
            this.sendFunctionPtr = Marshal.GetFunctionPointerForDelegate(sendFunction);
            this.recvFunction = RecvHandle;
            this.recvFunctionPtr = Marshal.GetFunctionPointerForDelegate(recvFunction);
            this.hook = new HookControl[2] { new HookControl() , new HookControl() };
            this.hook[0].Install("WS2_32.DLL", "send", sendFunctionPtr);
            this.hook[1].Install("WS2_32.DLL", "recv", recvFunctionPtr);
            this.Show();
        }
        private void InitIniFile()
        {
            if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
            string filePath = iniFilePath + "config.ini";
            if (!File.Exists(filePath)) File.Create(filePath).Close();
            IniFile iniFile = new IniFile(filePath);
            //精灵透明
            string result = iniFile.Read("config", "transparentPet");
            if (result == null  || (result != "0" && result != "1"))
            {
                iniFile.Write("config", "transparentPet", "1");
                this.hidePetToolStripMenuItem.Checked = true;
            }
            else
            {
                this.hidePetToolStripMenuItem.Checked = result == "1";
            }
            //技能透明
            result = iniFile.Read("config", "transparentSkill");
            if (result == null  || (result != "0" && result != "1"))
            {
                iniFile.Write("config", "transparentSkill", "1");
                this.hideSkillToolStripMenuItem.Checked = true;
            }
            else
            {
                this.hideSkillToolStripMenuItem.Checked = result == "1";
            }
            //游戏静音
            result = iniFile.Read("config", "muteGame");
            if (result == null  || (result != "0" && result != "1"))
            {
                iniFile.Write("config", "muteGame", "1");
                this.muteGameToolStripMenuItem.Checked = true;
            }
            else
            {
                this.muteGameToolStripMenuItem.Checked  = result == "1";
            }
            FormController.SetVolume(this.muteGameToolStripMenuItem.Checked ? 0 : 10);
            //关闭电池
            result = iniFile.Read("config", "batteryDormantSwitch");
            if (result == null  || (result != "0" && result != "1"))
            {
                iniFile.Write("config", "batteryDormantSwitch", "0");
                this.batteryDormantSwitchToolStripMenuItem.Checked = GlobalVariable.gameConfigFlag.batterySwitch = true;
            }
            else
            {
                this.batteryDormantSwitchToolStripMenuItem.Checked  = GlobalVariable.gameConfigFlag.batterySwitch = result == "0";
            }
            //非VIP自动回血
            result = iniFile.Read("config", "autoCharge");
            if (result == null  || (result != "0" && result != "1"))
            {
                iniFile.Write("config", "autoCharge", "1");
                this.autoChargeToolStripMenuItem.Checked = true;
            }
            else
            {
                this.autoChargeToolStripMenuItem.Checked  = result == "1";
            }
            GlobalVariable.gameConfigFlag.autoChargeFlag = this.autoChargeToolStripMenuItem.Checked;
            //关闭VIP自动回血
            result = iniFile.Read("config", "disableVipAutoCharge");
            if (result == null  || (result != "0" && result != "1"))
            {
                iniFile.Write("config", "disableVipAutoCharge", "1");
                this.disableVipAutoChargeToolStripMenuItem.Checked = true;
            }
            else
            {
                this.disableVipAutoChargeToolStripMenuItem.Checked  = result == "1";
            }
            GlobalVariable.gameConfigFlag.disableVipAutoChargeFlag = this.disableVipAutoChargeToolStripMenuItem.Checked;

            result = iniFile.Read("config", "autoClick");
            if (result == null  || (result != "0" && result != "1"))
            {
                iniFile.Write("config", "autoClick", "1");
                this.autoClickToolStripMenuItem.Checked = true;
            }
            else
            {
                this.autoClickToolStripMenuItem.Checked  = result == "1";
            }
            GlobalVariable.autoClick = this.autoClickToolStripMenuItem.Checked;
            this.threads.Add(new Thread(AutoClick));
            //GlobalVariable.gameConfigFlag.shouldDisableRecv = this.testToolStripMenuItem.Checked;
        }

        private void SeerWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
        private void SeerWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            GlobalVariable.loginUserInfo = new UserInfo();
            GlobalVariable.analyzeRecvDataController.RemoveAllEventListener();
            if (GlobalVariable.isLogin) GlobalVariable.isLogin = false;
            if(GlobalVariable.isLoginSend) GlobalVariable.isLoginSend = false;
            this.messageEncryptControl.SetKey("!crAckmE4nOthIng:-)");
            this.messageEncryptControl.InitKey();
        }
        private void SeerWebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.seerWebBrowser.IsWebBrowserContextMenuEnabled = false;
        }
        private void SeerWebBrowser_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Keys pressedKey = e.KeyCode;
            if (pressedKey == Keys.F5)
            {
                this.seerWebBrowser.Refresh();
                //this.seerWebBrowser.Navigate("https://seer.61.com/play.shtml?micro=1");
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SendHandleDelegate(int s, IntPtr dataPointer, int dataSize, int type);
        private int SendHandle(int s, IntPtr dataPointer, int dataSize, int type)
        {
            byte[] bytes = new byte[dataSize];
            Marshal.Copy(dataPointer, bytes, 0, dataSize);
            if(s == GlobalVariable.gameSocket && GlobalVariable.isLogin)
            {
                byte[] decryptBytes = this.messageEncryptControl.Decrypt(bytes);
                //Console.WriteLine(BitConverter.ToString(decryptBytes));
                byte[] encryptBytes = ByteConverter.TakeBytes(decryptBytes, 0, decryptBytes.Length - 1);
                this.sendPkgInfo = this.messageEncryptControl.GetHeadInfo(encryptBytes);
                new Task(() =>
                {
                    GlobalVariable.analyzeSendDataController.RunAnalyzeSendDataMethod(this.sendPkgInfo);
                    this.InsertTCPData(sendPkgInfo, true);
                }).Start();
                this.sendPkgInfo = this.messageEncryptControl.PackHeadInfo(this.sendPkgInfo);
                bytes = this.sendPkgInfo.encryptData;
            }


            dataPointer = ByteConverter.GetBytesIntPtr(bytes);
            dataSize  = bytes.Length;
            this.hook[0].Suspend();
            HookControl.send(s, dataPointer, dataSize, type);
            this.hook[0].Continue();
            if (!GlobalVariable.isLoginSend)
            {
                GlobalVariable.gameSocket = s;
                byte[] decrptByte = this.messageEncryptControl.Decrypt(bytes);
                if (decrptByte.Length > 8 && ByteConverter.BytesTo10(ByteConverter.TakeBytes(decrptByte, 5, 4)) == CmdId.LOGIN_IN)
                {
                    GlobalVariable.isLoginSend = true;
                }
            }
            return dataSize;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int RecvHandleDelegate(int s, IntPtr dataPointer, int dataSize, int type);
        private int RecvHandle(int s, IntPtr dataPointer, int dataSize, int type)
        {
            this.hook[1].Suspend();
            int ret = HookControl.recv(s, dataPointer, dataSize, type);
            this.hook[1].Continue();
            if(ret == 0 || ret == -1) return 0;
            if (GlobalVariable.gameSocket == s && GlobalVariable.isLoginSend)this.messageEncryptControl.LoadBasicMessage(dataPointer, ret);
            return GlobalVariable.gameConfigFlag.disableRecv ? 0 : ret;
        }

        private void SeerMainWindow_Destroyed(object sender, EventArgs e)
        {
            if (!this.releaseNotesForm.IsDisposed) this.releaseNotesForm.Dispose();
            for (int i = 0 ; i > this.hook.Length - 1; i++)
            {
                if(this.hook[i] != null) this.hook[i].Uninstall();
            }
            GlobalVariable.stopThread = true;
            foreach (var thread in threads) thread.Abort();
            //this.fiddlerProcess.Dispose();
            core.Utils.Logger.Log("CloseProgram", "关闭登录器。");
        }

        private void gameReloadMenu_Click(object sender, EventArgs e)
        {
            this.seerWebBrowser.Refresh();
            //this.seerWebBrowser.Navigate("https://seer.61.com/play.shtml?micro=1");
        }



        private void changSuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.changeSuitForm.Hide();
            this.changeSuitForm.Show();
            if(GlobalVariable.isLogin)this.changeSuitForm.InitGroupBoxs();
        }

        private void openSeerFiddlerWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormController.SendMessageToSeerFiddler("1");
        }

        private void hideSkillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.hideSkillToolStripMenuItem.Checked = !this.hideSkillToolStripMenuItem.Checked;
            new IniFile(this.iniFilePath + "config.ini").Write("config", "transparentSkill", this.hideSkillToolStripMenuItem.Checked ? "1" :"0");
            FormController.SendMessageToSeerFiddler("2");
        }

        private void showEditFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.editPetResourceForm.Hide();
            this.editPetResourceForm.Show();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否确认执行此操作?", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                FormController.SendMessageToSeerFiddler("4");
                MessageBox.Show("已重置，请重启登录器~");
                this.Dispose();
            }
        }

        private void hidePetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.hidePetToolStripMenuItem.Checked = !this.hidePetToolStripMenuItem.Checked;
            new IniFile(this.iniFilePath + "config.ini").Write("config", "transparentPet", this.hidePetToolStripMenuItem.Checked ? "1" : "0");
            FormController.SendMessageToSeerFiddler("2");
        }

        private void muteGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.muteGameToolStripMenuItem.Checked  = !this.muteGameToolStripMenuItem.Checked;
            new IniFile(this.iniFilePath + "config.ini").Write("config", "muteGame", this.muteGameToolStripMenuItem.Checked ? "1" : "0");
            FormController.SetVolume(this.muteGameToolStripMenuItem.Checked ? 0 : 10);
        }

        private void fightMapBossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fightMapBossForm.Hide();
            this.fightMapBossForm.Show();
        }

        private void batteryDormantSwitchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.batteryDormantSwitchToolStripMenuItem.Checked = GlobalVariable.gameConfigFlag.batterySwitch = !this.batteryDormantSwitchToolStripMenuItem.Checked;
            new IniFile(this.iniFilePath + "config.ini").Write("config", "batteryDormantSwitch", this.batteryDormantSwitchToolStripMenuItem.Checked ? "0" : "1");
            FormController.SendMessageToSeerFiddler("2");
            if (GlobalVariable.isLogin)
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.BATTERY_DORMANT_SWITCH, new int[1] { this.batteryDormantSwitchToolStripMenuItem.Checked ? 0 : 1 });
            }
        }

        private void openTcpCaptureFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.seerTcpCaptureForm.Hide();
            this.seerTcpCaptureForm.Show();
        }

        public void InsertTCPData(HeadInfo info , bool isSend)
        {
            this.seerTcpCaptureForm.InsertData(info,isSend);
        }

        private void buleBuffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyFireBuff(2);
        }
        private void CopyFireBuff(int type)
        {
            GlobalVariable.fireBuffCopyObj.copyFireBuffType = type;
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.LIST_MAP_PLAYER,new int[0]);
        }

        private void buleBuffPlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyFireBuff(7);
        }

        private void purpleBuffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyFireBuff(3);
        }

        private void purpleBuffPlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyFireBuff(8);
        }

        private void goldBuffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyFireBuff(4);
        }

        private void goldBuffPlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyFireBuff(9);
        }

        private void greenBuffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 如果正在借火，则防止反复点击
            if (GlobalVariable.fireBuffCopyObj.copyGreenBuff[0]) return;
            new Thread(() =>
            {
                //查看当前绿火玩家数是否大于0
                if (GlobalVariable.fireBuffCopyObj.greenFireBuffDic.Count > 0)
                {
                    // 将借绿火状态设为真
                    GlobalVariable.fireBuffCopyObj.copyGreenBuff[0] = true;
                    // 遍历当前绿火玩家的信息
                    foreach (var item in GlobalVariable.fireBuffCopyObj.greenFireBuffDic.ToList())
                    {
                        // 设置正在查询绿火玩家信息
                        GlobalVariable.fireBuffCopyObj.copyGreenBuff[1] = true;
                        // 设置正在查询的绿火玩家的ID
                        GlobalVariable.fireBuffCopyObj.copyGreenBuffUserId = item.Key;
                        // 发送查询封包
                        GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.GET_SIM_USERINFO, new int[1] { item.Key });
                        // 判断是否查询成功
                        while (GlobalVariable.fireBuffCopyObj.copyGreenBuff[1] || GlobalVariable.stopThread)
                        {
                            Thread.Sleep(100);
                        }
                        // 判断查询玩家是否携带绿火
                        if (GlobalVariable.fireBuffCopyObj.copyGreenBuff[2])
                        {
                            // 携带绿火发送蹭火封包
                            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.FIRE_ACT_COPY, new int[1] { item.Key });
                            GlobalVariable.fireBuffCopyObj.copyGreenBuff[0] = GlobalVariable.fireBuffCopyObj.copyGreenBuff[1] = GlobalVariable.fireBuffCopyObj.copyGreenBuff[2] = false;
                            break;
                        }
                        else
                        {
                            // 未携带则移除该玩家
                            GlobalVariable.fireBuffCopyObj.greenFireBuffDic.Remove(item.Key);
                        }
                    }
                    // 
                    if (GlobalVariable.fireBuffCopyObj.copyGreenBuff[0])
                    {
                        GlobalVariable.fireBuffCopyObj.copyGreenBuff[1] = true;
                        GlobalVariable.fireBuffCopyObj.copyGreenBuffUserId = GlobalVariable.fireBuffCopyObj.greenFireUserId;
                        GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.GET_SIM_USERINFO, new int[1] { GlobalVariable.fireBuffCopyObj.greenFireUserId });
                        while (GlobalVariable.fireBuffCopyObj.copyGreenBuff[1] || GlobalVariable.stopThread)
                        {
                            Thread.Sleep(100);
                        }
                        if (GlobalVariable.fireBuffCopyObj.copyGreenBuff[2])
                        {
                            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.FIRE_ACT_COPY, new int[1] { GlobalVariable.fireBuffCopyObj.greenFireUserId });
                            GlobalVariable.fireBuffCopyObj.copyGreenBuff[0] = GlobalVariable.fireBuffCopyObj.copyGreenBuff[1] = GlobalVariable.fireBuffCopyObj.copyGreenBuff[2] = false;
                        }
                    }
                }
            }).Start();
        }

        public void lowerHpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.isLogin || GlobalVariable.gameConfigFlag.inFight) return;
            core.Utils.Logger.Log("LowerHP","用户按下了压血");
            if (!this.disableVipAutoChargeToolStripMenuItem.Checked) disableVipAutoChargeToolStripMenuItem_Click(sender, e);
            if (this.autoChargeToolStripMenuItem.Checked) autoChargeToolStripMenuItem_Click(sender, e);
            GlobalVariable.gameConfigFlag.lowerHpFlag = true;
            this.SetLowerHpStatus("获取背包精灵中...");
            GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.GET_PET_INFO_BY_ONCE, new int[0]);
        }

        private void autoChargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.autoChargeToolStripMenuItem.Checked  = !this.autoChargeToolStripMenuItem.Checked;
            new IniFile(this.iniFilePath + "config.ini").Write("config", "autoCharge", this.autoChargeToolStripMenuItem.Checked ? "1" : "0");
            GlobalVariable.gameConfigFlag.autoChargeFlag = this.autoChargeToolStripMenuItem.Checked;
        }

        private void disableVipAutoChargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.disableVipAutoChargeToolStripMenuItem.Checked  = !this.disableVipAutoChargeToolStripMenuItem.Checked;
            new IniFile(this.iniFilePath + "config.ini").Write("config", "disableVipAutoCharge", this.disableVipAutoChargeToolStripMenuItem.Checked ? "1" : "0");
            GlobalVariable.gameConfigFlag.disableVipAutoChargeFlag = this.disableVipAutoChargeToolStripMenuItem.Checked;
            if (GlobalVariable.isLogin)
            {
                GlobalVariable.sendDataController.SendDataByCmdIdAndIntList(CmdId.COMMAND_42019,
                    new int[2] { 22439, GlobalVariable.gameConfigFlag.disableVipAutoChargeFlag ? 0 : 1 });
            }
        }


        private delegate void SetToolBarStatusCallback();
        public void SetLowerHpStatus(string statusText)
        {
            if (this.IsDisposed) return;
            SetToolBarStatusCallback callback = delegate
            {
                this.lowerHpStatusToolStripStatusLabel.Text = statusText;
            };
            this.Invoke(callback);
        }
        private delegate void FightNoteFormCallback();
        private void showFightNoteFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fightNoteForm.Hide();
            this.fightNoteForm.Show();
            this.SetFightFormLoaction();
        }
        private void SetFightFormLoaction()
        {
            this.fightNoteForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
        }
        public void SetPetFightNote(Dictionary<string,FightPetInfo> fightPlayers)
        {
            FightNoteFormCallback callback = delegate ()
            {
                if (!this.fightNoteForm.Visible) this.fightNoteForm.Show();
                this.fightNoteForm.SetPetFightNote(fightPlayers);

            };
            this.Invoke(callback);
        }
        public void OnUseSkill(Dictionary<string, AttackValueInfo> fightPlayers)
        {
 
            this.fightNoteForm.OnUseSkill(fightPlayers);

        }

        public void OnChangePet(ChangePetInfo changePetInfo)
        {
            this.fightNoteForm.OnChangePet(changePetInfo);
        }
        public void SetBatteryStatus(string status)
        {
            if (this.IsDisposed) return;
            SetToolBarStatusCallback callback = delegate ()
            {
                this.batteryStatusToolStripStatusLabel.Text = status;
            };
            this.Invoke(callback);
        }
        public void StopLoopUseSkill()
        {
            this.fightNoteForm.StopLoopUseSkill();
        }
        public void InitFightNote()
        {
            this.fightNoteForm.InitFightNote();
        }

        private void clearIECacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            DialogResult result = MessageBox.Show("是否确认执行此操作?", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                FormController.ClearIECache();
            }
        }

        private void clearCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否确认执行此操作?", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                if(Directory.Exists(Directory.GetCurrentDirectory() + "/cache"))
                {
                    try
                    {
                        Directory.Delete(Directory.GetCurrentDirectory() + "/cache", true);
                    }
                    catch { }
                }
            }
        }

        public void openPetStorageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.petStorageForm.Hide();
            this.petStorageForm.Show();
        }

        public void showPetBagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.petBagForm.Hide();
            this.petBagForm.Show();
        }

        private void releaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.releaseNotesForm.Hide();
            this.releaseNotesForm.Show();
        }

        private void autoClickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.autoClickToolStripMenuItem.Checked = GlobalVariable.autoClick = !this.autoClickToolStripMenuItem.Checked;
            new IniFile(this.iniFilePath + "config.ini").Write("config", "autoClick", this.autoClickToolStripMenuItem.Checked ? "1" : "0");
        }

        private void getScreenShotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoClickScriptController.GetCupture();
        }
    }
}
