using System;
using System.IO;
using System.Text.RegularExpressions;

namespace lll_seer_launcher.core.Utils
{
    class Logger
    {
        private readonly static Logger logger = new Logger();

        private Logger()
        {
            string directoryPath = Directory.GetCurrentDirectory() + "\\log\\";
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            logPath = directoryPath + "runtimeLog." + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
        }
        private string logPath = null;
        private void Write(string title,string message)
        {
            string timeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            File.AppendAllText(logPath,$"[{timeStr}]-[{title}]:{message + Environment.NewLine}");
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
