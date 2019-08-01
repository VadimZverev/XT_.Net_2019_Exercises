using System;
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

        /// <summary>
        /// Initializes an instance of a file recovery object with default paths
        /// for storing originals and backups.
        /// </summary>
        public Backup()
        {
            backupPath = $@"{Environment.CurrentDirectory}\Backup";
            storagePath = $@"{Environment.CurrentDirectory}\Storage";
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
        }

        /// <summary>
        /// Date and time for the rollback.
        /// </summary>
        public DateTime DateTime { get; set; }


        /// <summary>
        /// Starting the rollback process.
        /// </summary>
        public void Run()
        {
            // Extract all original and backup files.
            var storage = new DirectoryInfo(storagePath);
            var storageFInfo = storage.GetFiles("", SearchOption.AllDirectories);

            var backup = new DirectoryInfo(backupPath);
            var backupFInfo = backup.GetFiles("", SearchOption.AllDirectories);


            foreach (var sFile in storageFInfo)
            {
                string directoryName;
                string nameFileFolder = Path.GetFileNameWithoutExtension(sFile.Name);

                if (sFile.DirectoryName != storage.FullName)
                {
                    string nameFileSubFolders = sFile.Directory.FullName.Remove(0, storagePath.Length);
                    directoryName = $@"{backupPath}{nameFileSubFolders}\{nameFileFolder}";
                }
                else
                {
                    directoryName = $@"{backupPath}\{nameFileFolder}";
                }

                var bFiles = backupFInfo
                                    .Where(x => x.DirectoryName == directoryName)
                                    .OrderByDescending(x => x.Name)
                                    .ToList();

                foreach (var bFile in bFiles)
                {
                    var str = bFile.Name.Substring(0, bFile.Name.LastIndexOf('-'));
                    DateTime.TryParseExact(str,
                                           "yyyy.MM.dd-HH.mm.ss",
                                           null,
                                           DateTimeStyles.None,
                                           out DateTime date);

                    if (date.Date <= DateTime.Date)
                    {
                        if (date.Ticks <= DateTime.Ticks)
                        {
                            // reading contents from a backup file and writing to the original file.
                            var temp = File.ReadAllText(bFile.FullName);
                            File.WriteAllText(sFile.FullName, temp);

                            // extract name from backup file.
                            temp = bFile.Name.Substring(bFile.Name.LastIndexOf('-') + 1);

                            directoryName
                                = $@"{bFile.Directory.Parent.FullName}\{temp.Substring(0, temp.LastIndexOf('.'))}";

                            if (bFile.DirectoryName != directoryName)
                                Directory.Move(bFile.DirectoryName, directoryName);

                            // used to form the full path for changing the file name to the backup file name.
                            temp = $@"{sFile.DirectoryName}\{temp}";
                            sFile.MoveTo(temp);
                            break;  // exit from the search cycle for a suitable backup and go to the next file.
                        }
                    }
                }
            }
        }
    }
}
