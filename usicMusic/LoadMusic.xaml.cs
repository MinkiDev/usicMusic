using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace usicMusic
{
    /// <summary>
    /// LoadMusic.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoadMusic : Window
    {
        public LoadMusic()
        {
            InitializeComponent();
        }

        private void LoadComputer_Click(object sender, RoutedEventArgs e) //컴퓨터에서 불러오기
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //sfd.FileName = DateTime.Now.ToShortDateString() + " "
            //    + DateTime.Now.ToShortTimeString().Replace(":", "시 ") + "분";
            //sfd.Filter = "오디오 녹음|*.wav";
            ofd.InitialDirectory = "C:\\";
            ofd.ShowDialog();
            string loadFile = ofd.FileName;
            MessageBox.Show(loadFile);
        }

        private void LoadOnlie_Click(object sender, RoutedEventArgs e) //온라인에서 불러오기
        {

        }
    }
}
