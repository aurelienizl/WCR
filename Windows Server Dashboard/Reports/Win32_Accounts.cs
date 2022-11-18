namespace Windows_Server_Dashboard.Reports;

internal class Win32_Accounts
{
    public Win32_Accounts(string name, string authenticationStatus, string guid)
    {
        GetName = name;
        GetAuthenticationType = authenticationStatus;
        GetGuid = guid;
    }

    public string? GetName { get; set; }

    public string? GetAuthenticationType { get; set; }

    public string? GetGuid { get; set; }

}