namespace DungeonCrawler
{
    internal class MainGame
    {
        public int width = 64;
        public int height = 24;

        private Random rnd = new Random();

        public char[] floor = { '·' };
        private char[] playerChr = { '☺' };
        public char[] loot = { '*', '☤' };
        public char[] walls = { '═', '║', '╔', '╗', '╚', '╝' };

        private int playerX;
        private int playerZ;

        private int playerLevel = 0;
        private int goldCollected = 0;
        private int playerDamage = 1;
        private int playerHP = 100;
        private int playerKills = 0;

        private bool playersTurn = true;

        private bool playerAlive = true;

        public List<Enemy> enemyList = new();
        public List<Loot> lootList = new();

        static void Main(string[] args)
        {
            Console.WriteLine("Press [ F11 ] to Maximize.");
            Console.WriteLine("Press [ Spacebar ] to start game!");
            if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
            {
                Console.CursorVisible = false;
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                MainGame game = new MainGame();
                game.RunGame();
            }
        }

        public void RunGame()
        {
            Console.Clear();
            DungeonGenerator generator = new();
            DungeonLayout dungeonLayout = generator.GenerateDungeon(width, height, this);
            enemyList = dungeonLayout.enemies;
            lootList = dungeonLayout.loot;
            SpawnPlayer();

            GameLoop();
        }

        private void GameLoop()
        {
            while (playerAlive)
            {
                if (playersTurn && playerAlive)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow: MovePlayer(0, -1); break;
                        case ConsoleKey.DownArrow: MovePlayer(0, 1); break;
                        case ConsoleKey.LeftArrow: MovePlayer(-1, 0); break;
                        case ConsoleKey.RightArrow: MovePlayer(1, 0); break;

                        //case ConsoleKey.I:
                        //    // Inventory
                        //    break;

                        //case ConsoleKey.E:
                        //    // Interact
                        //    break;
                    }
                }
                else if (playerAlive)
                {
                    foreach (Enemy enemy in enemyList)
                    {
                        enemy.TakeTurn(playerX, playerZ);
                    }
                    playersTurn = true;
                }
            }
            Console.ReadKey();
        }

        private void AttackEnemy(Enemy enemy)
        {
            int attackDamage = 0;
            string hitText = string.Empty;
            int rndNum = rnd.Next(0, 101);
            switch (rndNum)
            {
                // 0–49  (50% chance) Attack missed
                case < 35:
                    attackDamage = 0; // 0 dmg
                    hitText = "Your attack missed!";
                    break;
                // 50–74 (25% chance) normal attack hit
                case < 75:
                    attackDamage = rnd.Next(16, 20);
                    hitText = "You did a weak attack!";
                    break;
                // 75–89 (15% chance) strong attack hit
                case < 90:
                    attackDamage = rnd.Next(21, 30);
                    hitText = "You did a strong attack!";
                    break;
                // 90–98 (9% chance) very strong attack hit
                case < 99:
                    attackDamage = rnd.Next(31, 50);
                    hitText = "You did a super strong attack!";
                    break;
                // 100 (1% chance) critical attack hit
                case 100:
                    attackDamage = rnd.Next(51, 101);
                    hitText = "You did a critical hit attack!";
                    break;
            }
            enemy.DamageEnemy(attackDamage * -1);
            playersTurn = false;
            UpdateStats(0, $"{hitText} {attackDamage} damage was done!                         ", 0, 0, 0, true);
        }
        public void DeleteEnemy(Enemy toDelete)
        {
            enemyList.Remove(toDelete);
        }
        private void MovePlayer(int nextX, int nextZ)
        {
            int newX = playerX + nextX;
            int newZ = playerZ + nextZ;

            // collision with walls
            if (newX <= 1 || newX >= width ||
                newZ <= 1 || newZ >= height)
                return; // can't move

            // collision with enemy - dont move, but attack
            foreach (Enemy enemy in enemyList)
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
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(floor[0]);
            Console.ResetColor();

            // draw new player
            Console.SetCursorPosition(newX, newZ);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(playerChr[0]);
            Console.ResetColor();

            UpdateStats(0, string.Empty, 0, 0, 0, true);

            CheckGround(newX, newZ);

            playerX = newX;
            playerZ = newZ;

            playersTurn = false;
        }

        private void CheckGround(int newX, int newZ)
        {
            for (int i = 0; i < lootList.Count(); i++)
            {
                if (newX == lootList[i].positionX && newZ == lootList[i].positionZ)
                {
                    if (lootList[i].isPotion)
                    {
                        UpdateStats(0, string.Empty, lootList[i].healingAmount, 0, 0, true);
                        lootList.RemoveAt(i);
                    }
                    else
                    {
                        UpdateStats(lootList[i].goldAmount, string.Empty, 0, 0, 0, true);
                        lootList.RemoveAt(i);
                    }
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
            UpdateStats(0, string.Empty, 0, 0, 0, false);
        }

        public void DamagePlayer(int damageTaken)
        {
            UpdateStats(0, string.Empty, damageTaken * -1, 0, 0, false);
        }

        public void UpdateStats(int gold, string dmg, int hp, int kills, int level, bool showMsg)
        {
            goldCollected += gold;
            playerHP += hp;
            if (playerHP > 100) playerHP = 100;
            playerKills += kills;
            playerLevel += level;
            Console.SetCursorPosition(width + 2, 8);
            Console.WriteLine($"Gold Collected: {goldCollected.ToString()}      ");
            Console.SetCursorPosition(width + 2, 10);
            Console.WriteLine($"Health Points: {playerHP.ToString()}      ");
            Console.SetCursorPosition(width + 2, 12);
            Console.WriteLine($"Kills: {playerKills.ToString()}       ");

            if (showMsg)
            {
                ShowMessage(gold, dmg, hp, kills);
            }
            if (playerHP <= 0)
            {
                playerAlive = false;
                Console.Clear();
                Thread.Sleep(1500);
                Console.SetCursorPosition(20, 10);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You died...");

                Thread.Sleep(1500);

                Console.SetCursorPosition(20, 11);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"You collected a total of {goldCollected} gold!");

                Thread.Sleep(1500);

                Console.SetCursorPosition(20, 12);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"You slayed {playerKills} foes!");
            }
        }

        private void ShowMessage(int gold, string dmg, int hp, int kills)
        {
            if (gold != 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"You Found {gold} gold!            ");
            }
            else if (dmg != string.Empty)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"{dmg}        ");
            }
            else if (hp < 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"This potion contained poison, and did {Math.Abs(hp)} damage!               ");
            }
            else if (hp > 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"This potion gave {Math.Abs(hp)} Health Points!             ");
            }
            else if (kills > 0)
            {
                Console.SetCursorPosition(0, height + 1);
                Console.WriteLine($"You defeated the enemy!                             ");
            }
            else
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"                                                                                  ");
            }
        }
    }
}
