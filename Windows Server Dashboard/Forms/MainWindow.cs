using Windows_Server_Dashboard.Reports;

namespace Windows_Server_Dashboard
{
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
#pragma warning disable CS8601 // Existence possible d'une assignation de référence null.


    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            foreach(var report in Program.Reports!)
            {
                string[] data = new string[]
                {
                    report.Win32_QuickFixEngineerings[0].GetCSName,
                    report.Win32_SystemInfo.GetOsVersion,
                    report.Win32_Bios[0].GetSMBIOSBIOSVersion,
                    report.Win32_Tpms[0].GetIsActivated_InitialValue.ToString(),
                    report.Win32_EncryptableVolumes[0].GetProtectionStatus.ToString(),
                    report.Win32_SystemInfo.GetLanIpAddress,
                    report.Win32_SystemInfo.GetMacAddress,
                    report.Win32_SystemInfo.GetHardwareID, 
                    report.Win32_SystemInfo.GetTimeDate


                };
                dataGridView1.Rows.Add(data);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}