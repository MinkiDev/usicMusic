using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using usicMusic.Core;

namespace usicMusic.View
{
    public partial class LoopStation : Window
    {
        private LoopStationCode lsc = new LoopStationCode();
        private LoopThread lt = new LoopThread();

        public LoopStation()
        {
            InitializeComponent();
            //FormLoad();
            ApplicationBorder.MouseLeftButtonDown += delegate { DragMove(); };
            int i = 0;
            while (i <= 100)
            {
                loopDelaySecSelectionBox_1.Items.Add((double)i / 10);
                loopDelaySecSelectionBox_2.Items.Add((double)i / 10);
                loopDelaySecSelectionBox_3.Items.Add((double)i / 10);
                loopDelaySecSelectionBox_4.Items.Add((double)i / 10);
                loopDelaySecSelectionBox_5.Items.Add((double)i / 10);
                i += 1;
            }
            loopDelaySecSelectionBox_1.SelectedIndex = 0;
            loopDelaySecSelectionBox_2.SelectedIndex = 0;
            loopDelaySecSelectionBox_3.SelectedIndex = 0;
            loopDelaySecSelectionBox_4.SelectedIndex = 0;
            loopDelaySecSelectionBox_5.SelectedIndex = 0;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopButton.Content == "STOP")
            {
                loopDelayCheckBox_1.IsChecked = false;
                loopDelayCheckBox_2.IsChecked = false;
                loopDelayCheckBox_3.IsChecked = false;
                loopDelayCheckBox_4.IsChecked = false;
                loopDelayCheckBox_5.IsChecked = false;
            }
            string btnContent = lsc.BtnStartClick();
            startAndStopButton.Content = btnContent;
        }

        private void loopDelayCheckBox_1_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopButton.Content == "START") //맨처음에 여기 들어감
            {
                loopDelayCheckBox_1.IsChecked = false;
                return;
            }
            lt.LoopStart(1, (int)(Double.Parse(loopDelaySecSelectionBox_1.Text) * 10));
        }

        private void loopDelayCheckBox_2_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopButton.Content == "START")
            {
                loopDelayCheckBox_2.IsChecked = false;
                return;
            }
            lt.LoopStart(2, (int)(Double.Parse(loopDelaySecSelectionBox_2.Text) * 10));
        }

        private void loopDelayCheckBox_3_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopButton.Content == "START")
            {
                loopDelayCheckBox_3.IsChecked = false;
                return;
            }
            lt.LoopStart(3, (int)(Double.Parse(loopDelaySecSelectionBox_3.Text) * 10));
        }

        private void loopDelayCheckBox_4_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopButton.Content == "START")
            {
                loopDelayCheckBox_4.IsChecked = false;
                return;
            }
            lt.LoopStart(4, (int)(Double.Parse(loopDelaySecSelectionBox_4.Text) * 10));
        }

        private void loopDelayCheckBox_5_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopButton.Content == "START")
            {
                loopDelayCheckBox_5.IsChecked = false;
                return;
            }
            lt.LoopStart(5, (int)(Double.Parse(loopDelaySecSelectionBox_5.Text) * 10));
        }

        private void loopDelayCheckBox_1_Unchecked(object sender, RoutedEventArgs e)
        {
            lt.LoopStop(1);
        }

        private void loopDelayCheckBox_2_Unchecked(object sender, RoutedEventArgs e)
        {
            lt.LoopStop(2);
        }

        private void loopDelayCheckBox_3_Unchecked(object sender, RoutedEventArgs e)
        {
            lt.LoopStop(3);
        }

        private void loopDelayCheckBox_4_Unchecked(object sender, RoutedEventArgs e)
        {
            lt.LoopStop(4);
        }

        private void loopDelayCheckBox_5_Unchecked(object sender, RoutedEventArgs e)
        {
            lt.LoopStop(5);
        }
        private void bar1_Click(object sender, RoutedEventArgs e)
        {
            var relCurPos = Mouse.GetPosition(Application.Current.MainWindow);
            Thickness btnMargin = new Thickness(relCurPos.X - 20, 552, 0, 0);

            if (btnMargin.Left < 285)
            {
                btnMargin.Left = 285;
            }
            if (btnMargin.Left > 1375)
            {
                btnMargin.Left = 1375;
            }

            Button newBtn = new Button();
            newBtn.Width = 60;
            newBtn.Height = 50;
            newBtn.Name = "DynamicButton";
            newBtn.Content = "Nyaa";
            newBtn.HorizontalAlignment = HorizontalAlignment.Left;
            newBtn.VerticalAlignment = VerticalAlignment.Top;
            newBtn.Background = Brushes.Fuchsia;
            newBtn.Margin = btnMargin;

            newBtn.MouseMove += NewBtn_MouseMove;

            AppGrid.Children.Add(newBtn);
        }

        private void NewBtn_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            double pX = position.X;
            double pY = position.Y;
        }

        private void btnC1_TouchUp(object sender, TouchEventArgs e)
        {
            lsc.BtnNumClick(0); // 0부터시작이니까
        }

        private void btnC2_TouchUp(object sender, TouchEventArgs e)
        {
            lsc.BtnNumClick(1); // 0부터시작이니까
        }

        private void btnC3_TouchUp(object sender, TouchEventArgs e)
        {
            lsc.BtnNumClick(2); // 0부터시작이니까
        }

        private void btnC4_TouchUp(object sender, TouchEventArgs e)
        {
            lsc.BtnNumClick(3); // 0부터시작이니까
        }

        private void btnC5_TouchUp(object sender, TouchEventArgs e)
        {
            lsc.BtnNumClick(4); // 0부터시작이니까
        }
    }
}