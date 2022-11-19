using System.Data;
using System.Security.Principal;
using Windows_Server_Dashboard.Reports;


namespace Windows_Server_Dashboard.Database
{
    internal class Convert
    {
        public static void ConvertReport(Win32_Report report)
        {
            string path = Program.DataFolder + @"database\";
            string filename = report.Win32_QuickFixEngineerings![0].GetCSName!;

            ExportAccounts(report.Win32_Accounts!, path, filename);
            ExportBios(report.Win32_Bios!, path, filename);
            ExportEncryptableVolumes(report.Win32_EncryptableVolumes!, path, filename);
            ExportProducts(report.Win32_Products!, path, filename);
            ExportQFE(report.Win32_QuickFixEngineerings!, path, filename);
            ExportStartup(report.Win32_Startup!, path, filename);
            ExportSysinfo(report.Win32_SystemInfo!, path, filename);
            ExportTpm(report.Win32_Tpms!, path, filename);
            ExportX509Cert(report.Win32_X509Certs!, path, filename);
        }

        public static void ExportAccounts(List<Win32_Accounts> accounts, string path, string filename)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Accounts";
            foreach (var prop in accounts[0].GetType().GetProperties())
            {
                DataColumn column = new DataColumn(prop.Name.Substring(3), typeof(string));
                dataTable.Columns.Add(column);
            }
            foreach (var account in accounts)
            {
                dataTable.Rows.Add(account.GetName!, account.GetGuid!, account.GetAuthenticationType!);
            }
            dataTable.WriteXml(path + @"accounts\" + filename + ".xml");
        }

        public static void ExportBios(List<Win32_Bios> bios, string path, string filename)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Bios";
            foreach (var prop in bios[0].GetType().GetProperties())
            {
                DataColumn column = new DataColumn(prop.Name.Substring(3), typeof(string));
                dataTable.Columns.Add(column);
            }
            foreach (var tmp in bios)
            {
                dataTable.Rows.Add(
                    String.Join(", ", tmp.GetBiosCharacteristics!.ToArray()),
                    String.Join(", ", tmp.GetBIOSVersion!.ToArray()),
                    tmp.GetBuildNumber,
                    tmp.GetCaption,
                    tmp.GetCodeSet,
                    tmp.GetCurrentLanguage,
                    tmp.GetDescription,
                    tmp.GetEmbeddedControllerMajorVersion,
                    tmp.GetEmbeddedControllerMinorVersion,
                    tmp.GetIdentificationCode,
                    tmp.GetInstallableLanguages,
                    tmp.GetInstallDate,
                    tmp.GetLanguageEdition,
                    String.Join(", ", tmp.GetListOfLanguages!.ToArray()),
                    tmp.GetManufacturer,
                    tmp.GetName,
                    tmp.GetOtherTargetOS,
                    tmp.GetPrimaryBIOS,
                    tmp.GetReleaseDate,
                    tmp.GetSerialNumber,
                    tmp.GetSMBIOSBIOSVersion,
                    tmp.GetSMBIOSMajorVersion,
                    tmp.GetSMBIOSMinorVersion,
                    tmp.GetSMBIOSPresent,
                    tmp.GetSoftwareElementID,
                    tmp.GetSoftwareElementState,
                    tmp.GetStatus,
                    tmp.GetSystemBiosMajorVersion,
                    tmp.GetSystemBiosMinorVersion,
                    tmp.GetTargetOperatingSystem,
                    tmp.GetVersion
                    );
            }
            dataTable.WriteXml(path + @"bios\" + filename + ".xml");
        }

        public static void ExportEncryptableVolumes(List<Win32_EncryptableVolume> volume, string path, string filename)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Encryptable_Volumes";
            foreach (var prop in volume[0].GetType().GetProperties())
            {
                DataColumn column = new DataColumn(prop.Name.Substring(3), typeof(string));
                dataTable.Columns.Add(column);
            }
            foreach (var tmp in volume)
            {
                dataTable.Rows.Add(
                    tmp.GetDeviceID,
                    tmp.GetPersistentVolumeID,
                    tmp.GetDriveLetter,
                    tmp.GetProtectionStatus
                    );
            }
            dataTable.WriteXml(path + @"encryptable_volumes\" + filename + ".xml");

        }

