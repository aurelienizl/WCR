using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using Windows_Server_Dashboard.Reports;

namespace Windows_Server_Dashboard
{
    internal static class Program
    {
        public static string DataFolder = @"C:\WCRS\";

        public static List<Win32_Report>? Reports;
        public static MainWindow? MainWindowUI;

        [STAThread]
        static void Main()
        {
            Database.Database.InitializeDatabase();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            MainWindowUI = new MainWindow();
            Application.Run(MainWindowUI);
        }


        

    }
}