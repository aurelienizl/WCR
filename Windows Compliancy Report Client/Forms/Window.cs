namespace Windows_Compliancy_Report_Client
{
    public partial class Window : Form
    {
        public Window()
        {
            InitializeComponent();
        }

        public void Writeline(string line, bool reset = false)
        {
            
            if (reset)
            {
                Invoke((Delegate)(() =>
                {
                    listBox1.Items.Clear();
                }));
            }
            Invoke((Delegate)(() =>
            {
                listBox1.Items.Add(line);
            }));
            
        }

        private void Launch_Click(object sender, EventArgs e)
        {
            Program.InitReportingTool();
        }

        private void Upload_Click(object sender, EventArgs e)
        {
            Program.InitNetworking();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}