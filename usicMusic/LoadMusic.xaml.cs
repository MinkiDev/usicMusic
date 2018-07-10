using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// LoadMusic.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoadMusic : Window
    {
        public LoadMusic()
        {
            InitializeComponent();
        }

        private void LoadComputer_Click(object sender, RoutedEventArgs e) //컴퓨터에서 불러오기
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //sfd.FileName = DateTime.Now.ToShortDateString() + " "
            //    + DateTime.Now.ToShortTimeString().Replace(":", "시 ") + "분";
            //sfd.Filter = "오디오 녹음|*.wav";
            ofd.InitialDirectory = "C:\\";
            ofd.ShowDialog();
            string loadFile = ofd.FileName;
            MessageBox.Show(loadFile);

            string sourceFile = @loadFile;
            string result_fileName = loadFile.Substring(loadFile.LastIndexOf("\\") + 1);
            MessageBox.Show(result_fileName);
      
            string destinationFile = @MainWindow.GetPath() + result_fileName;

            System.IO.File.Copy(sourceFile, destinationFile);
            //파일 이동
            /*
                https://www.google.co.kr/search?q=c%23+%ED%8C%8C%EC%9D%BC+%EC%9D%B4%EB%8F%99&oq=c%23+%ED%8C%8C%EC%9D%BC+%EC%9D%B4%EB%8F%99&aqs=chrome..69i57j69i58j0l4.3677j0j7&sourceid=chrome&ie=UTF-8
                https://docs.microsoft.com/ko-kr/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders         
             */
            //만약 .wav 가 아니라면 변경
            /*
                https://github.com/naudio/NAudio/blob/master/Docs/ConvertMp3ToWav.md
             */
        }

        private void LoadOnlie_Click(object sender, RoutedEventArgs e) //온라인에서 불러오기
        {

        }
    }
}
