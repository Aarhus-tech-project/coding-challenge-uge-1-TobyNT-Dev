namespace DungeonCrawler
{
    //Constructor for the room data
    public class Room(int _roomW, int _roomH, int _offsetX, int _offsetZ, int _gridPosX, int _gridPosZ)
    {
        public int roomW = _roomW;
        public int roomH = _roomH;

        public int offsetX = _offsetX;
        public int offsetZ = _offsetZ;

        public int gridPosX = _gridPosX;
        public int gridPosZ = _gridPosZ;
    }
}
