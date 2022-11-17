
namespace Windows_Server_Dashboard.Reports;

internal class X509Cert
{
    public X509Cert(string issuer, string subject, string expirationDate)
    {
        GetIssuer = issuer;
        GetSubject = subject;
        GetExpirationDate = expirationDate;
    }

    public string? GetIssuer { get; }

    public string? GetSubject { get; }

    public string? GetExpirationDate { get; }

     
}