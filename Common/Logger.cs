using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Logger
    {
        public static void LogReceivedData(string MachineID, string data)
        {
            string DirInfo = AppDomain.CurrentDomain.BaseDirectory;
            data = "-------------" + DateTime.Now.ToString("HH:mm:ss") + "--------------------\r\n" + data + "\r\n";
            if (!Directory.Exists(Path.Combine(DirInfo, "ReceivedDataLogFiles")))
                Directory.CreateDirectory(Path.Combine(DirInfo, "ReceivedDataLogFiles"));
            File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "ReceivedDataLogFiles"), MachineID+"_"+DateTime.Now.ToString("ddMMyyyy") + ".txt"), data);
        }
        public static void LogSentDataToServer(string data)
        {
            string DirInfo = AppDomain.CurrentDomain.BaseDirectory;
            data = "-------------" + DateTime.Now.ToString("HH:mm:ss") + "--------------------\r\n" + data + "\r\n";
            if (!Directory.Exists(Path.Combine(DirInfo, "SentDataToServer")))
                Directory.CreateDirectory(Path.Combine(DirInfo, "SentDataToServer"));
            File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "SentDataToServer"), System.DateTime.Now.ToString("ddMMyyyy") + ".txt"), data);
        }
        public static void LogReceivedDataFromServer(string data)
        {
            string DirInfo = AppDomain.CurrentDomain.BaseDirectory;
            data = "-------------" + DateTime.Now.ToString("HH:mm:ss") + "--------------------\r\n" + data + "\r\n";
            if (!Directory.Exists(Path.Combine(DirInfo, "ReceivedDataFromServer")))
                Directory.CreateDirectory(Path.Combine(DirInfo, "ReceivedDataFromServer"));
            File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "ReceivedDataFromServer"), System.DateTime.Now.ToString("ddMMyyyy") + ".txt"), data);
        }
        public static void LogExceptions(string data)
        {
              string DirInfo = AppDomain.CurrentDomain.BaseDirectory;
                data = "-------------" + DateTime.Now.ToString("HH:mm:ss") + "--------------------\r\n" + data + "\r\n";
                if (!Directory.Exists(Path.Combine(DirInfo, "Exceptions")))
                    Directory.CreateDirectory(Path.Combine(DirInfo, "Exceptions"));
                File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "Exceptions"), System.DateTime.Now.ToString("ddMMyyyy") + ".txt"), data);
            
        }
        public static void LogTimerExecution(string data)
        {
            string DirInfo = AppDomain.CurrentDomain.BaseDirectory;
            data = "-------------" + DateTime.Now.ToString("HH:mm:ss") + "--------------------\r\n" + data + "\r\n";
            if (!Directory.Exists(Path.Combine(DirInfo, "UpdateRemoteDatabaseTimer")))
                Directory.CreateDirectory(Path.Combine(DirInfo, "UpdateRemoteDatabaseTimer"));
            File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "UpdateRemoteDatabaseTimer"), System.DateTime.Now.ToString("ddMMyyyy") + ".txt"), data);

        }
        public static void LogParsedData(string data)
        {
            string DirInfo = AppDomain.CurrentDomain.BaseDirectory;
            data = "-------------" + DateTime.Now.ToString("HH:mm:ss") + "--------------------\r\n" + data + "\r\n";
            if (!Directory.Exists(Path.Combine(DirInfo, "ParsedData")))
                Directory.CreateDirectory(Path.Combine(DirInfo, "ParsedData"));
            File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "ParsedData"), DateTime.Now.ToString("ddMMyyyy") + ".txt"), data);
        }
    }
}
