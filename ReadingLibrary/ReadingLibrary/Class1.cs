using System;
using System.IO;

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
    }
}
