using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using usicMusic.View;

namespace usicMusic.View
{
    /// <summary>
    /// RecordOrLoad.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RecordOrLoad : Window
    {
        private int musicNum;

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

        public RecordOrLoad(int musicNum)
        {
            this.musicNum = musicNum;
            InitializeComponent();
        }

        private void btnRecord_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnRecord.Opacity = 0.8;
        }

        private void btnRecord_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnRecord.Opacity = 1;
        }

        private void btnRecord_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnRecord.Opacity = 0.5;
        }

        private void btnRecord_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnRecord.Opacity = 1;

            //Record.xaml 창 열기
            var myWindow = Window.GetWindow(this);
            myWindow.Close(); // 현재 창 닫기
            Recorder rcer = new Recorder(musicNum);
            rcer.Show();
        }

        private void btnLoad_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnLoad.Opacity = 0.8;
        }

        private void btnLoad_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnLoad.Opacity = 1;
        }

        private void btnLoad_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnLoad.Opacity = 0.5;
        }

        private void btnLoad_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnLoad.Opacity = 1;

            var myWindow = Window.GetWindow(this);
            myWindow.Close(); // 현재 창 닫기
            LoadMusic loadMusic = new LoadMusic(musicNum);
            loadMusic.Show();
        }
    }
}