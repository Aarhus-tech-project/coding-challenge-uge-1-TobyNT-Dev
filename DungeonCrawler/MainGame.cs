using System.CodeDom.Compiler;

namespace DungeonCrawler
{
    internal class MainGame
    {
        int width = 64;
        int height = 24;

        Random rnd = new Random();
        //                0    1    2    3    4    5
        char[] walls = { '═', '║', '╔', '╗', '╚', '╝' };
        char[] floor = { '⋅' };
        char[] loot = { '▣' };
        char[] player = { '⚉' };

        int lootChance = 1000;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            MainGame mainGame = new MainGame();
            
            mainGame.RunGame();
        }


        void RunGame()
        {
            GenerateDungeon();
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
                        int rndGrass = rnd.Next(0, floor.Length);
                        Console.SetCursorPosition(w, h);
                        Console.Write(floor[rndGrass]);
                    }
                }
            }
        }
    }
}
            

