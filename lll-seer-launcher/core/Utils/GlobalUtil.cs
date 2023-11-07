using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto;

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
        public static ArrayList CombineArrayList(ArrayList orgArrayList, ArrayList combinedArrayList)
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
                if (i != list.Count - 1) str += "-";
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

        public static long GetKey()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds  - new Random().Next(200, 1000);
        }

        public static bool StartFiddler()
        {
            if (FormController.FindWindow(GlobalVariable.seerFiddlerTitle) == IntPtr.Zero)
            {
                Process.Start(Directory.GetCurrentDirectory() + "\\seer-fiddler.exe");
            }
            int count = 0;
            while (count > 100)
            {
                if (FormController.FindWindow(GlobalVariable.seerFiddlerTitle) != IntPtr.Zero)
                {
                    break;
                }
                count++;
                Thread.Sleep(100);
            }
            return count <= 100;
        }

        public static string GetJsonString(string link)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            request.Method = "GET";
            string json = "";
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }
            catch { }
            return json;
        }
    }
    public class PetHeadSetter
    {
        private string petHeadDirectoryPath = Directory.GetCurrentDirectory() + "\\cache\\pet\\head\\";
        private const string petHeadLink = "https://seerh5.61.com/resource/assets/pet/head/@.png";
        public PetHeadSetter()
        {
            if(!Directory.Exists(petHeadDirectoryPath))Directory.CreateDirectory(petHeadDirectoryPath);
        }
        public string GetHeadPath(int petId)
        {
            lock (GlobalVariable.lockObjs["getRealId"])
            {
                petId = DBController.PetDBController.GetPetRealId(petId);
            }
            CheckHeadFile(petId);
            return $"{petHeadDirectoryPath}{petId}.png";
        }
        private async void CheckHeadFile(int petId)
        {
            if (File.Exists($"{petHeadDirectoryPath}{petId}.png")) return;
            try
            {
                HttpWebRequest request = WebRequest.Create(petHeadLink.Replace("@", petId.ToString())) as HttpWebRequest;
                request.Method = "GET";

                // 使用异步方式执行HTTP请求
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;

                            while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                memoryStream.Write(buffer, 0, bytesRead);
                            }

                            byte[] content = memoryStream.ToArray();
                            File.WriteAllBytes($"{petHeadDirectoryPath}{petId}.png", content);
                        }
                    }
                }
            }
            catch { }
        }
    }
    /// <summary>
    /// 对配置文件进行读写操作
    /// </summary>
    public class IniFile
    {
        private string filePath;

        public IniFile(string path)
        {
            filePath = path;
        }

        public string Read(string section, string key)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                string currentSection = null;
                foreach (string line in lines)
                {
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        currentSection = line.Substring(1, line.Length - 2);
                    }
                    else if (currentSection == section)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2 && parts[0].Trim() == key)
                        {
                            return parts[1].Trim();
                        }
                    }
                }
            }
            return null;
        }

        public void Write(string section, string key, string value)
        {
            if (File.Exists(filePath))
            {
                List<string> newLines = new List<string>();
                string currentSection = null;
                bool found = false;

                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        if (currentSection == section && !found)
                        {
                            newLines.Add($"{key}={value}");
                            found = true;
                        }
                        newLines.Add(line);
                        currentSection = line.Substring(1, line.Length - 2);
                    }
                    else if (currentSection == section && line.TrimStart().StartsWith(key + "="))
                    {
                        newLines.Add($"{key}={value}");
                        found = true;
                    }
                    else
                    {
                        newLines.Add(line);
                    }
                }

                if (!found)
                {
                    newLines.Add($"[{section}]");
                    newLines.Add($"{key}={value}");
                }

                File.WriteAllLines(filePath, newLines);
            }
            else
            {
                // Handle the case where the INI file does not exist.
            }
        }
    }
}

