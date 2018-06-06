using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ReadingLibrary
{
    public static class Decryptor
    {
        private static readonly byte[] _key = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");
        private static readonly byte[] _iv = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");

        private static readonly ICryptoTransform _decryptor;

        static Decryptor()
        {
            var myRijndael = new RijndaelManaged { Key = _key, IV = _iv, Padding = PaddingMode.PKCS7 };
            _decryptor = myRijndael.CreateDecryptor(myRijndael.Key, myRijndael.IV);
        }

        public static string ReadEncryptFile(string input)
        {
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(input)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, _decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }}
