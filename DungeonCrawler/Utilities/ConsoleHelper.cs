using System.Xml;

namespace DungeonCrawler.Utilities
{
    public static class ConsoleHelper
    {
        /// <summary>
        /// Writes input text at specific x/y coordinates. 
        /// [Optional]: Input a ConsoleColor to select print color.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void WriteAt(Coordinate coord, string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(coord.x, coord.y);
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static Coordinate GetRandomTile(int width, int height, List<DungeonTile> tileList, bool canBeBlocked)
        {
            Coordinate result = new Coordinate(0, 0);
            if (canBeBlocked)
            {
                int rndX = new Random().Next(1, width);
                int rndY = new Random().Next(1, width);
                result = new Coordinate(rndX, rndY);
            }
            else
            {
                while (true)
                {
                    int rndX = new Random().Next(1, width);
                    int rndY = new Random().Next(1, width);
                    if (tileList.Where(item => item.coords.x == rndX && item.coords.y == rndY && !item.isOccupied).Any())
                    {
                        result = new Coordinate(rndX, rndY);
                        break;
                    }
                }
            }
            return result;
        }
    }
}
