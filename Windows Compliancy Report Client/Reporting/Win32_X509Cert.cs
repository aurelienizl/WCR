using System.Security.Cryptography.X509Certificates;

namespace Windows_Compliancy_Report_Client.Reporting;

internal class Win32_X509Cert
{
    public Win32_X509Cert(string issuer, string subject, string expirationDate)
    {
        GetIssuer = issuer;
        GetSubject = subject;
        GetExpirationDate = expirationDate;
    }

    public string? GetIssuer { get; }

    public string? GetSubject { get; }

    public string? GetExpirationDate { get; }

    public static List<Win32_X509Cert>? GetX509Cert()
    {
        try
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var list = new List<Win32_X509Cert>();

            foreach (var certificate in store.Certificates)
                list.Add(
                    new Win32_X509Cert(
                        !string.IsNullOrEmpty(certificate.Issuer)
                            ? certificate.Issuer
                            : "N/A",
                        !string.IsNullOrEmpty(certificate.Subject)
                            ? certificate.Subject
                            : "N/A",
                        !string.IsNullOrEmpty(certificate.GetExpirationDateString())
                            ? certificate.GetExpirationDateString()
                            : "N/A"
                    )
                );


            return list;
        }
        catch (Exception e)
        {
            Program.window?.Writeline("Certificates exception : \n" + e.Message, false);
            return null;
        }
    }
}