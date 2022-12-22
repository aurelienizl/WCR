using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCRC_Service.Reporting
{
    internal class Win32_Softwares
    {
        public string GetKeyName { get; set; }
        public string GetDisplayName { get; set; }
        public string GetPublisher { get; set; }
        public string GetDisplayVersion { get; set; }
        public string GetUninstallString { get; set; }
        public string GetEstimatedSize { get; set; }
        public string GetInstallSource { get; set; }

        public Win32_Softwares(string KeyName, string DisplayName, string DisplayVersion, 
            string UninstallString, string EstimatedSize, string InstallSource, string Publisher)
        {
            GetKeyName = KeyName;
            GetDisplayName = DisplayName;
            GetDisplayVersion = DisplayVersion;
            GetUninstallString = UninstallString;
            GetEstimatedSize = EstimatedSize;
            GetInstallSource = InstallSource;
            GetPublisher = Publisher;
        }

        public static string GetRegistrykeySafe(RegistryKey key, string query)
        {
            try
            {
                string res = (string)key.GetValue(query);
                if (!String.IsNullOrEmpty(res))
                {
                    return res;
                }
                return "N/A";
            }
            catch (Exception)
            {
                return "N/A";
            }
        }

        public static List<Win32_Softwares> GetInstalledApps()
        {
            List<Win32_Softwares> win32_Softwares = new List<Win32_Softwares>();

            try
            {

                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (var key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                    {
                        foreach (string skName in key.GetSubKeyNames())
                        {
                            using (RegistryKey sk = key.OpenSubKey(skName))
                            {
                                try
                                {
                                    win32_Softwares.Add(
                                        new Win32_Softwares(
                                            sk.Name,
                                            GetRegistrykeySafe(sk, "DisplayName"),
                                            GetRegistrykeySafe(sk, "DisplayVersion"),
                                            GetRegistrykeySafe(sk, "UninstallString"),
                                            GetRegistrykeySafe(sk, "EstimatedSize"),
                                            GetRegistrykeySafe(sk, "InstallSource"),
                                            GetRegistrykeySafe(sk, "Publisher")

                                        ));

                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                    }
                }
              
                WCRC.log.LogWrite("Got softwares successfully");

                return win32_Softwares;
            }
            catch (Exception)
            {
                WCRC.log.LogWrite("Error : softwares");

                return win32_Softwares;
            }
            
        }
    }
}
