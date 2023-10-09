using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.Collections;


namespace lll_seer_launcher.core
{
    unsafe public class HookControl
    {
        [DllImport("ws2_32.dll")]
        public static extern int send(int s, IntPtr buf, int len, int flag);

        [DllImport("ws2_32.dll")]
        public static extern int recv(int s, IntPtr buf, int len, int flag);

        #region Api声明
        [DllImport("Kernel32.dll", EntryPoint = "GetModuleHandleA", CharSet = CharSet.Ansi)]
        static extern IntPtr GetModuleHandle(
        string lpModuleName
        );
        [DllImport("Kernel32.dll")]
        static extern bool VirtualProtect(
        IntPtr lpAddress,
        int dwSize,
        int flNewProtect,
        ref int lpflOldProtect
        );
        [DllImport("Kernel32.dll", EntryPoint = "lstrcpynA", CharSet = CharSet.Ansi)]
        static extern IntPtr lstrcpyn(
        byte[] lpString1,
        byte[] lpString2,
        int iMaxLength
        );
        [DllImport("Kernel32.dll")]
        static extern IntPtr GetProcAddress(
        IntPtr hModule,
        string lpProcName
        );
        [DllImport("Kernel32.dll")]
        static extern bool FreeLibrary(
        IntPtr hModule
        );
        #endregion

        #region 全局变量定义表
        public int gameSocket;
        #endregion
        
        #region 常量定义表
        const int PAGE_EXECUTE_READWRITE = 0x40;
        #endregion
        #region 变量表
        IntPtr ProcAddress;
        int lpflOldProtect = 0;
        byte[] OldEntry = new byte[5];
        byte[] NewEntry = new byte[5];
        IntPtr OldAddress;
        #endregion
        public HookControl() { }
        public HookControl(string ModuleName, string ProcName, IntPtr lpAddress)
        {
            Install(ModuleName, ProcName, lpAddress);
        }
        public bool Install(string ModuleName, string ProcName, IntPtr lpAddress)
        {
            IntPtr hModule = GetModuleHandle(ModuleName); //取模块句柄   
            if (hModule == IntPtr.Zero) return false;
            ProcAddress = GetProcAddress(hModule, ProcName); //取入口地址   
            if (ProcAddress == IntPtr.Zero) return false;
            if (!VirtualProtect(ProcAddress, 5, PAGE_EXECUTE_READWRITE, ref lpflOldProtect)) return false; //修改内存属性   
            Marshal.Copy(ProcAddress, OldEntry, 0, 5); //读取前5字节   
            NewEntry = AddBytes(new byte[1] { 233 }, BitConverter.GetBytes((Int32)((Int32)lpAddress - (Int32)ProcAddress - 5))); //计算新入口跳转   
            Marshal.Copy(NewEntry, 0, ProcAddress, 5); //写入前5字节   
            OldEntry = AddBytes(OldEntry, new byte[5] { 233, 0, 0, 0, 0 });
            OldAddress = lstrcpyn(OldEntry, OldEntry, 0); //取变量指针   
            Marshal.Copy(BitConverter.GetBytes((double)((Int32)ProcAddress - (Int32)OldAddress - 5)), 0, (IntPtr)(OldAddress.ToInt32() + 6), 4); //保存JMP   
            FreeLibrary(hModule); //释放模块句柄   
            return true;
        }
        public void Suspend()
        {
            Marshal.Copy(OldEntry, 0, ProcAddress, 5);
        }
        public void Continue()
        {
            Marshal.Copy(NewEntry, 0, ProcAddress, 5);
        }
        public bool Uninstall()
        {
            if (ProcAddress == IntPtr.Zero) return false;
            Marshal.Copy(OldEntry, 0, ProcAddress, 5);
            ProcAddress = IntPtr.Zero;
            return true;
        }
        public static byte[] AddBytes(byte[] a, byte[] b)
        {
            ArrayList retArray = new ArrayList();
            for (int i = 0; i < a.Length; i++)
            {
                retArray.Add(a[i]);
            }
            for (int i = 0; i < b.Length; i++)
            {
                retArray.Add(b[i]);
            }
            return (byte[])retArray.ToArray(typeof(byte));
        }
    }

}

