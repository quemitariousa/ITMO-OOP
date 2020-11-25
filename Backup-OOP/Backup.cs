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
        public long Size => _restorePoints.Sum(x => x.Size);
        private List<RestorePoint> _restorePoints;
        private List<string> _watchedFilePaths;

        private ICleanAlgorithm _cleanAlgorithm;
        private IStorageAlgorithm _storageAlgorithm;
        private IDateTimeProvider _dateTimeProvider;
        private IFileSystem _fileSystem;

        private string _backupPath;
        public Backup(List<string> watchedFilePaths, ICleanAlgorithm cleanAlgorithm, IStorageAlgorithm storageAlgorithm, IDateTimeProvider dateTimeProvider, IFileSystem fileSystem)
        {
            _watchedFilePaths = watchedFilePaths;
            _cleanAlgorithm = cleanAlgorithm;
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
            RestorePoint restorePoint = _storageAlgorithm.Storage(_backupPath, _dateTimeProvider.GetCurrentTime(),
                RestoreType.Full, files);
            _restorePoints.Add(restorePoint);
            WriteRestorePoint(restorePoint);

        }

        public void WriteRestorePoint(RestorePoint restorePoint)
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
                    _fileSystem.WriteArchive(restorePoint.Path, restorePoint.RestoreFiles.Cast<FileInformation>().ToList());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
