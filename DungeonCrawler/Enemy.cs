using System.Linq;

namespace DungeonCrawler
{
    internal class Enemy
    {
        private Random rnd = new Random();
        MainGame main = new();

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
        public void TakeTurn(int playerX, int playerZ)
        {
            int newX = positionX;
            int newZ = positionZ;

            int directionX = playerX - positionX;
            int directionZ = playerZ - positionZ;
            //is player within detection range
            if (Math.Abs(directionX) < detectionDistance || Math.Abs(directionZ) < detectionDistance)
            {
                MoveTowardPlayer(directionX, directionZ, newX, newZ);
            }
            //is player close enough to attack
            else if (playerX == positionX + 1 || playerX == positionX - 1 || playerZ == positionZ + 1 || playerZ == positionZ - 1)
            {
                AttackPlayer();
            }
            else
            {
                return;
            }
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

        private void MoveTowardPlayer(int directionX, int directionZ, int newX, int newZ)
        {
            if (rnd.Next(0, 2) == 0)
            {
                newX += Math.Sign(directionX);
            }
            else
            {
                newZ += Math.Sign(directionZ);
            }

            Console.SetCursorPosition(positionX, positionZ);
            Console.Write(main.floor[0]);

            Console.SetCursorPosition(newX, newZ);
            Console.Write(monsterType.ToString());

            positionX = newX;
            positionZ = newZ;
        }
    }
}
