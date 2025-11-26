using System.Diagnostics;
using System.Numerics;

namespace DungeonCrawler
{
    internal class MainGame
    {
        int dungeonWidth = 96;
        int dungeonHeight = 24;

        int minRoomWidth = 5;
        int minRoomHeight = 5;

        int roomAmountX = 3;
        int roomAmountZ = 3;

        Random rnd = new Random();

        List<Room> dungeonRooms = new List<Room>();

        // wall array     0    1    2    3    4    5
        char[] walls = { '═', '║', '╔', '╗', '╚', '╝' };
        char[] loot = { '■' };
        int lootChance = 1000;

        static void Main(string[] args)
        {
            MainGame program = new MainGame();
            program.RunGame();
        }

        private void RunGame()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            GenerateFloor();
        }

        private void GenerateFloor()
        {
            GenerateRooms();

            for (int h = 1; h < dungeonRooms.Count; h++)
            {

            }
        }

        //Generate each room, and add to dungeonRooms List
        private void GenerateRooms()
        {
            for (int x = 1; x < 4; x++)
            {
                dungeonRooms.Add(RandomizeRoomSize(x));
                for (int z = 1; z < 4; z++)
                {
                    dungeonRooms.Add(RandomizeRoomSize(z));
                }
            }
        }

        private Room RandomizeRoomSize(int roomPosition)
        {
            int dividedRoomW = dungeonWidth / roomAmountX;
            int dividedRoomH = dungeonHeight / roomAmountZ;

            int roomWidth = new Random().Next(minRoomWidth, dividedRoomW);
            int roomHeight = new Random().Next(minRoomHeight, dungeonHeight / roomAmountZ);

            int offsetX = new Random().Next(0, dividedRoomH - roomWidth);
            int offsetZ = new Random().Next(0, dividedRoomH - roomWidth);


            return new Room(roomWidth, roomHeight, offsetX, offsetZ, roomPosition);
        }
    }
}
