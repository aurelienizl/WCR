using System;
using System.Collections.Generic;
using System.Management;
using WCRC_Service.Modules;

namespace WCRC_Service.Reporting
{
    internal class Win32_Defender
    {
        public string GetinstanceGuid { get; set; }
        public string GetdisplayName { get; set; }
        public string GetpathToSignedProductExe { get; set; }
        public string GetpathToSignedReportingExe { get; set; }
        public string GetproductState { get; set; }
        public string Gettimestamp { get; set; }

        public Win32_Defender(string getinstanceGuid, string getdisplayName, string getpathToSignedProductExe, string getpathToSignedReportingExe, string getproductState, string gettimestamp)
        {
            GetinstanceGuid = getinstanceGuid;
            GetdisplayName = getdisplayName;
            GetpathToSignedProductExe = getpathToSignedProductExe;
            GetpathToSignedReportingExe = getpathToSignedReportingExe;
            GetproductState = getproductState;
            Gettimestamp = gettimestamp;
        }

        public static string QuerySafeGetter(ManagementObject obj, string query)
        {
            try
            {
                if (!String.IsNullOrEmpty(obj[query].ToString()))
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

        public static List<Win32_Defender> GetWin32_Defenders()
        {
            List<Win32_Defender> win32Defenders = new List<Win32_Defender>();

            try
            {

                ManagementObjectSearcher wmiData = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
                ManagementObjectCollection data = wmiData.Get();

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
