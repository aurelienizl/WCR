using System.Runtime.CompilerServices;

namespace Windows_Compliancy_Report_Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>

        public static Window? window;

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            window = new Window();
            Application.Run(window);
        }

        #region Network

        
        private static Thread? NetworkThread;
        public static void InitNetworking()
        {
            if (ReportingThread is null)
            {
                window?.Writeline("Generate report first !", false);
                return;
            }
            //Wait reporter to finished
            if (ReportingThread.IsAlive)
            {
                window?.Writeline("Please wait reporter thread to finished !", false);
                return;
            }

            if (NetworkThread is null)
            {
                NetworkThread = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Networking.UploadReport("127.0.0.1", 443);
                    window?.Writeline("Network upload job finished !", false);
                });
                NetworkThread.Start();
                window?.Writeline("Starting network upload...", false);
                return;
            }
            if (!NetworkThread.IsAlive)
            {
                NetworkThread = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Networking.UploadReport("127.0.0.1", 443);
                    window?.Writeline("Network upload job finished !", false);
                });
                NetworkThread.Start();
                window?.Writeline("Restarting network upload...", false);

            }
            window?.Writeline("Network upload already running !", false);
        }
        #endregion

        #region Report
        private static List<Win32_Bios>? bios { get; set; }
        private static List<Win32_EncryptableVolume>? win32_EncryptableVolumes { get; set; }
        private static List<Win32_Tpm>? win32_Tpm { get; set; }
        private static List<Win32_Product>? win32_Products { get; set; }
        private static List<X509Cert>? X509CertList { get; set; }
        private static List<Win32_QuickFixEngineering>? win32_QFE { get; set; }
        private static List<Account>? accounts { get; set; }
        private static SystemInfo? sysinfo { get; set; }
        private static Thread? ReportingThread;
        public static void InitReportingTool()
        {
            //Check 1 : If networking thread is running or reporting thread is running exit,
            


            //Wait reporter to finished
            if (NetworkThread is not null)
            {
                if (NetworkThread.IsAlive)
                {
                    window?.Writeline("Please wait networking thread to finished !", false);
                    return;
                }
            }

            if (ReportingThread is null)
            {
                LaunchReport();
                window?.Writeline("Starting reporting tool...", false);
                return;
            }
            if (!ReportingThread.IsAlive)
            {
                LaunchReport();
                window?.Writeline("Restarting reporting tool...", false);
                return;
            }
            window?.Writeline("Reporting tool already running !", false);

        }
        public static void LaunchReport()
        {
            ReportingThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                List<Thread> threads = new List<Thread>();

                threads.Add(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    bios = Win32_Bios.GetBios();
                    if (bios != null)
                    {
                        window?.Writeline("Bios check : OK !", false);
                    }
                    else
                    {
                        window?.Writeline("Bios check : ERROR !", false);
                    }
                }));

                threads.Add(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    win32_EncryptableVolumes = Win32_EncryptableVolume.GetEncryptableVolume();
                    
                    if (win32_EncryptableVolumes != null)
                    {
                        window?.Writeline("Volume check : OK !", false);
                    }
                    else
                    {
                        window?.Writeline("Volume check : ERROR !", false);
                    }
                }));

                threads.Add(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    win32_Tpm = Win32_Tpm.GetTpm();
                    if (win32_Tpm != null)
                    {
                        window?.Writeline("Tpm check : OK !", false);
                    }
                    else
                    {
                        window?.Writeline("Tpm check : ERROR !", false);
                    }
                }));

                threads.Add(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    win32_Products = Win32_Product.GetProducts();
                    if (win32_Products != null)
                    {
                        window?.Writeline("Softwares check : OK !", false);
                    }
                    else
                    {
                        window?.Writeline("Softwares check : ERROR !", false);
                    }
                    
                }));

                threads.Add(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    X509CertList = X509Cert.GetX509Cert();
                    
                    if (X509CertList != null)
                    {
                        window?.Writeline("Certificates check : OK !", false);
                    }
                    else
                    {
                        window?.Writeline("Certificates check : ERROR !", false);
                    }
                }));

                threads.Add(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    win32_QFE = Win32_QuickFixEngineering.GetQuickFixEngineering();
                   
                    if (win32_QFE != null)
                    {
                        window?.Writeline("Updates check : OK !", false);
                    }
                    else
                    {
                        window?.Writeline("Updates check : ERROR !", false);
                    }
                }));

                threads.Add(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    accounts = Account.GetLocalUsers();
                    
                    if (accounts != null)
                    {
                        window?.Writeline("Admins check : OK !", false);
                    }
                    else
                    {
                        window?.Writeline("Admins check : ERROR !", false);
                    }
                }));

                threads.Add(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    sysinfo = SystemInfo.GetSystemInfo();  
                    if (sysinfo != null)
                    {
                        window?.Writeline("System info check : OK !", false);
                    }
                    else
                    {
                        window?.Writeline("System info check : ERROR !", false);
                    }
                }));


                foreach (var item in threads)
                {
                    item.Start();
                }
                foreach (var item in threads)
                {
                    item.Join();
                }

                var report = new Report(
                    bios,
                    win32_EncryptableVolumes,
                    win32_Tpm,
                    win32_Products,
                    X509CertList,
                    win32_QFE,
                    accounts,
                    sysinfo
                );

                Report.GenerateReport(report);
                window?.Writeline("Reporting job finished !", false);
            });
            ReportingThread.Start();
        }
        #endregion
    }
}