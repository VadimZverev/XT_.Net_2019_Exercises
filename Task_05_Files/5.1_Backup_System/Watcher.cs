using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace _51_Backup_System
{
    /// <summary>
    /// It monitors file changes and creates copies in the backup storage folder.
    /// </summary>
    class Watcher
    {
        /// <summary>
        /// Initializes an instance of the tracking file to the default folder.
        /// </summary>
        public Watcher()
        {
            string pathStorage = $@"{Environment.CurrentDirectory}\Storage";

            FSWatcher = new FileSystemWatcher(pathStorage)
            {
                Filter = "*.*",
                NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName,
                IncludeSubdirectories = true
            };

            FSWatcher.Changed += OnChangedOrCreated;
            FSWatcher.Created += OnChangedOrCreated;
            FSWatcher.Renamed += OnRenamed;
            FSWatcher.Deleted += OnDeleted;
        }

        /// <summary>
        /// Initializes a file tracking instance with the folder installed manually.
        /// </summary>
        /// <param name="pathStorage">original storage path</param>
        public Watcher(string pathStorage, string pathBackup)
        {
            PathBackup = pathBackup;

            FSWatcher = new FileSystemWatcher(pathStorage)
            {
                Filter = "*.*",
                NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName,
                IncludeSubdirectories = true
            };

            FSWatcher.Changed += OnChangedOrCreated;
            FSWatcher.Created += OnChangedOrCreated;
            FSWatcher.Renamed += OnRenamed;
            FSWatcher.Deleted += OnDeleted;
        }

        /// <summary>
        /// Tracks changes in files and folders.
        /// </summary>
        public FileSystemWatcher FSWatcher { get; protected set; }

        private static DirectoryInfo BackupDir => new DirectoryInfo(PathBackup);

        public static string PathBackup { get; private set; } = $@"{Environment.CurrentDirectory}\Backup";

        /// <summary>
        /// Creates a backup file.
        /// </summary>
        private static void CreateBackup(FileSystemEventArgs e)
        {
            if (e != null)
            {
                string directory;
                string dateTime = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss");
                string fileName = Path.GetFileNameWithoutExtension(e.Name);
                string nameBackup = $"{dateTime}-{fileName}.txt";

                string subFolder = Path.GetDirectoryName(e.Name);

                if (subFolder == "")
                {
                    directory = $@"{PathBackup}\{fileName}\";
                }
                else
                {
                    directory = $@"{PathBackup}\{subFolder}\{fileName}\";
                }

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string destFullPath = $@"{directory}\{nameBackup}";

                if (!File.Exists(destFullPath))
                    File.Copy(e.FullPath, destFullPath);
            }
        }

        /// <summary>
        /// Handles a file creation or modification event.
        /// </summary>
        private static void OnChangedOrCreated(object source, FileSystemEventArgs e)
        {
            if (e != null)
            {
                var temp = new FileInfo(e.FullPath);

                if (temp.Attributes == FileAttributes.Directory)
                {
                    string newPath = $@"{Environment.CurrentDirectory}\Backup\{e.Name}";

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                }
                else if (temp.Length != 0)
                    CreateBackup(e);
            }
        }

        /// <summary>
        /// Handles the event to delete a file or folder.
        /// </summary>
        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            if (e != null)
            {
                bool isTryAgain = false;
                string nameFolder = "";

                string backupPath = $@"{PathBackup}\{e.Name}";
                bool isExists = Directory.Exists(backupPath);

                if (!isExists)
                {
                    isTryAgain = true;

                    nameFolder = e.Name.Substring(0, e.Name.LastIndexOf('.'));
                    backupPath = $@"{PathBackup}\{nameFolder}";

                    isExists = Directory.Exists(backupPath);
                }

                if (isExists)
                {
                    bool isContaintsSub = Directory.GetFileSystemEntries(backupPath).Any();

                    if (isContaintsSub)
                    {
                        string dateTimeRemoved = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss");
                        dateTimeRemoved = $@"Removed_{dateTimeRemoved}_";

                        if (isTryAgain)
                            nameFolder = nameFolder.Insert(nameFolder.LastIndexOf('\\') + 1, dateTimeRemoved);
                        else
                            nameFolder = e.Name.Insert(e.Name.LastIndexOf('\\') + 1, dateTimeRemoved);

                        string newBackupPath = $@"{PathBackup}\{nameFolder}";

                        Directory.Move(backupPath, newBackupPath);
                    }
                    else
                        Directory.Delete(backupPath);
                }
            }
        }

        /// <summary>
        /// Handles a file rename event.
        /// </summary>
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            if (e != null)
            {
                var extension = Path.GetExtension(e.OldName);

                if (extension != "")
                {
                    RenameDirectory(e);
                    CreateBackup(e);
                }
                else
                {
                    string oldPath = $@"{Environment.CurrentDirectory}\Backup\{e.OldName}\";

                    if (Directory.Exists(oldPath))
                    {
                        string newPath = $@"{Environment.CurrentDirectory}\Backup\{e.Name}\";

                        try
                        {
                            Directory.Move(oldPath, newPath);
                        }
                        catch
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(0.3));

                            // There will be a rollback of changing the name of the original folder.
                            Directory.Move(e.FullPath, e.OldFullPath);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Renames a folder.
        /// </summary>
        private static void RenameDirectory(RenamedEventArgs e)
        {
            if (e != null)
            {
                var oldName = e.OldName.Substring(0, e.OldName.LastIndexOf('.'));
                string oldPath = $@"{PathBackup}\{oldName}";

                if (!Directory.Exists(oldPath))
                {
                    var newName = Path.GetFileNameWithoutExtension(e.Name);
                    string newPath = $@"{PathBackup}\{newName}";

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                }
                else
                {
                    string newFullPathDir;
                    string oldFullPathDir = newFullPathDir = PathBackup;

                    string newNameFolder = Path.GetFileNameWithoutExtension(e.Name);
                    string oldNameFolder = Path.GetFileNameWithoutExtension(e.OldName);

                    string subFolder = Path.GetDirectoryName(e.Name);

                    if (subFolder == "")
                    {
                        newFullPathDir += $@"\{newNameFolder}\";
                        oldFullPathDir += $@"\{oldNameFolder}\";
                    }
                    else
                    {
                        newFullPathDir += $@"\{subFolder}\{newNameFolder}\";
                        oldFullPathDir += $@"\{subFolder}\{oldNameFolder}\";
                    }

                    Directory.Move(oldFullPathDir, newFullPathDir);
                }

            }
        }
    }
}
