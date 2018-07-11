using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace usicMusic
{
    /// <summary>
    /// LoopStation.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoopStation : Window
    {
        public LoopStation()
        {
            InitializeComponent();
            MouseLeftButtonDown += delegate { DragMove(); };
        }

        RecordWithWaveIn wave = new RecordWithWaveIn();
        private Boolean state = false;

        public static void FileExist(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

        //public static string GetPath()
        //{
        //    // 컴터마다 파일경로 다를거같아서 일단만들어놓음
        //    string path = @"D:\class_study\usicMusic\usicMusic\resources\musicTemp\";
        //    return path;
        //}


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            state = !state;
            if (state)
            {
                IsExist Exist = new IsExist();

                if (Exist.FiveExists()) // 파일개수가 5개가 아니면.
                {
                    state = !state;
                    MessageBox.Show("파일 부족함.");
                    return;
                }
                wave.StartRecord("ex01");
                StartAndStop.Content = "stop";
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = DateTime.Now.ToShortDateString() + " "
                    + DateTime.Now.ToShortTimeString().Replace(":", "시 ") + "분";
                sfd.Filter = "오디오 녹음|*.wav";
                sfd.ShowDialog();

                StartAndStop.Content = "start";
            }
        }

        private void Button(String str)
        {
            RecordOrLoad rl = new RecordOrLoad(str);
            rl.Show();
        }
    }
}
