using NAudio.Wave;
using NLayer.NAudioSupport;
using System;
using System.IO;

namespace usicMusic.Core
{
    internal class Converter
    {
        private string InPath = "";
        private string OutPath = "";

        public Converter(string InPath)
        {
            this.InPath = InPath;
            OutPath = Path.GetDirectoryName(InPath) + "\\" + Path.GetFileNameWithoutExtension(InPath) + ".wav";
        }

        public void WavtoMp3()
        {
            FileExist();
            using (var reader = new Mp3FileReader(InPath, wf => new Mp3FrameDecompressor(wf)))
            {
                WaveFileWriter.CreateWaveFile(OutPath, reader);
            }
            //이전파일 삭제
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