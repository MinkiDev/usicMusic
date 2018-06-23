using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace usicMusic
{
    internal class Record
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        public void StartRecord()
        {
            mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);
        }

        public void StopRecord()
        {
            mciSendString("close recsound ", "", 0, 0);
        }

        public bool SaveRecord(string path)
        {
            string str = "save recsound " + path.Replace("\\", "\\\\");
            MessageBox.Show(str);
            try
            {
                mciSendString(str, "", 0, 0);
                //mciSendString("save recsound " + path, "", 0, 0);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("예외 발생: " + e.Message);
            }
            return false;
        }

        public void StopAndSave(string path)
        {
            string str = null;
            mciSendString(str, "", 0, 0);
            mciSendString("close recsound ", "", 0, 0);
        }

        public void PlayRecord(string path)
        {
            try
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                player.Play();
            }
            catch (Exception e)
            {
            }
        }
    }
}