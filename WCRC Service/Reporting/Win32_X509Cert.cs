using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


internal class Win32_X509Cert
{
    public Win32_X509Cert(string issuer, string subject, string expirationDate)
    {
        GetIssuer = issuer;
        GetSubject = subject;
        GetExpirationDate = expirationDate;
    }

    public string GetIssuer { get; }

    public string GetSubject { get; }

    public string GetExpirationDate { get; }

    public static List<Win32_X509Cert> GetX509Cert()
    {
        try
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var list = new List<Win32_X509Cert>();

            foreach (var certificate in store.Certificates)
            {
                try
                {
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
                }
                catch (Exception ex)
                {
                    WCRC.log.LogWrite("Internal error on certificates...");
                    WCRC.log.LogWrite(ex.Message);

                }

            }


            return list;
        }
        catch (Exception ex)
        {
            WCRC.log.LogWrite("Internal error on certificates...");
            WCRC.log.LogWrite(ex.Message);
            return null;
        }
    }
}