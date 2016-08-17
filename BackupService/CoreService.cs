using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BackupService {
    public partial class CoreService : ServiceBase {
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
        }

        protected override void OnStop() {
        }
    }
}
