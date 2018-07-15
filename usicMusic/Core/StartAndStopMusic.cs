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
        }

        public void MusicStart()
        {
            OutputDevice.Init(AudioFile);
            OutputDevice.Play();
        }
    }
}