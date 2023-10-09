using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using lll_seer_launcher.core;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;
using System.Security.Cryptography;
using System.Text;

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
        public seerMainWindow()
        {
            InitializeComponent();
        }

        private void SeerMainWindow_Load(object sender, EventArgs e)
        {
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

        }

        private void seerWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        private void seerWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (this.messageEncryptControl.isLogin) this.messageEncryptControl.isLogin = false;
        }
        private void seerWebBrowser_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Keys pressedKey = e.KeyCode;
            if (pressedKey == Keys.F5)
            {
                this.seerWebBrowser.Url = new System.Uri("https://seer.61.com/play.shtml", System.UriKind.Absolute);
            }
        }
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SendHandleDelegate(int s, IntPtr dataPointer, int dataSize, int type);
        private int SendHandle(int s, IntPtr dataPointer, int dataSize, int type)
        {
            byte[] bytes = new byte[dataSize];
            Marshal.Copy(dataPointer, bytes, 0, dataSize);
            if(ByteConverter.BytesTo10(ByteConverter.TakeBytes(bytes, 0, 2)) == 0)
            {
                hook[1].gameSocket = s;
                //Console.WriteLine("sendEncrypt000:" + BitConverter.ToString(bytes));
                if (this.messageEncryptControl.isLogin)
                {
                    bytes = this.messageEncryptControl.Decrypt(bytes);
                    byte[] encryptBytes = ByteConverter.TakeBytes(bytes, 0, bytes.Length - 1);
                    this.sendPkgInfo = this.messageEncryptControl.GetHeadInfo(encryptBytes);
                    this.sendPkgInfo = this.messageEncryptControl.PackHeadInfo(this.sendPkgInfo);
                    bytes = this.sendPkgInfo.encryptData;
                    //Console.WriteLine("sendDecrypt111:" + BitConverter.ToString(this.sendPkgInfo.decryptData));
                }
                //Console.WriteLine("sendEncrypt111:" + BitConverter.ToString(bytes));
                //Console.WriteLine();
            }
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
            if (hook[1].gameSocket == s) this.messageEncryptControl.LoadBasicMessage(dataPointer, ret);
            return ret;
        }

        private void SeerMainWindow_Destroyed(object sender, EventArgs e)
        {
            for (int i = 0 ; i > this.hook.Length - 1; i++)
            {
                if(this.hook[i] != null) this.hook[i].Uninstall();
            }
        }
    }
}
