using System;
using System.Collections.Generic;
using System.Text;

namespace IniLaboratory.Models
{
    public class Property
    {
        public string key, value;
        public Property(string keyOfProperty, string valueOfProperty)
        {
            key = keyOfProperty;
            value = valueOfProperty;
        }
    }
}
