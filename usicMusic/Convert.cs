using NAudio.Wave;
using NLayer.NAudioSupport;
using System;
using System.IO;

namespace usicMusic
{
    internal class Convert
    {
        private string InPath = "";
        private string OutPath = "";

        public Convert(string InPath)
        {
            this.InPath = InPath;
            OutPath = Path.GetDirectoryName(InPath) + "\\" + Path.GetFileNameWithoutExtension(InPath) + ".wav";
            // Path.GetDirectoryName(filepath) 경로만 갖고오는거 + "\" + Path.GetFileNameWithoutExtension(filepath) 파일명만 갖고옴 + ".wav"
        }

        public void WavtoMp3()
        {
            FileExist();
            using (var reader = new Mp3FileReader(InPath, wf => new Mp3FrameDecompressor(wf)))
            {
                WaveFileWriter.CreateWaveFile(OutPath, reader);
            }
            DeleteBeforeFile();
        }

        private void FileExist()
        {
            if (File.Exists(OutPath))
            {
                File.Delete(OutPath);
            }
        }

        public void FileExist(string OutPath)
        {
            if (File.Exists(OutPath))
            {
                File.Delete(OutPath);
            }
        }

        public void DeleteBeforeFile(string InPath)
        {
            try
            {
                System.IO.File.Delete(@InPath);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        public void DeleteBeforeFile()
        {
            try
            {
                System.IO.File.Delete(@InPath);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}