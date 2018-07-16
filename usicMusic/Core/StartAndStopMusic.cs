using NAudio.Wave;
using System;
using System.IO;

namespace usicMusic.Core
{
    internal class StartAndStopMusic
    {
        private WaveOutEvent OutputDevice;
        private AudioFileReader AudioFile;

        public StartAndStopMusic(int musicNum)
        {
            string path = Path.GetTempPath() + "musicTemp/temp";
            AudioFile = new AudioFileReader(path + musicNum + ".wav");
            OutputDevice = new WaveOutEvent();
        }

        public void MusicStop()
        {
            OutputDevice.Stop();
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
        }

        public void MusicStart()
        {
            OutputDevice.Init(AudioFile);
            OutputDevice.Play();
        }
    }
}