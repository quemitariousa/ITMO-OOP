using System;
using System.Collections.Generic;
using System.Text;

namespace IniLaboratory.Exeptions
{
    public class InvalidPropertyTypeExeption : Exception
    {
        public InvalidPropertyTypeExeption(string sectionName, string propertyName) : base($"Wrong type for property: {propertyName} in section: {sectionName}") { }
    }
}
