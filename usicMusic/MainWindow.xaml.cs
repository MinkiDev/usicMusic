using Microsoft.Win32;
using System;
using System.Windows;

namespace usicMusic
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        RecordWithWaveIn wave = new RecordWithWaveIn();
        private Boolean state = false;

        public static string GetPath()
        {
            // 컴터마다 파일경로 다를거같아서 일단만들어놓음
            string path = @"D:\class_study\usicMusic\usicMusic\resources\musicTemp\";
            return path;
        }

        public MainWindow()
        {
            InitializeComponent();
            MouseLeftButtonDown += delegate { DragMove(); };
        }

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
                    
                if(Exist.FiveExists()) // 파일개수가 5개가 아니면.
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