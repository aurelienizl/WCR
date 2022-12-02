using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WCRC_Service
{
    public partial class ServiceWCRC : ServiceBase
    {
        public ServiceWCRC()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WCRC wCRC = new WCRC();
            wCRC.Report();
            wCRC.Send();
            
        }

        protected override void OnStop()
        {
            
        }
    }
}
