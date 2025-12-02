namespace DungeonCrawler
{
    internal class Enemy
    {
        private Random rnd = new Random();
        MainGame main = new();
        PlayerCharacter player = new();

        char[] monsterTypes = { 'Z' };

        public int positionX;
        public int positionZ;

        public char monsterType;

        int detectionDistance = 8;

        public Enemy(int _positionX, int _positionZ)
        {
            positionX = _positionX;
            positionZ = _positionZ;
            monsterType = monsterTypes[rnd.Next(0, monsterTypes.Length)];
        }
        public void TakeTurn(int playerX, int playerZ, List<Enemy> enemies)
        {
            int newX = positionX;
            int newZ = positionZ;

            // Coordinate distance between me (enemy) and player
            int distX = playerX - positionX;
            int distZ = playerZ - positionZ;

            // absolute distance between me (enemy) and player
            int absDistX = Math.Abs(distX);
            int absDistZ = Math.Abs(distZ);

            // direction player is in
            int dirX = Math.Sign(distX);
            int dirZ = Math.Sign(distZ);

            

            //if player is close enough to attack
            if (absDistX <= 1 || absDistZ <= 1)
            {
                AttackPlayer();
            }
            //if player is within detection range [ Math.Abs -> negative numbers = positive]
            else if (absDistX < detectionDistance || absDistZ < detectionDistance)
            {
                List<Enemy> enemiesBlocked = CheckForBlockedTiles(enemies);

                int[] positions = main.MoveEnemyTowardPlayer(positionX, positionZ, absDistX, absDistZ, dirX, dirZ, newX, newZ, monsterType, enemiesBlocked);
                positionX = positions[0];
                positionZ = positions[1];
            }
            //if not, do noting
            else
            {
                return;
            }
        }

        private List<Enemy> CheckForBlockedTiles(List<Enemy> enemies)
        {
            List<Enemy> enemiesBlocking = enemies.Where(item => item.positionX == positionX++ && item.positionZ == positionZ ||
                                                                item.positionX == positionX-- && item.positionZ == positionZ ||
                                                                item.positionZ == positionZ++ && item.positionX == positionX ||
                                                                item.positionZ == positionZ-- && item.positionX == positionX).ToList();
            return enemiesBlocking;
        }

        private void AttackPlayer()
        {
            //roll attack damage
            int rndNum = rnd.Next(0, 101);
            int attackDamage;

            switch (rndNum)
            {
                // 0–49  (50% chance) Attack missed
                case < 50:
                    attackDamage = 0; // 0 dmg
                    break;
                // 50–74 (25% chance) normal attack hit
                case < 75:
                    attackDamage = rnd.Next(10, 16); // 10–15 dmg
                    break;
                // 75–89 (15% chance) strong attack hit
                case < 90:
                    attackDamage = rnd.Next(15, 21); // 15–20 dmg
                    break;
                // 90–98 (9% chance) very strong attack hit
                case < 99:
                    attackDamage = rnd.Next(20, 31); // 20–30 dmg
                    break;
                // 100 (1% chance) critical attack hit
                case 100:
                    attackDamage = rnd.Next(50, 101); // 50–100 dmg
                    break;
            }
            //attack player
        }
    }
}
