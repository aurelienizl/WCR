
namespace Windows_Server_Dashboard.Reports;

internal class Win32_EncryptableVolume
{
    public Win32_EncryptableVolume(string deviceID, string persistentVolumeID, string driveLetter,
        uint protectionStatus)
    {
        GetDeviceID = deviceID;
        GetPersistentVolumeID = persistentVolumeID;
        GetDriveLetter = driveLetter;
        GetProtectionStatus = protectionStatus;
    }

    public string? GetDeviceID { get; set; }

    public string? GetPersistentVolumeID { get; set; }

    public string? GetDriveLetter { get; set; }

    public uint? GetProtectionStatus { get; set; }


}