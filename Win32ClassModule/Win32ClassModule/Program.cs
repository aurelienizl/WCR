using Win32ClassModule.System;
using Win32ClassModule.Win32_Class;

namespace Win32ClassModule;

public static class Program
{
    public static void Main()
    {
        
        var bios = Win32_Bios.GetBios();
        PrintData.PrintBios(bios);

        var win32_EncryptableVolumes =
            Win32_EncryptableVolume.GetEncryptableVolume();
        PrintData.PrintEncryptableVolume(win32_EncryptableVolumes);

        var win32_Tpm = Win32_Tpm.GetTpm();
        PrintData.PrintTpm(win32_Tpm);

        var win32_Products = Win32_Product.GetProducts();
        PrintData.PrintProducts(win32_Products);

        var X509CertList = X509Cert.GetX509Cert();
        PrintData.PrintCerts(X509CertList);
        
        var win32_QFE = Win32_QuickFixEngineering.GetQuickFixEngineering();
        PrintData.printQFE(win32_QFE);
        

        var x = Account.GetLocalUsers();
        PrintData.PrintAccounts(x);
    }
}