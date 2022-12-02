using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCRC_Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WCRC wCRC = new WCRC();
            wCRC.Report();
            wCRC.Send();
        }
    }
}
