using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Controller;

namespace lll_seer_launcher.core.Servise
{
    class SendDataServise
    {
        public static void SendHexStringData(string hexStringData , int s , IntPtr keyPtr, int keyLen)
        {
            hexStringData = hexStringData.Replace(" ", "").Replace("　", "");
            SendHexBytesData(ByteConverter.HexToBytes(hexStringData), s , keyPtr , keyLen);
        }

        public static void SendHexBytesData(byte[] bytesData , int s ,IntPtr keyPtr ,int keyLen)
        {
            byte[] bytes = EncryptService.Encrypt(bytesData ,keyPtr ,keyLen);
            HookControl.send(s , ByteConverter.GetBytesIntPtr(bytes) , bytes.Length ,0);
        }
    }
}
