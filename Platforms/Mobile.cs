using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Platforms
{
    internal class Mobile : Platform
    {
        public Mobile() : base(){
            Random random = new Random();
            this.Performance = 400 + (int)(random.NextDouble() * 400);
            this.DiskSpace = 256 + (int)(random.NextDouble() * 100);
            this.FreeDiskSpace = DiskSpace;
            isStreaming = false;
        }
        public bool isStreaming { get; private set; }
        public void StartStreaming()
        {
            if (isStreaming)
            {
                ChildEventStateChanged("Is already streaming");
            }
            else
            {
                isStreaming = true;
                ChildEventStateChanged("Streaming is started");
            }
        }
        public void StopStreaming()
        {
            if (isStreaming)
            {
                isStreaming = false;
                ChildEventStateChanged("Streaming is stopped");
            }
            else
            {
                ChildEventStateChanged("Streaming is already stopped");
            }
        }

    }
}
