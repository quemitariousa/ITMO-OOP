using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backup_OOP.Enums;
using Backup_OOP.Interfaces;

namespace Backup_OOP
{
    public class Backup
    {
        public Guid Id { get; }
        public List<RestorePoint> RestorePoints => _restorePoints.ToList();
        public long Size => _restorePoints.Sum(x => x.Size);
        private  List<RestorePoint> _restorePoints;
        private readonly List<string> _watchedFilePaths;

        private readonly IStorageAlgorithm _storageAlgorithm;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IFileSystem _fileSystem;

        private readonly string _backupPath;
        public Backup(IStorageAlgorithm storageAlgorithm, IDateTimeProvider dateTimeProvider, IFileSystem fileSystem)
        {
            _storageAlgorithm = storageAlgorithm;
            _dateTimeProvider = dateTimeProvider;
            _fileSystem = fileSystem;

            Id = Guid.NewGuid();
            _backupPath = "Backup" + Id.ToString();
            _restorePoints = new List<RestorePoint>();
            _watchedFilePaths = new List<string>();

        }

        public void Watch(string path)
        {
            _watchedFilePaths.Add(path);
        }

        public void CreateRestorePoint(RestoreType restoreType)
        {
            List<FileInformation> files = _watchedFilePaths.Select(x => _fileSystem.Read(x)).ToList();

            if (restoreType == RestoreType.Increment)
            {
                RestorePoint lastFullRestorePoint = _restorePoints.Last(x => x.RestoreType == RestoreType.Full);
                List<RestorePoint> lastRestorePoints = _restorePoints.AsEnumerable().Reverse()
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

            RestorePoint restorePoint = _storageAlgorithm.Storage(_backupPath, _dateTimeProvider.GetCurrentTime(),
                restoreType, files);

            _restorePoints.Add(restorePoint);
            WriteRestorePoint(restorePoint);

        }

        private void WriteRestorePoint(RestorePoint restorePoint)
        {
            switch (restorePoint.StorageType)
            {
                case StorageType.Separate:
                    foreach (RestoreFileInformation file in restorePoint.RestoreFiles)
                    {
                        _fileSystem.Write(file);
                    }
                    break;
                case StorageType.Shared:
                    _fileSystem.Write(new ArchiveFileInformation(restorePoint.Path, restorePoint.RestoreFiles.Cast<FileInformation>().ToList()));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Clean(ICleanAlgorithm cleanAlgorithm)
        {
            int count = cleanAlgorithm.GetForRemove(_restorePoints);
            if (count == _restorePoints.Count)
            {
                count--;
            }
            if (count == 0)
            {
                return;
            }

            IEnumerable<RestorePoint> restorePointsForRemove = _restorePoints.Take(count);
            if (_restorePoints[count].RestoreType == RestoreType.Increment)
            {
                restorePointsForRemove = restorePointsForRemove
                    .Reverse()
                    .SkipWhile(x => x.RestoreType == RestoreType.Increment)
                    .Skip(1);
            }

            _restorePoints = _restorePoints.Skip(restorePointsForRemove.Count()).ToList();
        }

        private void RemoveRestorePoint(RestorePoint restorePoint)
        {
            switch (restorePoint.StorageType)
            {
                case StorageType.Separate:
                    foreach (var file in restorePoint.RestoreFiles)
                    {
                        _fileSystem.Remove(file.Path);
                    }
                    break;
                case StorageType.Shared:
                    _fileSystem.Remove(restorePoint.Path);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
