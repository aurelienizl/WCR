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

    public bool? GetIsActivated_InitialValue { get; }

    public bool? GetIsEnabled_InitialValue { get; }

    public bool? GetIsOwned_InitialValue { get; }

    public string? GetSpecVersion { get; }

    public string? GetManufacturerVersion { get; }

    public string? GetManufacturerVersionInfo { get; }

    public uint? GetManufacturerId { get; }

    public string? GetPhysicalPresenceVersionInfo { get; }

}