using System;
using System.ServiceProcess;
using System.Threading;

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
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    WCRC wCRC = new WCRC();
                    wCRC.Report();
                    wCRC.Send();
                    for(int i = 0; i < 3600; i++)
                    {
                        Thread.Sleep(10000);
                        WCRC.log.LogWrite("Launching in " + (60 * 60 * 10000 - i * 10000));
                    }
                }
            }).Start();
        }

        protected override void OnStop()
        {
            Environment.Exit(0);
        }
    }
}
