using System;
using System.Collections.Generic;
using System.Text;

namespace IniLaboratory.Exeptions
{
    public class FileSystemException : Exception
    {
        public FileSystemException(string fileName, Exception e): base($"Unable to open file {fileName}", e) { }
    }
}
