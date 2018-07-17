using FMUtils.KeyboardHook;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        private Hook KeyboardHook = new Hook("Global Action Hook");
        private bool[] isCheckedBool = new bool[5] { true, true, true, true, true };

        private DoubleAnimation AnimateCursor = new DoubleAnimation();
        private Storyboard CursorAnimation = new Storyboard();
        private Thread[] loop = new Thread[5];
        private int[] delaySec = new int[5];
        private int[] musicSec = new int[5];
        private StartAndStopMusic[] startMusic = new StartAndStopMusic[5];

        private List<int> line1 = new List<int>();
        private List<int> line2 = new List<int>();
        private List<int> line3 = new List<int>();
        private List<int> line4 = new List<int>();
        private List<int> line5 = new List<int>();

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
            AnimateCursor.Duration = new Duration(TimeSpan.FromSeconds(20));

            Storyboard.SetTargetName(AnimateCursor, "timeCurLine");
            Storyboard.SetTargetProperty(AnimateCursor,
                new PropertyPath(Canvas.LeftProperty));

            CursorAnimation.Children.Add(AnimateCursor);
            CursorAnimation.Completed += CursorAnimation_Completed;
        }

        private void CursorAnimation_Completed(object sender, EventArgs e)
        {
            StopNavCursor();

            Label[] labels = { timelabel_0, timelabel_1, timelabel_2, timelabel_3, timelabel_4 };

            foreach (var label in labels)
            {
                TimeSpan ts = new TimeSpan(0, Int32.Parse(label.Content.ToString().Split(':')[0])
                    , Int32.Parse(label.Content.ToString().Split(':')[1])) + new TimeSpan(0, 0, 20);
                label.Content = ts.Minutes.ToString() + ":" + ts.Seconds.ToString();
            }

            StartNavCursor();

            if (!(line5.Count.Equals(0)))
            {
                if (line5[0] > 0)
                {
                    AddBeat(5);
                    line5[0]--;
                }
                else
                {
                    line5.RemoveAt(0);
                }
            }
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
                    loopDelayCheckBox_1.IsChecked = isCheckedBool[0];
                    isCheckedBool[0] = !isCheckedBool[0];
                }
                else if (e.Key == System.Windows.Forms.Keys.D2)
                {
                    loopDelayCheckBox_2.IsChecked = isCheckedBool[1];
                    isCheckedBool[1] = !isCheckedBool[1];
                }
                else if (e.Key == System.Windows.Forms.Keys.D3)
                {
                    loopDelayCheckBox_3.IsChecked = isCheckedBool[2];
                    isCheckedBool[2] = !isCheckedBool[2];
                }
                else if (e.Key == System.Windows.Forms.Keys.D4)
                {
                    loopDelayCheckBox_4.IsChecked = isCheckedBool[3];
                    isCheckedBool[3] = !isCheckedBool[3];
                }
                else if (e.Key == System.Windows.Forms.Keys.D5)
                {
                    loopDelayCheckBox_5.IsChecked = isCheckedBool[4];
                    isCheckedBool[4] = !isCheckedBool[4];
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

        #region 창 종료 버튼(x)

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

        #endregion 창 종료 버튼(x)

        #region 최소화버튼

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

        #endregion 최소화버튼

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

        #region 쓰레드
        public void MusicLoop(int musicNum)
        {
            while (true)
            {
                startMusic[musicNum] = new StartAndStopMusic(musicNum + 1);
                startMusic[musicNum].MusicStart();
                Dispatcher.Invoke(() =>
                {
                    AddBeat(musicNum + 1, musicSec[musicNum]);
                });
                Thread.Sleep(delaySec[musicNum] * 100);
                startMusic[musicNum].MusicStop();
            }
        }

        public void LoopStart(int loopNum, int delaySec)
        {
            //loop[0] = new Thread(new ThreadStart(MusicLoop0));
            Task.Factory.StartNew(() =>
            {
                MusicLoop(loopNum - 1);
            });
            this.delaySec[loopNum - 1] = delaySec;
            //loop[loopNum - 1].Start();
        }

        public void LoopStop(int loopNum)
        {
            // 여기뭐지/
            //startMusic[loopNum - 1].MusicStop();
            if (loop[loopNum - 1].IsAlive)
            {
                loop[loopNum - 1].Abort();
            }
            startMusic[loopNum - 1].MusicStop();
        }

        #endregion 쓰레드

        private void loopDelayCheckBox_1_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START") //맨처음에 여기 들어감
            {
                loopDelayCheckBox_1.IsChecked = false;
                return;
            }
            //여기 쓰레드
            LoopStart(1, (int)(Double.Parse(loopDelaySecSelectionBox_1.Text) * 10));
            //이족
        }

        private void loopDelayCheckBox_2_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START")
            {
                loopDelayCheckBox_2.IsChecked = false;
                return;
            }
            LoopStart(2, (int)(Double.Parse(loopDelaySecSelectionBox_2.Text) * 10));
        }

        private void loopDelayCheckBox_3_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START")
            {
                loopDelayCheckBox_3.IsChecked = false;
                return;
            }
            LoopStart(3, (int)(Double.Parse(loopDelaySecSelectionBox_3.Text) * 10));
        }

        private void loopDelayCheckBox_4_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START")
            {
                loopDelayCheckBox_4.IsChecked = false;
                return;
            }
            LoopStart(4, (int)(Double.Parse(loopDelaySecSelectionBox_4.Text) * 10));
        }

        private void loopDelayCheckBox_5_Checked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "START")
            {
                loopDelayCheckBox_5.IsChecked = false;
                return;
            }
            LoopStart(5, (int)(Double.Parse(loopDelaySecSelectionBox_5.Text) * 10));
        }

        private void loopDelayCheckBox_1_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                MessageBox.Show("a");
                LoopStop(1);
            }
        }

        private void loopDelayCheckBox_2_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                LoopStop(2);
            }
        }

        private void loopDelayCheckBox_3_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                LoopStop(3);
            }
        }

        private void loopDelayCheckBox_4_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                LoopStop(4);
            }
        }

        private void loopDelayCheckBox_5_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((string)startAndStopLabel.Content == "STOP")
            {
                LoopStop(5);
            }
        }

        public void StartNavCursor() => CursorAnimation.Begin(timeCurLine, true);

        public void StopNavCursor()
        {
            CursorAnimation.Stop(timeCurLine);
            timelineGrid.Children.Clear();
        }

        #region AddBeat

        private void AddBeat(int line)
        {
            switch (line)
            {
                case 5:
                    {
                        Thickness btnMargin = new Thickness(200, 552, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 1400,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        timelineGrid.Children.Add(currentBeat);

                        break;
                    }
            }
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
                            Width = 0.05775 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        timelineGrid.Children.Add(currentBeat);

                        break;
                    }

                case 2:
                    {
                        Thickness btnMargin = new Thickness(timeCurLine.PointToScreen(new Point(0, 0)).X - PointToScreen(new Point(0, 0)).X, 608, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 0.05775 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        timelineGrid.Children.Add(currentBeat);

                        break;
                    }

                case 3:
                    {
                        Thickness btnMargin = new Thickness(timeCurLine.PointToScreen(new Point(0, 0)).X - PointToScreen(new Point(0, 0)).X, 664, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 0.05775 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        timelineGrid.Children.Add(currentBeat);

                        break;
                    }

                case 4:
                    {
                        Thickness btnMargin = new Thickness(timeCurLine.PointToScreen(new Point(0, 0)).X - PointToScreen(new Point(0, 0)).X, 720, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 0.05775 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        timelineGrid.Children.Add(currentBeat);

                        break;
                    }

                case 5:
                    {
                        Thickness btnMargin = new Thickness(timeCurLine.PointToScreen(new Point(0, 0)).X - PointToScreen(new Point(0, 0)).X, 775, 0, 0);

                        Button currentBeat = new Button
                        {
                            Width = 0.05775 * time_ms,
                            Height = 50,
                            Name = "DynamicButton",
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Foreground = Brushes.White,
                            Background = Brushes.DarkCyan,
                            Margin = btnMargin
                        };

                        if ((btnMargin.Left + Width) > 1440)
                        {
                            line5.Add((int)currentBeat.Width / 1440);
                            currentBeat.Width = 1440 - btnMargin.Left;
                        }

                        timelineGrid.Children.Add(currentBeat);

                        break;
                    }
            }
        }

        #endregion AddBeat

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
            }
            else
            {
                StartNavCursor();
            }
            string btnContent = loopStationCode.BtnStartClick();
            if (btnContent != null)
            {
                StartAndStopMusic startAndStopMusic = new StartAndStopMusic();
                musicSec[0] = startAndStopMusic.GetMusicSec(1);
                musicSec[1] = startAndStopMusic.GetMusicSec(2);
                musicSec[2] = startAndStopMusic.GetMusicSec(3);
                musicSec[3] = startAndStopMusic.GetMusicSec(4);
                musicSec[4] = startAndStopMusic.GetMusicSec(5);
                MessageBox.Show(delaySec[0].ToString() + "\n" + delaySec[1].ToString() + "\n" + delaySec[2].ToString() + "\n" + delaySec[3].ToString() + "\n" + delaySec[4].ToString() );
                startAndStopLabel.Content = btnContent;
            }
            if ((string)startAndStopLabel.Content == "START")
            {
                StopNavCursor();
            }
        }

        #region 터치 구현기능(현재 주석처리)

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

        #endregion 터치 구현기능(현재 주석처리)
    }
}