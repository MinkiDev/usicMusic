using NAudio.Wave;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using NAudio.CoreAudioApi;

namespace usicMusic.Core
{
    class LoopbackRecorder
    {
        string outputFilePath = Environment.CurrentDirectory + @"\..\..\Resource\musicTemp\temp6.wav";

        WasapiCapture capture = new WasapiLoopbackCapture();

        private WaveFileWriter RecordedAudioWriter = null;
        private WasapiLoopbackCapture CaptureInstance = null;

        public void StartCapture()
        {

            if (File.Exists(outputFilePath))
            {
                try
                {
                File.Delete(outputFilePath);
                } catch(Exception)
                {

                }
            }
            CaptureInstance = new WasapiLoopbackCapture();
            RecordedAudioWriter = new WaveFileWriter(outputFilePath, CaptureInstance.WaveFormat);

            CaptureInstance.DataAvailable += (s, a) =>
            {
                RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };

            CaptureInstance.RecordingStopped += (s, a) =>
            {
                RecordedAudioWriter.Dispose();
                RecordedAudioWriter = null;
                CaptureInstance.Dispose();
            };

            CaptureInstance.StartRecording();

        }

        public void StopCapture()
        {
            CaptureInstance.StopRecording();
        }
    }
}
