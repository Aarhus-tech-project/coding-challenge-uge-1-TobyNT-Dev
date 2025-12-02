namespace DungeonCrawler
{
    internal class Enemy
    {
        private Random rnd = new Random();
        MainGame main = new();
        PlayerCharacter player = new();
        AttackCalculation attackCalculation = new();

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
            //if not, do noting
            else
            {
                return;
            }
        }

        private void AttackPlayer()
        {
            int attackDamage = attackCalculation.CalculateAttack("player");
            //attack player
            player.TakeDamage(attackDamage);
        }
    }
}
