using System.Threading;

namespace usicMusic.Core
{
    internal class LoopThread
    {
        private Thread[] loop = new Thread[5];
        private int[] delaySec = new int[5];
        private StartAndStopMusic[] startMusic = new StartAndStopMusic[5];

        public LoopThread()
        {
            //loop[0] = new Thread(new ThreadStart(MusicLoop0));
            //loop[1] = new Thread(new ThreadStart(MusicLoop1));
            //loop[2] = new Thread(new ThreadStart(MusicLoop2));
            //loop[3] = new Thread(new ThreadStart(MusicLoop3));
            //loop[4] = new Thread(new ThreadStart(MusicLoop4));
        }

        public void LoopStart(int loopNum, int delaySec)
        {
            if (loopNum == 1)
            {
                loop[0] = new Thread(new ThreadStart(MusicLoop0));
            }
            else if (loopNum == 2)
            {
                loop[1] = new Thread(new ThreadStart(MusicLoop1));
            }
            else if (loopNum == 3)
            {
                loop[2] = new Thread(new ThreadStart(MusicLoop2));
            }
            else if (loopNum == 4)
            {
                loop[3] = new Thread(new ThreadStart(MusicLoop3));
            }
            else if (loopNum == 5)
            {
                loop[4] = new Thread(new ThreadStart(MusicLoop4));
            }
            this.delaySec[loopNum - 1] = delaySec;
            loop[loopNum - 1].Start();
        }

        public void LoopStop(int loopNum)
        {
            // 여기뭐지/
            startMusic[loopNum - 1].MusicStop();
            loop[loopNum - 1].Abort();
        }

        private void MusicLoop0()
        {
            while (true)
            {
                startMusic[0] = new StartAndStopMusic(1);
                startMusic[0].MusicStart();
                Thread.Sleep(delaySec[0] * 100);
                startMusic[0].MusicStop();
            }
        }

        private void MusicLoop1()
        {
            while (true)
            {
                startMusic[1] = new StartAndStopMusic(2);
                startMusic[1].MusicStart();
                Thread.Sleep(delaySec[1] * 100);
                startMusic[1].MusicStop();
            }
        }

        private void MusicLoop2()
        {
            while (true)
            {
                startMusic[2] = new StartAndStopMusic(3);
                startMusic[2].MusicStart();
                Thread.Sleep(delaySec[2] * 100);
                startMusic[2].MusicStop();
            }
        }

        private void MusicLoop3()
        {
            while (true)
            {
                startMusic[3] = new StartAndStopMusic(4);
                startMusic[3].MusicStart();
                Thread.Sleep(delaySec[3] * 100);
                startMusic[3].MusicStop();
            }
        }

        private void MusicLoop4()
        {
            while (true)
            {
                startMusic[4] = new StartAndStopMusic(5);
                startMusic[4].MusicStart();
                Thread.Sleep(delaySec[4] * 100);
                startMusic[4].MusicStop();
            }
        }
    }
}