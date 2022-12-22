using System.ServiceProcess;

namespace WCRC_Service
{
    internal static class Program
    {
        /// <summary>
        ///     Point d'entrée principal de l'application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ServiceWCRC()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}