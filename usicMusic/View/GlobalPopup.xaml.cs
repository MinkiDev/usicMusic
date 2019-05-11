using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace usicMusic.View
{
    public partial class GlobalPopup : Window
    {
        public GlobalPopup(string content)
        {
            InitializeComponent();
            PopupContent.Content = content;
            DispatcherTimer closingTimer = new DispatcherTimer();
            closingTimer.Tick += ClosingTimer_Tick;
            closingTimer.Interval = TimeSpan.FromMilliseconds(1000);
            closingTimer.Start();
        }

        private void ClosingTimer_Tick(object sender, EventArgs e)
        {
            Close();
        }

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
            Close();
        }
    }
}