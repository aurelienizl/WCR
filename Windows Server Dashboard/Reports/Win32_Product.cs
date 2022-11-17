namespace Windows_Server_Dashboard.Reports;

internal class Win32_Product
{
    public Win32_Product(ushort assignmentType, string caption, string description,
        string identifyingNumber, string installDate, string installDate2,
        string installLocation, short installState, string helpLink,
        string helpTelephone, string installSource, string language,
        string localPackage, string name, string packageCache,
        string packageCode, string packageName, string productID,
        string regOwner, string regCompany, string sKUNumber,
        string transforms, string uRLInfoAbout, string uRLUpdateInfo,
        string vendor, string version)
    {
        GetAssignmentType = assignmentType;
        GetCaption = caption;
        GetDescription = description;
        GetIdentifyingNumber = identifyingNumber;
        GetInstallDate = installDate;
        GetInstallDate2 = installDate2;
        GetInstallLocation = installLocation;
        GetInstallState = installState;
        GetHelpLink = helpLink;
        GetHelpTelephone = helpTelephone;
        GetInstallSource = installSource;
        GetLanguage = language;
        GetLocalPackage = localPackage;
        GetName = name;
        GetPackageCache = packageCache;
        GetPackageCode = packageCode;
        GetPackageName = packageName;
        GetProductID = productID;
        GetRegOwner = regOwner;
        GetRegCompany = regCompany;
        GetSKUNumber = sKUNumber;
        GetTransforms = transforms;
        GetURLInfoAbout = uRLInfoAbout;
        GetURLUpdateInfo = uRLUpdateInfo;
        GetVendor = vendor;

        GetVersion = version;
    }

    public ushort? GetAssignmentType { get; }

    public string? GetCaption { get; }

    public string? GetDescription { get; }

    public string? GetIdentifyingNumber { get; }

    public string? GetInstallDate { get; }

    public string? GetInstallDate2 { get; }

    public string? GetInstallLocation { get; }

    public short? GetInstallState { get; }

    public string? GetHelpLink { get; }
        
    public string? GetHelpTelephone { get; }

    public string? GetInstallSource { get; }

    public string? GetLanguage { get; }

    public string? GetLocalPackage { get; }

    public string? GetName { get; }

    public string? GetPackageCache { get; }

    public string? GetPackageCode { get; }

    public string? GetPackageName { get; }

    public string? GetProductID { get; }

    public string? GetRegOwner { get; }

    public string? GetRegCompany { get; }

    public string? GetSKUNumber { get; }

    public string? GetTransforms { get; }

    public string? GetURLInfoAbout { get; }

    public string? GetURLUpdateInfo { get; }

    public string? GetVendor { get; }

    public string? GetVersion { get; }
}