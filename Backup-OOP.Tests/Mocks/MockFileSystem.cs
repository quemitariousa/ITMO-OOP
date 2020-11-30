using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backup_OOP.Interfaces;

namespace Backup_OOP.Tests.Mocks
{
    public class MockFileSystem : IFileSystem
    {
        private List<FileInformation> _files;

        public MockFileSystem()
        {
            _files = new List<FileInformation>();
        }
        public FileInformation Read(string path)
        {
            return _files.First(x => x.Path == path);
        }

        public void Write(FileInformation file)
        {
            _files.Add(file);
        }
        public void Remove(string path)
        {
            FileInformation file = _files.First(x => x.Path == path);

            _files.Remove(file);
        }

        public void Update(string path, long newSize)
        {
            Remove(path);
            _files.Add(new FileInformation(newSize, path));
        }
    }
}
