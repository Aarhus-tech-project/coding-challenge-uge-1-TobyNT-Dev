namespace DungeonCrawler
{
    internal class MainGame
    {
        private int width = 64;
        private int height = 24;

        private Random rnd = new Random();

        private char[] walls = { '═', '║', '╔', '╗', '╚', '╝' };
        private char[] floor = { '·' };
        private char[] loot = { '*' };
        private char[] playerChr = { '⚉' };

        private int lootChance = 100;

        private int playerX;
        private int playerZ;

        int playerLevel = 0;
        int goldCollected = 0;
        int playerDamage = 1;
        int playerHP = 100;
        int playerKills = 0;

        List<Loot> droppedLoot = new List<Loot>();

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            MainGame game = new MainGame();
            game.RunGame();
        }

        public void RunGame()
        {
            Console.Clear();
            GenerateDungeon();
            SpawnPlayer();

            GameLoop();
        }

        private void GameLoop()
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow: MovePlayer(0, -1); break;
                    case ConsoleKey.DownArrow: MovePlayer(0, 1); break;
                    case ConsoleKey.LeftArrow: MovePlayer(-1, 0); break;
                    case ConsoleKey.RightArrow: MovePlayer(1, 0); break;

                    case ConsoleKey.Spacebar:
                        // Player attack
                        break;

                    case ConsoleKey.I:
                        // Inventory
                        break;

                    case ConsoleKey.E:
                        // Interact
                        break;

                    case ConsoleKey.Backspace:
                        return; // Quit game
                }
            }
        }

        private void MovePlayer(int nextX, int nextZ)
        {
            UpdateStats(0, 0, 0, 0, 0);
            int newX = playerX + nextX;
            int newZ = playerZ + nextZ;

            // collision with walls
            if (newX <= 1 || newX >= width ||
                newZ <= 1 || newZ >= height)
                return; // can't move

            // erase old player
            Console.SetCursorPosition(playerX, playerZ);
            Console.Write(floor[0]);

            // draw new player
            Console.SetCursorPosition(newX, newZ);
            Console.Write(playerChr[0]);

            CheckGround(newX, newZ);

            playerX = newX;
            playerZ = newZ;
        }

        private void CheckGround(int newX, int newZ)
        {
            for (int i = 0; i < droppedLoot.Count(); i++)
            {
                if (newX == droppedLoot[i].positionX && newZ == droppedLoot[i].positionZ)
                {
                    UpdateStats(droppedLoot[i].goldAmount, 0, 0, 0, 0);
                    droppedLoot.RemoveAt(i);
                    break;
                }
            }

        }

        private void SpawnPlayer()
        {
            playerX = rnd.Next(2, width - 2);
            playerZ = rnd.Next(2, height - 2);

            Console.SetCursorPosition(playerX, playerZ);
            Console.Write(playerChr[0]);
        }

        private void UpdateStats(int gold, int dmg, int hp, int kills, int level)
        {
            goldCollected += gold;
            playerDamage += dmg;
            playerHP += hp;
            playerKills += kills;
            playerLevel += level;
            Console.SetCursorPosition(width + 2, 6);
            Console.WriteLine($"Gold Collected: {goldCollected.ToString()}");
            Console.SetCursorPosition(width + 2, 8);
            Console.WriteLine($"Damage: {playerDamage.ToString()}");
            Console.SetCursorPosition(width + 2, 10);
            Console.WriteLine($"Health Points: {playerHP.ToString()}");
            Console.SetCursorPosition(width + 2, 12);
            Console.WriteLine($"Kills: {playerKills.ToString()}");

            ShowMessage(gold, dmg, hp, kills);
        }

        private void ShowMessage(int gold, int dmg, int hp, int kills)
        {
            if (gold != 0)
            {
                Console.SetCursorPosition(0, height + 1);
                Console.WriteLine($"You Found {gold} gold!");
            }
            else if (dmg != 0)
            {
                Console.SetCursorPosition(0, height + 1);
                Console.WriteLine($"Your Physical Damage increased by {dmg}");
            }
            else if (hp > 0)
            {
                Console.SetCursorPosition(0, height + 1);
                Console.WriteLine($"You Gained {hp} Health Points!");
            }
            else if (hp < 0)
            {
                Console.SetCursorPosition(0, height + 1);
                Console.WriteLine($"You took {hp} damage!");
            }
            else if (kills > 0)
            {
                Console.SetCursorPosition(0, height + 1);
                Console.WriteLine($"boi what the hell boi");
            }
            else
            {
                Console.SetCursorPosition(0, height + 1);
                Console.WriteLine($"                                             ");
            }
        }

        private void GenerateDungeon()
        {
            for (int w = 1; w <= width; w++)
            {
                for (int h = 1; h <= height; h++)
                {
                    Console.SetCursorPosition(w, h);

                    if (w == 1 && h == 1) Console.Write(walls[2]);

                    else if (w == width && h == 1) Console.Write(walls[3]);

                    else if (w == 1 && h == height) Console.Write(walls[4]);

                    else if (w == width && h == height) Console.Write(walls[5]);

                    else if (w == 1 || w == width) Console.Write(walls[1]);

                    else if (h == 1 || h == height) Console.Write(walls[0]);

                    else if (rnd.Next(0, lootChance) == 0)
                    {
                        Console.Write(loot[0]);
                        droppedLoot.Add(new Loot(w, h));
                    }

                    else Console.Write(floor[0]);
                }
            }
        }
    }
}
