using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Tools;

namespace lll_seer_launcher.core.Servise
{
    class DecryptService
    {

        /// <summary>
        /// 解密指定的字节数组
        /// 对字节数组需要解密的长度进行计算，并判断是否需要解密
        /// </summary>
        /// <param name="targetData">欲解密的字节数组</param>
        /// <returns>解密完成的字节数组</returns>
        public static byte[] Decrypt(byte[] targetData, IntPtr keyPtr, int keyLen)
        {
            int encryptedDataLen = targetData.Length - 4;
            int decryptedDataLen = encryptedDataLen + 1;
            byte[] decryptedData = ByteConverter.TakeBytes(targetData, 4, decryptedDataLen);
            if (decryptedData.Length > 4)
            {
                decryptedData = DecryptData(decryptedData, keyPtr, keyLen);
                targetData = ByteConverter.RepalaceBytes(targetData, decryptedData, 4);
            }
            return targetData;
        }

        /// <summary>
        /// 解密函数control
        /// </summary>
        /// <param name="targetData">欲解密的字节数组</param>
        /// <returns>解密完成的字节数组</returns>
        public static byte[] DecryptData(byte[] targetData, IntPtr keyPtr, int keyLen)
        {
            byte[] encryptedData = targetData;
            int encryptedDataLen = encryptedData.Length;
            int decryptedDataLen = encryptedDataLen - 1;
            byte[] decryptData = new byte[decryptedDataLen];
            if (decryptedDataLen >= 1)
            {
                encryptedData = EncryptDecryptTools.RevertData(decryptedDataLen, encryptedData, false, keyPtr, keyLen);
                IntPtr dataPtr = ByteConverter.GetBytesIntPtr(encryptedData);

                DecryptFunction(decryptedDataLen, dataPtr);

                EncryptDecryptTools.KeyXOr(decryptedDataLen, dataPtr, keyPtr, keyLen);

                Marshal.Copy(dataPtr, decryptData, 0, decryptedDataLen);

                Marshal.FreeHGlobal(dataPtr);
            }
            return decryptData;
        }

        /// <summary>
        /// 解密函数1
        /// </summary>
        /// <param name="DecryptedDataLen">欲解密的字节数组</param>
        /// <param name="dataPtr">欲解密的字节数组的指针</param>
        /// <returns></returns>
        public static void DecryptFunction(int DecryptedDataLen, IntPtr dataPtr)
        {
            int i = 0;
            int dataValue = (int)EncryptDecryptTools.li8(dataPtr);                      //取出原数据 指针 的第一个数据
            byte[] decrptedBytes = new byte[DecryptedDataLen];
            while (i < DecryptedDataLen)
            {
                int tmpValue = dataValue & 224;                     //对原数据进行位与计算
                int keyI = tmpValue >> 5;                           //对原数据进行右移5，并保存
                dataValue = (int)EncryptDecryptTools.li8(dataPtr + i + 1);              //取出下一个数据
                tmpValue = dataValue << 3;                          //将下一个数据左移3
                tmpValue = tmpValue | keyI;                         //进行位或运算
                decrptedBytes[i] = (byte)tmpValue;
                i++;
            }
            Marshal.Copy(decrptedBytes, 0, dataPtr, DecryptedDataLen);//解密后的数据写入内存
        }
    }
}
