using System;
using System.Collections.Generic;
using System.Text;

namespace IniLaboratory.Exeptions
{
    public class FileSystemException : Exception
    {
        public FileSystemException(string fileName) : base($"Unable to open file {fileName}") { }
    }
}
