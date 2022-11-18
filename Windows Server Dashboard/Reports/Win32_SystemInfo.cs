
namespace Windows_Server_Dashboard.Reports;

internal class Win32_SystemInfo
{
    public Win32_SystemInfo(string osVersion, string biosManufacturer, string mainboardName, string cpuName,
        int totalPhysicalMemoryInMb, string gpuName, string lanIpAddress,
        string macAddress, string timeDate, string hardwareID)
    {
        GetOsVersion = osVersion;
        GetBiosManufacturer = biosManufacturer;
        GetMainboardName = mainboardName;
        GetCpuName = cpuName;
        GetTotalPhysicalMemoryInMb = totalPhysicalMemoryInMb;
        GetGpuName = gpuName;
        GetLanIpAddress = lanIpAddress;
        GetMacAddress = macAddress;
        GetTimeDate = timeDate;
        GetHardwareID = hardwareID;
    }

    public string? GetOsVersion { get; set; }

    public string? GetBiosManufacturer { get; set; }

    public string? GetMainboardName { get; set; }

    public string? GetCpuName { get; set; }

    public int? GetTotalPhysicalMemoryInMb { get; set; }

    public string? GetGpuName { get; set; }

    public string? GetLanIpAddress { get; set; }

    public string? GetMacAddress { get; set; }

    public string? GetTimeDate { get; set; }

    public string? GetHardwareID { get; set; }

}