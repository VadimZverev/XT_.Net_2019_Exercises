using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace _51_Backup_System
{
    /// <summary>
    /// Recovers files to the set date and time.
    /// </summary>
    class Backup
    {
        readonly string backupPath;
        readonly string storagePath;
        private DirectoryInfo backupDir;
        private DirectoryInfo storageDir;

        #region Constructors

        /// <summary>
        /// Initializes an instance of a file recovery object with default paths
        /// for storing originals and backups.
        /// </summary>
        public Backup()
        {
            backupPath = $@"{Environment.CurrentDirectory}\Backup";
            storagePath = $@"{Environment.CurrentDirectory}\Storage";

            backupDir = new DirectoryInfo(backupPath);
            storageDir = new DirectoryInfo(storagePath);
        }

        /// <summary>
        /// Initializes an instance of a file recovery object with paths
        /// for storing originals and backups installed manually.
        /// </summary>
        /// <param name="backupPath">backup storage path</param>
        /// <param name="storagePath">original storage path</param>
        public Backup(string backupPath, string storagePath)
        {
            this.backupPath = backupPath;
            this.storagePath = storagePath;

            backupDir = new DirectoryInfo(backupPath);
            storageDir = new DirectoryInfo(storagePath);
        }

        #endregion

        /// <summary>
        /// Date and time for the rollback.
        /// </summary>
        public DateTime DateTimeRolback { get; set; }

        /// <summary>
        /// Starting the rollback process.
        /// </summary>
        public void Run()
        {
            // Extract all original files.
            FileInfo[] storageFInfo = storageDir.GetFiles("*.txt", SearchOption.AllDirectories);

            foreach (FileInfo sFile in storageFInfo)
            {
                FindBackupFile(sFile);
            }

            FindRemoveFiles();
        }

        private void FindBackupFile(FileInfo sFile)
        {
            string directoryName;
            string nameFileFolder = Path.GetFileNameWithoutExtension(sFile.Name);

            if (sFile.DirectoryName != storageDir.FullName)
            {
                string nameFileSubFolders = sFile.Directory.FullName.Replace(storagePath, backupPath);
                directoryName = Path.Combine(nameFileSubFolders, nameFileFolder);
            }
            else
            {
                directoryName = Path.Combine(backupPath, nameFileFolder);
            }

            FileInfo[] bFiles = backupDir.GetFiles("*.txt", SearchOption.AllDirectories)
                                         .Where(x => x.DirectoryName == directoryName)
                                         .OrderByDescending(x => x.Name)
                                         .ToArray();

            foreach (FileInfo bFile in bFiles)
            {
                string temp = bFile.Name.Substring(0, bFile.Name.LastIndexOf('-'));
                bool isParse = DateTime.TryParseExact(temp,
                                       "yyyy.MM.dd-HH.mm.ss",
                                       null,
                                       DateTimeStyles.None,
                                       out DateTime date);

                if (isParse)
                {
                    if (date <= DateTimeRolback)
                    {
                        // reading contents from a backup file and writing to the original file.
                        temp = File.ReadAllText(bFile.FullName);
                        File.WriteAllText(sFile.FullName, temp);

                        // extract name from backup file.
                        temp = Path.GetFileNameWithoutExtension(bFile.Name);
                        temp = temp.Substring(temp.LastIndexOf('-') + 1);

                        directoryName
                            = Path.Combine(bFile.Directory.Parent.FullName, temp);

                        if (bFile.DirectoryName != directoryName)
                            Directory.Move(bFile.DirectoryName, directoryName);

                        // used to form the full path for changing the file name to the backup file name.
                        temp = Path.Combine(sFile.DirectoryName, $"{temp}{bFile.Extension}");
                        sFile.MoveTo(temp);
                        break;  // exit from the search cycle for a suitable backup and go to the next file.
                    }
                }
            }
        }

        private void FindRemoveFiles()
        {
            List<string> lastViewDirectories = new List<string>();
            DirectoryInfo dInfo;

            do
            {
                dInfo = backupDir.GetDirectories("⚰*", SearchOption.AllDirectories)
                                    .OrderBy(x => x.FullName).FirstOrDefault(x =>
                                    {
                                        foreach (var item in lastViewDirectories)
                                        {
                                            if (x.FullName == item)
                                                return false;
                                        }
                                        return true;
                                    });

                if (dInfo != null)
                    lastViewDirectories.Add(dInfo.FullName);

                if (dInfo != null)
                {
                    string str = dInfo.Name.Substring(1, dInfo.Name.LastIndexOf('_') - 1);

                    if (DateTime.TryParseExact(str, "yyyy.MM.dd-HH.mm.ss", null, DateTimeStyles.None, out DateTime dtRIP))
                    {
                        if (dtRIP >= DateTimeRolback)
                        {
                            if (!dInfo.GetFiles("*.txt").Any())
                            {
                                str = dInfo.Name.Substring(dInfo.Name.LastIndexOf('_') + 1);
                                string newPath = $@"{dInfo.Parent.FullName}\{str}";

                                if (Directory.Exists(newPath))
                                {
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
                                else
                                    dInfo.MoveTo(newPath);

                                dInfo.Refresh();

                                string directoryName = newPath.Replace(backupPath, storagePath);

                                if (!Directory.Exists(directoryName))
                                {
                                    Directory.CreateDirectory(directoryName);
                                }
                            }
                            else
                            {
                                var bFiles = dInfo.GetFiles("*.txt").OrderByDescending(x => x.Name);

                                foreach (var bFile in bFiles)
                                {
                                    string temp = bFile.Name.Substring(0, bFile.Name.LastIndexOf('-'));
                                    bool isParse = DateTime.TryParseExact(temp,
                                                           "yyyy.MM.dd-HH.mm.ss",
                                                           null,
                                                           DateTimeStyles.None,
                                                           out DateTime date);

                                    if (isParse)
                                    {
                                        if (date <= DateTimeRolback)
                                        {
                                            // extract name from backup file.
                                            temp = Path.GetFileNameWithoutExtension(bFile.Name);
                                            temp = temp.Substring(temp.LastIndexOf('-') + 1);

                                            string newFilePath = $@"{dInfo.Parent.FullName}\{temp}";

                                            dInfo.MoveTo(newFilePath);

                                            string sourceFileName = Path.Combine(newFilePath, bFile.Name);
                                            string destFileName = newFilePath + ".txt";
                                            
                                            destFileName = destFileName.Replace(backupPath, storagePath);
                                            File.Copy(sourceFileName, destFileName, true);

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            while (dInfo != null);
        }
    }
}
