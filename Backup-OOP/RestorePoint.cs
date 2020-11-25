using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Backup_OOP.Enums;

namespace Backup_OOP
{
    public class RestorePoint
    {

        public Guid Id { get; }
        public string Path { get; }
        public DateTime RestoreTime { get; }
        public StorageType StorageType { get; }
        public RestoreType RestoreType { get; }
        public List<RestoreFileInformation> RestoreFiles { get; }
        public long Size => RestoreFiles.Sum(x => x.Size);

        public RestorePoint(string backupPath, DateTime restoreTime, StorageType storageType, RestoreType restoreType, List<FileInformation> files)
        {
            
            Id = Guid.NewGuid();
            RestoreTime = restoreTime;
            StorageType = storageType;
            RestoreType = restoreType;
            Path = backupPath + "/" + "Restore" + Id.ToString();
            RestoreFiles = files.Select(x => x.Restore(Path)).ToList();
        }
    }
}
