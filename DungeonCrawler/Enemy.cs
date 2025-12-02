using System.Linq;

namespace DungeonCrawler
{
    internal class Enemy
    {
        private Random rnd = new Random();
        private MainGame main;

        char[] monsterTypes = { 'Z' };

        public int positionX;
        public int positionZ;

        public char monsterType;

        int detectionDistance = 8;

        int healthPoints = 100;

        public Enemy(int x, int z, MainGame game)
        {
            positionX = x;
            positionZ = z;
            main = game;
            monsterType = monsterTypes[rnd.Next(monsterTypes.Length)];
        }
        public void TakeTurn(int playerX, int playerZ)
        {
            int newX = positionX;
            int newZ = positionZ;

            int absDistX = Math.Abs(playerX - positionX);
            int absDistZ = Math.Abs(playerZ - positionZ);
            //is player within detection range
            
            //is player close enough to attack
            if (absDistX <= 1 && absDistZ <= 1)
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
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                                             ");
            Thread.Sleep(500);
            //roll attack damage
            int rndNum = rnd.Next(0, 101);
            int attackDamage = 0;
            string hitText = string.Empty;
            switch (rndNum)
            {
                // 0–49  (50% chance) Attack missed
                case < 50:
                    attackDamage = 0; // 0 dmg
                    hitText = "Enemy attack missed!";
                    break;
                // 50–74 (25% chance) normal attack hit
                case < 75:
                    attackDamage = rnd.Next(10, 16); // 10–15 dmg
                    hitText = "Enemy hit you with a weak attack!";
                    break;
                // 75–89 (15% chance) strong attack hit
                case < 90:
                    attackDamage = rnd.Next(15, 21); // 15–20 dmg
                    hitText = "Enemy hit you with a strong attack!";
                    break;
                // 90–98 (9% chance) very strong attack hit
                case < 99:
                    attackDamage = rnd.Next(20, 31); // 20–30 dmg
                    hitText = "Enemy hit you with a super strong attack!";
                    break;
                // 100 (1% chance) critical attack hit
                case 100:
                    attackDamage = rnd.Next(50, 101); // 50–100 dmg
                    hitText = "Enemy hit you with a critical hit attack!";
                    break;
            }
            //attack player
            main.DamagePlayer(attackDamage);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{hitText} You Took {attackDamage} damage.");
        }

        public void DamageEnemy(int damage)
        {
            healthPoints += damage;

            //enemy die
            if (healthPoints <= 0)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("                                                             ");
                main.DeleteEnemy(this);
                Console.SetCursorPosition(positionX, positionZ);
                Console.WriteLine(main.floor[0]);
                main.UpdateStats(0, string.Empty, 0, 1, 0, true);
            }
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
