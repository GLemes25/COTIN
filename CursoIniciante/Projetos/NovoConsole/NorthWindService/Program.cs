using System.ServiceProcess;

namespace NorthWindService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ServiceNorthWind()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
