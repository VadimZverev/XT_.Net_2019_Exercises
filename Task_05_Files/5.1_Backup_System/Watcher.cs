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
        #region Constructors

        /// <summary>
        /// Initializes static monitoring and backup paths.
        /// </summary>
        static Watcher()
        {
            PathBackup = Path.Combine(Environment.CurrentDirectory, "Backup");
            PathStorage = Path.Combine(Environment.CurrentDirectory, "Storage");
        }

        /// <summary>
        /// Initializes an instance of the tracking file to the default folder.
        /// </summary>
        public Watcher()
        {
            FSWatcher = new FileSystemWatcher(PathStorage)
            {
                Filter = "*.*",
                NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName,
                IncludeSubdirectories = true,
                InternalBufferSize = 65536
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
            PathStorage = pathStorage;

            FSWatcher = new FileSystemWatcher(pathStorage)
            {
                Filter = "*.*",
                NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName,
                IncludeSubdirectories = true,
                InternalBufferSize = 65536
            };

            FSWatcher.Changed += OnChangedOrCreated;
            FSWatcher.Created += OnChangedOrCreated;
            FSWatcher.Renamed += OnRenamed;
            FSWatcher.Deleted += OnDeleted;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Tracks changes in files and folders.
        /// </summary>
        public FileSystemWatcher FSWatcher { get; protected set; }

        /// <summary>
        /// Backup storage path.
        /// </summary>
        public static string PathBackup { get; private set; }

        /// <summary>
        /// Folder monitoring path
        /// </summary>
        public static string PathStorage { get; private set; }

        #endregion

        #region Methods

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
                string nameBackup = $"🖬{dateTime}🖬{fileName}.txt";

                string subFolder = Path.GetDirectoryName(e.Name);

                if (subFolder == "")
                    directory = Path.Combine(PathBackup, fileName);
                else
                    directory = Path.Combine(PathBackup, subFolder, fileName);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string destFullPath = Path.Combine(directory, nameBackup);

                if (!File.Exists(destFullPath))
                    File.Copy(e.FullPath, destFullPath);
            }
        }

        /// <summary>
        /// Checks the file for readiness to open.
        /// </summary>
        /// <exception cref="IOException"></exception>
        /// <param name="path">path of the checked file</param>
        /// <returns>Returns true if the file is available; otherwise false.</returns>
        private static bool IsFileReady(string path)
        {
            try
            {
                // If we can't open the file, it's still copying
                using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Handles a file creation or modification event.
        /// </summary>
        private static void OnChangedOrCreated(object sender, FileSystemEventArgs e)
        {
            if (e != null)
            {
                string extension = Path.GetExtension(e.FullPath);

                if (extension == "" || extension == ".txt")
                {

                    FileInfo temp = new FileInfo(e.FullPath);

                    if (temp.Attributes == FileAttributes.Directory)
                    {
                        string newPath = Path.Combine(PathBackup, e.Name);

                        if (!Directory.Exists(newPath))
                            Directory.CreateDirectory(newPath);
                    }
                    else if (temp.Extension == ".txt" && temp.Length != 0)
                    {
                        (sender as FileSystemWatcher).EnableRaisingEvents = false;

                        if (IsFileReady(e.FullPath))
                            CreateBackup(e);

                        (sender as FileSystemWatcher).EnableRaisingEvents = true;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the event to delete a file or folder.
        /// </summary>
        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            if (e != null)
            {
                string extension = Path.GetExtension(e.FullPath);

                if (extension == "" || extension == ".txt")
                {
                    bool isTryAgain = false;
                    string nameFolder = "";

                    string backupPath = Path.Combine(PathBackup, e.Name);

                    bool isExists = Directory.Exists(backupPath);

                    if (!isExists)
                    {
                        isTryAgain = true;

                        int lastIndex = e.Name.LastIndexOf('.');

                        if (lastIndex != -1)
                        {
                            nameFolder = e.Name.Substring(0, lastIndex);
                            backupPath = Path.Combine(PathBackup, nameFolder);

                            isExists = Directory.Exists(backupPath);
                        }
                    }

                    if (isExists)
                    {
                        bool isContaintsSub = Directory.GetFileSystemEntries(backupPath).Any();

                        if (isContaintsSub)
                        {
                            string dateTimeRemoved = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss");
                            dateTimeRemoved = $"⚰{dateTimeRemoved}_";

                            if (isTryAgain)
                                nameFolder = nameFolder.Insert(nameFolder.LastIndexOf('\\') + 1, dateTimeRemoved);
                            else
                                nameFolder = e.Name.Insert(e.Name.LastIndexOf('\\') + 1, dateTimeRemoved);

                            string newBackupPath = Path.Combine(PathBackup, nameFolder);

                            Directory.Move(backupPath, newBackupPath);
                        }
                        else
                            Directory.Delete(backupPath);
                    }
                }
            }
        }

        /// <summary>
        /// Handles a file rename event.
        /// </summary>
        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (e != null)
            {
                FileSystemInfo fsInfo = new FileInfo(e.FullPath);

                if (fsInfo.Attributes == FileAttributes.Directory)
                {
                    WorkWithDirectory(e);
                }
                else if (fsInfo.Attributes == FileAttributes.Archive
                            && fsInfo.Extension == ".txt")
                {
                    (sender as FileSystemWatcher).EnableRaisingEvents = false;

                    RenameBackupDirectory(e);
                    CreateBackup(e);

                    (sender as FileSystemWatcher).EnableRaisingEvents = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void WorkWithDirectory(RenamedEventArgs e)
        {
            string oldPath = Path.Combine(PathBackup, e.OldName);

            if (Directory.Exists(oldPath))
            {
                string newPath = Path.Combine(PathBackup, e.Name);

                if (!Directory.Exists(newPath))
                {
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
                else
                {
                    DirectoryInfo dInfo = new DirectoryInfo(oldPath);

                    if (dInfo.GetFileSystemInfos().Any())
                    {
                        string dest;

                        foreach (var item in dInfo.EnumerateFileSystemInfos())
                        {
                            if (item is DirectoryInfo dir)
                            {
                                dest = Path.Combine(newPath, dir.Name);
                                dir.MoveTo(dest);
                            }
                            else if (item is FileInfo file)
                            {
                                dest = Path.Combine(newPath, file.Name);
                                file.MoveTo(dest);
                            }
                        }
                    }

                    dInfo.Delete();
                }
            }
            else
            {
                string newPath = Path.Combine(PathBackup, e.Name);
                Directory.CreateDirectory(newPath);
            }
        }

        /// <summary>
        /// Renames a folder.
        /// </summary>
        private static void RenameBackupDirectory(RenamedEventArgs e)
        {
            if (e != null)
            {
                string oldName = e.OldName.Substring(0, e.OldName.LastIndexOf('.'));
                string oldPath = Path.Combine(PathBackup, oldName);

                if (!Directory.Exists(oldPath))
                {
                    string newName = e.Name.Substring(0, e.Name.LastIndexOf('.'));
                    string newPath = Path.Combine(PathBackup, newName);

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                }
                else
                {
                    string newFullPathDir;
                    string oldFullPathDir;

                    string newNameFolder = Path.GetFileNameWithoutExtension(e.Name);
                    string oldNameFolder = Path.GetFileNameWithoutExtension(e.OldName);

                    string subFolder = Path.GetDirectoryName(e.Name);

                    if (subFolder == "")
                    {
                        newFullPathDir = Path.Combine(PathBackup, newNameFolder);
                        oldFullPathDir = Path.Combine(PathBackup, oldNameFolder);
                    }
                    else
                    {
                        newFullPathDir = Path.Combine(PathBackup, subFolder, newNameFolder);
                        oldFullPathDir = Path.Combine(PathBackup, subFolder, oldNameFolder);
                    }

                    if (Directory.Exists(newFullPathDir))
                    {
                        if (Directory.GetFileSystemEntries(oldFullPathDir).Any())
                        {
                            string dest;
                            DirectoryInfo dInfo = new DirectoryInfo(oldFullPathDir);

                            foreach (var item in dInfo.EnumerateFileSystemInfos())
                            {
                                if (item is DirectoryInfo dir)
                                {
                                    dest = Path.Combine(newFullPathDir, dir.Name);
                                    dir.MoveTo(dest);
                                }
                                else if (item is FileInfo file)
                                {
                                    dest = Path.Combine(newFullPathDir, file.Name);
                                    file.MoveTo(dest);
                                }
                            }

                            dInfo.Delete();
                        }
                    }
                    else
                        Directory.Move(oldFullPathDir, newFullPathDir);
                }
            }
        }

        #endregion
    }
}
