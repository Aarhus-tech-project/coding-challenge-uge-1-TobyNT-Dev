namespace DungeonCrawler
{
    public class Loot
    {
        private Random rnd = new Random();

        public int positionX;
        public int positionZ;
        public int goldAmount;

        public Loot(int _positionX, int _positionZ)
        {
            positionX = _positionX;
            positionZ = _positionZ;
            goldAmount = rnd.Next(1, 50);
        }
    }
}
