using DungeonCrawler.Utilities;

namespace DungeonCrawler
{

    internal class MainGame
    {
        PlayerCharacter player = new();
        private int width = 64;
        private int height = 24;

        public char[] floor = { '·' };

        private bool playersTurn = true;

        public DungeonLayout dungeonLayout;

        int[] playerCoordinates = new int[2];

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
            player.SpawnPlayer(dungeonLayout, width, height);

            GameLoop(playerCoordinates);
        }

        private void GameLoop(int[] playerCoordinates)
        {
            while (true)
            {
                if (playersTurn)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            playerCoordinates = player.MovePlayer(0, -1);
                            playersTurn = false;
                            break;
                        case ConsoleKey.DownArrow:
                            playerCoordinates = player.MovePlayer(0, 1);
                            playersTurn = false;
                            break;
                        case ConsoleKey.LeftArrow:
                            playerCoordinates = player.MovePlayer(-1, 0);
                            playersTurn = false;
                            break;
                        case ConsoleKey.RightArrow:
                            playerCoordinates = player.MovePlayer(1, 0);
                            playersTurn = false;
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
                else
                {
                    foreach (Enemy enemy in dungeonLayout.enemies)
                    {
                        enemy.TakeTurn(playerCoordinates[0], playerCoordinates[1], dungeonLayout.enemies);
                    }
                    playersTurn = true;
                }
            }
        }
    }
}
