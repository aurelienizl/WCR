namespace Windows_Server_Dashboard.Reports;

internal class Win32_Tpm
{
    public Win32_Tpm(bool isActivated_InitialValue, bool isEnabled_InitialValue,
        bool isOwned_InitialValue, string specVersion, string manufacturerVersion,
        string manufacturerVersionInfo, uint manufacturerId, string physicalPresenceVersionInfo)
    {
        GetIsActivated_InitialValue = isActivated_InitialValue;
        GetIsEnabled_InitialValue = isEnabled_InitialValue;
        GetIsOwned_InitialValue = isOwned_InitialValue;
        GetSpecVersion = specVersion;
        GetManufacturerVersion = manufacturerVersion;
        GetManufacturerVersionInfo = manufacturerVersionInfo;
        GetManufacturerId = manufacturerId;
        GetPhysicalPresenceVersionInfo = physicalPresenceVersionInfo;
    }

    public bool? GetIsActivated_InitialValue { get; set; }

    public bool? GetIsEnabled_InitialValue { get; set; }

    public bool? GetIsOwned_InitialValue { get; set; }

    public string? GetSpecVersion { get; set; }

    public string? GetManufacturerVersion { get; set; }

    public string? GetManufacturerVersionInfo { get; set; }

    public uint? GetManufacturerId { get; set; }

    public string? GetPhysicalPresenceVersionInfo { get; set; }

}