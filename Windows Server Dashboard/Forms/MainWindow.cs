using System.Data;
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
            InitMainWindow();

        }
 
        private void InitMainWindow()
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(@"C:\WCRS\database\sysinfo\L90-L02.xml");
            DataTable dt1 = dataSet.Tables[0];

            DataSet dataSet1 = new DataSet();
            dataSet1.ReadXml(@"C:\WCRS\database\sysinfo\L90-L19.xml");
            DataTable dt2 = dataSet.Tables[0];
            dt1.Merge(dt2);
            dataGridView1.DataSource = dt1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}