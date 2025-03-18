using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Games.Interface
{
    public interface IGameEventNotifier
    {
        event Action<string> GameStateChanged;
    }
}
