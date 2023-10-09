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
        public static byte[] Encrypt(byte[] targetData, IntPtr keyPtr, int keyLen)
        {
            int decryptDataLen = targetData.Length - 4;

            byte[] encryptData = ByteConverter.TakeBytes(targetData, 4, decryptDataLen);
            byte[] encryptBytes = new byte[targetData.Length];
            if (encryptData.Length > 4)
            {
                encryptData = EncryptData(encryptData, keyPtr, keyLen);
                encryptBytes = ByteConverter.RepalaceBytes(encryptBytes, ByteConverter.HexToBytes(ByteConverter.DecimalToHex(targetData.Length, 4)), 0);
                encryptBytes = ByteConverter.RepalaceBytes(encryptBytes, encryptData, 4);
            }
            return encryptBytes;
        }

        private static byte[] EncryptData(byte[] targetData, IntPtr keyPtr, int keyLen)
        {
            byte[] encryptData = new byte[targetData.Length + 1];
            encryptData = ByteConverter.RepalaceBytes(encryptData, targetData, 0);
            int encrptDataLen = encryptData.Length;
            int decrptDataLen = encrptDataLen - 1;

            if (decrptDataLen >=1)
            {
                IntPtr encrptDataPtr = ByteConverter.GetBytesIntPtr(encryptData);
                Marshal.Copy(encrptDataPtr, encryptData, 0, encrptDataLen);

                EncryptDecryptTools.KeyXOr(decrptDataLen, encrptDataPtr, keyPtr, keyLen);
                Marshal.Copy(encrptDataPtr, encryptData, 0, encrptDataLen);

                EncryptFunction(encrptDataLen, encrptDataPtr);
                Marshal.Copy(encrptDataPtr, encryptData, 0, encrptDataLen);

                encryptData = EncryptDecryptTools.RevertData(decrptDataLen, encryptData, true , keyPtr , keyLen);
                //Marshal.Copy(encrptDataPtr, encryptData, 0, encrptDataLen);
                //Console.WriteLine($"RevertData:{BitConverter.ToString(encryptData)}");
            }

            return encryptData;
        }



        private static void EncryptFunction(int dataLen, IntPtr dataPtr)
        {
            int i = 0;
            IntPtr dataPtrSted = dataPtr + dataLen;
            byte[] encrptedBytes = new byte[dataLen];
            int dataValue;
            int nextValue;
            while (i < dataLen)
            {
                dataValue = (int)EncryptDecryptTools.li8(dataPtrSted);
                nextValue = (int)EncryptDecryptTools.li8(dataPtrSted - 1);
                nextValue = nextValue >> 3;
                dataValue = nextValue | dataValue;

                //Marshal.WriteIntPtr(dataPtrSted, 1, (IntPtr)dataValue);
                Marshal.WriteByte(dataPtrSted, 0, (byte)dataValue);
                //int index = dataLen - i - 1;
                //encrptedBytes[index] = (byte)dataValue;

                dataValue = (int)EncryptDecryptTools.li8(dataPtrSted - 1);
                dataValue = dataValue << 5;
                dataPtrSted -= 1;
                //Marshal.WriteIntPtr(dataPtrSted, 1, (IntPtr)dataValue);
                Marshal.WriteByte(dataPtrSted, 0, (byte)dataValue);
                //index -= 1;
                //if(index > 0) encrptedBytes[index] = (byte)dataValue;
                i++;
            }
            dataValue = (int)EncryptDecryptTools.li8(dataPtrSted);
            dataValue = dataValue | 3;
            //Marshal.WriteIntPtr(dataPtrSted, 1, (IntPtr)dataValue);
            Marshal.WriteByte(dataPtrSted, 0, (byte)dataValue);
            //encrptedBytes[0] = (byte)dataValue;
            //Marshal.Copy(encrptedBytes, 0, dataPtr, dataLen);//解密后的数据写入内存
        }
        public static int MSerial(int seq, int pkgLen, int crc8Val, int cmdId)
        {
            seq = seq + crc8Val + (int)(seq / -3) + (int)(pkgLen % 17) + (int)(cmdId % 23) + 120;
            return seq;
        }
    }
}
