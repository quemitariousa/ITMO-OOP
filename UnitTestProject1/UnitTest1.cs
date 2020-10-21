using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using IniLaboratory.Models;
using IniLaboratory;
using System.Runtime.CompilerServices;
using IniLaboratory.Exeptions;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReadStringProperty()
        {
            // Arrange
            string fileName = "test.txt";
            string text = "[A]\n B=C";
            File.WriteAllText(fileName, text);
            IniParser parser = new IniParser();
            IniData iniData = parser.Parse(fileName);

            string supposedResult = "C";

            // Act
            string result = iniData.GetString("A", "B");

            // Assert
            Assert.AreEqual(result, supposedResult);
        }

        [TestMethod]
        public void ReadIntProperty()
        {
            string fileName = "test.txt";
            string text = "[A]\n B=6";
            File.WriteAllText(fileName, text);
            IniParser parser = new IniParser();
            IniData iniData = parser.Parse(fileName);

            string supposedResult = "6";

            string result = iniData.GetString("A", "B");
            Assert.AreEqual(result, supposedResult);
        } 

        [TestMethod]
        public void ReadFloatProperty()
        {
            string fileName = "test.txt";
            string text = "[A]\n B=0.5";
            File.WriteAllText(fileName, text);
            IniParser parser = new IniParser();
            IniData iniData = parser.Parse(fileName);

            string supposedResult = "0.5";

            string result = iniData.GetString("A", "B");
            Assert.AreEqual(result, supposedResult);
        }

        [TestMethod]
        public void ParseWithComments()
        {
            string fileName = "test.txt";
            string text = "[A]\n B=C ;poebota";
            File.WriteAllText(fileName, text);
            IniParser parser = new IniParser();
            IniData iniData = parser.Parse(fileName);

            string supposedResult = "C";

            string result = iniData.GetString("A", "B");
            Assert.AreEqual(result, supposedResult);
        }

        [TestMethod]
        public void MissingSection()
        {
            string fileName = "test.txt";
            string text = "B=C ;poebota";
            File.WriteAllText(fileName, text);
            IniParser parser = new IniParser();
            
           
            Assert.ThrowsException<IncorrectFormatException>(() => parser.Parse(fileName));
        }

        [TestMethod]
        public void MissingKey() 
        {
            string fileName = "test.txt";
            string text = "[A]\n C";
            File.WriteAllText(fileName, text);
            IniParser parser = new IniParser();

            Assert.ThrowsException<IncorrectFormatException>(() => parser.Parse(fileName));
        }

        [TestMethod]
        public void MissingSectionAndKey()
        {
            string fileName = "test.txt";
            string text = "C";
            File.WriteAllText(fileName, text);
            IniParser parser = new IniParser();

            Assert.ThrowsException<IncorrectFormatException>(() => parser.Parse(fileName));
        }

        [TestMethod]
        public void MissingEqually()
        {
            string fileName = "test.txt";
            string text = "[A]\n B C ;poebota";
            File.WriteAllText(fileName, text);
            IniParser parser = new IniParser();

            Assert.ThrowsException<IncorrectFormatException>(() => parser.Parse(fileName));
        }

    }
}
