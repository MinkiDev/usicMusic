using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace usicMusic
{
    class StartAndStopMusic
    {
        private WaveOutEvent OutputDevice;
        private AudioFileReader AudioFile;

        public StartAndStopMusic(string MusicPath)
        {
            string path = MainWindow.GetPath();
            AudioFile = new AudioFileReader(@path + "temp" + MusicPath + ".wav");
            OutputDevice = new WaveOutEvent();
        }

        public void IsLoop(string str) //반복재생?
        {

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
