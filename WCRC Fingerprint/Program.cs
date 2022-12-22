using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace WCRC_Fingerprint
{
    internal class Program
    {
        static void Main(string[] args)
        {

            foreach (var val in GetWin32_Defenders())
                foreach (var data in val)
                    Console.WriteLine(data);
            Console.ReadLine();

        }
        public static string QuerySafeGetter(ManagementObject obj, string query)
        {
            try
            {
                if(!String.IsNullOrEmpty(obj[query].ToString()))
                {
                    return obj[query].ToString();
                }

                string res = (string)obj[query];
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

        public static List<List<string>> GetWin32_Defenders()
        {
            List<List<string>> win32_Defenders = new List<List<string>>();

            try
            {

                ManagementObjectSearcher wmiData = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
                ManagementObjectCollection data = wmiData.Get();

                foreach (ManagementObject obj in data)
                {
                    win32_Defenders.Add(
                        new List<string>(

                        )
                        { QuerySafeGetter(obj, "instanceGuid"),
                        QuerySafeGetter(obj, "displayName"),
                        QuerySafeGetter(obj, "pathToSignedProductExe"),
                        QuerySafeGetter(obj, "pathToSignedReportingExe"),
                        QuerySafeGetter(obj, "productState"),
                        QuerySafeGetter(obj, "timestamp")});
                }

                return win32_Defenders;
            }
            catch (Exception)
            {

                return win32_Defenders;
            }

        }

    }
}
