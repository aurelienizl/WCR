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
            foreach(var elt in bios.GetBiosCharacteristics) Console.WriteLine("Bios characteristics " + elt);
            foreach (var elt in bios.GetBIOSVersion) Console.WriteLine("Bios versions : " + elt);
            Console.WriteLine("Build number : " + bios.GetBuildNumber);
            Console.WriteLine("Caption : " + bios.GetCaption);
            Console.WriteLine("Code set : " + bios.GetCodeSet);
            Console.WriteLine("Current language : " + bios.GetCurrentLanguage);
            Console.WriteLine("Descriptions : " + bios.GetDescription);
            Console.WriteLine("Embedded controller major version : " + bios.GetEmbeddedControllerMajorVersion);
            Console.WriteLine("Embedded controller minor version : " + bios.GetEmbeddedControllerMinorVersion);
            Console.WriteLine("Identification code : " + bios.GetIdentificationCode);
            Console.WriteLine("Installable language : " + bios.GetInstallableLanguages);
            Console.WriteLine("Installation date : " + bios.GetInstallDate);
            Console.WriteLine("Language edition : " + bios.GetLanguageEdition);
            foreach(var elt in bios.GetListOfLanguages) Console.WriteLine("Installed languages : " + elt);
            Console.WriteLine("Manifacturer : " + bios.GetManufacturer);
            Console.WriteLine("Name : " + bios.GetName);
            Console.WriteLine("Others target os : " + bios.GetOtherTargetOS);
            Console.WriteLine("Primary bios : " + bios.GetPrimaryBIOS);
            Console.WriteLine("Release date : " + bios.GetReleaseDate);
            Console.WriteLine("Serial number : " + bios.GetSerialNumber);
            Console.WriteLine("Bios version : " + bios.GetSMBIOSBIOSVersion);
            Console.WriteLine("Bios major version : " + bios.GetSMBIOSMajorVersion);
            Console.WriteLine("Bios minor version : " + bios.GetSMBIOSMinorVersion);
            Console.WriteLine("Bios is present : " + bios.GetSMBIOSPresent);
            Console.WriteLine("Software element ID " + bios.GetSoftwareElementID);
            Console.WriteLine("Software element state : " + bios.GetSoftwareElementState);
            Console.WriteLine("Bios status : " + bios.GetStatus);
            Console.WriteLine("Bios system major version : " + bios.GetSystemBiosMajorVersion);
            Console.WriteLine("Bios system minor version : " + bios.GetSystemBiosMinorVersion);
            Console.WriteLine("Target operating system : " + bios.GetTargetOperatingSystem);
            Console.WriteLine("Version : " + bios.GetVersion);

        }
    }
}
