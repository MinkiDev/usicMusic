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
    /// RecordOrLoad.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RecordOrLoad : Window
    {
        string str = "";
        public RecordOrLoad(string str)
        {
            this.str = str;
            InitializeComponent();
        }

        private void LetsRecord(object sender, RoutedEventArgs e)
        {
            //Record.xaml 창 열기
            var myWindow = Window.GetWindow(this); 
            myWindow.Close(); // 현재 창 닫기
            Record rcer = new Record(str);
            rcer.Show();
        }

        private void LetsLoad(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.Close(); // 현재 창 닫기
            LoadMusic loadMusic = new LoadMusic(str);
            loadMusic.Show();

            //음악 불러오기
        }
    }
}
