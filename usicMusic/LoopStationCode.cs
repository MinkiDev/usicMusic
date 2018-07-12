using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace usicMusic
{
    class LoopStationCode
    {
        StartAndStopMusic[] startMusic = new StartAndStopMusic[5];
        private bool[] isLoop = new bool[5];
        RecordWithWaveIn wave = new RecordWithWaveIn();
        private Boolean state = false;

        public LoopStationCode()
        {

        }

        public void Loop(object sender)
        {

        }

        public void Button_Click_1(object sender)
        {
            string str = sender.ToString().Replace("System.Windows.Controls.Button: ", string.Empty);
            int musicNum = Int32.Parse(str) - 1;
            if (!state)
            {
                Button(str);
            }
            else
            {
                if(startMusic[musicNum] != null)
                {
                    Stop(musicNum);
                }
                startMusic[musicNum] = new StartAndStopMusic(str);
                startMusic[musicNum].MusicStart();
            }
        }

        public string Button_Click_2()
        {

            if (!state) //false일때
            {
                IsExist Exist = new IsExist();
                string isExists = Exist.FiveExists();
                if (isExists != null) // 파일개수가 5개가 아니면.
                {
                    MessageBox.Show(isExists);
                    return null;
                }
                state = !state;
                wave.StartRecord("ex01");
                return "stop";
            }
            else
            {
                Stop(0); Stop(1); Stop(2); Stop(3); Stop(4);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = DateTime.Now.ToShortDateString() + " "
                    + DateTime.Now.ToShortTimeString().Replace(":", "시 ") + "분";
                sfd.Filter = "오디오 녹음|*.wav";
                sfd.ShowDialog();
                string savePath = sfd.FileName;

                // 여기서 만든 음악 저장해야됨.

                state = !state;
                return "start";
            }
        }

        private void Button(String str)
        {
            RecordOrLoad rl = new RecordOrLoad(str);
            rl.Show();
        }

        private void Stop(int i)
        {
            if(startMusic[i] != null)
            {
                startMusic[i].MusicStop();
                return;
            }
            return;
        }
    }
}
