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
                    for(int i = 0; i< 60 * 60; i++)
                    {
                        Thread.Sleep(6000);
                        WCRC.log.LogWrite("Launching in " + (60 * 60 * 6000 - i * 6000));
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
