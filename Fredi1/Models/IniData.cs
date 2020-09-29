using IniLaboratory.Exeptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IniLaboratory.Models
{
    class IniData
    {
        private List<Section> _sections;
        private CultureInfo _cultureInfo;

        public IniData(List<Section> sections)
        {
            this._sections = sections;

            _cultureInfo = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            _cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
        }
        public Int32 GetInt(string sectionName, string propertyName)
        {
            if (int.TryParse(GetString(sectionName, propertyName), out int result))
                return result;
            throw new WrongSectionPropertyException(sectionName, propertyName);
        }

        public float GetFloat(string sectionName, string propertyName)
        {
            if (float.TryParse(GetString(sectionName, propertyName), NumberStyles.Any, _cultureInfo, out float result))
                return result;

            throw new WrongSectionPropertyException(sectionName, propertyName);
        }

        public string GetString(string sectionName, string propertyName) // !
        {
            return _sections.Find(s => s.name == sectionName)?
                    .properties
                    .Find(p => p.key == propertyName)?
                    .value ?? throw new WrongSectionPropertyException(sectionName, propertyName);
        }
    }
}
