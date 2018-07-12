using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace usicMusic
{
    class IsExist
    {
        private string currentPath = Environment.CurrentDirectory + @"\..\..\Resource\musicTemp\temp";
        public string FiveExists()
        {
            //string path = MainWindow.GetPath();

            string resultString = "없는 파일 : ";
            for(int i = 1; i <= 5; i++)
            {
                resultString += FileExists(i.ToString()) + " ";
            }
            if(resultString.Length == 13)
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
            } catch (Exception)
            {
                return fileName;
            }
            return null;
        }
    }
}