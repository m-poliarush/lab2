using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.Games;

namespace lab2.Platforms.PlatformFactory
{
    internal abstract class BaseCreator
    {
        public abstract Platform CreatePlatform();
    }
}
