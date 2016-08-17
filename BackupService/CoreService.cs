using System.Configuration;
using System.ServiceProcess;

namespace BackupService {
    public partial class CoreService : ServiceBase {
        private Configuration _config;

        public CoreService() {
            InitializeComponent();

            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

#if DEBUG
        public void DebugStart() {
            // TODO: add log
            OnStart(null);
        }
#endif

        protected override void OnStart(string[] args) {
            var t = _config.GetSectionGroup("folderGroup");

        }

        protected override void OnStop() {
        }
    }
}
