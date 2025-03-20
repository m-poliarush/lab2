using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.Games.Interface;

namespace lab2.Games
{
    public class Game : IGame, IObservable<string>
    {
        private List<IObserver<string>> _observers = new List<IObserver<string>>();
        public string Name { get; private set; }
        public int RequiredPerformance { get; private set; }
        public int RequiredDiskSpace { get; private set; }
        public List<string> savings { get; }
        public bool isLogged { get; private set; }
        public bool isLaunched { get; private set; }
        public bool isInstalled {get; private set;}
        public Game(string name, int Performance, int DiskSpace)
        {
            isInstalled = false;
            this.Name = name;
            this.RequiredDiskSpace = DiskSpace;
            this.RequiredPerformance = Performance;
            savings = new List<string>();
        }
        public void Install()
        {
            if (!isInstalled)
            {
                isInstalled=true;
            }
        }
        public void Uninstall()
        {
            if (isInstalled)
            {
                isInstalled = false;
            }
        }
        public void Login()
        {
            if (!this.isInstalled)
            {
                NotifyObservers("First you need to install the game");
                return;
            }
            this.isLogged = true;
            NotifyObservers("Successfully logged in");
        }
        public void Save(string saveName)
        {
            if (!this.isInstalled)
            {
                NotifyObservers("First you need to install the game");
                return;
            }
            savings.Add(saveName);
            NotifyObservers($"Save {saveName} is created");
        }
        public void LoadFromSave(int index)
        {
            if (!this.isInstalled)
            {
                    NotifyObservers("First you need to install the game");
                return;
            }
            if(savings.Count!= 0 && index > -1&& index < savings.Count)
            {
                NotifyObservers($"Loaded save {savings.ElementAt(index)}");
                return;
            }
            else
            {
                NotifyObservers("Index out of range");
            }


        }
        public void Launch()
        {
            if (!this.isInstalled)
            {
                NotifyObservers("First you need to install the game");
                return;
            }
            if (isLaunched)
            {
                NotifyObservers($"The game {Name} is already launched");
            }
            else
            {
                isLaunched = true;
                NotifyObservers($"The game {Name} is launched");
                if (savings.Any())
                {
                    NotifyObservers($"Loaded from {savings.Last()} save");
                }
                else NotifyObservers("No previous saves found");

            }
        }
        public void Close()
        {
            if (!this.isInstalled)
            {
                NotifyObservers("First you need to install the game");
                return;
            }
            if (isLaunched)
            {
                isLaunched = false;
                isLogged = false;
                NotifyObservers($"The game {Name} is closed");
            }
            else
            {
                NotifyObservers($"The game {Name} isn`t launched");
            }
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(_observers, observer);
        }
        public void Unsubscribe(IObserver<string> observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
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
