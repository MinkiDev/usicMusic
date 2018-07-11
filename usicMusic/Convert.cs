using NAudio.Wave;
using NLayer.NAudioSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace usicMusic
{
    class Convert
    {
        string InPath = "";
        string OutPath = "";
        public Convert(string InPath)
        {
            this.InPath = InPath;
            OutPath = Path.GetDirectoryName(InPath) + "\\" + Path.GetFileNameWithoutExtension(InPath) + ".wav";
            // Path.GetDirectoryName(filepath) 경로만 갖고오는거 + "\" + Path.GetFileNameWithoutExtension(filepath) 파일명만 갖고옴 + ".wav"
            MessageBox.Show(OutPath);
        }
        

        public void Mp3toWav()
        {
            //using (var reader = new Mp3FileReader(InPath))
            //{
            //    WaveFileWriter.CreateWaveFile(OutPath, reader);
            //}
            using (var reader = new Mp3FileReader(InPath, wf => new Mp3FrameDecompressor(wf)))
            {
                WaveFileWriter.CreateWaveFile(OutPath, reader);
            }
            try
            {
                System.IO.File.Delete(InPath);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        
        public void Mp4toWav()
        {

        }

        public void DeleteBeforeFile()
        {

        }
    }
}