namespace Windows_Compliancy_Report_Client
{
    public partial class Window : Form
    {
        public Window()
        {
            InitializeComponent();
        }

        #region ListBox
        public void Writeline(string line, bool reset)
        {
            if (Program.window is not null)
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


        }
        #endregion

        #region WindowEvents

        private void Window_Load(object sender, EventArgs e)
        {
            Program.Automatic_Launch();
        }

        private void Window_Shown(object sender, EventArgs e)
        {
            resetForm();
            notifyIcon.Visible = true;
            this.Hide();
        }

        #endregion   

        #region Buttons
        private void Launch_Click(object sender, EventArgs e)
        {
            Program.InitReportingTool();
        }

        private void Upload_Click(object sender, EventArgs e)
        {
            Program.InitNetworking();
        }

        public static bool exit = false;

        private void Exit_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Program.ExitApp();
                Thread.Sleep(2000);
                Application.Exit();
            }).Start();

        }
        #endregion

        #region Notifyicons

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            resetForm();
            Show();

            notifyIcon.Visible = false;
        }

        private void resetForm()
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width + 10, Screen.PrimaryScreen.Bounds.Height - this.Height - 40);
            this.Size = new Size(275, 330);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                resetForm();
                notifyIcon.Visible = true;
                this.Hide();
                e.Cancel = true;
            }

        }

        #endregion



    }
}