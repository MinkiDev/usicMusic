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
        RecordWithWaveIn wave = new RecordWithWaveIn();
        private Boolean state = false;

        public LoopStationCode()
        {

        }

        public void Button_Click_1(object sender)
        {
            string str = sender.ToString().Replace("System.Windows.Controls.Button: ", string.Empty);
            if (!state)
            {
                Button(str);
            }
            else
            {
                StartMusic startMusic = new StartMusic(str);
                startMusic.MusicStart();
                // 노래틀어야됨 여기서 str 값에 따라서
            }
        }

        public string Button_Click_2()
        {
            state = !state;
            if (state)
            {
                IsExist Exist = new IsExist();

                if (Exist.FiveExists()) // 파일개수가 5개가 아니면.
                {
                    state = !state;
                    MessageBox.Show("파일 부족함.");
                    return null;
                }
                wave.StartRecord("ex01");
                return "stop";
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = DateTime.Now.ToShortDateString() + " "
                    + DateTime.Now.ToShortTimeString().Replace(":", "시 ") + "분";
                sfd.Filter = "오디오 녹음|*.wav";
                sfd.ShowDialog();
                string savePath = sfd.FileName;
                // 여기서 만든 음악 저장해야됨.
                return "start";
            }
        }

        private void Button(String str)
        {
            RecordOrLoad rl = new RecordOrLoad(str);
            rl.Show();
        }
    }
}
