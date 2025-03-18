using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Games.Interface
{
    internal interface IRPGGame : IGame
    {
        bool isMultiplayerEnabled { get; }
        void ConnectedManipulators(int num);
    }
}
