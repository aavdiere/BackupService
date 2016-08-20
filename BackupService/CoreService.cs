using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;

using BackupService.Logging;
using BackupService.ConfigSections;
using BackupService.Handlers;

namespace BackupService {
    public partial class CoreService : ServiceBase {
        public readonly string ThreadName = "Main thread";

        private Configuration _config;
        private ServiceData _data;
        private List<Thread> _threadHandler;

        public CoreService() {
            InitializeComponent();
            Logger.Filename = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, @"BackupService.log");
        }

#if DEBUG
        public void DebugStart() {
            Logger.Debug(ThreadName, "Entered debug mode.");
            OnStart(null);
        }

        public void DebugStop() {
            Quit();
        }
#endif

        protected override void OnStart(string[] args) {
            Logger.Info(ThreadName, "Service started.");
            try {
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, @"User.config");

                _config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                Logger.Debug(ThreadName, "Configuration file loaded.");
            } catch (Exception ex) {
                Logger.Error(ThreadName, "Error while loading configuration file.");
                Logger.Error(ThreadName, ex.Message);
                Logger.StackTrace(ex.StackTrace);
                Quit();
            }
            try {
                _data = _config.GetSection("serviceData") as ServiceData;
                Logger.Debug(ThreadName, "Retrieving service data from config file.");
            } catch (ConfigurationErrorsException ex) {
                Logger.Error(ThreadName, "Error while retrieving service data from config file.");
                Logger.Error(ThreadName, ex.Message);
                Logger.StackTrace(ex.StackTrace);
                Quit();
            }
            _threadHandler = new List<Thread>(_data.BackupConfigurations.Count);
            InitBackupConfigurations();
        }

        protected override void OnStop() {
            if (_threadHandler != null) {
                Logger.Debug(ThreadName, "Joining threads.");
                _threadHandler.ForEach(thread => thread.Join());
            }

            if (_data != null) {
                Logger.Debug(ThreadName, "Saving changes to config file.");
                _data.SectionInformation.ForceSave = true;
                _config.Save(ConfigurationSaveMode.Minimal);
            }

            Logger.Info(ThreadName, "Service stopped.");
        }

        private void InitBackupConfigurations() {
            foreach (BackupConfiguration configuration in _data.BackupConfigurations) {
                Thread thr = new Thread(() => new BackupHandler(configuration));
                _threadHandler.Add(thr);
                thr.Start();
            }
        }

        private void Quit() {
            Stop();
#if DEBUG
            Logger.Debug(ThreadName, "Quited debug mode.");
#endif
            Environment.Exit(1);
        }
    }
}
