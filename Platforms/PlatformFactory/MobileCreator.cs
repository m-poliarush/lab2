using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.Platforms.Models;

namespace lab2.Platforms.PlatformFactory
{
    internal class MobileCreator : BaseCreator
    {
        public override Platform CreatePlatform()
        {
            return new Mobile();
        }
    }
}
