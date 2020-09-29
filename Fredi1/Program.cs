using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;
using IniLaboratory.Exeptions;
using IniLaboratory.Models;

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
}


