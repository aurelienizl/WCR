using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Compliancy_Report_Server
{
    public interface IHostService
    {
        string GetHostIP();

        bool IsConnected(string ip);
    }
}
