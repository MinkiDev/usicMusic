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

namespace usicMusic
{
    /// <summary>
    /// LoadMusic.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoadMusic : Window
    {
        string str = "";
        public LoadMusic(string str)
        {
            InitializeComponent();
            this.str = str;
        }

        private void LoadComputer_Click(object sender, RoutedEventArgs e) //컴퓨터에서 불러오기
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\";
            ofd.ShowDialog();
            string loadFile = ofd.FileName;
            MessageBox.Show(loadFile);

            string sourceFile = @loadFile;
            string realFileName = loadFile.Substring(loadFile.LastIndexOf("\\") + 1); // 파일이름이 저장됨 ex> hello.txt, temp1.wav
            MessageBox.Show("Hello" + realFileName);

            string destinationFile = @MainWindow.GetPath() + realFileName; // 붙여넣을 경로가 저장됨 ex> c:\\test\\hello.wav

            //MainWindow.FileExist(destinationFile);
            string tempFile = Path.GetDirectoryName(destinationFile) + "\\temp" + str + Path.GetExtension(destinationFile);

            Convert convert = new Convert(tempFile);
            try
            {
                convert.FileExist(destinationFile);
                System.IO.File.Copy(sourceFile, destinationFile); // sourceFile -> destinationFile로 copy&paste
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


            string notInExtension = realFileName.Substring(realFileName.LastIndexOf(".") + 1); // 확장자가 저장됨 ex> txt, mp3...



            convert.FileExist(tempFile); //있으면 삭제
            File.Move(destinationFile, tempFile); // 이름바꾸기 temp로 확장자는 아직 그대로


            convert.DeleteBeforeFile(destinationFile);

            if (notInExtension == "wav")
            {
                //아무것도할게없음
            }
            else
            {
                
                if (notInExtension == "mp3")
                {
                    convert.WavtoMp3();
                }
                else if (notInExtension == "mp4")
                {
                    convert.WavtoMp4();
                }
                else
                {
                    MessageBox.Show("잘못된 확장자!");
                    //옮긴 파일 삭제해줘야됨
                    convert.DeleteBeforeFile();
                    return;
                }
            }

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
