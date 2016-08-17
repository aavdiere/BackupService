using System;
using System.Configuration;

namespace BackupService.ConfigSections {
    #region Sections

    public class General : ConfigurationSection {
        /// <summary>
        /// The name of this section in the App.config
        /// </summary>
        public const string SectionName = "general";
        
        private General() { }

        [ConfigurationProperty("basePath")]
        public BasePath BasePath {
            get { return (BasePath)this["basePath"]; }
        }

        [ConfigurationProperty("lastBackup")]
        public LastBackup LastBackup {
            get { return (LastBackup)this["lastBackup"]; }
        }

        [ConfigurationProperty("interval")]
        public Interval Interval {
            get { return (Interval)this["lastBackup"]; }
        }
    }

    public class BackupFolders : ConfigurationSection {
        /// <summary>
        /// The name of this section in the App.config
        /// </summary>
        public const string SectionName = "backupFolders";

        private BackupFolders() { }

        [ConfigurationProperty("folders")]
        public FoldersCollection Folders {
            get { return (FoldersCollection)base["folders"]; }
        }
    }

    #endregion

    #region General

    public class BasePath : ConfigurationElement {
        /// <summary>
        /// The name of this section in the App.config
        /// </summary>
        public const string ElementName = "basePath";

        private BasePath() { }

        [ConfigurationProperty("path", IsRequired = true)]
        public string Path {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }
    }

    public class LastBackup : ConfigurationElement {
        /// <summary>
        /// The name of this section in the App.config
        /// </summary>
        public const string ElementName = "lastBackup";

        private LastBackup() { }

        [ConfigurationProperty("date", IsRequired = true)]
        public DateTime Date {
            get { return (DateTime)this["date"]; }
            set { this["date"] = value; }
        }
    }

    public class Interval : ConfigurationElement {
        /// <summary>
        /// The name of this section in the App.config
        /// </summary>
        public const string ElementName = "interval";

        private Interval() { }

        [ConfigurationProperty("months", IsRequired = false, DefaultValue = 0)]
        public int Months {
            get { return (int)this["months"]; }
            set { this["months"] = value; }
        }

        [ConfigurationProperty("weeks", IsRequired = false, DefaultValue = 0)]
        public int Weeks {
            get { return (int)this["weeks"]; }
            set { this["weeks"] = value; }
        }

        [ConfigurationProperty("days", IsRequired = false, DefaultValue = 0)]
        public int Days {
            get { return (int)this["days"]; }
            set { this["days"] = value; }
        }
    }

    #endregion

    #region Folders

    [ConfigurationCollection(typeof(FolderElement))]
    public class FoldersCollection : ConfigurationElementCollection {
        private FoldersCollection() { }

        protected override ConfigurationElement CreateNewElement() {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((FolderElement)element).Path;
        }

        public FolderElement this[int index] {
            get { return (FolderElement)BaseGet(index); }
            set {
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        /*new public FolderElement this[string name] {
            get { return (FolderElement)BaseGet(name); }
        }

        public int IndexOf(FolderElement folder) {
            return BaseIndexOf(folder);
        }
        
        public void Add(FolderElement folder) {
            BaseAdd(folder);
        }

        protected override void BaseAdd(ConfigurationElement element) {
            BaseAdd(element, false);
        }

        public void Remove(FolderElement folder) {
            if (BaseIndexOf(folder) >= 0) {
                BaseRemove(folder.Name);
            }
        }

        public void RemoveAt(int index) {
            BaseRemoveAt(index);
        }

        public void Remove(string name) {
            BaseRemove(name);
        }

        public void Clear() {
            BaseClear();
        }
        */
    }

    public class FolderElement : ConfigurationElement {
        public FolderElement(string path) {
            Path = path;
        }

        public FolderElement() { }

        [ConfigurationProperty("path", IsRequired = true, IsKey = true)]
        public string Path {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }
    }

    #endregion
}
