using System.CodeDom.Compiler;
using System.Net.Mail;

namespace DungeonCrawler
{
    internal class MainGame
    {
        private int width = 64;
        private int height = 24;

        private Random rnd = new Random();
        //                0    1    2    3    4    5
        private char[] walls = { '═', '║', '╔', '╗', '╚', '╝' };
        private char[] floor = { '⋅' };
        private char[] loot = { '▣' };
        private char[] player = { '⚉' };

        private int lootChance = 1000;

        private bool playerActionPerformed = false;
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            MainGame mainGame = new MainGame();
            bool gameEnd = false;


            mainGame.RunGame();
            while (!gameEnd) 
            { 
                if (Console.ReadKey().Key == ConsoleKey.Backspace)
                {
                    gameEnd = true;
                }
            }
        }


        private void RunGame()
        {
            GenerateDungeon();
            SpawnPlayer();

            RunGameLoop();
        }

        private void RunGameLoop()
        {
            if (playerActionPerformed)
            {
                //enemy move or attack
            }
            else if (!playerActionPerformed)
            {
                PlayerAction();
            }
        }

        private void PlayerAction()
        {
            while (!playerActionPerformed)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        MovePlayer("up");
                        break;
                    case ConsoleKey.DownArrow:
                        MovePlayer("down");
                        break;
                    case ConsoleKey.LeftArrow:
                        MovePlayer("left");
                        break;
                    case ConsoleKey.RightArrow:
                        MovePlayer("right");
                        break;
                    default:
                        break;
                }
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    //Player Attack
                }
                if (Console.ReadKey().Key == ConsoleKey.I)
                {
                    //Show player Inventory [Not An Action]
                }
                if (Console.ReadKey().Key == ConsoleKey.E)
                {
                    //Open Chest / Interact with Special Tiles
                }
            }
        }

        private void MovePlayer(string direction)
        {
            if (direction == "up")
            {
                //player position up
            }
            if (direction == "down")
            {
                //player position down
            }
            if (direction == "left")
            {
                //player position left
            }
            if (direction == "right")
            {
                //player position right
            }
        }
        private void SpawnPlayer()
        {
            int spawnX = new Random().Next(2, width - 2);
            int spawnZ = new Random().Next(2, height - 2);

            Console.SetCursorPosition(spawnX, spawnZ);
            Console.Write(player[0]);
        }
        private void GenerateDungeon()
        {
            for (int w = 1; w <= width; w++)
            {
                for (int h = 1; h <= height; h++)
                {
                    if (w == 1 && h == 1)
                    {
                        Console.SetCursorPosition(w, h);
                        Console.Write(walls[2]);
                    }
                    else if (w == width && h == 1)
                    {
                        Console.SetCursorPosition(w, h);
                        Console.Write(walls[3]);
                    }
                    else if (h == height && w == 1)
                    {
                        Console.SetCursorPosition(w, h);
                        Console.Write(walls[4]);
                    }
                    else if (h == height && w == width)
                    {
                        Console.SetCursorPosition(w, h);
                        Console.Write(walls[5]);
                    }
                    else if (w == width || w == 1)
                    {
                        Console.SetCursorPosition(w, h);
                        Console.Write(walls[1]);
                    }
                    else if (h == height || h == 1)
                    {
                        Console.SetCursorPosition(w, h);
                        Console.Write(walls[0]);
                    }
                    else if (rnd.Next(0, lootChance) < 1)
                    {
                        int rndLoot = rnd.Next(0, loot.Length);
                        Console.SetCursorPosition(w, h);
                        Console.Write(loot[rndLoot]);
                    }
                    else
                    {
                        int rndFloor = rnd.Next(0, floor.Length);
                        Console.SetCursorPosition(w, h);
                        Console.Write(floor[rndFloor]);
                    }
                }
                Thread.Sleep(15);
            }
        }
    }
}
            

