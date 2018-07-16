using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace usicMusic.Core
{
    class RecordWithWaveIn
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
                //File.Delete(FilePath);
            }
        }

        public void StartRecord(int musicNum)
        {
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);
            Directory.CreateDirectory(Path.GetTempPath() + "musicTemp");
            string savePath = Path.GetTempPath() + "musicTemp/temp" + musicNum + ".wav";

            FileExist(savePath);
            waveFile = new WaveFileWriter(savePath, waveSource.WaveFormat); // 절대경로라서 경로설정 해줘야됨.
            waveSource.StartRecording();
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
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
