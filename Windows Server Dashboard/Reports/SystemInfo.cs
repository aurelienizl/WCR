
namespace Windows_Server_Dashboard.Reports;

internal class SystemInfo
{
    public SystemInfo(string osVersion, string biosManufacturer, string mainboardName, string cpuName,
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

    public string? GetOsVersion { get; }

    public string? GetBiosManufacturer { get; }

    public string? GetMainboardName { get; }

    public string? GetCpuName { get; }

    public int? GetTotalPhysicalMemoryInMb { get; }

    public string? GetGpuName { get; }

    public string? GetLanIpAddress { get; }

    public string? GetMacAddress { get; }

    public string? GetTimeDate { get; }

    public string? GetHardwareID { get; }

}