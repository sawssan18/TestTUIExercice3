using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Xml;

namespace ReadingLibrary
{
    public class ReadFile
    {
        public  string readFileTxt(string fileName)
        {

            if (File.Exists(fileName))
            {
                string text = File.ReadAllText(fileName);
                return text;
            }
            return null;
        }

        public  XmlDocument readFileXml(string fileName,string role)
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

        public  string ReadEncryptedFile(String FileName, byte[] Key, byte[] vector)
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

    
        }
}


