using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using lab2.Games;
using lab2.Games.Interface;
using lab2.Platforms;

namespace lab2
{
    public class Client
    {
        Platform ActivePlatform;

        List<IGame> games;
        public Client()
        {
            games = new List<IGame>();

            games.Add(new RPGGame("The Witcher", 4000, 300));
            games.Add(new RPGGame("Skyrim", 500, 25));
            games.Add(new StrategyGame("Civilization", 300, 20));
            games.Add(new StrategyGame("Hearts Of Iron", 600, 30));
            games.Add(new AdventureGame("Uncharted", 700, 28));
            games.Add(new AdventureGame("Tomb Raider", 650, 26));
        }
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void LoadSave(IGame game)
        {
            
            if (game.savings.Count != 0)
            {
                int index = -1;
                for (int i = 0; i < game.savings.Count; i++)
                {
                    Console.WriteLine($"{i}. {game.savings.ElementAt(i)}");
                }
                Console.WriteLine("Enter the num of save");
                try
                {
                    index = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Incorrect format");
                    return;
                }
                game.LoadFromSave(index);

            }
            else
            {
                Console.WriteLine("No created saves");
            }
        }
        public void Multiplayer(IGame game)
        {
            if (game is IRPGGame rpg) {
                int num = 1;
                Console.WriteLine("Enter a num of manipulators:");
                try
                {
                    num = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Incorrect format");
                    return;
                }
                rpg.ConnectedManipulators(num);
            }


        }
        public void SelectPlatform()
        {
            while (true) {
                Console.Clear();
                Console.WriteLine("Select the platform (PC, Mobile, Console)");
                string platform = Console.ReadLine().ToLower();
                switch (platform)
                {
                    case "pc":
                        ActivePlatform = new PC();
                        
                        break;
                    case "mobile":
                        ActivePlatform = new Mobile();
                        break;
                    case "console":
                        ActivePlatform = new GamingConsole();
                        break;
                        
                }
                if (ActivePlatform == null) continue;
                ActivePlatform.PlatformStateChanged += DisplayMessage;
                break;
            }
        }
        public void LaunchedGameMenu(IGame game)
        {
            
            while (ActivePlatform.ActiveGame!=null)
            {
                Console.WriteLine($"The active game is {game.Name}");
                if (!game.isLogged)
                {
                    Console.WriteLine("Enter Login:");
                    Console.ReadLine();
                    Console.WriteLine("Enter Password");
                    Console.ReadLine();
                    game.Login();
                }
                if(ActivePlatform is Mobile mobile && !mobile.isStreaming)
                {
                    Console.WriteLine("Enable streaming? y/n");
                    switch (Console.ReadLine().ToLower())
                    {
                        case "y":
                            mobile.StartStreaming();
                                break;
                        case "n":
                            break;
                    }
                }



                if (game is IRPGGame)
                {

                    Console.WriteLine("1 - Save game\n2 - Load save\n3 - Multiplayer\n4 - Close game");
                    switch (Console.ReadLine().ToLower())
                    {
                        case "1":
                            Console.WriteLine("Enter a save name^");
                            game.Save(Console.ReadLine());
                            break;
                        case "2":
                            LoadSave(game);
                            break;
                        case "4":
                            CloseGame();
                            break;
                        case "3":
                            Multiplayer(game);
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("1 - Save game\n2 - Load save\n3 - Close game");
                    switch (Console.ReadLine().ToLower())
                    {
                        case "1":
                            Console.WriteLine("Enter a save name^");
                            game.Save(Console.ReadLine());
                            break;
                        case "2":
                            LoadSave(game);
                            break;
                        case "3":
                            CloseGame();
                            break;
                    }
                }
                
                
            }
        }
        public void CloseGame()
        {
            ActivePlatform.CloseGame(ActivePlatform.ActiveGame);
        }
        public void PrintGameList(List<IGame> list)
        {
            Console.WriteLine("\nGame list:");
            for(int i = 0; i< list.Count; i++)
            {
                Console.WriteLine($"{i+1}. {list.ElementAt(i).Name}");
            }
            Console.WriteLine();
        }
        public void InstallGame()
        {
            int index = -1;
            PrintGameList(games);
            Console.WriteLine("Enter the number of select game:");
            try
            {
                index = Int32.Parse(Console.ReadLine()) - 1;
            }
            catch {
                Console.WriteLine("Incorrect format");
                return;
            }
            if (index < 0 || index > games.Count - 1)
            {
                Console.WriteLine("Index out of range");
                return;
            }
            else
            {
                if (ActivePlatform.InstallGame(games.ElementAt(index)))
                {
                    games.ElementAt(index).GameStateChanged += DisplayMessage; 
                }
            }

        }
        public void UninstallGame()
        {
            if(ActivePlatform.InstalledGames.Count < 0)
            {
                Console.WriteLine("The list of installed games is empty");
            }
            else
            {
                int index = -1;
                PrintGameList(ActivePlatform.InstalledGames);
                Console.WriteLine("Enter the number of select game:");
                try
                {
                    index = Int32.Parse(Console.ReadLine()) - 1;
                }
                catch
                {
                    Console.WriteLine("Incorrect format");
                    return;
                }
                if (index < 0 || index > ActivePlatform.InstalledGames.Count - 1)
                {
                    Console.WriteLine("Index out of range");
                    return;
                }
                else
                {
                    if (ActivePlatform.UninstallGame(ActivePlatform.InstalledGames.ElementAt(index)))
                    {
                        games.ElementAt(index).GameStateChanged -= DisplayMessage;
                    }
                }
            }
        }
        public void LaunchGame()
        {
            if(ActivePlatform.InstalledGames.Count < 0)
            {
                Console.WriteLine("The list of installed games is empty");
            }
            else
            {
                int index = -1;
                PrintGameList(ActivePlatform.InstalledGames);
                Console.WriteLine("Enter the number of select game:");
                try
                {
                    index = Int32.Parse(Console.ReadLine()) - 1;
                }
                catch
                {
                    Console.WriteLine("Incorrect format");
                    return;
                }
                if (index < 0 || index > ActivePlatform.InstalledGames.Count - 1)
                {
                    Console.WriteLine("Index out of range");
                    return;
                }
                else
                {
                    if (ActivePlatform.InstalledGames.ElementAt(index).RequiredPerformance > ActivePlatform.Performance)
                    {
                        Console.WriteLine($"Not enough performance. Need {ActivePlatform.InstalledGames.ElementAt(index).RequiredPerformance}");
                        return;
                    }
                    IGame game = ActivePlatform.InstalledGames.ElementAt(index);
                    ActivePlatform.LaunchGame(game);
                }
            }
        }
        public void Main()
        {   
            SelectPlatform();
            while (true)
            {
                if(ActivePlatform.ActiveGame != null) LaunchedGameMenu(ActivePlatform.ActiveGame);
                Console.WriteLine("1 - Game List \n2 - Install Game\n3 - Uninstall Game\n4 - Launch game");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        PrintGameList(games);
                        break;
                    case "2":
                        InstallGame();
                        break;
                    case "3":
                        UninstallGame();
                        break;
                    case "4":
                        LaunchGame();
                        break;
                }
                
            }
        }

    }
}
