using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace usicMusic
{
    class IsExist
    {
        public bool FiveExists()
        {
            string path = MainWindow.GetPath();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@path);
            int FileCnt = di.GetFiles().Length;
            if(FileCnt == 5)
            {
                return false; //5개여야 정상. if에서 거를거기때문에 false 반환
            }
            return true;
        }
    }
}
