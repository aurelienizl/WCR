using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Server_Dashboard.Database
{
    internal class Database
    {
        public static void InitializeDatabase()
        {
            CheckFolders();
            Parser.ReadFiles();
        }

        public static void CheckFolders()
        {
            string DriveLetter = Program.DataFolder;

            if (!Directory.Exists(DriveLetter + "reports"))
            {
                Directory.CreateDirectory(DriveLetter + "reports");
            }
            if (!Directory.Exists(DriveLetter + "database"))
            {
                Directory.CreateDirectory(DriveLetter + "database");
            }
            //Create database folders
            if (!Directory.Exists(DriveLetter + @"database\accounts"))
            {
                Directory.CreateDirectory(DriveLetter + @"database\accounts");
            }
            if (!Directory.Exists(DriveLetter + @"database\bios"))
            {
                Directory.CreateDirectory(DriveLetter + @"database\bios");
            }
            if (!Directory.Exists(DriveLetter + @"database\encryptable_volumes"))
            {
                Directory.CreateDirectory(DriveLetter + @"database\encryptable_volumes");
            }
            if (!Directory.Exists(DriveLetter + @"database\products"))
            {
                Directory.CreateDirectory(DriveLetter + @"database\products");
            }
            if (!Directory.Exists(DriveLetter + @"database\qfe"))
            {
                Directory.CreateDirectory(DriveLetter + @"database\qfe");
            }
            if (!Directory.Exists(DriveLetter + @"database\startup"))
            {
                Directory.CreateDirectory(DriveLetter + @"database\startup");
            }
            if (!Directory.Exists(DriveLetter + @"database\sysinfo"))
            {
                Directory.CreateDirectory(DriveLetter + @"database\sysinfo");
            }
            if (!Directory.Exists(DriveLetter + @"database\tpm"))
            {
                Directory.CreateDirectory(DriveLetter + @"database\tpm");
            }
            if (!Directory.Exists(DriveLetter + @"database\x509crt"))
            {
                Directory.CreateDirectory(DriveLetter + @"database\x509crt");
            }
        }

        public static void UpdateDatabase()
        {
            throw new NotImplementedException();

        }
    }
}
