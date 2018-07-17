using FMUtils.KeyboardHook;
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
        private LoopStationCode loopStationCode = new LoopStationCode();
        private LoopThread loopThread = new LoopThread();
        private Hook KeyboardHook = new Hook("Global Action Hook");

        private DoubleAnimation AnimateCursor = new DoubleAnimation();
        private Storyboard CursorAnimation = new Storyboard();

        public LoopStation()
        {
            InitializeComponent();

            KeyboardHook.KeyUpEvent += KeyUp;
            //KListener.KeyUp += new RawKeyEventHandler(KListener_KeyUp);
            ApplicationBorder.MouseLeftButtonDown += delegate { DragMove(); };
            int i = 1;
            while (i <= 100)
            {
                loopDelaySecSelectionBox_1.Items.Add((double)i / 10);
                loopDelaySecSelectionBox_2.Items.Add((double)i / 10);
                loopDelaySecSelectionBox_3.Items.Add((double)i / 10);
                loopDelaySecSelectionBox_4.Items.Add((double)i / 10);
                loopDelaySecSelectionBox_5.Items.Add((double)i / 10);
                i += 1;
            }
            loopDelaySecSelectionBox_1.SelectedIndex = 9;
            loopDelaySecSelectionBox_2.SelectedIndex = 9;
            loopDelaySecSelectionBox_3.SelectedIndex = 9;
            loopDelaySecSelectionBox_4.SelectedIndex = 9;
            loopDelaySecSelectionBox_5.SelectedIndex = 9;

            AnimateCursor.From = 0;
            AnimateCursor.To = 1150;
            AnimateCursor.Duration =
                new Duration(TimeSpan.FromSeconds(30));

            Storyboard.SetTargetName(AnimateCursor, "timeCurLine");
            Storyboard.SetTargetProperty(AnimateCursor,
                new PropertyPath(Canvas.LeftProperty));

            CursorAnimation.Children.Add(AnimateCursor);
        }

        private void KListener_KeyUp(object sender, RawKeyEventArgs args)
        {
            Console.WriteLine(args.Key.ToString());
        }

        // Also: KeyboardHook.KeyUpEvent += KeyUp;

        private new void KeyUp(KeyboardHookEventArgs e)
        {
            if (e.Key == System.Windows.Forms.Keys.Space)
            {
                //?? button_click 호출해야됨
            }
            else if (!e.isCtrlPressed) //그냥 숫자만
            {
                if (e.Key == System.Windows.Forms.Keys.D1)
                {
                    if (loopStationCode.BtnNumClick(0))
                    {
                        AddBeat(1, loopStationCode.MusicSec(0));
                    }
                }
                else if (e.Key == System.Windows.Forms.Keys.D2)
                {
                    if (loopStationCode.BtnNumClick(1))
                    {
                        AddBeat(2, loopStationCode.MusicSec(1));
                    }
                }
                else if (e.Key == System.Windows.Forms.Keys.D3)
                {
                    if (loopStationCode.BtnNumClick(2))
                    {
                        AddBeat(3, loopStationCode.MusicSec(2));
                    }
                }
                else if (e.Key == System.Windows.Forms.Keys.D4)
                {
                    if (loopStationCode.BtnNumClick(3))
                    {
                        AddBeat(4, loopStationCode.MusicSec(3));
                    }
                }
                else if (e.Key == System.Windows.Forms.Keys.D5)
                {
                    if (loopStationCode.BtnNumClick(4))
                    {
                        AddBeat(5, loopStationCode.MusicSec(4));
                    }
                }
            }
            else
            { //이쪽 변경해야됨 (ctrl + 숫자)
                if (e.Key == System.Windows.Forms.Keys.D1)
                {
                    loopDelayCheckBox_1.IsChecked = true;
                }
                else if (e.Key == System.Windows.Forms.Keys.D2)
                {
                    loopDelayCheckBox_2.IsChecked = true;
                }
                else if (e.Key == System.Windows.Forms.Keys.D3)
                {
                    loopDelayCheckBox_3.IsChecked = true;
                }
                else if (e.Key == System.Windows.Forms.Keys.D4)
                {
                    loopDelayCheckBox_4.IsChecked = true;
                }
                else if (e.Key == System.Windows.Forms.Keys.D5)
                {
                    loopDelayCheckBox_5.IsChecked = true;
                }
            }
            KeyboardHook.isPaused = true;
            KeyboardHook.isPaused = false;
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
            if (loopStationCode.BtnNumClick(0))
            {
                AddBeat(1, loopStationCode.MusicSec(0));
            }
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
            if (loopStationCode.BtnNumClick(1))
            {
                AddBeat(2, loopStationCode.MusicSec(1));
            }
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
            if (loopStationCode.BtnNumClick(2))
            {
                AddBeat(3, loopStationCode.MusicSec(2));
            }
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
            if (loopStationCode.BtnNumClick(3))
            {
                AddBeat(4, loopStationCode.MusicSec(3));
            }
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
            if (loopStationCode.BtnNumClick(4))
            {
                AddBeat(5, loopStationCode.MusicSec(4));
            }
        }

        private void loopDelayCheckBox_1_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START") //맨처음에 여기 들어감
            {
                loopDelayCheckBox_1.IsChecked = false;
                return;
            }
            loopThread.LoopStart(1, (int)(Double.Parse(loopDelaySecSelectionBox_1.Text) * 10));
            //이족
        }

        private void loopDelayCheckBox_2_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START")
            {
                loopDelayCheckBox_2.IsChecked = false;
                return;
            }
            loopThread.LoopStart(2, (int)(Double.Parse(loopDelaySecSelectionBox_2.Text) * 10));
        }

        private void loopDelayCheckBox_3_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START")
            {
                loopDelayCheckBox_3.IsChecked = false;
                return;
            }
            loopThread.LoopStart(3, (int)(Double.Parse(loopDelaySecSelectionBox_3.Text) * 10));
        }

        private void loopDelayCheckBox_4_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START")
            {
                loopDelayCheckBox_4.IsChecked = false;
                return;
            }
            loopThread.LoopStart(4, (int)(Double.Parse(loopDelaySecSelectionBox_4.Text) * 10));
        }

        private void loopDelayCheckBox_5_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START")
            {
                loopDelayCheckBox_5.IsChecked = false;
                return;
            }
            loopThread.LoopStart(5, (int)(Double.Parse(loopDelaySecSelectionBox_5.Text) * 10));
        }

        private void loopDelayCheckBox_1_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                loopThread.LoopStop(1);
            }
        }

        private void loopDelayCheckBox_2_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                loopThread.LoopStop(2);
            }
        }

        private void loopDelayCheckBox_3_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                loopThread.LoopStop(3);
            }
        }

        private void loopDelayCheckBox_4_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                loopThread.LoopStop(4);
            }
        }

        private void loopDelayCheckBox_5_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                loopThread.LoopStop(5);
            }
        }

        private UserControl uc;

        public void StartNavCursor() => CursorAnimation.Begin(timeCurLine, true);

        public void StopNavCursor()
        {
            CursorAnimation.Stop(timeCurLine);
        }

        private void AddBeat(int line, int time_ms)
        {
            switch (line)
            {
                case 1:
                    {
                        Thickness btnMargin = new Thickness(timeCurLine.PointToScreen(new Point(0, 0)).X - PointToScreen(new Point(0, 0)).X, 552, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 0.0385 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        AppGrid.Children.Add(currentBeat);

                        break;
                    }

                case 2:
                    {
                        Thickness btnMargin = new Thickness(timeCurLine.PointToScreen(new Point(0, 0)).X - PointToScreen(new Point(0, 0)).X, 608, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 0.0385 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        AppGrid.Children.Add(currentBeat);

                        break;
                    }

                case 3:
                    {
                        Thickness btnMargin = new Thickness(timeCurLine.PointToScreen(new Point(0, 0)).X - PointToScreen(new Point(0, 0)).X, 664, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 0.0385 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        AppGrid.Children.Add(currentBeat);

                        break;
                    }

                case 4:
                    {
                        Thickness btnMargin = new Thickness(timeCurLine.PointToScreen(new Point(0, 0)).X - PointToScreen(new Point(0, 0)).X, 720, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 0.0385 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        AppGrid.Children.Add(currentBeat);

                        break;
                    }

                case 5:
                    {
                        Thickness btnMargin = new Thickness(timeCurLine.PointToScreen(new Point(0, 0)).X - PointToScreen(new Point(0, 0)).X, 775, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 0.0385 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        AppGrid.Children.Add(currentBeat);

                        break;
                    }
            }
        }

        private void startAndStopButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            startAndStopButton.Opacity = 0.8;
        }

        private void startAndStopButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            startAndStopButton.Opacity = 1;
        }

        private void startAndStopButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startAndStopButton.Opacity = 0.5;
        }

        private void startAndStopButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            startAndStopButton.Opacity = 1;

            if ((string)startAndStopLabel.Content == "STOP")
            {
                loopDelayCheckBox_1.IsChecked = false;
                loopDelayCheckBox_2.IsChecked = false;
                loopDelayCheckBox_3.IsChecked = false;
                loopDelayCheckBox_4.IsChecked = false;
                loopDelayCheckBox_5.IsChecked = false;
                StopNavCursor();
                //여기서 이미지 초기화
            }
            else
            {
                StartNavCursor();
            }
            string btnContent = loopStationCode.BtnStartClick();
            if (btnContent != null)
            {
                startAndStopLabel.Content = btnContent;
            }
            if ((string)startAndStopLabel.Content == "START")
            {
                StopNavCursor();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        //private void btnC1_TouchUp(object sender, TouchEventArgs e)
        //{
        //    lsc.BtnNumClick(0); // 0부터시작이니까
        //}

        //private void btnC2_TouchUp(object sender, TouchEventArgs e)
        //{
        //    lsc.BtnNumClick(1); // 0부터시작이니까
        //}

        //private void btnC3_TouchUp(object sender, TouchEventArgs e)
        //{
        //    lsc.BtnNumClick(2); // 0부터시작이니까
        //}

        //private void btnC4_TouchUp(object sender, TouchEventArgs e)
        //{
        //    lsc.BtnNumClick(3); // 0부터시작이니까
        //}

        //private void btnC5_TouchUp(object sender, TouchEventArgs e)
        //{
        //    lsc.BtnNumClick(4); // 0부터시작이니까
        //}
    }
}