using System;
using System.Collections.Generic;
using System.Text;

namespace IniLaboratory.Models
{
    public class Section
    {
        public string name;
        public List<Property> properties;

        public Section(string sectionName)
        {
            name = sectionName;
            properties = new List<Property>();
        }

    }
}
