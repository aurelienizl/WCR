using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Windows_COmpliancy_Report_Service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        Process p;

        protected override void OnStart(string[] args)
        {
            if(p == null)
            {
                p = Process.Start(@"C:\Program Files (x86)\IEDOM-IEOM\WCRC\WCRC.exe");
                p.Start();
            }
            if(p.HasExited)
            {
                p = Process.Start(@"C:\Program Files (x86)\IEDOM-IEOM\WCRC\WCRC.exe");
                p.Start();
            }
        }

        protected override void OnStop()
        {
            foreach (Process Proc in Process.GetProcesses())
            {
                if (Proc.ProcessName.Equals("WCRC"))
                {
                    Proc.Kill();
                }
            }
        }
    }
}
