using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.CompilerServices;
using fredioop1;
using System.Reflection.Metadata;
using System.Globalization;

namespace fredioop1
{
    class Program
    {
        static void Main(string[] args)
        {
            string theFile = "file.txt";
            IniParser iniParser = new IniParser();

            IniFile iniFile = iniParser.Parse(theFile);
            Console.WriteLine($"[COMMON] DiskCachePath = {iniFile.GetString("COMMON", "DiskCachePath")}");
            Console.WriteLine($"[COMMON] LogXML = {iniFile.GetInt("COMMON", "LogXML")}");
            Console.WriteLine($"[ADC_DEV] BufferLenSecons = {iniFile.GetFloat("ADC_DEV", "BufferLenSecons")}");
        }
    }



    class IniFile
    { 
        List<Section> sections;
        
        public IniFile(List<Section> sections)
        {
            this.sections = sections;
        }
        public Int32 GetInt(string sectionName, string propertyName)
        {
            Int32 toReturn;
            foreach (var section in sections)
            {
                if (section.name == sectionName)
                {
                    foreach (var property in section.properties)
                    {
                        if (property.key == propertyName)
                        {
                            if (Int32.TryParse(property.value, out toReturn))
                            {
                                return toReturn;
                            }
                            else
                            {
                                throw new InvalidPropertyTypeExeption(sectionName, propertyName);
                            }
                        }
                    }
                }
            }
            throw new WrongSectionPropertyException(sectionName, propertyName);
        }

        public float GetFloat(string sectionName, string propertyName)
        {
            CultureInfo cultureInfo = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            float toReturn;
            foreach (var section in sections)
            {
                if (section.name == sectionName)
                {
                    foreach (var property in section.properties)
                    {
                        if (property.key == propertyName)
                        {
                            if (float.TryParse(property.value, NumberStyles.Any, cultureInfo, out toReturn))
                            {
                                return toReturn;
                            }
                            else
                            {
                                throw new InvalidPropertyTypeExeption(sectionName, propertyName);
                            }
                        }
                    }
                }
            }
            throw new WrongSectionPropertyException(sectionName, propertyName);
        }

        public string GetString(string sectionName, string propertyName)
        {
            foreach (var section in sections)
            {
                if (section.name == sectionName)
                {
                    foreach (var property in section.properties)
                    {
                        if (property.key == propertyName)
                        {
                            return property.value;
                        }
                    }
                }
            }

            throw new WrongSectionPropertyException(sectionName, propertyName);
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

                    string sectionName = currentLine.Substring(1, currentLine.Length - 2);
                    if (sections.Find(s => s.name == sectionName) == null)
                    {
                        lastSection = new Section(sectionName);
                        sections.Add(lastSection);
                    }
                    else 
                    {
                        lastSection = sections.Single(s => s.name == sectionName);
                    }
                }
                else if(currentLine == String.Empty)
                {
                    continue;
                }
                else if (lastSection != null)
                {
                    string[] keyValue = currentLine.Split("=");
                    if (keyValue.Length != 2)
                        throw new IncorrectFormatException(currentLine);

                    Property property = new Property(keyValue[0].Trim(), keyValue[1].Trim());
                    lastSection.properties.Add(property);
                } else
                {
                    throw new IncorrectFormatException(currentLine);
                }
            }
            return sections;
        }

        string DropComments(string line)
        {
            if (line.IndexOf(";") != -1)
            {
                line = line.Substring(0, line.IndexOf(";"));

            }
            return line;
        }

        public IniFile Parse(string theFile)
        {
            List<Section> sections = ReadFile(theFile);

            IniFile iniFile = new IniFile(sections);
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

