using System;
using System.IO;
using System.Xml;

namespace ReadingLibrary
{
    public class ReadFile
    {
        public string readFileTxt(string fileName)
        {

            if (File.Exists(fileName))
            {
                string text = File.ReadAllText(fileName);
                return text;
            }
            return null;
        }

        public XmlDocument readFileXml(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists(fileName))
            {
                xmlDoc.Load(fileName);
                //xmlDoc.Save(output);
                return xmlDoc;
            }
            return xmlDoc;
        }
    }
}
