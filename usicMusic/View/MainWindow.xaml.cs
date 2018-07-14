﻿using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using usicMusic.Connection;

namespace usicMusic.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplicationBorder.MouseLeftButtonDown += delegate { DragMove(); };
            idTextBox.Focus();
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
            Application.Current.Shutdown();
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

        private void btnLetsFeel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnLetsFeel.Opacity = 0.8;
        }

        private void btnLetsFeel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnLetsFeel.Opacity = 1;
        }

        private void btnLetsFeel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnLetsFeel.Opacity = 0.5;
        }

        private void btnLetsFeel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnLetsFeel.Opacity = 1;
            DoLogin();
        }

        private void pwTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                DoLogin();
            }
        }

        private bool DoLogin()
        {
            HttpConnection http = new HttpConnection();
            string username = idTextBox.Text;
            string password = pwTextBox.Password;
            var json = new JObject();
            json.Add("username", username);
            json.Add("password", password);
            pwTextBox.Password = "";
            if (http.HttpLogin(json.ToString()))
            {
                MessageBox.Show(username + "님 환영합니다!");
                LoopStation ls = new LoopStation();
                ls.ShowDialog();
                return true;
            }
            MessageBox.Show("로그인에 실패하였습니다.");
            return false;
        }
    }
}