        public static void ExportProducts(List<Win32_Product> products, string path, string filename)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Products";
            foreach (var prop in products[0].GetType().GetProperties())
            {
                DataColumn column = new DataColumn(prop.Name.Substring(3), typeof(string));
                dataTable.Columns.Add(column);
            }
            foreach (var tmp in products)
            {
                dataTable.Rows.Add(
                    tmp.GetAssignmentType,
                    tmp.GetCaption,
                    tmp.GetDescription,
                    tmp.GetIdentifyingNumber,
                    tmp.GetInstallDate,
                    tmp.GetInstallDate2,
                    tmp.GetInstallLocation,
                    tmp.GetInstallState,
                    tmp.GetHelpLink,
                    tmp.GetHelpTelephone,
                    tmp.GetInstallSource,
                    tmp.GetLanguage,
                    tmp.GetLocalPackage,
                    tmp.GetName,
                    tmp.GetPackageCache,
                    tmp.GetPackageCode,
                    tmp.GetPackageName,
                    tmp.GetProductID,
                    tmp.GetRegOwner,
                    tmp.GetRegCompany,
                    tmp.GetSKUNumber,
                    tmp.GetTransforms,
                    tmp.GetURLInfoAbout,
                    tmp.GetURLUpdateInfo,
                    tmp.GetVendor,
                    tmp.GetVersion
                    );
            }
            dataTable.WriteXml(path + @"products\" + filename + ".xml");

        }

        public static void ExportQFE(List<Win32_QuickFixEngineering> win32_QuickFixEngineering, string path, string filename)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "qfe";
            foreach (var prop in win32_QuickFixEngineering[0].GetType().GetProperties())
            {
                DataColumn column = new DataColumn(prop.Name.Substring(3), typeof(string));
                dataTable.Columns.Add(column);
            }
            foreach (var tmp in win32_QuickFixEngineering)
            {
                dataTable.Rows.Add(
                    tmp.GetCaption,
                    tmp.GetDescription,
                    tmp.GetInstallDate,
                    tmp.GetName,
                    tmp.GetStatus,
                    tmp.GetCSName,
                    tmp.GetFixComments,
                    tmp.GetHotFixID,
                    tmp.GetInstalledBy,
                    tmp.GetInstalledOn,
                    tmp.GetServicePackInEffect
                    );
            }
            dataTable.WriteXml(path + @"qfe\" + filename + ".xml");

        }

        public static void ExportStartup(List<Win32_Startup> win32_Startup, string path, string filename)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Startup";
            foreach (var prop in win32_Startup[0].GetType().GetProperties())
            {
                DataColumn column = new DataColumn(prop.Name.Substring(3), typeof(string));
                dataTable.Columns.Add(column);
            }
            foreach (var tmp in win32_Startup)
            {
                dataTable.Rows.Add(
                    tmp.Name
                    );
            }
            dataTable.WriteXml(path + @"startup\" + filename + ".xml");

        }

        public static void ExportSysinfo(Win32_SystemInfo win32_SystemInfo, string path, string filename)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Sysinfo";
            foreach (var prop in win32_SystemInfo.GetType().GetProperties())
            {
                DataColumn column = new DataColumn(prop.Name.Substring(3), typeof(string));
                dataTable.Columns.Add(column);
            }

            dataTable.Rows.Add(
                win32_SystemInfo.GetOsVersion,
                win32_SystemInfo.GetBiosManufacturer,
                win32_SystemInfo.GetMainboardName,
                win32_SystemInfo.GetCpuName,
                win32_SystemInfo.GetTotalPhysicalMemoryInMb,
                win32_SystemInfo.GetGpuName,
                win32_SystemInfo.GetLanIpAddress,
                win32_SystemInfo.GetMacAddress,
                win32_SystemInfo.GetTimeDate,
                win32_SystemInfo.GetHardwareID
                );

            dataTable.WriteXml(path + @"sysinfo\" + filename + ".xml");
        }

        public static void ExportTpm(List<Win32_Tpm> win32_Tpm, string path, string filename)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Tpm";
            foreach (var prop in win32_Tpm[0].GetType().GetProperties())
            {
                DataColumn column = new DataColumn(prop.Name.Substring(3), typeof(string));
                dataTable.Columns.Add(column);
            }
            foreach (var tmp in win32_Tpm)
            {
                dataTable.Rows.Add(
                    tmp.GetIsActivated_InitialValue,
                    tmp.GetIsEnabled_InitialValue,
                    tmp.GetIsOwned_InitialValue,
                    tmp.GetSpecVersion,
                    tmp.GetManufacturerVersion,
                    tmp.GetManufacturerVersionInfo,
                    tmp.GetManufacturerId,
                    tmp.GetPhysicalPresenceVersionInfo
                    );
            }
            dataTable.WriteXml(path + @"tpm\" + filename + ".xml");
        }

        public static void ExportX509Cert(List<Win32_X509Cert> win32_X509Cert, string path, string filename)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "X509Cert";
            foreach (var prop in win32_X509Cert[0].GetType().GetProperties())
            {
                DataColumn column = new DataColumn(prop.Name.Substring(3), typeof(string));
                dataTable.Columns.Add(column);
            }
            foreach (var tmp in win32_X509Cert)
            {
                dataTable.Rows.Add(
                    tmp.GetIssuer,
                    tmp.GetSubject,
                    tmp.GetExpirationDate
                    );
            }
            dataTable.WriteXml(path + @"x509crt\" + filename + ".xml");
        }
    }
}
