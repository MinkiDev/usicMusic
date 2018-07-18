using NAudio.FileFormats.Mp3;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using usicMusic.Core;

namespace usicMusic.View
{
    /// <summary>
    /// UploadOrSave.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UploadOrSave : Window
    {
        public UploadOrSave()
        {
            InitializeComponent();
            WaveToMP3(Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.wav", Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.mp3");
        }

        public static void WaveToMP3(string waveFileName, string mp3FileName, int bitRate = 128)
        {
            using (var reader = new AudioFileReader(waveFileName))
            using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate))
                reader.CopyTo(writer);
        }

        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = DateTime.Now.ToShortDateString() + " "
                + DateTime.Now.ToShortTimeString().Replace(":", "시 ") + "분";
            sfd.Filter = "오디오 녹음|*.mp3";
            sfd.ShowDialog();
            string savePath = sfd.FileName;
            //여기오류
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            File.Move(Path.GetTempPath() + "um_export_tmp.mp3", savePath);
            File.Delete(Path.GetTempPath() + "um_export_tmp.mp3");
            File.Delete(Path.GetTempPath() + "um_export_tmp.wav");
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