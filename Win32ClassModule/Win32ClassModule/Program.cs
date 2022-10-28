using Iedom_Client;

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
    }
}