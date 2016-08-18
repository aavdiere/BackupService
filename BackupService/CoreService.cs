using System;
using System.Configuration;
using System.ServiceProcess;

using BackupService.ConfigSections;

namespace BackupService {
    public partial class CoreService : ServiceBase {
        private Configuration _config;

        public CoreService() {
            InitializeComponent();
        }

#if DEBUG
        public void DebugStart() {
            // TODO: add log
            OnStart(null);
        }
#endif

        protected override void OnStart(string[] args) {
            try {
                _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var s = (ServiceData)_config.GetSection("serviceData");
                var t = s.BackupConfigurations;
                foreach (var u in t) {
                    var v = u.Folders;
                    foreach (var w in v) {
                        System.Console.WriteLine(String.Format("{0}{1}", u.General.BasePath.Path, w.Path));
                    }
                }
            } catch (System.Exception ex) {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.StackTrace);
            }
        }

        protected override void OnStop() {
        }
    }
}
