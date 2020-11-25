using System;
using System.Collections.Generic;
using Backup_OOP.Enums;

namespace Backup_OOP.Interfaces
{
    public interface IStorageAlgorithm
    {
        public RestorePoint Storage(string backupPath, DateTime restoreTime, RestoreType restoreType, List<FileInformation> files);
    }
}
