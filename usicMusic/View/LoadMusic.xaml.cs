using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using usicMusic.Connection;
using usicMusic.Core;

namespace usicMusic.View
{
    public partial class LoadMusic : Window
    {
        private int musicNum;

        public LoadMusic(int musicNum)
        {
            InitializeComponent();
            this.musicNum = musicNum;
        }

        private void LoadComputer_Click(object sender, RoutedEventArgs e) //컴퓨터에서 불러오기
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\";
            ofd.ShowDialog();
            string loadFile = ofd.FileName;
            if (ofd.FileName != "")
            {
                string realFileName = loadFile.Substring(loadFile.LastIndexOf("\\") + 1); // 파일이름이 저장됨 ex> hello.txt, temp1.wav
                string destinationFile = Path.GetTempPath() + "musicTemp\\" + realFileName; // 붙여넣을 경로가 저장됨 ex> c:\\test\\hello.wav

                Directory.CreateDirectory(Path.GetTempPath() + "musicTemp");

                //MainWindow.FileExist(destinationFile);
                string tempFile = Path.GetDirectoryName(destinationFile) + "\\temp" + musicNum + Path.GetExtension(destinationFile);

                Converter converter = new Converter(tempFile);
                try
                {
                    converter.FileExist(destinationFile);
                    System.IO.File.Copy(loadFile, destinationFile); // sourceFile -> destinationFile로 copy&paste
                }
                catch (Exception exception)
                {
                    new GlobalPopup(exception.Message).ShowDialog();
                }

                string notInExtension = realFileName.Substring(realFileName.LastIndexOf(".") + 1); // 확장자가 저장됨 ex> txt, mp3...

                converter.FileExist(tempFile); //있으면 삭제
                try
                {
                    File.Move(destinationFile, tempFile); // 이름바꾸기 temp로 확장자는 아직 그대로
                }
                catch
                {
                }

                converter.DeleteBeforeFile(destinationFile);

                if (notInExtension == "wav")
                {
                    //아무것도할게없음
                }
                else
                {
                    if (notInExtension == "mp3")
                    {
                        converter.WavtoMp3();
                    }
                    else
                    {
                        new GlobalPopup("잘못된 확장자!").ShowDialog();
                        //옮긴 파일 삭제해줘야됨
                        converter.DeleteBeforeFile();
                        return;
                    }
                }
            }
        }

        private void LoadOnline_Click(object sender, RoutedEventArgs e)
        {
            HttpConnection http = new HttpConnection();
            http.getMusicList(musicNum);
        }

        private void btnExit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MainWindow.ChangeSource(btnExit, (ImageSource)new ImageSourceConverter()
                .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/ExitButtonHover.png")),
                new TimeSpan(0), new TimeSpan(0, 0, 0, 0, 150));
        }

        private void btnExit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MainWindow.ChangeSource(btnExit, (ImageSource)new ImageSourceConverter()
                .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/ExitButton.png")),
                new TimeSpan(0, 0, 0, 0, 150), new TimeSpan(0));
        }

        private void btnExit_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnExit.Source = (ImageSource)new ImageSourceConverter()
                .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/ExitButtonDown.png"));
        }

        private void btnExit_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }
    }
}