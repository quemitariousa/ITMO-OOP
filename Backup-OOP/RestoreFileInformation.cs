using System;
using System.Collections.Generic;
using System.Text;

namespace Backup_OOP
{
    public class RestoreFileInformation : FileInformation
    {
        public string SourcePath { get; }

        public RestoreFileInformation(long size, string path, string sourcePath) : base(size, path)
        {
            SourcePath = sourcePath;
        }
    }
}
