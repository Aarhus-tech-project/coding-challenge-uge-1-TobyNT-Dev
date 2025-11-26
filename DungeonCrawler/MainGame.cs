namespace DungeonCrawler
{
    internal class MainGame
    {
        int dungeonWidth = 66;
        int dungeonHeight = 24;

        int minRoomWidth = 4;
        int minRoomHeight = 4;

        int roomAmountX = 3;
        int roomAmountZ = 3;

        Random rnd = new Random();

        List<Room> dungeonRooms = new List<Room>();

        // wall array     0    1    2    3    4    5
        char[] walls = { '═', '║', '╔', '╗', '╚', '╝' };
        char[] loot = { '■' };
        int lootChance = 1000;

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            MainGame program = new MainGame();
            program.RunGame();
    
            bool running = true;
            while (running)
            {
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    Console.Clear();
                    Console.WriteLine("\x1b[3J");

                    program.RunGame();
                }
            }
        }

        private void RunGame() 
        {
            Console.SetWindowSize(dungeonWidth + 20, dungeonHeight + 20);
            Console.BufferWidth = dungeonWidth + 50;
            Console.BufferHeight = dungeonHeight + 50;

            GenerateNewFloor();
        }

        private void GenerateNewFloor()
        {
            dungeonRooms.Clear();

            GenerateRooms();

            foreach(Room room in dungeonRooms)
            {
                for (int x = 0; x <= room.roomW; x++)
                {
                    for (int z = 0; z <= room.roomH; z++)
                    {
                        int xPosition = (dungeonWidth / roomAmountX) * room.gridPosX + room.offsetX + x;
                        int zPosition = (dungeonHeight / roomAmountZ) * room.gridPosZ + room.offsetZ + z;

                        Console.SetCursorPosition(xPosition, zPosition);
                        Console.Write("▉");
                    }
                }
                Thread.Sleep(50);
            }
        }

        //Generate each room, and add to dungeonRooms List
        private void GenerateRooms()
        {
            for (int x = 1; x < roomAmountX + 1; x++)
            {
                for (int z = 1; z < roomAmountZ + 1; z++)
                {
                    dungeonRooms.Add(RandomizeRoomSize(x, z));
                }
            }
        }

        private Room RandomizeRoomSize(int roomPosX, int roomPosZ)
        {
            int dividedRoomW = dungeonWidth / roomAmountX;
            int dividedRoomH = dungeonHeight / roomAmountZ;

            int roomWidth = new Random().Next(minRoomWidth, dividedRoomW - 2);
            int roomHeight = new Random().Next(minRoomHeight, dividedRoomH - 2);

            int offsetX = new Random().Next(1, dividedRoomW - roomWidth - 1);
            int offsetZ = new Random().Next(1, dividedRoomH - roomHeight - 1);

            return new Room(roomWidth, roomHeight, offsetX, offsetZ, roomPosX, roomPosZ);
        }
    }
}
