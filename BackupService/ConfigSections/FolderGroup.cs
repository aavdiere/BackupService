using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace BackupService.ConfigSections {
    public class ServiceData : ConfigurationSection {
        /// <summary>
        /// The name of this section in the App.config
        /// </summary>
        public const string SectionName = "serviceData";

        private ServiceData() { }

        [ConfigurationProperty("backupConfigurations")]
        [ConfigurationCollection(typeof(BackupConfiguration), AddItemName = "backupConfiguration")]
        public BackupConfigurations BackupConfigurations {
            get { return (BackupConfigurations)this["backupConfigurations"]; }
            set { this["backupConfigurations"] = value; }
        }
    }

    #region ServiceData

    [ConfigurationCollection(typeof(BackupConfiguration))]
    public class BackupConfigurations : ConfigurationElementCollection, IList<BackupConfiguration>, ICollection<BackupConfiguration>, IEnumerable<BackupConfiguration> {
        /// <summary>
        /// The name of this collection in the App.config
        /// </summary>
        public const string CollectionName = "backupConfigurations";

        internal const string PropertyName = "backupConfiguration";

        protected override string ElementName {
            get {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName) {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly() {
            return false;
        }

        protected override ConfigurationElement CreateNewElement() {
            return new BackupConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((BackupConfiguration)element).General.Identifier.Name;
        }

        public new BackupConfigurationEnumerator GetEnumerator() {
            return new BackupConfigurationEnumerator(this);
        }

        IEnumerator<BackupConfiguration> IEnumerable<BackupConfiguration>.GetEnumerator() {
            return GetEnumerator();
        }

        bool ICollection<BackupConfiguration>.IsReadOnly {
            get {
                return false;
            }
        }

        public BackupConfiguration this[int index] {
            get {
                BackupConfiguration result = (BackupConfiguration)BaseGet(index);
                if (result == null)
                    throw new IndexOutOfRangeException();
                return result;
            }

            set {
                BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        BackupConfiguration IList<BackupConfiguration>.this[int index] {
            get {
                return (BackupConfiguration)BaseGet(index);
            }

            set {
                BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        public int IndexOf(BackupConfiguration item) {
            return BaseIndexOf(item);
        }

        public void Insert(int index, BackupConfiguration item) {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException();
            BaseRemoveAt(index);
            BaseAdd(index, item);
        }

        public void RemoveAt(int index) {
            BaseRemoveAt(index);
        }

        public void Add(BackupConfiguration item) {
            BaseAdd(item, false);
        }

        public void Clear() {
            BaseClear();
        }

        public bool Contains(BackupConfiguration item) {
            return BaseIndexOf(item) >= 0;
        }

        public void CopyTo(BackupConfiguration[] array, int arrayIndex) {
            if (array == null)
                throw new ArgumentNullException();
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();
            if (array.Length < Count - arrayIndex)
                throw new ArgumentException();

            for (int i = arrayIndex; i < Count; i++) {
                array[i - arrayIndex] = (BackupConfiguration)BaseGet(arrayIndex);
            }
        }

        public bool Remove(BackupConfiguration item) {
            BaseRemove(item);
            return true;
        }

        public class BackupConfigurationEnumerator : IEnumerator<BackupConfiguration> {
            int _position = -1;
            BackupConfigurations _BackupConfigurations;

            public BackupConfigurationEnumerator(BackupConfigurations BackupConfigurations) {
                _BackupConfigurations = BackupConfigurations;
            }

            public BackupConfiguration Current {
                get {
                    try {
                        return _BackupConfigurations[_position];
                    } catch (IndexOutOfRangeException ex) {
                        throw new InvalidOperationException("Invalid operation.", ex);
                    }
                }
            }

            object IEnumerator.Current {
                get {
                    return Current;
                }
            }

            public void Dispose() { }

            public bool MoveNext() {
                _position++;
                return (_position < _BackupConfigurations.Count);
            }

            public void Reset() {
                _position = -1;
            }
        }
    }

    public class BackupConfiguration : ConfigurationElement {
        /// <summary>
        /// The name of this element in the App.config
        /// </summary>
        public const string ElementName = "backupConfiguration";

        public BackupConfiguration() { }

        public string Identifier {
            get { return General.Identifier.Name; }
            set { General.Identifier.Name = value; }
        }

        [ConfigurationProperty("general")]
        public General General {
            get { return (General)this["general"]; }
            set { this["general"] = value; }
        }

        [ConfigurationProperty("folders")]
        [ConfigurationCollection(typeof(Folder), AddItemName = "folder")]
        public Folders Folders {
            get { return (Folders)this["folders"]; }
            set { this["folders"] = value; }
        }
    }

    #region General

    public class General : ConfigurationElement {
        /// <summary>
        /// The name of this section in the App.config
        /// </summary>
        public const string SectionName = "general";

        public General() { }

        [ConfigurationProperty("identifier", IsRequired = true, IsKey = true)]
        public Identifier Identifier {
            get { return (Identifier)this["identifier"]; }
            set { this["identifier"] = value; }
        }

        [ConfigurationProperty("basePath", IsRequired = true)]
        public BasePath BasePath {
            get { return (BasePath)this["basePath"]; }
            set { this["basePath"] = value; }
        }

        [ConfigurationProperty("lastBackup", IsRequired = true)]
        public LastBackup LastBackup {
            get { return (LastBackup)this["lastBackup"]; }
            set { this["lastBackup"] = value; }
        }

        [ConfigurationProperty("interval", IsRequired = true)]
        public Interval Interval {
            get { return (Interval)this["interval"]; }
            set { this["interval"] = value; }
        }
    }

    public class Identifier : ConfigurationElement {
        /// <summary>
        /// The name of this element in the App.config
        /// </summary>
        public const string ElementName = "identifier";

        private Identifier() { }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }

    public class BasePath : ConfigurationElement {
        /// <summary>
        /// The name of this element in the App.config
        /// </summary>
        public const string ElementName = "basePath";

        public BasePath() { }

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

    public class Folder : ConfigurationElement {
        /// <summary>
        /// The name of this element in the App.config
        /// </summary>
        public const string ElementName = "folder";

        public Folder() { }

        [ConfigurationProperty("name", IsRequired = false)]
        public string Name {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("path", IsRequired = true)]
        public string Path {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }
    }

    [ConfigurationCollection(typeof(Folder))]
    public class Folders : ConfigurationElementCollection, IList<Folder>, ICollection<Folder>, IEnumerable<Folder> {
        /// <summary>
        /// The name of this collection in the App.config
        /// </summary>
        public const string CollectionName = "folders";

        private Folders() { }

        public override bool IsReadOnly() {
            return false;
        }

        protected override ConfigurationElement CreateNewElement() {
            return new Folder();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            Folder f = (Folder)element;
            return f.Path + f.Name;
        }

        public new FolderEnumerator GetEnumerator() {
            return new FolderEnumerator(this);
        }

        IEnumerator<Folder> IEnumerable<Folder>.GetEnumerator() {
            return GetEnumerator();
        }

        bool ICollection<Folder>.IsReadOnly {
            get {
                return false;
            }
        }

        public Folder this[int index] {
            get {
                Folder result = (Folder)BaseGet(index);
                if (result == null)
                    throw new IndexOutOfRangeException();
                return result;
            }

            set {
                BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        Folder IList<Folder>.this[int index] {
            get {
                return (Folder)BaseGet(index);
            }

            set {
                BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        public int IndexOf(Folder item) {
            return BaseIndexOf(item);
        }

        public void Insert(int index, Folder item) {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException();
            BaseRemoveAt(index);
            BaseAdd(index, item);
        }

        public void RemoveAt(int index) {
            BaseRemoveAt(index);
        }

        public void Add(Folder item) {
            BaseAdd(item, false);
        }

        public void Clear() {
            BaseClear();
        }

        public bool Contains(Folder item) {
            return BaseIndexOf(item) >= 0;
        }

        public void CopyTo(Folder[] array, int arrayIndex) {
            if (array == null)
                throw new ArgumentNullException();
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();
            if (array.Length < Count - arrayIndex)
                throw new ArgumentException();

            for (int i = arrayIndex; i < Count; i++) {
                array[i - arrayIndex] = (Folder)BaseGet(arrayIndex);
            }
        }

        public bool Remove(Folder item) {
            BaseRemove(item);
            return true;
        }

        public class FolderEnumerator : IEnumerator<Folder> {
            int _position = -1;
            Folders _folders;

            public FolderEnumerator(Folders folders) {
                _folders = folders;
            }

            public Folder Current {
                get {
                    try {
                        return _folders[_position];
                    } catch (IndexOutOfRangeException ex) {
                        throw new InvalidOperationException("Invalid operation.", ex);
                    }
                }
            }

            object IEnumerator.Current {
                get {
                    return Current;
                }
            }

            public void Dispose() { }

            public bool MoveNext() {
                _position++;
                return (_position < _folders.Count);
            }

            public void Reset() {
                _position = -1;
            }
        }
    }

    #endregion

    #endregion
}
