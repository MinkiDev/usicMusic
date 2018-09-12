using NAudio.Wave;
using System;
using System.IO;

namespace usicMusic.Core
{
    internal class RecordWithWaveIn
    {
        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;

        public void StopRecord()
        {
            waveSource.StopRecording();
        }

        public void FileExist(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                if (waveSource != null)
                {
                    waveSource.Dispose();
                }
                File.Delete(FilePath);
            }
        }

        private string savePath = "";

        public void StartRecord(int musicNum)
        {
            Dispose();
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);
            Directory.CreateDirectory(Path.GetTempPath() + "musicTemp");
            savePath = Path.GetTempPath() + "musicTemp\\temp" + musicNum + ".wav";

            waveFile = new WaveFileWriter(savePath, waveSource.WaveFormat); // 절대경로라서 경로설정 해줘야됨.
            waveSource.StartRecording();
        }

        private void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        public void Dispose()
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
            }
        }

        private void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }
    }
}