using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backup_OOP
{
    public class FileInformation
    {
        public long Size { get; }
        public string Path { get; }

        public FileInformation(long size, string path)
        {
            Size = size;
            Path = path;
        }

        public RestoreFileInformation Restore(string restorePath)
        {
            string targetPath = restorePath + "/" + Path.Split("/").Last();
            return new RestoreFileInformation(Size, targetPath, Path);
        }

        public FileInformation GetDiff(List<FileInformation> diffs)
        {
            long diffSize = Size - diffs.Sum(x => x.Size);
            return new FileInformation(diffSize, Path);
        }
    }
}
