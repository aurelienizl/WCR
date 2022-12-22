using System.Threading;
using WCRC_Service.Modules;

internal class WCRC
{
    public static Logs Log;

    private Thread _network;
    private Thread _report;

    public void Report()
    {
        Log = new Logs("Initialised logs");

        Logs.LogWrite("Initialising reporting tool");
        _report = new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            WCRC_Reporting.LaunchReport();
        });
        _report.Start();
        _report.Join();
    }

    public void Send()
    {
        Logs.LogWrite("Initialising networking tool");
        _network = new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            WCRC_Networking.StartUpload();
        });
        _network.Start();
        _network.Join();
    }
}