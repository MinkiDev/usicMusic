﻿using NAudio.Wave;
using NLayer.NAudioSupport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        
        public void WavtoMp4()
        {
            var ffmpeg = new Process
            {
                StartInfo = { UseShellExecute = false, RedirectStandardError = true, FileName = OutPath }
            };

            var arguments =
                String.Format(
                    @"-i ""{0}"" -c:a flac ""{1}""",
                    InPath, OutPath);

            ffmpeg.StartInfo.Arguments = arguments;

            try
            {
                if (!ffmpeg.Start())
                {
                    Debug.WriteLine("Error starting");
                    return;
                }
                var reader = ffmpeg.StandardError;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Debug.WriteLine(line);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
                return;
            }

            ffmpeg.Close();
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