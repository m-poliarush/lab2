using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Platforms.Models
{
    internal class GamingConsole : Platform
    {
        public GamingConsole() : base()
        {
            Random random = new Random();
            Performance = 600 + (int)(random.NextDouble() * 400);
            DiskSpace = 1000 + (int)(random.NextDouble() * 1000);
            FreeDiskSpace = DiskSpace;
        }
    }
}
