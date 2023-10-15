using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace lll_seer_launcher.core.Utils
{
    public class GlobalUtil
    {
        /// <summary>
        /// 将字节数组转化为ArrayList
        /// </summary>
        /// <param name="bytes">欲转换的字节数组</param>
        /// <returns>转换完成的ArrayList</returns>
        public static ArrayList BytesToArray(byte[] bytes)
        {
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < bytes.Length; i++)
            {
                arrayList.Add(bytes[i]);
            }
            return arrayList;
        }

        /// <summary>
        /// 合并两个ArrayList,将第二个ArrayList插入到第一个ArrayList的末尾
        /// </summary>
        /// <param name="orgArrayList">ArrayList1</param>
        /// <param name="combinedArrayList">ArrayList2</param>
        /// <returns>合并完成的ArrayList</returns>
        public static  ArrayList CombineArrayList(ArrayList orgArrayList, ArrayList combinedArrayList)
        {
            foreach (byte i in combinedArrayList)
            {
                orgArrayList.Add(i);
            }
            return orgArrayList;
        }
        public static string ArrayListToString(ArrayList list)
        {
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i].ToString();
                if(i != list.Count - 1) str += "-";
            }
            return str;
        }
        public static string IntListToString(List<int> intList)
        {
            return string.Join(",", intList);
        }
        public static List<int> StringToIntList(string intListString)
        {
            string[] stringList = intListString.Split(',');
            return stringList.Select(int.Parse).ToList();
        }
    }
}

