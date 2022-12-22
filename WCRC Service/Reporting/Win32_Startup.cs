using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class Win32_Startup
{
    public Win32_Startup(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static List<Win32_Startup> GetStartupApps()
    {
        var apps = new List<Win32_Startup>();

        try
        {
            foreach (var val in StartupFolder()) apps.Add(val);
            foreach (var val in RegistryChecks(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce",
                         RegistryHive.Users)) apps.Add(val);
            foreach (var val in RegistryChecks(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce",
                         RegistryHive.LocalMachine))
                apps.Add(val);
            foreach (var val in RegistryChecks(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",
                         RegistryHive.Users)) apps.Add(val);
            foreach (var val in RegistryChecks(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",
                         RegistryHive.LocalMachine))
                apps.Add(val);

            WCRC.log.LogWrite("Got startup successfully");
            return apps;
        }
        catch (Exception)
        {
            WCRC.log.LogWrite("Error : startup");

            return apps;
        }
    }

    private static List<Win32_Startup> StartupFolder()
    {
        if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup))) return new List<Win32_Startup>();
        var files = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Startup)).GetFiles();
        return Convert(files.Where(info => info.Name != "desktop.ini").Select(info => info.Name).ToList());
    }

    private static List<Win32_Startup> RegistryChecks(string key, RegistryHive hive)
    {
        RegistryKey registryKey = RegistryKey.OpenBaseKey(hive, RegistryView.Registry32)
                    .OpenSubKey(key);
        var startupKey = registryKey;
        var valueNames = startupKey?.GetValueNames();
        if (valueNames is null) return new List<Win32_Startup>();
        return Convert(valueNames.ToList());
    }

    private static List<Win32_Startup> Convert(List<string> list)
    {
        var res = new List<Win32_Startup>();
        foreach (var val in list) res.Add(new Win32_Startup(val));
        return res;
    }
}