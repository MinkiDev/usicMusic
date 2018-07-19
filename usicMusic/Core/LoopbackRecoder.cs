using NAudio.Wave;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;


namespace usicMusic.Core
{
    class LoopbackRecorder
    {
        WasapiLoopbackCapture capture = new WasapiLoopbackCapture();
        string path = Environment.CurrentDirectory + @"\..\..\Resource\musicTemp\temp6.wav";



        public void StartCapture()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            var writer = new WaveFileWriter(path, capture.WaveFormat);

            capture.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
            };

            capture.RecordingStopped += (s, a) =>
            {
                writer.Dispose();
                writer = null;
                capture.Dispose();
            };

            WasapiCapture waveLoop = new WasapiLoopbackCapture();
            waveLoop.Initialize();
            waveLoop.DataAvailable += waveLoop_DataAvailable;
            waveLoop.Stopped += waveLoop_Stopped;
            waveLoop.Start();

            capture.StartRecording();
        }

        public void StopCapture()
        {
            capture.StopRecording();
        }
    }
}
