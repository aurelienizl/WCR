using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Management;
using Microsoft.Win32;

#pragma warning disable CA1416 // Valider la compatibilité de la plateforme


namespace Win32ClassModule.System
{
    internal class Accounts
    {
        public static void ListLocalUsers()
        {
            // Find all the information about all users...
            ManagementObjectSearcher mosAccounts =
                new ManagementObjectSearcher("SELECT * FROM Win32_Account");

            // Iterate over all those users and display it
            foreach (ManagementObject moUA in mosAccounts.Get())
            {
                Console.WriteLine(moUA["Name"]);
            }
        }

    }
}
