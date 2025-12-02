using DungeonCrawler.Utilities;

namespace DungeonCrawler
{
    class PlayerCharacter
    {
        HUD hud = new();
        DungeonGenerator gen = new();

        public DungeonLayout dungeonLayout;

        private Random rnd = new Random();

        public int locationX;
        public int locationZ;

        private char[] playerChr = { '☺' };
        
        private int playerLevel = 0;
        private int goldCollected = 0;
        private int playerDamage = 1;
        private int playerHP = 100;
        private int playerKills = 0;

        int mapWidth;
        int mapHeight;


        private bool AttackEnemy(Enemy enemy)
        {
            return true;
        }

        public void TakeDamage(int damage)
        {
            hud.UpdateStats(mapWidth, mapHeight, 0, damage, 0, 0);
        }

        public int[] MovePlayer(int nextX, int nextZ)
        {

            int newX = locationX + nextX;
            int newZ = locationZ + nextZ;

            // collision with walls, dont move
            if (newX <= 1 || newX >= mapWidth ||
                newZ <= 1 || newZ >= mapHeight)
                return [locationX, locationZ];

            // collision with enemy - dont move, but attack
            foreach (Enemy enemy in dungeonLayout.enemies)
            {
                if (newX == enemy.positionX && newZ == enemy.positionZ)
                {
                    //attack enemy
                    AttackEnemy(enemy);
                    return [locationX, locationZ];
                }
            }

            //overwrite old player char
            ConsoleHelper.WriteAt(locationX, locationZ, gen.floor[0]);

            //write new player char
            ConsoleHelper.WriteAt(newX, newZ, playerChr[0]);

            CheckGround(newX, newZ);
            ConsoleHelper.WriteAt(mapWidth + 1, 20, $"[X/Z]: {Math.Abs(locationX)}, {Math.Abs(locationZ)}  ");

            locationX = newX;
            locationZ = newZ;
            return [locationX, locationZ];
        }
        //check for gold/loot on the ground
        private void CheckGround(int newX, int newZ)
        {
            for (int i = 0; i < dungeonLayout.loot.Count(); i++)
            {
                if (newX == dungeonLayout.loot[i].positionX && newZ == dungeonLayout.loot[i].positionZ)
                {
                    hud.UpdateStats(mapWidth, mapHeight, dungeonLayout.loot[i].goldAmount, 0, 0, 0);
                    dungeonLayout.loot.RemoveAt(i);
                    break;
                }
            }
        }

        //Create the character
        public void SpawnPlayer(DungeonLayout layout, int width, int height)
        {
            dungeonLayout = layout;
            mapWidth = width;
            mapHeight = height;

            locationX = rnd.Next(2, width - 2);
            locationZ = rnd.Next(2, height - 2);

            ConsoleHelper.WriteAt(locationX, locationZ, playerChr[0]);

            hud.UpdateStats(mapWidth, mapHeight, 0, 0, 0, 0);
        }
    }
}
