using DungeonCrawler.Utilities;

namespace DungeonCrawler
{
    public class DungeonTile
    {
        public Coordinate coords;
        public bool isOccupied;
        public char originalVisual;
        public char currentVisual;
        public ConsoleColor color;
        public Character currentOccupant;
        public Loot currentLoot;

        public DungeonTile InstantiateDungeonTile(Coordinate coordinates, char tileVisual)
        {
            coords = coordinates;
            isOccupied = false;
            originalVisual = tileVisual;

            RevertVisual();
            return this;
        }

        public void SetOccupant(object occupant)
        {
            if (occupant is Character)
            {
                isOccupied = true;
                Character character = (Character) occupant;
                currentOccupant = character;
                UpdateVisual(character.visualIcon);
            }
            else if (occupant is Loot)
            {
                isOccupied = true;
                Loot loot = (Loot)occupant;
                currentLoot = loot;
                UpdateVisual(loot.visual);
            }
        }


        public void UpdateVisual(char newVisual)
        {
            currentVisual = newVisual;
            ConsoleHelper.WriteAt(coords, currentVisual.ToString());
        }

        public void RevertVisual()
        {
            ConsoleHelper.WriteAt(coords, originalVisual.ToString());
        }
    }
}
