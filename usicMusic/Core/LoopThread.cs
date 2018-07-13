using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace usicMusic.Core
{
    class LoopThread
    {
        Thread[] loop = new Thread[5];
        int[] delaySec = new int[5];
        StartAndStopMusic[] startMusic = new StartAndStopMusic[5];
        bool[] flag = new bool[5];

        public LoopThread()
        {
            loop[0] = new Thread(new ThreadStart(MusicLoop0));
            loop[1] = new Thread(new ThreadStart(MusicLoop1));
            loop[2] = new Thread(new ThreadStart(MusicLoop2));
            loop[3] = new Thread(new ThreadStart(MusicLoop3));
            loop[4] = new Thread(new ThreadStart(MusicLoop4));
            startMusic[1] = new StartAndStopMusic(2);
            startMusic[2] = new StartAndStopMusic(3);
            startMusic[3] = new StartAndStopMusic(4);
            startMusic[4] = new StartAndStopMusic(5);
        }

        public void LoopStart(int loopNum, int delaySec)
        {
            this.delaySec[loopNum - 1] = delaySec;
            loop[loopNum - 1].Start();
            flag[loopNum - 1] = true;
        }

        public void LoopStop(int loopNum)
        {
            flag[loopNum - 1] = false;
        }

        private void MusicLoop0()
        {
            while(true)
            {
                startMusic[0] = new StartAndStopMusic(1);
                startMusic[0].MusicStart();
                Thread.Sleep(delaySec[0] * 1000);
                startMusic[0].MusicStop();
                if(!flag[0])
                {
                    return;
                }
            }
        }

        private void MusicLoop1()
        {
            while (true)
            {
                startMusic[1] = new StartAndStopMusic(2);
                startMusic[1].MusicStart();
                Console.WriteLine("hi");
                Thread.Sleep(delaySec[1] * 1000);
                Console.WriteLine("bi");
                startMusic[1].MusicStop();
                if (!flag[1])
                {
                    break;
                }
            }
        }

        private void MusicLoop2()
        {
            while (true)
            {
                startMusic[2] = new StartAndStopMusic(3);
                startMusic[2].MusicStart();
                Thread.Sleep(delaySec[2] * 1000);
                startMusic[2].MusicStop();
                if (!flag[2])
                {
                    return;
                }
            }
        }

        private void MusicLoop3()
        {
            while (true)
            {
                startMusic[3] = new StartAndStopMusic(4);
                startMusic[3].MusicStart();
                Thread.Sleep(delaySec[3] * 1000);
                startMusic[3].MusicStop();
                if (!flag[3])
                {
                    return;
                }
            }
        }

        private void MusicLoop4()
        {
            while (true)
            {
                startMusic[4] = new StartAndStopMusic(5);
                startMusic[4].MusicStart();
                Thread.Sleep(delaySec[4] * 1000);
                startMusic[4].MusicStop();
                if (!flag[4])
                {
                    return;
                }
            }
        }

    }
}
