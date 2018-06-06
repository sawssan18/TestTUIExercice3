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

        public string ReadEncryptedTxtFile(String FileName, byte[] Key, byte[] vector)
        {
            //Decrypts a file using Rijndael algorithm.

            try
            {

                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                Rijndael RijndaelAlg = Rijndael.Create();

                CryptoStream cStream = new CryptoStream(fStream,
                    RijndaelAlg.CreateDecryptor(Key, vector),
                    CryptoStreamMode.Read);

                StreamReader sReader = new StreamReader(cStream);

                string val = null;

                try
                {

                    val = sReader.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: {0}", e.Message);
                }
                finally
                {
                    sReader.Close();
                    sReader.Dispose();
                    cStream.Close();
                    cStream.Dispose();
                    fStream.Close();
                    fStream.Dispose();
                }


                return val;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
                return null;
            }
        }

        public DataSet ReadEncryptedXmlFile(string fileName)
        {

            DataSet ds = new DataSet();
            FileStream aFileStream = new FileStream(fileName, FileMode.Open);
            StreamReader aStreamReader = new StreamReader(aFileStream);
            UnicodeEncoding aUE = new UnicodeEncoding();
            byte[] key = aUE.GetBytes("password");
            RijndaelManaged RMCrypto = new RijndaelManaged();
            CryptoStream aCryptoStream = new CryptoStream(aFileStream, RMCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read);

            ds.ReadXml(aCryptoStream);
            aStreamReader.Close();
            aFileStream.Close();
            return ds;
            
        }


        public JObject readJsonFile(string fileName)
        {
           
            using (StreamReader file = File.OpenText(fileName))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
               return (JObject)JToken.ReadFrom(reader);
            }

           
        }
    }
}


