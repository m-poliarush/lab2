using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Games.Interface
{
    public interface IGame
    {
        string Name { get; }
        int RequiredPerformance { get; }
        int RequiredDiskSpace { get; }
        bool isInstalled { get; }
        bool isLaunched { get; }
        bool isLogged { get; }
        List<string> savings { get; }
        void Login();
        void Install();
        void Uninstall();
        void Save(string saveName);
        void LoadFromSave(int index);
        void Launch();
        void Close();
    }
}
