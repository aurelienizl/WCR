using Microsoft.VisualBasic.Logging;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics.Metrics;


namespace Windows_Compliancy_Report_Client
{
    internal class Startup
    {
        public string? Name { get; }

        public Startup(string? name)
        {
            Name = name;
        }

        public static List<Startup>? GetStartupApps()
        {
            List<Startup> apps = new List<Startup>();

            try
            {
                foreach (var val in StartupFolder())
                {
                    apps.Add(val);
                }
                foreach (var val in RegistryChecks(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", RegistryHive.Users))
                {
                    apps.Add(val);
                }
                foreach (var val in RegistryChecks(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce",
                             RegistryHive.LocalMachine))
                {
                    apps.Add(val);
                }
                foreach (var val in RegistryChecks(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", RegistryHive.Users))
                {
                    apps.Add(val);
                }
                foreach (var val in RegistryChecks(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",
                             RegistryHive.LocalMachine))
                {
                    apps.Add(val);
                }
                return apps;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        private static List<Startup> StartupFolder()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup)))
            {
                return new List<Startup>();
            }
            var files = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Startup)).GetFiles();
            return Convert(files.Where(info => info.Name != "desktop.ini").Select(info => info.Name).ToList());
        }

        private static List<Startup> RegistryChecks(string key, RegistryHive hive)
        {
            using RegistryKey? startupKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry32)
                .OpenSubKey(key);
            var valueNames = startupKey?.GetValueNames();
            if (valueNames is null) return new List<Startup>();
            return Convert(valueNames.ToList());
        }

        private static List<Startup> Convert(List<string> list)
        {
            List<Startup> res = new List<Startup>();
            foreach(var val in list)
            {
                res.Add(new Startup(val));
            }
            return res;
        }
    }
}
