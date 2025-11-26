namespace DungeonCrawler
{
    //Constructor for the room data
    public class Room(int roomW, int roomH, int offsetX, int offsetZ, int roomPosition)
    {
        private int _roomPosition = roomPosition;

        private int _roomW = roomW;
        private int _roomH = roomH;

        private int _offsetX = offsetX;
        private int _offsetZ = offsetZ;
    }
}
