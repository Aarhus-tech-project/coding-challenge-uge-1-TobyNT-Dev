namespace DungeonCrawler
{
    class DungeonGenerator
    {
        private Random rnd = new Random();
        
        private int lootChance = 100;
        private int enemyChance = 300;

        private List<Loot> generatorLoot = [];
        private List<Enemy> generatorEnemies = [];

        public DungeonLayout GenerateDungeon(int width, int height, MainGame gameReference)
        {
            for (int w = 1; w <= width; w++)
            {
                for (int h = 1; h <= height; h++)
                {
                    Console.SetCursorPosition(w, h);

                    if (w == 1 && h == 1) Console.Write(gameReference.walls[2]);

                    else if (w == width && h == 1) Console.Write(gameReference.walls[3]);

                    else if (w == 1 && h == height) Console.Write(gameReference.walls[4]);

                    else if (w == width && h == height) Console.Write(gameReference.walls[5]);

                    else if (w == 1 || w == width) Console.Write(gameReference.walls[1]);

                    else if (h == 1 || h == height) Console.Write(gameReference.walls[0]);

                    else if (rnd.Next(0, lootChance) == 0)
                    {
                        Loot lootGenerated = new Loot(w, h);
                        if (lootGenerated.isPotion)
                        {
                            Console.Write(gameReference.loot[1]);
                        } else
                        {
                            Console.Write(gameReference.loot[0]);
                        }
                        generatorLoot.Add(lootGenerated);
                    }
                    else if (rnd.Next(0, enemyChance) == 0)
                    {
                        Enemy enemy = new Enemy(w, h, gameReference);
                        generatorEnemies.Add(enemy);
                        Console.Write(enemy.monsterType.ToString());
                    }

                    else Console.Write(gameReference.floor[0]);
                }
            }
            DungeonLayout layout = new DungeonLayout(generatorLoot, generatorEnemies);
            return layout;
        }
    }
}