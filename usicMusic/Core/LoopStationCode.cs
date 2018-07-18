using Microsoft.Win32;
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

        public int MusicSec(int musicNum)
        {
            return startMusic[musicNum].GetMusicSec(musicNum + 1);
        }

        public bool BtnNumClick(int musicNum)
        {
            if (!state)
            {
                Button(musicNum + 1); //파일은 1부터니까
                return false;
            }
            else
            {
                if (startMusic[musicNum] != null)
                {
                    Stop(musicNum);
                }
                startMusic[musicNum] = new StartAndStopMusic(musicNum + 1);
                startMusic[musicNum].MusicStart();
                return true;
            }
        }

        public string BtnStartClick()
        {
            LoopbackRecorder recorder = new LoopbackRecorder();
            if (!state) //false일때
            {
                IsExist Exist = new IsExist();
                string isExists = Exist.FiveExists();
                if (!string.IsNullOrEmpty(isExists)) // 파일개수가 5개가 아니면.
                {
                    new GlobalPopup(isExists).Show();
                    return null;
                }
                recorder.StartRecording(Path.GetTempPath() + "um_export_tmp.wav");
                state = !state;
                return "STOP";
            }
            else
            {
                Stop(0); Stop(1); Stop(2); Stop(3); Stop(4);
                recorder.StopRecording();
                
                state = !state;
                return "START";
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