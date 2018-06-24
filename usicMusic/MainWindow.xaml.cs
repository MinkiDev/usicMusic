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
        private Record rc = new Record();
        private Boolean state = false;

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
                // 노래틀어야됨 여기서 str 값에 따라서
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            state = !state;
            if (state)
            {
                rc.StartRecord();
                StartAndStop.Content = "stop";
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = DateTime.Now.ToShortDateString() + " "
                    + DateTime.Now.ToShortTimeString().Replace(":", "시 ") + "분";
                sfd.Filter = "오디오 녹음|*.wav";
                sfd.ShowDialog();
                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    if (rc.SaveRecord(sfd.FileName))
                    {
                        MessageBox.Show("파일이 저장되었습니다");
                    }
                }
                else
                {
                    MessageBox.Show("저장 실패");
                }
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