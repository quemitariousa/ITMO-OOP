using System;
using System.Collections.Generic;
using System.Text;

namespace IniLaboratory.Exeptions
{
    public class WrongSectionPropertyException : Exception
    {
        public WrongSectionPropertyException(string sectionName, string propertyName) : base($"Unable to find property: {propertyName} in section: {sectionName}") { }

    }
}
