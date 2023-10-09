using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace lll_seer_launcher.core.Utils
{
    class ByteConverter
    {
        /// <summary>
        /// 从字节数组中从指定位置开始取出指定数量的字节数组
        /// </summary>
        /// <param name="bytes">欲取出数据的字节数组</param>
        /// <param name="index">开始位置</param>
        /// <param name="len">取出个数</param>
        /// <returns>被取出的字节数组</returns>
        public static byte[] TakeBytes(byte[] bytes , int index , int len)
        {
            return bytes.Skip(index).Take(len).ToArray();
        }

        /// <summary>
        /// 获取指定字节数组在内存中的指针
        /// </summary>
        /// <param name="bytes">欲获取指针的字节数组</param>
        /// <returns>字节数组在内存中的指针</returns>
        public static IntPtr GetBytesIntPtr(byte[] bytes)
        {
            IntPtr bytesPtr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, bytesPtr, bytes.Length);
            return bytesPtr;
        }

        /// <summary>
        /// 将一个字节数组替换到另一个字节数组的指定位置中
        /// </summary>
        /// <param name="orgBytes">被替换的原字节数组</param>
        /// <param name="replacedBytes">替换原字节数组的字节数组</param>
        /// <param name="index">插入起始位置</param>
        /// <returns>替换后的字节数组</returns>
        public static byte[] RepalaceBytes(byte[] orgBytes , byte[] replacedBytes , int index)
        {
            for(int i = 0; i < replacedBytes.Length; i++)
            {
                if (i + index >= orgBytes.Length) break;
                orgBytes[i + index] = replacedBytes[i];
            }
            return orgBytes;
        }

        /// <summary>
        /// 获取指定字节数组对应的10进制Int32数字
        /// </summary>
        /// <param name="bytes">欲转换的原始字节数组</param>
        /// <returns>10进制Int32数字</returns>
        public static int BytesTo10(byte[] bytes)
        {
            return Convert.ToInt32(BitConverter.ToString(bytes).Replace("-", string.Empty), 16);
        }

        /// <summary>
        /// 将16进制字符串转化为字节数组
        /// </summary>
        /// <param name="hexString">欲转换的原始16进制字符串</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] HexToBytes(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for(int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring( i * 2 , 2 ) , 16);
            }
            return bytes;
        }

        /// <summary>
        /// 将10进制数字转换为16进制字符串
        /// </summary>
        /// <param name="decimalValue">欲转换的原始10进制数字</param>
        /// <param name="bytesLen">转换后的字节长度</param>
        /// <returns>转换后的16进制字符串</returns>
        public static string DecimalToHex(int decimalValue,int bytesLen)
        {
            string hexValue = decimalValue.ToString("X2");
            while (hexValue.Length / 2 < bytesLen)
            {
                hexValue = "0" + hexValue;
            }
            return hexValue;
        }
    }
}
