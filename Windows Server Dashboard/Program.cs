using Windows_Server_Dashboard.Reports;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Windows_Server_Dashboard
{
    internal static class Program
    {

        public static List<Win32_Report>? Reports;
        public static MainWindow? MainWindowUI;

        [STAThread]
        static void Main()
        {
            ReadFiles(@"C:\reports");
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            MainWindowUI = new MainWindow();
            Application.Run(MainWindowUI);
        }

        public static void ReadFiles(string path)
        {
            Reports = new List<Win32_Report>();
            foreach (var files in Directory.GetFiles(path))
            {
                if (files.Contains(".json"))
                {
                    try
                    {
                        var source = File.ReadAllText(files);
                        dynamic data = JObject.Parse(source);
                        var d = JsonConvert.SerializeObject(data);

                        Win32_Report? report = JsonConvert.DeserializeObject<Win32_Report>(d);

                        if (report is not null)
                        {
                            if(CheckJsonFile(report, files))
                            {
                                Reports.Add(report);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error parsing " + files + "\n" + e.Message);
                    }
                }
            }
        }

        public static bool CheckJsonFile(Win32_Report report, string file)
        {
            Type type = report.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach(var prop in props)
            {
                if (prop.GetValue(report) == null)
                {
                    MessageBox.Show("Error parsing " + file);
                    return false;
                }
            }
            return true;
        }
    }
}