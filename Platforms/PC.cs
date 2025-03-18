using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Platforms
{
     class PC : Platform
    {
        public PC() : base()
        {
            Random random = new Random();
            this.Performance = 700 + (int)(random.NextDouble()*400);
            this.DiskSpace = 1000 + (int)(random.NextDouble()*1000);
            this.FreeDiskSpace = DiskSpace;
        }
    }
}
