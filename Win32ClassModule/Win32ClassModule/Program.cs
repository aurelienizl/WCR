using Iedom_Client;

namespace Win32ClassModule;

public static class Program
{
    public static void Main()
    {
        var x = Win32_Bios.GetBios();
        PrintData.PrintBios(x);
    }
}
