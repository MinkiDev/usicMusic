using NAudio.Wave;
using System;
using System.IO;

namespace usicMusic.Core
{
    internal class IsExist
    {
        private string currentPath = Path.GetTempPath() + "musicTemp\\temp";

        public string FiveExists()
        {
            //string path = MainWindow.GetPath();

            string resultString = "없는 파일 : ";
            for (int i = 1; i <= 5; i++)
            {
                resultString += FileExists(i.ToString()) + " ";
            }
            if (resultString.Length == 13)
            {
                return null;
            }
            return resultString;
        }

        private string FileExists(string fileName)
        {
            try
            {
                new AudioFileReader(currentPath + fileName + ".wav");
            }
            catch (Exception)
            {
                return fileName;
            }
            return null;
        }
    }
}