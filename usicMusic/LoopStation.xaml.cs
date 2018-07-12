using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//이제안건듬
namespace usicMusic
{
    /// <summary>
    /// LoopStation.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoopStation : Window
    {
        LoopStationCode lsc = null;
        public LoopStation()
        {
            InitializeComponent();
            MouseLeftButtonDown += delegate { DragMove(); };
            lsc = new LoopStationCode();
        }

        
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lsc.Button_Click_1(sender);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string btnText = lsc.Button_Click_2();
            if(btnText != null)
            {
                StartAndStop.Content = btnText;
            }
        }
    }
}
