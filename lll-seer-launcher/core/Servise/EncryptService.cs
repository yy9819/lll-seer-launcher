using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Tools;

namespace lll_seer_launcher.core.Servise
{
    class EncryptService
    {
        /// <summary>
        /// 加密传入的封包
        /// </summary>
        /// <param name="targetData">需要加密的数据</param>
        /// <returns>加密完成的数据</returns>
        public static byte[] Encrypt(byte[] targetData)
        {
            int decryptDataLen = targetData.Length - 4;

            byte[] encryptData = ByteConverter.TakeBytes(targetData, 4, decryptDataLen);
            byte[] encryptBytes = new byte[targetData.Length];
            if (encryptData.Length > 4)
            {
                encryptData = EncryptData(encryptData);
                ByteConverter.HexToBytes(ByteConverter.DecimalToHex(targetData.Length, 4)).CopyTo(encryptBytes, 0);
                encryptData.CopyTo(encryptBytes, 4);
            }
            return encryptBytes;
        }

        /// <summary>
        /// 加密方法controller
        /// </summary>
        /// <param name="targetData">需加密的数据</param>
        /// <returns>加密完成的数据</returns>
        private static byte[] EncryptData(byte[] targetData)
        {
            byte[] encryptData = new byte[targetData.Length];
            targetData.CopyTo(encryptData, 0);
            int encrptDataLen = encryptData.Length;
            int decrptDataLen = encrptDataLen - 1;

            if (decrptDataLen >=1)
            {
                IntPtr encrptDataPtr = ByteConverter.GetBytesIntPtr(encryptData);

                EncryptDecryptTools.KeyXOr(decrptDataLen, encrptDataPtr);
                Marshal.Copy(encrptDataPtr, encryptData, 0, encrptDataLen);

                EncryptFunction(decrptDataLen, encrptDataPtr);
                Marshal.Copy(encrptDataPtr, encryptData, 0, encrptDataLen);

                encryptData = EncryptDecryptTools.RevertData(decrptDataLen, encryptData, true);
                Marshal.FreeHGlobal(encrptDataPtr);
            }

            return encryptData;
        }

        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="dataLen">需加密的数据长度</param>
        /// <param name="dataPtr">需加密的数据指针</param>
        private static void EncryptFunction(int dataLen, IntPtr dataPtr)
        {
            int i = 0;
            IntPtr dataPtrSted = dataPtr + dataLen;
            int dataValue;
            int nextValue;
            while (i < dataLen)
            {
                dataValue = (int)EncryptDecryptTools.li8(dataPtrSted);
                nextValue = (int)EncryptDecryptTools.li8(dataPtrSted - 1);
                nextValue = nextValue >> 3;
                dataValue = nextValue | dataValue;

                Marshal.WriteByte(dataPtr, dataLen - i, (byte)dataValue);

                dataValue = (int)EncryptDecryptTools.li8(dataPtrSted - 1);
                dataValue = dataValue << 5;
                dataPtrSted -= 1;
                Marshal.WriteByte(dataPtr, dataLen - i - 1, (byte)dataValue);
                i++;
            }
            dataValue = (int)EncryptDecryptTools.li8(dataPtrSted);
            dataValue = dataValue | 3;
            Marshal.WriteByte(dataPtr, 0, (byte)dataValue);
        }
        /// <summary>
        /// seq计算
        /// </summary>
        /// <param name="seq">当前seq</param>
        /// <param name="pkgLen">数据长度</param>
        /// <param name="crc8Val">与欲发送包数据进行异或异与计算后的值</param>
        /// <param name="cmdId">欲发送的cmdId</param>
        /// <returns>计算完成的顺序码</returns>
        public static int MSerial(int seq, int pkgLen, int crc8Val, int cmdId)
        {
            int result = (int)seq + crc8Val + (int)(seq / -3) + (pkgLen % 17) + (cmdId % 23) + 120;
            return result;
        }
    }
}
