using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsReportingClient
{
    public interface IHostService
    {
        string GetHostIP();

        bool IsConnected(string ip);
    }
}
