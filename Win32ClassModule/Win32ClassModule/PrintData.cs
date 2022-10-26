using Iedom_Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32ClassModule
{
    internal class PrintData
    {
        public static void PrintTpm(Win32_Tpm tpm)
        {
            Console.WriteLine("Tpm is activated : " + tpm.GetIsActivated_InitialValue);
            Console.WriteLine("Tpm is enabled : " + tpm.GetIsEnabled_InitialValue);
            Console.WriteLine("Tpm is owned : " + tpm.GetIsOwned_InitialValue);
            Console.WriteLine("Tpm spec version : " + tpm.GetSpecVersion);
            Console.WriteLine("Tpm Manufacturer version : " + tpm.GetManufacturerVersion);
            Console.WriteLine("Tpm Manufacturer version info : " + tpm.GetManufacturerVersionInfo);
            Console.WriteLine("Tpm Manufacturer id : " + tpm.GetManufacturerId);
            Console.WriteLine("Tpm physical presence version : " + tpm.GetPhysicalPresenceVersionInfo);
        }

        public static void PrintBios(Win32_Bios bios)
        {
            Console.WriteLine(bios.GetBiosCharacteristics);
            Console.WriteLine(bios.GetBIOSVersion);
            Console.WriteLine(bios.GetBuildNumber);
            Console.WriteLine(bios.GetCaption);
            Console.WriteLine(bios.GetCodeSet);
            Console.WriteLine(bios.GetCurrentLanguage);
            Console.WriteLine(bios.GetDescription);
            Console.WriteLine(bios.GetEmbeddedControllerMajorVersion);
            Console.WriteLine(bios.GetEmbeddedControllerMinorVersion);
            Console.WriteLine(bios.GetIdentificationCode);
            Console.WriteLine(bios.GetInstallableLanguages);
            Console.WriteLine(bios.GetInstallDate);
            Console.WriteLine(bios.GetLanguageEdition);
            Console.WriteLine(bios.GetListOfLanguages);
            Console.WriteLine(bios.GetManufacturer);
            Console.WriteLine(bios.GetName);
            Console.WriteLine(bios.GetOtherTargetOS);
            Console.WriteLine(bios.GetPrimaryBIOS);
            Console.WriteLine(bios.GetReleaseDate);
            Console.WriteLine(bios.GetSerialNumber);
            Console.WriteLine(bios.GetSMBIOSBIOSVersion);
            Console.WriteLine(bios.GetSMBIOSMajorVersion);
            Console.WriteLine(bios.GetSMBIOSMinorVersion);
            Console.WriteLine(bios.GetSMBIOSPresent);
            Console.WriteLine(bios.GetSoftwareElementID);
            Console.WriteLine(bios.GetSoftwareElementState);
            Console.WriteLine(bios.GetStatus);
            Console.WriteLine(bios.GetSystemBiosMajorVersion);
            Console.WriteLine(bios.GetSystemBiosMinorVersion);
            Console.WriteLine(bios.GetTargetOperatingSystem);
            Console.WriteLine(bios.GetVersion);

        }
    }
}
