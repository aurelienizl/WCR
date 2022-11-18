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

    public ushort? GetAssignmentType { get; set; }

    public string? GetCaption { get; set; }

    public string? GetDescription { get; set; }

    public string? GetIdentifyingNumber { get; set; }

    public string? GetInstallDate { get; set; }

    public string? GetInstallDate2 { get; set; }

    public string? GetInstallLocation { get; set; }

    public short? GetInstallState { get; set; }

    public string? GetHelpLink { get; set; }

    public string? GetHelpTelephone { get; set; }

    public string? GetInstallSource { get; set; }

    public string? GetLanguage { get; set; }

    public string? GetLocalPackage{ get; set; }

    public string? GetName { get; set; }

    public string? GetPackageCache { get; set; }

    public string? GetPackageCode { get; set; }

    public string? GetPackageName { get; set; }

    public string? GetProductID { get; set; }

    public string? GetRegOwner { get; set; }

    public string? GetRegCompany { get; set; }

    public string? GetSKUNumber { get; set; }

    public string? GetTransforms { get; set; }

    public string? GetURLInfoAbout { get; set; }

    public string? GetURLUpdateInfo { get; set; }

    public string? GetVendor { get; set; }

    public string? GetVersion { get; set; }
}