using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Windows_Server_Dashboard.Reports;


namespace Windows_Server_Dashboard.Database
{
    internal class Parser
    {
        public static void ReadFiles()
        {
            string path = Program.DataFolder + @"reports\";

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
                            if (CheckJsonFile(report, files))
                            {
                                Convert.ConvertReport(report);
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
            foreach (var prop in props)
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
