using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.Games.Interface;

namespace lab2.Games
{
    public class RPGGame : Game, IRPGGame
    {

        public bool isMultiplayerEnabled { get; private set; }
        public void ConnectedManipulators(int num)
        {
            if (num > 1)
            {
                isMultiplayerEnabled = true;
                NotifyObservers("Multiplayer is enabled");
            }
            else {
                isMultiplayerEnabled = false;
                NotifyObservers("2 or more manipulators are needed");
            }
        }

        
        public RPGGame(string name, int Performance, int DiskSpace) : base(name, Performance, DiskSpace)
        {
        }

    }
}
