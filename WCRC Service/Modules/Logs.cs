using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class Logs
{
    public static string hostname;
    public Logs(string logMessage)
    {
        hostname = Dns.GetHostName();
        File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "log.txt");
        LogWrite(logMessage);
    }
    public void LogWrite(string logMessage)
    {
        string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        try
        {
            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
            {
                Log(logMessage, w);
            }
        }
        catch (Exception)
        {
        }
    }

    public void Log(string logMessage, TextWriter txtWriter)
    {
        try
        {
            txtWriter.Write("\r\nLog Entry : ");
            txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            txtWriter.WriteLine(hostname);
            txtWriter.WriteLine("  $ {0}", logMessage);
            txtWriter.WriteLine("-------------------------------");
        }
        catch (Exception)
        {
        }
    }
}

