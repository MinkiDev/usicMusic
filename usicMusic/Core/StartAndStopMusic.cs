using NAudio.Wave;
using System;
using System.IO;

namespace usicMusic.Core
{
    internal class StartAndStopMusic
    {
        private WaveOutEvent OutputDevice;
        private AudioFileReader AudioFile;
        private string path = "";

        public StartAndStopMusic(int musicNum)
        {
            path = Path.GetTempPath() + "musicTemp\\temp";
            AudioFile = new AudioFileReader(path + musicNum + ".wav");
            OutputDevice = new WaveOutEvent();
        }

        public void MusicStop()
        {
            if (OutputDevice != null)
            {
                OutputDevice.Dispose();
                OutputDevice = null;
            }

            if (AudioFile != null)
            {
                AudioFile.Dispose();
                AudioFile = null;
            }
            //OutputDevice.Stop();
        }

        public int GetMusicSec(int musicNum)
        {
            WaveFileReader wf = new WaveFileReader(path + musicNum + ".wav");
            return (int)(((Double.Parse(wf.TotalTime.TotalSeconds.ToString())) * 60 / 100) * 1000);
        }

        public void MusicStart()
        {
            OutputDevice.Init(AudioFile);
            OutputDevice.Play();
        }
    }
}