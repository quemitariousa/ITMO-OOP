using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;

namespace IniLaboratory
{
    class Program
    {
        static void Main(string[] args)
        {
            string theFile = "file.txt";
            IniParser iniParser = new IniParser();

            IniData iniFile = iniParser.Parse(theFile);
            Console.WriteLine($"[COMMON] DiskCachePath = {iniFile.GetString("COMMON", "DiskCachePath")}");
            Console.WriteLine($"[COMMON] LogXML = {iniFile.GetInt("COMMON", "LogXML")}");
            Console.WriteLine($"[ADC_DEV] BufferLenSecons = {iniFile.GetFloat("ADC_DEV", "BufferLenSecons")}");
        }
    }



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

    class IniParser
    { 

        private List<Section> ReadFile(string theFile)
        {
            string[] text;
            try 
            {
                text = File.ReadAllLines(theFile);
            }
            catch(Exception)
            {
                throw new FileSystemException(theFile);
            }

            for (int i = 0; i < text.Length; i++)
            {
                text[i] = DropComments(text[i]);
            }
            List<Section> sections = new List<Section>();
            Section lastSection = null;

            for (int i = 0; i < text.Length; i++)
            {
                string currentLine = text[i].Trim();
                if (currentLine.StartsWith("[") && currentLine.EndsWith("]"))
                {
                    lastSection = ParseSection(currentLine, sections);
                }
                else if(currentLine == String.Empty)
                {
                    continue;
                }
                else if (lastSection == null)
                {
                        throw new IncorrectFormatException(currentLine);
                } else
                {
                    Property property = ParseProperty(currentLine);

                    lastSection.properties.Add(property);
                }
            }
            return sections;
        }

        private string DropComments(string line)
        {
            if (line.IndexOf(";") != -1)
            {
                line = line.Substring(0, line.IndexOf(";"));

            }
            return line;
        }

        private Section ParseSection(string line, List<Section> sections)
        {
            string sectionName = line.Substring(1, line.Length - 2);
            Section currentSection = sections.Find(s => s.name == sectionName);

            if (currentSection == null)
            {
                currentSection = new Section(sectionName);
                sections.Add(currentSection);
            }

            return currentSection;
        }

        private Property ParseProperty(string currentLine)
        {
            string[] values = currentLine.Split("=");
            if (values.Length != 2)
                throw new IncorrectFormatException(currentLine);

            Property property = new Property(values[0].Trim(), values[1].Trim());
            return property;
        }

        public IniData Parse(string theFile)
        {
            List<Section> sections = ReadFile(theFile);

            IniData iniFile = new IniData(sections);
            return iniFile;
        }
    }
}
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

public class Property
{
    public string key, value;
    public Property(string keyOfProperty, string valueOfProperty)
    {
        key = keyOfProperty;
        value = valueOfProperty;
    }
}



public class FileSystemException : Exception
{
    public FileSystemException(string fileName) : base($"Unable to open file {fileName}") { }
}
public class IncorrectFormatException : Exception
{
    public IncorrectFormatException(string wrongRow) : base($"Wrong format in row: {wrongRow}") { }
}

public class InvalidPropertyTypeExeption : Exception
{
    public InvalidPropertyTypeExeption(string sectionName, string propertyName) : base($"Wrong type for property: {propertyName} in section: {sectionName}") { }
}

public class WrongSectionPropertyException : Exception
{
    public WrongSectionPropertyException(string sectionName, string propertyName) : base($"Unable to find property: {propertyName} in section: {sectionName}") { }

}

