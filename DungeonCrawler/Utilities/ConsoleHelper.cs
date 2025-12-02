namespace DungeonCrawler.Utilities;
static class ConsoleHelper
{
    /// <summary>
    /// Moves the console cursor to the specified (x, z) position and writes the
    /// provided text at that location.
    /// </summary>
    public static void WriteAt(int x, int z, object text)
    {
        text.ToString();

        Console.SetCursorPosition(x, z);
        Console.WriteLine(text);
    }
}
