using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace usicMusic
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

            string sourceFile = @loadFile;
            string realFileName = loadFile.Substring(loadFile.LastIndexOf("\\") + 1); // 파일이름이 저장됨 ex> hello.txt, temp1.wav
            string destinationFile = Environment.CurrentDirectory + @"\..\..\Resource\musicTemp\" + realFileName; // 붙여넣을 경로가 저장됨 ex> c:\\test\\hello.wav

            //MainWindow.FileExist(destinationFile);
            string tempFile = Path.GetDirectoryName(destinationFile) + "\\temp" + musicNum + Path.GetExtension(destinationFile);

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

        public static void ChangeSource(Image image, ImageSource source, TimeSpan fadeOutTime, TimeSpan fadeInTime)
        {
            var fadeInAnimation = new DoubleAnimation(1d, fadeInTime);

            if (image.Source != null)
            {
                var fadeOutAnimation = new DoubleAnimation(0d, fadeOutTime);

                fadeOutAnimation.Completed += (o, e) =>
                {
                    image.Source = source;
                    image.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
                };

                image.BeginAnimation(Image.OpacityProperty, fadeOutAnimation);
            }
            else
            {
                image.Opacity = 0d;
                image.Source = source;
                image.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
            }
        }

        private void btnExit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ChangeSource(btnExit, (ImageSource)new ImageSourceConverter()
                .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/ExitButtonHover.png")),
                new TimeSpan(0), new TimeSpan(0, 0, 0, 0, 150));
        }

        private void btnExit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ChangeSource(btnExit, (ImageSource)new ImageSourceConverter()
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