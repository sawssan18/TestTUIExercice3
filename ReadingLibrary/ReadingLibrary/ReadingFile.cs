using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace ReadingLibrary
{
    public class ReadFile
    {
        public string ReadFileTxt(string fileName,string role)
        {
            if (Thread.CurrentPrincipal.IsInRole(role))
            {
                string text = string.Empty;
                if (File.Exists(fileName))
                {
                    text = File.ReadAllText(fileName);
                    return text;
                }
                return text;
            }
            else
            {
               return ("UnauthorizedAccess");
            }
        }

        public XmlDocument ReadFileXml(string fileName, string role)
        {
            if (Thread.CurrentPrincipal.IsInRole(role))
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
            else
            {
                Console.WriteLine("UnauthorizedAccess");
                return null;
            }

        }

        public string ReadEncryptedTxtFile(String fileName)
        {

            return Decryptor.ReadEncryptFile(fileName);

        }

        public string  ReadEncryptedXmlFile(string fileName)
        {

            return Decryptor.ReadEncryptFile(fileName);
        }


        public JObject ReadJsonFile(string fileName, string role)
        {
            if (Thread.CurrentPrincipal.IsInRole(role))
            {
                using (StreamReader file = File.OpenText(fileName))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
               return (JObject)JToken.ReadFrom(reader);
            }
            }
            else
            {
                Console.WriteLine("UnauthorizedAccess");
                return null;
            }

        }

        public string  ReadEncryptedJsonFile(string fileName)
        {
            return  Decryptor.ReadEncryptFile(fileName);
        }
    }
}


