using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Platforms.Models
{
    internal class Mobile : Platform
    {
        public Mobile() : base()
        {
            Random random = new Random();
            Performance = 400 + (int)(random.NextDouble() * 400);
            DiskSpace = 256 + (int)(random.NextDouble() * 100);
            FreeDiskSpace = DiskSpace;
            isStreaming = false;
        }
        public bool isStreaming { get; private set; }
        public void StartStreaming()
        {
            if (isStreaming)
            {
                NotifyObservers("Is already streaming");
            }
            else
            {
                isStreaming = true;
                NotifyObservers("Streaming is started");
            }
        }
        public void StopStreaming()
        {
            if (isStreaming)
            {
                isStreaming = false;
                NotifyObservers("Streaming is stopped");
            }
            else
            {
                NotifyObservers("Streaming is already stopped");
            }
        }

    }
}
