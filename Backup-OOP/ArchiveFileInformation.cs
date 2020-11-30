using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backup_OOP
{
    public class ArchiveFileInformation : FileInformation
    {
        private List<FileInformation> _files;
        public ArchiveFileInformation(string path, List<FileInformation> files) : base(files.Sum(x => x.Size), path)
        {
            _files = files;
        }
    }
}
