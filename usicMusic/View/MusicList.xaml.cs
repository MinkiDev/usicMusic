using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {

                dynamic selectedItem = musicListView.SelectedItem;
                string downloadServerPath = "http://192.168.43.94:3000" + selectedItem.music.ToString();
                string downloadComputerPath = Path.GetTempPath() + "musicTemp\\temp" + musicNum + ".mp3";

                WebClient wcToilet = new WebClient();

                Directory.CreateDirectory(Path.GetTempPath() + "musicTemp");

                wcToilet.DownloadFile(downloadServerPath, downloadComputerPath);
                Converter converter = new Converter(downloadComputerPath);
                converter.WavtoMp3();

                var myWindow = Window.GetWindow(this);
                myWindow.Close(); // 현재 창 닫기

                new GlobalPopup("완료").ShowDialog();

            }
            catch (Exception e1)
            {
                new GlobalPopup(e1.Message).ShowDialog();

            }
        }

        private void ChooseMusic()
        {
            dynamic raw = JsonConvert.DeserializeObject(jsonData);
            List<musicItem> miList = new List<musicItem>();
            try
            {
                for (int i = 0; i < raw.music.Count; i++)
                {
                    Console.WriteLine(raw.music[i].isMusic.ToString());
                    if (raw.music[i].isMusic.ToString() == "True")
                    {
                        continue;
                    }
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
            } catch
            {
                new GlobalPopup("온라인에 등록된 소스가 없습니다.").ShowDialog();
            }
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