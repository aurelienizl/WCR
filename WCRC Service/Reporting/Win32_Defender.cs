using System;
using System.Collections.Generic;
using System.Management;
using WCRC_Service.Modules;

namespace WCRC_Service.Reporting
{
    internal class Win32_Defender
    {
        public Win32_Defender(string getinstanceGuid, string getdisplayName, string getpathToSignedProductExe,
            string getpathToSignedReportingExe, string getproductState, string gettimestamp)
        {
            GetinstanceGuid = getinstanceGuid;
            GetdisplayName = getdisplayName;
            GetpathToSignedProductExe = getpathToSignedProductExe;
            GetpathToSignedReportingExe = getpathToSignedReportingExe;
            GetproductState = getproductState;
            Gettimestamp = gettimestamp;
        }

        public string GetinstanceGuid { get; set; }
        public string GetdisplayName { get; set; }
        public string GetpathToSignedProductExe { get; set; }
        public string GetpathToSignedReportingExe { get; set; }
        public string GetproductState { get; set; }
        public string Gettimestamp { get; set; }

        public static string QuerySafeGetter(ManagementObject obj, string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj[query].ToString())) return obj[query].ToString();

                var res = (string)obj[query];
                if (!string.IsNullOrEmpty(res)) return res;
                return "N/A";
            }
            catch (Exception)
            {
                return "N/A";
            }
        }

        public static List<Win32_Defender> GetWin32_Defenders()
        {
            var win32Defenders = new List<Win32_Defender>();

            try
            {
                var wmiData = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
                var data = wmiData.Get();

                foreach (var o in data)
                {
                    var obj = (ManagementObject)o;
                    win32Defenders.Add(
                        new Win32_Defender(
                            QuerySafeGetter(obj, "instanceGuid"),
                            QuerySafeGetter(obj, "displayName"),
                            QuerySafeGetter(obj, "pathToSignedProductExe"),
                            QuerySafeGetter(obj, "pathToSignedReportingExe"),
                            QuerySafeGetter(obj, "productState"),
                            QuerySafeGetter(obj, "timestamp")
                        ));
                }

                Logs.LogWrite("Got anti-virus data successfully");
                return win32Defenders;
            }
            catch (Exception)
            {
                Logs.LogWrite("Error : anti-virus data");

                return win32Defenders;
            }
        }
    }
}