using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _51_Backup_System
{
    /// <summary>
    /// Recovers files to the set date and time.
    /// </summary>
    class Backup
    {
        #region Fields

        private readonly string backupPath;
        private readonly string storagePath;
        private readonly Regex regex;
        private DirectoryInfo backupDir;
        private DirectoryInfo storageDir;
        #endregion

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

            regex = new Regex(@"\d{4}\.\d{2}\.\d{2}-\d{2}\.\d{2}\.\d{2}");
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

            regex = new Regex(@"\d{4}\.\d{2}\.\d{2}-\d{2}\.\d{2}\.\d{2}");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Date and time for the rollback.
        /// </summary>
        public DateTime DateTimeRolback { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Starting the rollback process.
        /// </summary>
        public void Run()
        {
            // Extract all original files.
            FileInfo[] storageFInfo = storageDir.GetFiles("*.txt", SearchOption.AllDirectories);

            foreach (FileInfo sFile in storageFInfo)
            {
                if (!FindBackupFile(sFile))
                    sFile.Delete();
            }

            RecoverDeletedFiles();
        }


        private bool FindBackupFile(FileInfo sFile)
        {
            string directoryName;
            string nameFileFolder = Path.GetFileNameWithoutExtension(sFile.Name);

            if (sFile.DirectoryName != storageDir.FullName)
            {
                string nameFileSubFolders = sFile.DirectoryName.Replace(storagePath, backupPath);
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
                if (TryGetDateTime(regex.Match(bFile.Name).Value, out DateTime date))
                {
                    if (date <= DateTimeRolback)
                    {
                        File.Copy(bFile.FullName, sFile.FullName, overwrite: true);

                        // extract name from backup file.
                        string temp = bFile.Name.Substring(bFile.Name.LastIndexOf("🖬") + 2);

                        if (temp != sFile.Name)
                        {
                            temp = Path.GetFileNameWithoutExtension(temp);

                            directoryName = Path.Combine(bFile.Directory.Parent.FullName, temp);

                            if (bFile.DirectoryName != directoryName)
                                Directory.Move(bFile.DirectoryName, directoryName);

                            // used to form the full path for changing the file name to the backup file name.
                            temp = Path.Combine(sFile.DirectoryName, $"{temp}{bFile.Extension}");

                            if (sFile.FullName != temp)
                                sFile.MoveTo(temp);
                        }

                        return true;  // exit from the search cycle for a suitable backup and go to the next file.
                    }
                }
            }

            return false;
        }

        private void RecoverDeletedFiles()
        {
            List<string> lastViewDirectories = new List<string>();
            DirectoryInfo dInfo;

            do
            {
                dInfo = backupDir.GetDirectories("⚰*", SearchOption.AllDirectories)
                                 .OrderBy(x => x.FullName)
                                 .FirstOrDefault(x =>
                                 {
                                     foreach (var handledDirectory in lastViewDirectories)
                                     {
                                         if (x.FullName == handledDirectory)
                                             return false;
                                     }
                                     return true;
                                 });

                if (dInfo != null)
                {
                    lastViewDirectories.Add(dInfo.FullName);

                    if (TryGetDateTime(regex.Match(dInfo.Name).Value, out DateTime dtRIP))
                    {
                        if (dtRIP >= DateTimeRolback)
                        {
                            if (!dInfo.GetFiles("*.txt").Any())
                            {
                                string str = dInfo.Name.Substring(dInfo.Name.LastIndexOf('_') + 1);
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
                                    if (TryGetDateTime(regex.Match(bFile.Name).Value, out DateTime date))
                                    {
                                        if (date <= DateTimeRolback)
                                        {
                                            // extract name from backup file.
                                            string temp = Path.GetFileNameWithoutExtension(bFile.Name);
                                            temp = temp.Substring(bFile.Name.LastIndexOf("🖬") + 2);

                                            string newFilePath = Path.Combine(dInfo.Parent.FullName, temp);

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

        /// <summary>
        /// Pattern wrapper for extracting date and time: "yyyy.MM.dd-HH.mm.ss" .
        /// </summary>
        private bool TryGetDateTime(string dateTimeBackup, out DateTime result) =>
            DateTime.TryParseExact(dateTimeBackup, "yyyy.MM.dd-HH.mm.ss",
                                null, DateTimeStyles.None, out result);

        #endregion
    }
}