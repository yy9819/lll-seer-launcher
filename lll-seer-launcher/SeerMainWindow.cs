using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using lll_seer_launcher.core;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
using mshtml;
using System.Diagnostics;
using System.Threading;

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
        private InitJsonController initJsonControl = new InitJsonController();
        public seerMainWindow()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            Thread initJsonThread = new Thread(() =>
            {
                this.initJsonControl.InitJson();
            });
            initJsonThread.Start();
        }

        private void SeerMainWindow_Load(object sender, EventArgs e)
        {
            Thread getUsedMemory = new Thread(GetUsedMemorySize);
            getUsedMemory.Start();

            this.messageEncryptControl = new MessageEncryptDecryptController();
            this.sendFunction = SendHandle;
            this.sendFunctionPtr = Marshal.GetFunctionPointerForDelegate(sendFunction);
            this.recvFunction = RecvHandle;
            this.recvFunctionPtr = Marshal.GetFunctionPointerForDelegate(recvFunction);
            this.hook = new HookControl[2];
            this.hook[0] = new HookControl();
            this.hook[0].Install("WS2_32.DLL", "send", sendFunctionPtr);
            this.hook[1] = new HookControl();
            this.hook[1].Install("WS2_32.DLL", "recv", recvFunctionPtr);

            this.changeSuitForm = new ChangeSuitForm();
            this.changeSuitForm.Hide();
        }

        private void SeerWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
        private void SeerWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (GlobalVariable.isLogin)
            {
                GlobalVariable.isLogin = false;
            }
            this.messageEncryptControl.SetKey("!crAckmE4nOthIng:-)");
            this.messageEncryptControl.InitKey();
        }
        private void SeerWebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            HtmlElement head = this.seerWebBrowser.Document.GetElementsByTagName("head")[0];
            HtmlElement scriptEl = this.seerWebBrowser.Document.CreateElement("script");
            mshtml.IHTMLScriptElement element = (mshtml.IHTMLScriptElement)scriptEl.DomElement;
            string alertBlocker = "window.alert = function () { };" +
                "window.confirm = function () { return true; }";
            element.text = alertBlocker;
            head.AppendChild(scriptEl);
            this.seerWebBrowser.ScriptErrorsSuppressed = true;
        }
        private void SeerWebBrowser_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Keys pressedKey = e.KeyCode;
            if (pressedKey == Keys.F5)
            {
                this.seerWebBrowser.Refresh();
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
            if(ByteConverter.BytesTo10(ByteConverter.TakeBytes(bytes, 0, 2)) == 0)
            {
                GlobalVariable.gameSocket = s;
                //Console.WriteLine("sendEncrypt:" + BitConverter.ToString(bytes));
                if (GlobalVariable.isLogin)
                {
                    bytes = this.messageEncryptControl.Decrypt(bytes);
                    //Console.WriteLine("sendDecrypt:" + BitConverter.ToString(bytes));
                    byte[] encryptBytes = ByteConverter.TakeBytes(bytes, 0, bytes.Length - 1);
                    this.sendPkgInfo = this.messageEncryptControl.GetHeadInfo(encryptBytes);
                    this.sendPkgInfo = this.messageEncryptControl.PackHeadInfo(this.sendPkgInfo);
                    bytes = this.sendPkgInfo.encryptData;
                    //Console.WriteLine("sendEncrypt:" + BitConverter.ToString(bytes));
                }
            }
            dataPointer = ByteConverter.GetBytesIntPtr(bytes);
            dataSize  = bytes.Length;
            this.hook[0].Suspend();
            HookControl.send(s, dataPointer, dataSize, type);
            this.hook[0].Continue();
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
            if (GlobalVariable.gameSocket == s) this.messageEncryptControl.LoadBasicMessage(dataPointer, ret);
            return ret;
        }

        private void SeerMainWindow_Destroyed(object sender, EventArgs e)
        {
            for (int i = 0 ; i > this.hook.Length - 1; i++)
            {
                if(this.hook[i] != null) this.hook[i].Uninstall();
            }
            GlobalVariable.stopThread = true;
        }

        private void gameReloadMenu_Click(object sender, EventArgs e)
        {
            this.seerWebBrowser.Refresh();
        }



        private void changSuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.changeSuitForm.Show();
            this.changeSuitForm.InitGroupBoxs();
        }
    }
}
