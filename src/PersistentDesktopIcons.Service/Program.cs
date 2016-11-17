using System.ServiceProcess;

namespace PersistentDesktopIcons.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
#if DEBUG
            var service = new PersistentDesktopIconsService();
            service.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new PersistentDesktopIconsService()
            };
            ServiceBase.Run(ServicesToRun);

#endif
        }
    }
}