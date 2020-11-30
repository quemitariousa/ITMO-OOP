﻿using System;
using System.Collections.Generic;
using System.Text;
using Backup_OOP.Enums;
using Backup_OOP.Interfaces;

namespace Backup_OOP
{
    public class SeparateStorageAlgorithm : IStorageAlgorithm
    {
        public RestorePoint Storage(string backupPath, DateTime restoreTime, RestoreType restoreType, List<FileInformation> files)
        {
            return new RestorePoint(backupPath, restoreTime, StorageType.Separate, restoreType, files);
        }
    }
}