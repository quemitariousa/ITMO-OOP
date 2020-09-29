using IniLaboratory.Exeptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IniLaboratory.Models;


namespace IniLaboratory
{

    class IniParser
    {

        private List<Section> ReadFile(string theFile)
        {
            string[] text;
            try
            {
                text = File.ReadAllLines(theFile);
            }
            catch (Exception e)
            {
                throw new FileSystemException(theFile, e);
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
                else if (currentLine == String.Empty)
                {
                    continue;
                }
                else if (lastSection == null)
                {
                    throw new IncorrectFormatException(currentLine);
                }
                else
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

