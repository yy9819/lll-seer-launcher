using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Tools
{
    class EncryptDecryptTools
    {
        /// <summary>
        /// 取出指定指针中的一个数据
        /// </summary>
        /// <param name="targetDataPtr">欲取出数据的指针</param>
        /// <returns>被取出的一字节数据</returns>
        public static IntPtr li8(IntPtr targetDataPtr)
        {
            byte[] data = new byte[1];
            Marshal.Copy(targetDataPtr, data, 0, 1);
            IntPtr result = (IntPtr)ByteConverter.BytesTo10(data);
            return result;
        }

        /// <summary>
        /// 经过第一次解密的数据与加密key进行位异或运算进行二次解密
        /// </summary>
        /// <param name="DecryptedDataLen">欲解密的字节数组</param>
        /// <param name="dataPtr">欲解密的字节数组的指针</param>
        /// <returns></returns>
        public static void KeyXOr(int DecryptedDataLen, IntPtr dataPtr , IntPtr keyPtr ,int keyLen)
        {
            int i = 0;
            int keyI = 0;                                           //key i 对应的value
            byte[] decrptedBytes = new byte[DecryptedDataLen];
            while (i < DecryptedDataLen)
            {
                int dataValue = (int)li8(dataPtr + i);              //取出原数据 指针+i 的数据
                int ptrKeyI = 0;                                    //临时已取数据数
                IntPtr tmpValue = keyPtr;                      //取出当前key的指针设为目标取出值
                if (keyI != keyLen)                            //判断已取数据个数是否等于key的长度
                {
                    tmpValue = keyPtr + keyI;                  //已取数据个数不等于key的长度时，将目标取出值设为当前key的指针 + 已取数
                    ptrKeyI = keyI + 1;                             //更新已取数据数
                }
                keyI = (int)li8(tmpValue);                          //取出目标key值
                dataValue = keyI ^ (int)dataValue;                  //位异或运算  
                keyI = ptrKeyI;                                     //更新已取数据数
                decrptedBytes[i] = (byte)dataValue;
                i++;
            }
            Marshal.Copy(decrptedBytes, 0, dataPtr, DecryptedDataLen);//将还原后的值写入内存
        }



        /// <summary>
        /// 还原数据原始顺序
        /// </summary>
        /// <param name="decryptedDataLen">字节数组长度</param>
        /// <param name="encryptedData">被还原的字节数组</param>
        /// <param name="isEncrypt">是否加密</param>
        /// <returns>还原完成的字节数组</returns>
        public static byte[] RevertData(int decryptedDataLen, byte[] encryptedData, bool isEncrypt, IntPtr keyPtr, int keyLen)
        {
            int encryptedDataLen = decryptedDataLen + 1;
            //Console.WriteLine($"this.keyPtr:{this.keyPtr}  this.keyLen:{this.keyLen}");
            IntPtr result = keyPtr + (decryptedDataLen % keyLen);
            result = (IntPtr)((int)li8(result) * 13);
            result = (IntPtr)((int)result % encryptedDataLen);
            if (result != IntPtr.Zero && (encryptedData != null || encryptedData.Length != 0))
            {
                if (isEncrypt)
                {
                    result = (IntPtr)(encryptedDataLen - (int)result);
                }
                byte[] tempBytesLeft = ByteConverter.TakeBytes(encryptedData, encryptedDataLen - (int)result, (int)result);
                byte[] tempBytesRight = ByteConverter.TakeBytes(encryptedData, 0, encryptedDataLen - (int)result);
                encryptedData = ByteConverter.RepalaceBytes(encryptedData, tempBytesLeft, 0);
                encryptedData = ByteConverter.RepalaceBytes(encryptedData, tempBytesRight, (int)result);
            }

            return encryptedData;
        }

    }
}
