namespace Windows_Server_Dashboard.Reports;

internal class Account
{
    public Account(string name, string authenticationStatus, string guid)
    {
        GetName = name;
        GetAuthenticationType = authenticationStatus;
        GetGuid = guid;
    }

    public string? GetName { get; }

    public string? GetAuthenticationType { get; }

    public string? GetGuid { get; }

}