using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using WCRC_Service;
using WCRC_Service.Reporting;

class WCRC
{
    public static Logs log;

    Thread network;
    Thread report;

    public void Report()
    {
        log = new Logs("Initialised logs");

        log.LogWrite("Initialising reporting tool");
        report = new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            WCRC_Reporting.LaunchReport();
        });
        report.Start();
        report.Join();
    }
    public void Send()
    {
        log.LogWrite("Initialising networking tool");
        network = new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            WCRC_Networking.StartUpload();
        });
        network.Start();
        network.Join();
    }

    

    
}