﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace usicMusic
{
    class RecordWithWaveIn
    {
        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;

        public void StopRecord()
        {
            waveSource.StopRecording();
        }

        public void StartRecord(string str)
        {
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);
            string path = MainWindow.GetPath(); //파일경로 받아오기
            waveFile = new WaveFileWriter(@path + "temp" + str+".wav", waveSource.WaveFormat); // 절대경로라서 경로설정 해줘야됨.
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
