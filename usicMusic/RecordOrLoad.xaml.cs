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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(str);
        }

        private void LetsRecord(object sender, RoutedEventArgs e)
        {
            //Record.xaml 창 열기
            Recorder rcer = new Recorder(str);
            rcer.Show();
        }

        private void LetsLoad(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("d");
        }
    }
}
