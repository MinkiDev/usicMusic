using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using usicMusic.Core;

namespace usicMusic.View
{
    /// <summary>
    /// MusicList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MusicList : Window
    {
        private string jsonData;
        private int musicNum;

        public MusicList(string jsonData, int musicNum)
        {
            InitializeComponent();
            this.jsonData = jsonData;
            this.musicNum = musicNum;
            ChooseMusic();
            musicListView.MouseDoubleClick += MusicListView_MouseDoubleClick;
        }

        private void MusicListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic selectedItem = musicListView.SelectedItem;
            string downloadServerPath = "http://10.80.162.221:3000" + selectedItem.music.ToString();
            string downloadComputerPath = Path.GetTempPath() + "musicTemp\\temp" + musicNum + ".mp3";

            WebClient wcToilet = new WebClient();

            Directory.CreateDirectory(Path.GetTempPath() + "musicTemp");

            wcToilet.DownloadFile(downloadServerPath, downloadComputerPath);
            Converter converter = new Converter(downloadComputerPath);
            converter.WavtoMp3();
        }

        private void ChooseMusic()
        {
            dynamic raw = JsonConvert.DeserializeObject(jsonData);
            List<musicItem> miList = new List<musicItem>();

            for (int i = 0; i < raw.music.Count; i++)
            {
                Button button = new Button();
                button.Text = "선택";
                button.Width = 52;
                miList.Add(new musicItem()
                {
                    title = raw.music[i].title.ToString(),
                    music = raw.music[i].music.ToString(),
                    rate = raw.music[i].rate.Count,
                    date = raw.music[i].date.ToString()
                });
            };
            musicListView.ItemsSource = miList;
            //string dashboardStr = raw.result.statDataList[1].ToString();
            //var b = a["music"]["__v"][0];
            //MessageBox.Show(b.ToString());
            //musicListView.Items.Add(b.ToString());
        }
    }
}

public class musicItem
{
    public string title { get; set; }
    public string music { get; set; }
    public int rate { get; set; }
    public string date { get; set; }
}

/*
foreach(JObject fElement in jFriends)
{
    var fName = fElement["name"] ?? "<NULL>";
Console.WriteLine(fName);
}
*/