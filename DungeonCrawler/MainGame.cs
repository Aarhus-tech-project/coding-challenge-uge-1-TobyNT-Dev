namespace DungeonCrawler
{
    internal class MainGame
    {
        private int width = 64;
        private int height = 24;

        private Random rnd = new Random();

        public char[] floor = { '·' };
        private char[] playerChr = { '☺' };

        private int playerX;
        private int playerZ;

        private int playerLevel = 0;
        private int goldCollected = 0;
        private int playerDamage = 1;
        private int playerHP = 100;
        private int playerKills = 0;

        private bool playersTurn = true;

        public DungeonLayout dungeonLayout;

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
            DungeonGenerator generator = new();
            dungeonLayout = generator.GenerateDungeon(width, height);
            SpawnPlayer();

            GameLoop();
        }

        private void GameLoop()
        {
            while (true)
            {
                if (playersTurn)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow: MovePlayer(0, -1); break;
                        case ConsoleKey.DownArrow: MovePlayer(0, 1); break;
                        case ConsoleKey.LeftArrow: MovePlayer(-1, 0); break;
                        case ConsoleKey.RightArrow: MovePlayer(1, 0); break;

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
                else
                {
                    foreach (Enemy enemy in dungeonLayout.enemies)
                    {
                        enemy.TakeTurn(playerX, playerZ);
                    }
                    playersTurn = true;
                }
            }
        }

        private bool AttackEnemy(Enemy enemy)
        {
            return true;
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

            // collision with enemy - dont move, but attack
            foreach (Enemy enemy in dungeonLayout.enemies)
            {
                if (newX == enemy.positionX && newZ == enemy.positionZ)
                {
                    //attack enemy
                    AttackEnemy(enemy);
                    return;
                }
            }

            // erase old player
            Console.SetCursorPosition(playerX, playerZ);
            Console.Write(floor[0]);

            // draw new player
            Console.SetCursorPosition(newX, newZ);
            Console.Write(playerChr[0]);

            CheckGround(newX, newZ);

            playerX = newX;
            playerZ = newZ;

            playersTurn = false;
        }

        private void CheckGround(int newX, int newZ)
        {
            for (int i = 0; i < dungeonLayout.loot.Count(); i++)
            {
                if (newX == dungeonLayout.loot[i].positionX && newZ == dungeonLayout.loot[i].positionZ)
                {
                    UpdateStats(dungeonLayout.loot[i].goldAmount, 0, 0, 0, 0);
                    dungeonLayout.loot.RemoveAt(i);
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
    }
}
