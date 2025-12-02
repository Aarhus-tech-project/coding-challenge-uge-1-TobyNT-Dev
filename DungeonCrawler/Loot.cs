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
        public Loot(int _positionX, int _positionZ)
        {
            positionX = _positionX;
            positionZ = _positionZ;
            if (rnd.Next(0, 2) == 1)
            {
                goldAmount = rnd.Next(1, 50);
                isPotion = false;
            }
            else
            {
                healingAmount = rnd.Next(1, 50);
                isPotion = true;
            }
        }
    }
}
