namespace DungeonCrawler
{
    public class Loot
    {
        private Random rnd = new Random();

        public int positionX;
        public int positionZ;
        public int goldAmount;
        public bool isPotion;
        public int healingAmount;
        public char visual;

        private char[] lootVisual = { '*', '☤' };
        public Loot(int _positionX, int _positionZ)
        {
            positionX = _positionX;
            positionZ = _positionZ;
            if (rnd.Next(0, 2) == 1)
            {
                goldAmount = rnd.Next(1, 50);
                isPotion = false;
                visual = lootVisual[0];
            }
            else
            {
                if (rnd.Next(0, 6) == 0)
                {
                    healingAmount = rnd.Next(20, 60) * -1;
                    isPotion = true;
                    visual = lootVisual[1];
                }
                else
                {
                    healingAmount = rnd.Next(10, 50);
                    isPotion = true;
                    visual = lootVisual[1];
                }
            }
        }
    }
}
