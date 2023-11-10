using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace lll_seer_launcher.core.Utils
{
    class Logger
    {
        private readonly static Logger logger = new Logger();
        private static readonly object lockObject = new object();

        private Logger()
        {
            string directoryPath = Directory.GetCurrentDirectory() + "\\log\\";
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            logPath = directoryPath + "runtimeLog." + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
        }
        private string logPath = null;
        private void Write(string title,string message)
        {
            Task writeLogThread = new Task(() =>
            {
                try
                {
                    lock (lockObject)
                    {
                        string timeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        File.AppendAllText(logPath, $"[{timeStr}]-[{title}]:{message + Environment.NewLine}");
                    }
                }
                catch
                {
                    Write(title, message);
                }
            });
            writeLogThread.Start();
        }
        public static void CheckLogRotation()
        {
            string directoryPath = Directory.GetCurrentDirectory() + "\\log\\";
            string[] logPath = Directory.GetFiles(directoryPath, "*.log", SearchOption.AllDirectories);
            foreach (string fileName in logPath)
            {
                string logCreatedDate = fileName.Substring(fileName.IndexOf("og.") + 3 , 10);
                try
                {
                    int value = (int)(DateTime.Now - DateTime.ParseExact(logCreatedDate, "yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)).TotalDays;
                    if(value > 31)
                    {
                        File.Delete(fileName);
                        Logger.Log("DeleteLogFile",$"日志:{fileName},创建日期超过1个月,已被自动清理.");
                    }
                }
                catch { }
            }
        }
        public static void Log(string title,string msg)
        {
            logger.Write(title,msg);
        }
        public static void Error(string msg)
        {
            logger.Write("Error",msg);
        }
    }
}
