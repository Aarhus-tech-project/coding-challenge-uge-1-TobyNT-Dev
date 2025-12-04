namespace DungeonCrawler
{
    public class Character
    {
        public DungeonTile myTile;
        public char visualIcon = '#';
        public Character InstantiateCharacter(DungeonTile tileOccupied)
        {
            myTile = tileOccupied;
            return this;
        }

        public void MoveCharacter()
        {
            
        }
    }
}
