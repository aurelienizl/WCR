
namespace Windows_Server_Dashboard.Reports;

internal class Win32_X509Cert
{
    public Win32_X509Cert(string issuer, string subject, string expirationDate)
    {
        GetIssuer = issuer;
        GetSubject = subject;
        GetExpirationDate = expirationDate;
    }

    public string? GetIssuer { get; set; }

    public string? GetSubject { get; set; }

    public string? GetExpirationDate { get; set; }


}