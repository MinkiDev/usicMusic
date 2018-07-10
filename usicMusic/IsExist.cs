using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace usicMusic
{
    class IsExist
    {
        public bool FiveExists()
        {
            string path = MainWindow.GetPath();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@path + "temp");
            return true;
        }
    }
}
