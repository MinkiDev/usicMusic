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
        StartAndStopMusic startMusic;
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
                startMusic = new StartAndStopMusic(str);
                startMusic.MusicStart();
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
                startMusic.MusicStop(); // 여기코드 테스트해봐야됨@@@@@@
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
