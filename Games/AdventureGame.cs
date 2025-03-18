using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.Games.Interface;

namespace lab2.Games
{
    internal class AdventureGame : Game, IAdventureGame
    {
        public AdventureGame(string name, int Performance, int DiskSpace) : base(name, Performance, DiskSpace)
        {
        }
    }
}
