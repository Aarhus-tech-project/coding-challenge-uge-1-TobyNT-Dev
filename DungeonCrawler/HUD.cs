using DungeonCrawler.Utilities;
using System.Runtime.ExceptionServices;

namespace DungeonCrawler
{
    internal class HUD
    {
        int goldCollected;
        int playerDamage;
        int playerHP;
        int playerKills;
        int playerLevel;

        //Update Stats, and write in hud
        public void UpdateStats(int width, int height, int gold, int dmg, int hp, int kills, int level)
        {
            goldCollected += gold;
            playerDamage += dmg;
            playerHP += hp;
            playerKills += kills;
            playerLevel += level;
            

            ShowMessage(height, gold, dmg, hp, kills);

            GenerateHudBox(width);
        }

        //Status message generator
        private void ShowMessage(int height, int gold, int dmg, int hp, int kills)
        {
            height++;
            if (gold != 0)
            {
                ConsoleHelper.WriteAt(0, height, $"You Found {gold} gold!");
            }
            else if (dmg != 0)
            {
                ConsoleHelper.WriteAt(0, height, $"Your Physical Damage increased by {dmg}");
            }
            else if (hp > 0)
            {
                ConsoleHelper.WriteAt(0, height, $"You Gained {hp} Health Points!");
            }
            else if (hp < 0)
            {
                ConsoleHelper.WriteAt(0, height, $"You took {hp} damage!");
            }
            else if (kills > 0)
            {
                ConsoleHelper.WriteAt(0, height, $"boi what the hell boi");
            }
            else
            {
                ConsoleHelper.WriteAt(0, height, "                                              ");
            }
        }

        private void GenerateHudBox(int width)
        {
            // write stats
            ConsoleHelper.WriteAt(width + 2, 6,  $" Gold Collected: {goldCollected}");
            ConsoleHelper.WriteAt(width + 2, 8,  $" Damage: {playerDamage}");
            ConsoleHelper.WriteAt(width + 2, 10, $" Health Points: {playerHP}");
            ConsoleHelper.WriteAt(width + 2, 12, $" Kills: {playerKills}");

            // generate box around stats
            GenerateBox(30, 10, width + 1, 4);
        }
        private static void GenerateBox(int width, int height, int offsetLeft, int offsetTop)
        {
            for (int w = offsetLeft; w <= width + offsetLeft; w++)
            {
                for (int h = offsetTop; h <= height + offsetTop; h++)
                {
                    if (w == offsetLeft && h == offsetTop)
                    {
                        ConsoleHelper.WriteAt(w, h, "╔");
                    }
                    else if (w == width + offsetLeft && h == offsetTop)
                    {
                        ConsoleHelper.WriteAt(w, h, "╗");
                    }
                    else if (w == offsetLeft && h == height + offsetTop)
                    {
                        ConsoleHelper.WriteAt(w, h, "╚");
                    }
                    else if (w == width + offsetLeft && h == height + offsetTop)
                    {
                        ConsoleHelper.WriteAt(w, h, "╝");
                    }
                    else if (h == offsetTop || h == height + offsetTop)
                    {
                        ConsoleHelper.WriteAt(w, h, "═");
                    }
                    else if (w == offsetLeft || w == width + offsetLeft)
                    {
                        ConsoleHelper.WriteAt(w, h, "║");
                    }
                }
            }
        }
    }
}
