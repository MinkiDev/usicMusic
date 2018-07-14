using NAudio.Wave;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using NAudio.CoreAudioApi;
using System.Windows.Forms;
using System.Collections.Generic;

namespace usicMusic.Core
{
    class LoopbackRecorder
    {
        string outputFilePath = Environment.CurrentDirectory + @"\..\..\Resource\musicTemp\temp6.wav";

        public void StartCapture()
        {
            string[] array = new string[3];
            array[0] = Environment.CurrentDirectory + @"\..\..\Resource\musicTemp\temp1.wav";
            array[1] = Environment.CurrentDirectory + @"\..\..\Resource\musicTemp\temp2.wav";
            array[2] = Environment.CurrentDirectory + @"\..\..\Resource\musicTemp\temp3.wav";

            Combine(array, outputFilePath);
        }

        public static void Combine(string[] mp3Files, string mp3OuputFile)
        {
            using (var w = new BinaryWriter(File.Create(mp3OuputFile)))
            {
                new List<string>(mp3Files).ForEach(f => w.Write(File.ReadAllBytes(f)));
            }
        }


        public void StopCapture()
        {
        }
    }
}
