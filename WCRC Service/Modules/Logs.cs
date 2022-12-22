using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace WCRC_Service.Modules
{
    public class Logs
    {
        private static string _hostname;
        public Logs(string logMessage)
        {
            _hostname = Dns.GetHostName();
            File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "log.txt");
            LogWrite(logMessage);
        }
        public static void LogWrite(string logMessage)
        {
            var mExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (var w = File.AppendText(mExePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine(_hostname);
                txtWriter.WriteLine("  $ {0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}

