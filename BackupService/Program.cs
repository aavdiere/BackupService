using System.Globalization;
#if DEBUG
    using System.Threading;
#else
    using System.ServiceProcess;
#endif

namespace BackupService {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");
#if DEBUG
            CoreService service = new CoreService();
            service.DebugStart();
            Thread.Sleep(5 * 1000);
            service.DebugStop();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new CoreService()
            };
            ServiceBase.Run(ServicesToRun);
#endif // DEBUG
        }
    }
}
