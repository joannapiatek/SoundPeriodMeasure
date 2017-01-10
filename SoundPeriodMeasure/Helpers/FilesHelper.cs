using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SoundPeriodMeasure.Helpers
{
    public static class FilesHelper
    {
        private static string GetChosenDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public static void SaveTextToFile(string content, string fileName)
        {
            var dir = GetChosenDirectory();
            fileName += ".txt";           
            var path = Path.Combine(dir, fileName);

            using (var streamWriter = new StreamWriter(path, false))
            {
                streamWriter.WriteLine(content);
            }
        }

        public static string ReadTextFromFile(string fileName)
        {    
            var path = Path.Combine(GetChosenDirectory(), fileName);

            string content = "";
            using (var streamReader = new StreamReader(path))
            {
                content = streamReader.ReadToEnd();
            }

            return content;
        }

        public static string[] GetFilesNames()
        {
            var filesTemp = Directory.GetFiles(GetChosenDirectory(), "*.txt");
            var filesFinal = new List<string>();

            foreach (var file in filesTemp)
            {
                filesFinal.Add(Regex.Match(file, @"([A-Za-z0-9])*.txt").ToString());
            }

            return filesFinal.ToArray();
        }
    }
}