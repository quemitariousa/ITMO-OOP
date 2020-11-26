

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Backup_OOP.Interfaces
{
    public interface IFileSystem
    {
        public FileInformation Read(string path);
        public void Write(FileInformation file);
        public void Remove(string path);
        
    }
}
