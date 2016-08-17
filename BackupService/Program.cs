using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BackupService {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
#if DEBUG
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            CoreService service = new CoreService();
            service.DebugStart();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
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
