using DungeonCrawler.Utilities;

namespace DungeonCrawler
{
    class DungeonGenerator
    {
        private Random rnd = new Random();

        private int lootChance = 100;
        private int enemyChance = 300;

        private List<Loot> generatorLoot = [];
        private List<Enemy> generatorEnemies = [];

        private DungeonTile tile;

        public List<DungeonTile> GenerateDungeon(int width, int height, MainGame gameReference)
        {
            int totalTiles = width * height;

            List<DungeonTile> map = new List<DungeonTile>();

            for (int w = 1; w <= width; w++)
            {
                for (int h = 1; h <= height; h++)
                {
                    Coordinate coordinate = new Coordinate(w, h);

                    if (w == 1 && h == 1)
                    {
                        tile = new DungeonTile().InstantiateDungeonTile(coordinate, '╔');
                    }

                    else if (w == width && h == 1)
                    {
                        tile = new DungeonTile().InstantiateDungeonTile(coordinate, '╗');
                    }

                    else if (w == 1 && h == height)
                    {
                        tile = new DungeonTile().InstantiateDungeonTile(coordinate, '╚');
                    }

                    else if (w == width && h == height)
                    {
                        tile = new DungeonTile().InstantiateDungeonTile(coordinate, '╝');
                    }

                    else if (w == 1 || w == width)
                    {
                        tile = new DungeonTile().InstantiateDungeonTile(coordinate, '║');
                    }

                    else if (h == 1 || h == height)
                    {
                        tile = new DungeonTile().InstantiateDungeonTile(coordinate, '═');
                    }
                    else
                    {
                        tile = new DungeonTile().InstantiateDungeonTile(coordinate, '·');
                    }
                    map.Add(tile);
                }
            }
            
            return map;
        }
    }
}