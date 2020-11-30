using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backup_OOP.Enums;
using Backup_OOP.Interfaces;

namespace Backup_OOP
{
    public class RestorePointCreator: IRestorePointCreator
    {
        public RestorePoint Create(string backupPath, List<FileInformation> files, List<RestorePoint> restorePoints,
            IDateTimeProvider dateTimeProvider, IStorageAlgorithm storageAlgorithm, RestoreType restoreType)
        {

            if (restoreType == RestoreType.Increment)
            {
                RestorePoint lastFullRestorePoint = restorePoints.Last(x => x.RestoreType == RestoreType.Full);
                List<RestorePoint> lastRestorePoints = restorePoints.AsEnumerable().Reverse()
                    .TakeWhile(x => x.RestoreType == RestoreType.Increment).ToList();
                lastRestorePoints.Add(lastFullRestorePoint);

                List<FileInformation> incrementedFiles = new List<FileInformation>();

                foreach (var file in files)
                {
                    List<FileInformation> fileUpdates = lastRestorePoints
                        .Select(x => x.GetFile(file.Path))
                        .Where(x => x != null)
                        .Cast<FileInformation>()
                        .ToList();
                    FileInformation diff = file.GetDiff(fileUpdates);
                    if (diff.Size > 0)
                    {
                        incrementedFiles.Add(diff);
                    }
                }

                files = incrementedFiles;
            }

            RestorePoint restorePoint = storageAlgorithm.Storage(backupPath, dateTimeProvider.GetCurrentTime(),
                restoreType, files);

            return restorePoint;
        }
    }
}
