using System;
using System.Collections.Generic;
using Microsoft.Win32;
using WCRC_Service.Modules;

namespace WCRC_Service.Reporting
{
    internal class Win32_Software
    {
        public Win32_Software(string KeyName, string DisplayName, string DisplayVersion,
            string UninstallString, string InstallSource, string Publisher)
        {
            GetKeyName = KeyName;
            GetDisplayName = DisplayName;
            GetDisplayVersion = DisplayVersion;
            GetUninstallString = UninstallString;
            GetInstallSource = InstallSource;
            GetPublisher = Publisher;
        }

        public string GetKeyName { get; set; }
        public string GetDisplayName { get; set; }
        public string GetPublisher { get; set; }
        public string GetDisplayVersion { get; set; }
        public string GetUninstallString { get; set; }
        public string GetInstallSource { get; set; }

        public static string GetRegistrykeySafe(RegistryKey key, string query)
        {
            try
            {
                var res = (string)key.GetValue(query);
                if (!string.IsNullOrEmpty(res)) return res;
                return "N/A";
            }
            catch (Exception)
            {
                return "N/A";
            }
        }

        public static List<Win32_Software> GetInstalledApps()
        {
            var win32Softwares = new List<Win32_Software>();

            try
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (var key = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                    {
                        if (key != null)
                            foreach (var skName in key.GetSubKeyNames())
                                using (var sk = key.OpenSubKey(skName))
                                {
                                    try
                                    {
                                        if (sk != null)
                                            win32Softwares.Add(
                                                new Win32_Software(
                                                    sk.Name,
                                                    GetRegistrykeySafe(sk, "DisplayName"),
                                                    GetRegistrykeySafe(sk, "DisplayVersion"),
                                                    GetRegistrykeySafe(sk, "UninstallString"),
                                                    GetRegistrykeySafe(sk, "InstallSource"),
                                                    GetRegistrykeySafe(sk, "Publisher")
                                                ));
                                    }
                                    catch (Exception)
                                    {
                                        // ignored
                                    }
                                }
                    }
                }

                Logs.LogWrite("Got softwares successfully");

                return win32Softwares;
            }
            catch (Exception)
            {
                Logs.LogWrite("Error : softwares");

                return win32Softwares;
            }
        }
    }
}