using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WCRC_Service.Modules
{
    public class WCRC_Parameters
    {
        public static string FileName { get; set; }
        public static string Path { get; set; }
        public static string Hostname { get; set; }
        public static int HostnamePort { get; set; }
        public static string Backup { get; set; }
        public static int BackupPort { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }

        private static string GetParameter(string id, string[] data)
        {
            foreach (var elt in data)
            {
                if (elt.StartsWith(id))
                {
                    int startIndex = elt.IndexOf('=') + 1; // Get the index of the first character after 'q'
                    int endIndex = elt.IndexOf(';'); // Get the index of 'f'
                    return elt.Substring(startIndex, endIndex - startIndex);
                }
            }
            return null;
        }

        public static void SetParameters()
        {
            string[] settings =
            File.ReadAllLines(Assembly.GetEntryAssembly()?.Location + @"\settings.txt");
            FileName = GetParameter("filename", settings);
            Path = GetParameter("path", settings);
            Hostname = GetParameter("hostname", settings);
            HostnamePort = Convert.ToInt32(GetParameter("hostname_port", settings));
            Backup = GetParameter("backup", settings);
            BackupPort = Convert.ToInt32(GetParameter("backup_port", settings));
            Username = GetParameter("username", settings);
            Password = GetParameter("password", settings);

            CheckParameters();
        }

        public static void CheckParameters()
        {
            if (FileName == "[HOSTNAME]")
            {
                FileName = Dns.GetHostName();
            }
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

    }
}
