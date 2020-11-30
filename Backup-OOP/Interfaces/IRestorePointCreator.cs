using System.Collections.Generic;
using Backup_OOP.Enums;

namespace Backup_OOP.Interfaces
{
    public interface IRestorePointCreator
    {
        public RestorePoint Create(string backupPath, List<FileInformation> files, List<RestorePoint> restorePoints,
            IDateTimeProvider dateTimeProvider, IStorageAlgorithm storageAlgorithm, RestoreType restoreType);
    }
}