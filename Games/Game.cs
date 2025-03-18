using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab2.Games.Interface;

namespace lab2.Games
{
    public class Game : IGame, IGameEventNotifier
    {
        public event Action<string> GameStateChanged;
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
                GameStateChanged.Invoke("First you need to install the game");
                return;
            }
            this.isLogged = true;
            GameStateChanged.Invoke("Successfully logged in");
        }
        public void Save(string saveName)
        {
            if (!this.isInstalled)
            {
                GameStateChanged.Invoke("First you need to install the game");
                return;
            }
            savings.Add(saveName);
            GameStateChanged.Invoke($"Save {saveName} is created");
        }
        public void LoadFromSave(int index)
        {
            if (!this.isInstalled)
            {
                GameStateChanged.Invoke("First you need to install the game");
                return;
            }
            if(savings.Count!= 0 && index > -1&& index < savings.Count)
            {
                GameStateChanged.Invoke($"Loaded save {savings.ElementAt(index)}");
                return;
            }
            else
            {
                GameStateChanged.Invoke("Index out of range");
            }


        }
        public void Launch()
        {
            if (!this.isInstalled)
            {
                GameStateChanged.Invoke("First you need to install the game");
                return;
            }
            if (isLaunched)
            {
                GameStateChanged.Invoke($"The game {Name} is already launched");
            }
            else
            {
                isLaunched = true;
                GameStateChanged.Invoke($"The game {Name} is launched");
                if (savings.Any())
                {
                    GameStateChanged.Invoke($"Loaded from {savings.Last()} save");
                }
                else GameStateChanged.Invoke("No previous saves found");

            }
        }
        public void Close()
        {
            if (!this.isInstalled)
            {
                GameStateChanged.Invoke("First you need to install the game");
                return;
            }
            if (isLaunched)
            {
                isLaunched = false;
                isLogged = false;
                GameStateChanged.Invoke($"The game {Name} is closed");
            }
            else
            {
                GameStateChanged.Invoke($"The game {Name} isn`t launched");
            }
        }

        protected void ChildClassEvent(string message)
        {
            GameStateChanged.Invoke(message);
        }
    }
}
