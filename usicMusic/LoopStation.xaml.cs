using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace usicMusic
{
    public partial class LoopStation : Window
    {

        LoopStationCode lsc = new LoopStationCode();

        public LoopStation()
        {
            InitializeComponent();
            ApplicationBorder.MouseLeftButtonDown += delegate { DragMove(); };
            
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

        private void btnExit_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeSource(btnExit, (ImageSource)new ImageSourceConverter()
                .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/ExitButtonHover.png")),
                new TimeSpan(0), new TimeSpan(0, 0, 0, 0, 150));
        }

        private void btnExit_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeSource(btnExit, (ImageSource)new ImageSourceConverter()
                .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/ExitButton.png")),
                new TimeSpan(0, 0, 0, 0, 150), new TimeSpan(0));
        }

        private void btnExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnExit.Source = (ImageSource)new ImageSourceConverter()
                .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/ExitButtonDown.png"));
        }

        private void btnExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void btnMinimize_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ChangeSource(btnMinimize, (ImageSource)new ImageSourceConverter()
               .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/MinButtonHover.png")),
               new TimeSpan(0), new TimeSpan(0, 0, 0, 0, 150));
        }

        private void btnMinimize_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ChangeSource(btnMinimize, (ImageSource)new ImageSourceConverter()
               .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/MinButton.png")),
               new TimeSpan(0, 0, 0, 0, 150), new TimeSpan(0));
        }

        private void btnMinimize_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnMinimize.Source = (ImageSource)new ImageSourceConverter()
                .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/MinButtonDown.png"));
        }

        private void btnMinimize_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnC1_MouseEnter(object sender, MouseEventArgs e)
        {
            btnC1.Opacity = 0.8;
        }

        private void btnC1_MouseLeave(object sender, MouseEventArgs e)
        {
            btnC1.Opacity = 1;
        }

        private void btnC1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnC1.Opacity = 0.5;
        }

        private void btnC1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnC1.Opacity = 1;
            ChangeSource(btnC1, (ImageSource)new ImageSourceConverter()
               .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/c1_down.png")),
               new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 0, 0, 100));
            
            lsc.BtnNumClick(0); // 0부터시작이니까
        }

        private void btnC2_MouseEnter(object sender, MouseEventArgs e)
        {
            btnC2.Opacity = 0.8;
        }

        private void btnC2_MouseLeave(object sender, MouseEventArgs e)
        {
            btnC2.Opacity = 1;
        }

        private void btnC2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnC2.Opacity = 0.5;
        }

        private void btnC2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnC2.Opacity = 1;
            ChangeSource(btnC2, (ImageSource)new ImageSourceConverter()
               .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/c2_down.png")),
               new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 0, 0, 100));

            lsc.BtnNumClick(1); // 0부터시작이니까
        }

        private void btnC3_MouseEnter(object sender, MouseEventArgs e)
        {
            btnC3.Opacity = 0.8;
        }

        private void btnC3_MouseLeave(object sender, MouseEventArgs e)
        {
            btnC3.Opacity = 1;
        }

        private void btnC3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnC3.Opacity = 0.5;
        }

        private void btnC3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnC3.Opacity = 1;
            ChangeSource(btnC3, (ImageSource)new ImageSourceConverter()
               .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/c3_down.png")),
               new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 0, 0, 100));

            lsc.BtnNumClick(2); // 0부터시작이니까
        }

        private void btnC4_MouseEnter(object sender, MouseEventArgs e)
        {
            btnC4.Opacity = 0.8;
        }

        private void btnC4_MouseLeave(object sender, MouseEventArgs e)
        {
            btnC4.Opacity = 1;
        }

        private void btnC4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnC4.Opacity = 0.5;
        }

        private void btnC4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnC4.Opacity = 1;
            ChangeSource(btnC4, (ImageSource)new ImageSourceConverter()
               .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/c4_down.png")),
               new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 0, 0, 100));

            lsc.BtnNumClick(3); // 0부터시작이니까
        }

        private void btnC5_MouseEnter(object sender, MouseEventArgs e)
        {
            btnC5.Opacity = 0.8;
        }

        private void btnC5_MouseLeave(object sender, MouseEventArgs e)
        {
            btnC5.Opacity = 1;
        }

        private void btnC5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnC5.Opacity = 0.5;
        }

        private void btnC5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnC5.Opacity = 1;
            ChangeSource(btnC5, (ImageSource)new ImageSourceConverter()
               .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/c5_down.png")),
               new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 0, 0, 100));

            lsc.BtnNumClick(4); // 0부터시작이니까
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lsc.BtnStartClick();
        }
    }
}