using FMUtils.KeyboardHook;
using Microsoft.Win32;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    public partial class LoopStation1 : Window
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
        private bool keyUpFlag = true;

        private List<int> line1 = new List<int>();
        private List<int> line2 = new List<int>();
        private List<int> line3 = new List<int>();
        private List<int> line4 = new List<int>();
        private List<int> line5 = new List<int>();

        public LoopStation1()
        {
            InitializeComponent();
            if (File.Exists(Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.wav"))
            {
                File.Delete(Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.wav");
            }
            KeyboardHook.KeyUpEvent += KeyUp;
            //KListener.KeyUp += new RawKeyEventHandler(KListener_KeyUp);
            ApplicationBorder.MouseLeftButtonDown += delegate { DragMove(); };

            AnimateCursor.From = 0;
            AnimateCursor.To = 1150;
            AnimateCursor.Duration = new Duration(TimeSpan.FromSeconds(20));

            Storyboard.SetTargetName(AnimateCursor, "timeCurLine");
            Storyboard.SetTargetProperty(AnimateCursor,
                new PropertyPath(Canvas.LeftProperty));

            CursorAnimation.Children.Add(AnimateCursor);
            CursorAnimation.Completed += CursorAnimation_Completed;
        }

        #region CursonAnimation_Completed

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
        }

        #endregion CursonAnimation_Completed

        private new void KeyUp(KeyboardHookEventArgs e)
        {
            Debug.WriteLine(e.Key);
            if ((string)startAndStopLabel.Content == "STOP")
            {
                if (e.Key == System.Windows.Forms.Keys.F1)
                {
                    delaySec[0] = (int)((double.Parse(loopDelaySecTextBox_1.Text)) * 10);
                    Loop(0);
                }
                else if (e.Key == System.Windows.Forms.Keys.F2)
                {
                    delaySec[1] = (int)((double.Parse(loopDelaySecTextBox_2.Text)) * 10);
                    Loop(1);
                }
                else if (e.Key == System.Windows.Forms.Keys.F3)
                {
                    delaySec[2] = (int)((double.Parse(loopDelaySecTextBox_3.Text)) * 10);
                    Loop(2);
                }
                else if (e.Key == System.Windows.Forms.Keys.F4)
                {
                    delaySec[3] = (int)((double.Parse(loopDelaySecTextBox_4.Text)) * 10);
                    Loop(3);
                }
                else if (e.Key == System.Windows.Forms.Keys.F5)
                {
                    delaySec[4] = (int)((double.Parse(loopDelaySecTextBox_5.Text)) * 10);
                    Loop(4);
                }
            }

            if (e.Key == System.Windows.Forms.Keys.Space)
            {
                StartAndStopBtnEvent();
            }
            else if (e.Key == System.Windows.Forms.Keys.S && e.isCtrlPressed)
            {
                //저장하기
                SaveButtonEvent();
            }
            else if (e.isAltPressed)
            { // (Alt + ?) 루프 시간 지정
                if (e.Key == System.Windows.Forms.Keys.D1)
                {
                    loopDelaySecTextBox_1.Focusable = true;
                    keyUpFlag = false;
                    loopDelaySecTextBox_1.Focus();
                }
                else if (e.Key == System.Windows.Forms.Keys.D2)
                {
                    loopDelaySecTextBox_2.Focusable = true;
                    keyUpFlag = false;
                    loopDelaySecTextBox_2.Focus();
                }
                else if (e.Key == System.Windows.Forms.Keys.D3)
                {
                    loopDelaySecTextBox_3.Focusable = true;
                    keyUpFlag = false;
                    loopDelaySecTextBox_3.Focus();
                }
                else if (e.Key == System.Windows.Forms.Keys.D4)
                {
                    loopDelaySecTextBox_4.Focusable = true;
                    keyUpFlag = false;
                    loopDelaySecTextBox_4.Focus();
                }
                else if (e.Key == System.Windows.Forms.Keys.D5)
                {
                    loopDelaySecTextBox_5.Focusable = true;
                    keyUpFlag = false;
                    loopDelaySecTextBox_5.Focus();
                    //isCheckedBool[4] = !isCheckedBool[4];
                }
            }
            else if (keyUpFlag) //그냥 숫자만
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

            KeyboardHook.isPaused = true; // 끄기
            KeyboardHook.isPaused = false; // 켜기
        }

        #region invoke

        public void MusicLoop(int musicNum)
        {
            while (!isCheckedBool[musicNum])
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

        public void Loop(int loopNum)
        {
            isCheckedBool[loopNum] = !isCheckedBool[loopNum];
            ChangeSpinnerVisibility(loopNum, !isCheckedBool[loopNum]);
            if (!isCheckedBool[loopNum]) //false
            {
                LoopStart(loopNum);
            }
        }

        public void LoopStart(int loopNum)
        {
            Task.Factory.StartNew(() =>
            {
                MusicLoop(loopNum);
            });
        }

        #endregion invoke

        #region 창 종료 버튼(x)

        private void btnExit_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindow.ChangeSource(btnExit, (ImageSource)new ImageSourceConverter()
                .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/ExitButtonHover.png")),
                new TimeSpan(0), new TimeSpan(0, 0, 0, 0, 150));
        }

        private void btnExit_MouseLeave(object sender, MouseEventArgs e)
        {
			MainWindow.ChangeSource(btnExit, (ImageSource)new ImageSourceConverter()
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
            Application.Current.Shutdown();
        }

        #endregion 창 종료 버튼(x)

        #region 최소화버튼

        private void btnMinimize_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
			MainWindow.ChangeSource(btnMinimize, (ImageSource)new ImageSourceConverter()
               .ConvertFrom(new Uri(@"pack://application:,,,/Resource/Buttons/MinButtonHover.png")),
               new TimeSpan(0), new TimeSpan(0, 0, 0, 0, 150));
        }

        private void btnMinimize_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
			MainWindow.ChangeSource(btnMinimize, (ImageSource)new ImageSourceConverter()
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

        #region MouseEvenHandler

        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            image.Opacity = 0.8;
        }

        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Image image = sender as Image;
            image.Opacity = 1;
        }

        private void Btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            image.Opacity = 0.5;
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

        #endregion MouseEvenHandler

        private void BtnImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            image.Opacity = 1;
            int tag = Int32.Parse(image.Tag.ToString());
            if (loopStationCode.BtnNumClick(tag - 1))
            {
                AddBeat(tag, loopStationCode.MusicSec(tag - 1));
            }
        }

        public void StartNavCursor() => CursorAnimation.Begin(timeCurLine, true);

        public void StopNavCursor()
        {
            CursorAnimation.Stop(timeCurLine);
            timelineGrid.Children.Clear();
        }

        #region AddBeat

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

                        if ((btnMargin.Left + currentBeat.Width) > 1440)
                        {
                            currentBeat.Width = 1440 - btnMargin.Left;
                        }

                        timelineGrid.Children.Add(currentBeat);

                        break;
                    }
            }
        }

        #endregion AddBeat

        #region ChangeSpinnerVisibility

        private void ChangeSpinnerVisibility(int index, bool IsVisible) // true : 보임, false : 안보임 0~4
        {
            Image[] spinners = new Image[] { spinner_1, spinner_2, spinner_3, spinner_4, spinner_5 };
            if (IsVisible) spinners[index].Visibility = Visibility.Visible;
            else spinners[index].Visibility = Visibility.Hidden;
        }

        #endregion ChangeSpinnerVisibility

        private void startAndStopButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StartAndStopBtnEvent();
        }

        private void StartAndStopBtnEvent()
        {
            startAndStopButton.Opacity = 1;

            if ((string)startAndStopLabel.Content == "STOP")
            {
                StopNavCursor();
                Label[] labels = new Label[] { timelabel_0, timelabel_1, timelabel_2, timelabel_3, timelabel_4 };
                TimeSpan ts = new TimeSpan(0, 0, 0);
                foreach (var label in labels)
                {
                    label.Content = ts.Minutes.ToString() + ":" + ts.Seconds.ToString();
                    ts += new TimeSpan(0, 0, 20);
                }
                for (int i = 0; i < 5; i++)
                {
                    if (!isCheckedBool[i])
                    {
                        Loop(i);
                    }
                }
            }
            else
            {
                StartNavCursor();
            }
            string btnContent = loopStationCode.BtnStartClick();
            startAndStopLabel.Content = btnContent;
            if (btnContent == "STOP")
            {
                StartAndStopMusic startAndStopMusic = new StartAndStopMusic(-1);
                musicSec[0] = startAndStopMusic.GetMusicSec(1);
                musicSec[1] = startAndStopMusic.GetMusicSec(2);
                musicSec[2] = startAndStopMusic.GetMusicSec(3);
                musicSec[3] = startAndStopMusic.GetMusicSec(4);
                musicSec[4] = startAndStopMusic.GetMusicSec(5);
            }
            else if (btnContent == "START")
            {
                StopNavCursor();
                for (int i = 0; i < 5; i++)
                {
                    if (!isCheckedBool[i])
                    {
                        Loop(i);
                    }
                }
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

        private void SaveButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SaveButtonEvent();
        }

        private void SaveButtonEvent()
        {
            SaveButton.Opacity = 1;
            if ((string)startAndStopLabel.Content == "START")
            {
                if (!File.Exists(Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.wav"))
                {
                    GlobalPopup globalPopup = new GlobalPopup("제작된 음악이 없습니다.");
                    globalPopup.Show();
                    return;
                }
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = DateTime.Now.ToShortDateString() + " "
                    + DateTime.Now.ToShortTimeString().Replace(":", "시 ") + "분";
                    saveFileDialog.Filter = "오디오 녹음|*.mp3";
                    saveFileDialog.ShowDialog();
                    if (saveFileDialog.FileName != "")
                    {
                        if (File.Exists(saveFileDialog.FileName))
                        {
                            File.Delete(saveFileDialog.FileName);
                        }
                        WaveToMP3(Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.wav", Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.mp3");

                        File.Move(Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.mp3", saveFileDialog.FileName);
                        File.Delete(Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.wav");
                        File.Delete(Path.GetTempPath() + "musicTemp\\" + "um_export_tmp.mp3");
                    }
                }
            }
        }

        public static void WaveToMP3(string waveFileName, string mp3FileName, int bitRate = 128)
        {
            using (var reader = new AudioFileReader(waveFileName))
            using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate))
                reader.CopyTo(writer);
        }

        private void SaveButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveButton.Opacity = 0.8;
        }

        private void SaveButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveButton.Opacity = 1;
        }

        private void SaveButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveButton.Opacity = 0.5;
        }

        private void LoopDelaySecTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int tag = Int32.Parse(textBox.Tag.ToString());
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    delaySec[tag] = (int)((double.Parse(textBox.Text)) * 10);
                }
                catch
                {
                    textBox.Text = "1.0";
                    delaySec[tag] = 10;
                }
                keyUpFlag = true;
                loopDelaySecTextBox_1.Focusable = false;
            }
        }

        private void loopDelaySecTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            keyUpFlag = false;
        }

        private void UploadImage_MouseEnter(object sender, MouseEventArgs e)
        {
            UploadImage.Opacity = 0.8;
        }

        private void UploadImage_MouseLeave(object sender, MouseEventArgs e)
        {
            UploadImage.Opacity = 1.0;
        }

        private void UploadImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UploadImage.Opacity = 0.5;
        }

        private void UploadImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UploadImage.Opacity = 1.0;
            if ((string)startAndStopLabel.Content == "START")
            {
                System.Diagnostics.Process.Start("http://115.68.22.74/music"); //TODO: hyoseong - 나중에 주소 변경해야됨
            }
        }
    }
}