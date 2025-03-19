using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.Games;
using lab2.Games.Interface;

namespace lab2.Platforms
{
    public abstract class Platform : IObservable<string>
    {
        private List<IObserver<string>> _observers = new List<IObserver<string>>();
        public List<IGame> InstalledGames { get; private set; }
        public int Performance { get; protected set; }
        protected int DiskSpace;
        public int FreeDiskSpace { get; protected set; }
        public IGame ActiveGame = null;
        public Platform()
        {
            InstalledGames = new List<IGame>();
        }
        public bool InstallGame(IGame game)
        {
            if (this.FreeDiskSpace < game.RequiredDiskSpace)
            {
                NotifyObservers($"Not enough disk space. Needed {game.RequiredDiskSpace}");
                return false;
            }
            if (game is IStrategyGame && !(this is PC))
            {
                NotifyObservers("The strategy game can only be installed on PC ");
                return false;
            }
            else
            {
                if (InstalledGames.Contains(game))
                {
                    NotifyObservers($"The game {game.Name} is already installed");
                    return false;
                }
                InstalledGames.Add(game);
                game.Install();
                FreeDiskSpace -= game.RequiredDiskSpace;
                NotifyObservers($"The game {game.Name} is successfully installed");
                NotifyObservers($"Disk space left: {FreeDiskSpace}");
                return true;
            }

        }
        public bool UninstallGame(IGame game) {
            if (InstalledGames.Contains(game))
            {
                InstalledGames.Remove(game);
                FreeDiskSpace += game.RequiredDiskSpace;
                game.Uninstall();
                NotifyObservers($"The game {game.Name} is successfully uninstalled");
                NotifyObservers($"Disk space left: {FreeDiskSpace}");
                return true;
            }
            return false;
        }
        public bool LaunchGame(IGame game)
        {
            if (InstalledGames.Contains(game))
            {
                ActiveGame = game;
                game.Launch();
                return true;
            }
            return false;
        }
        public bool CloseGame(IGame game)
        {
            if(this is Mobile mobile)
            {
                mobile.StopStreaming();
            }
            if(ActiveGame == game)
            {
                ActiveGame.Close();
                ActiveGame = null;
                return true;
            }
            return false;

        }
        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(_observers, observer);
        }

        
        protected void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(message);
            }
        }

    }
}
