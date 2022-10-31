using System.Security.Cryptography.X509Certificates;

namespace Win32ClassModule.System;

internal class X509Cert
{
    private string Issuer;
    private string Subject;
    private string ExpirationDate;

    public string GetIssuer => Issuer;
    public string GetSubject => Subject;
    public string GetExpirationDate => ExpirationDate;

    public X509Cert(string issuer, string subject, string expirationDate)
    {
        Issuer = issuer;
        Subject = subject;
        ExpirationDate = expirationDate;
    }

    public static List<X509Cert>? GetX509Cert()
    {
        try
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var list = new List<X509Cert>();

            foreach (var certificate in store.Certificates)
                list.Add(
                    new X509Cert(
                        String.IsNullOrEmpty(certificate.Issuer)
                        ? certificate.Issuer
                        : "N/A",
                        String.IsNullOrEmpty(certificate.Subject)
                        ? certificate.Subject
                        : "N/A",
                        String.IsNullOrEmpty(certificate.GetExpirationDateString())
                        ? certificate.GetExpirationDateString()
                        : "N/A"
                    )
                ) ;

            return list;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }
}