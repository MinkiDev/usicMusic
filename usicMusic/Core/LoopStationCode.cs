﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using usicMusic.View;

// x누를때 노래 다 스탑 해야됨

namespace usicMusic.Core
{
    internal class LoopStationCode
    {
        private StartAndStopMusic[] startMusic = new StartAndStopMusic[5];
        private bool[] isLoop = new bool[5];
        private RecordWithWaveIn wave = new RecordWithWaveIn();
        private Boolean state = false;

        public LoopStationCode()
        {
        }

        public void BtnNumClick(int musicNum)
        {
            if (!state)
            {
                Button(musicNum + 1); //파일은 1부터니까
            }
            else
            {
                if (startMusic[musicNum] != null)
                {
                    Stop(musicNum);
                }
                startMusic[musicNum] = new StartAndStopMusic(musicNum + 1);
                startMusic[musicNum].MusicStart();
            }
        }

        public string BtnStartClick()
        {
            LoopbackRecorder recorder = new LoopbackRecorder();
            if (!state) //false일때
            {
                IsExist Exist = new IsExist();
                string isExists = Exist.FiveExists();
                if (isExists != null) // 파일개수가 5개가 아니면.
                {
                    MessageBox.Show(isExists);
                    return null;
                }
                recorder.StartRecording(Path.GetTempPath() + "um_export_tmp.wav");
                state = !state;
                return "stop";
            }
            else
            {
                Stop(0); Stop(1); Stop(2); Stop(3); Stop(4);
                recorder.StopRecording();
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = DateTime.Now.ToShortDateString() + " "
                    + DateTime.Now.ToShortTimeString().Replace(":", "시 ") + "분";
                sfd.Filter = "오디오 녹음|*.wav";
                sfd.ShowDialog();
                string savePath = sfd.FileName;
                File.Move(Path.GetTempPath() + "um_export_tmp.wav", savePath);
                File.Delete(Path.GetTempPath() + "um_export_tmp.wav");
                state = !state;
                return "start";
            }
        }

        private void Button(int musicNum)
        {
            RecordOrLoad rl = new RecordOrLoad(musicNum);
            rl.Show();
        }

        private void Stop(int i)
        {
            if (startMusic[i] != null)
            {
                startMusic[i].MusicStop();
                return;
            }
            return;
        }
    }
}