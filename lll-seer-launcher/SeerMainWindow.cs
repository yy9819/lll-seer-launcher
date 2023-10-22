using System;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using lll_seer_launcher.core;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
using mshtml;
using System.IO;
using System.Threading;
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
        private ChangeSuitForm changeSuitForm;
        private EditPetResourceForm editPetResourceForm;
        private string iniFilePath = Directory.GetCurrentDirectory() + "\\bin\\ini\\";
        //private FiddlerController fiddlerController = new FiddlerController();
        //private Process fiddlerProcess;
        public seerMainWindow()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            GlobalVariable.mainForm = this;
        }

        private void SeerMainWindow_Load(object sender, EventArgs e)
        {
            
            //Thread initJsonThread = new Thread(() =>
            //{

            //});
            //initJsonThread.Start();

            Thread getUsedMemory = new Thread(GetUsedMemorySize);
            getUsedMemory.Start();

            this.InitIniFile();

            this.messageEncryptControl = new MessageEncryptDecryptController();
            this.sendFunction = SendHandle;
            this.sendFunctionPtr = Marshal.GetFunctionPointerForDelegate(sendFunction);
            this.recvFunction = RecvHandle;
            this.recvFunctionPtr = Marshal.GetFunctionPointerForDelegate(recvFunction);
            this.hook = new HookControl[2] { new HookControl() , new HookControl() };
            this.hook[0].Install("WS2_32.DLL", "send", sendFunctionPtr);
            this.hook[1].Install("WS2_32.DLL", "recv", recvFunctionPtr);

            this.changeSuitForm = new ChangeSuitForm();
            this.changeSuitForm.Hide();
            this.editPetResourceForm = new EditPetResourceForm();

            this.Show();
        }
        private void InitIniFile()
        {
            if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
            string filePath = iniFilePath + "config.ini";
            if (!File.Exists(filePath)) File.Create(filePath).Close();
            IniFile iniFile = new IniFile(filePath);
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
            iniFile = new IniFile(filePath);
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
        }

        private void SeerWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
        private void SeerWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (GlobalVariable.isLogin) GlobalVariable.isLogin = false;
            if(GlobalVariable.isLoginSend) GlobalVariable.isLoginSend = false;
            this.messageEncryptControl.SetKey("!crAckmE4nOthIng:-)");
            this.messageEncryptControl.InitKey();
        }
        private void SeerWebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {

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
        private void SeerWebBrowser_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 取消新窗口的创建
            e.Cancel = true;
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
                byte[] encryptBytes = ByteConverter.TakeBytes(decryptBytes, 0, decryptBytes.Length - 1);
                this.sendPkgInfo = this.messageEncryptControl.GetHeadInfo(encryptBytes);
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
            if (GlobalVariable.gameSocket == s && GlobalVariable.isLoginSend) this.messageEncryptControl.LoadBasicMessage(dataPointer, ret);
            return ret;
        }

        private void SeerMainWindow_Destroyed(object sender, EventArgs e)
        {
            for (int i = 0 ; i > this.hook.Length - 1; i++)
            {
                if(this.hook[i] != null) this.hook[i].Uninstall();
            }
            GlobalVariable.stopThread = true;
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
            this.changeSuitForm.Show();
            if(GlobalVariable.isLogin)this.changeSuitForm.InitGroupBoxs();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //MainFormController.SendMessage(seerFiddlerIntPtr, MainFormController.WM_SETTEXT, 0, "1");
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
            this.editPetResourceForm.Show();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否确认执行此操作?", "确认框", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                FormController.SendMessageToSeerFiddler("4");
                MessageBox.Show("已重置，即将重启登录器~");
                this.Dispose();
            }
        }

        private void hidePetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.hidePetToolStripMenuItem.Checked = !this.hidePetToolStripMenuItem.Checked;
            new IniFile(this.iniFilePath + "config.ini").Write("config", "transparentPet", this.hidePetToolStripMenuItem.Checked ? "1" : "0");
            FormController.SendMessageToSeerFiddler("2");
        }
    }
}
