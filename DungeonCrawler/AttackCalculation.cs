using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    internal class AttackCalculation
    {
        Random rnd = new Random();
        int attackDamage;

        public int CalculateAttack(string attacker)
        {
            //roll attack damage
            int rndNum = rnd.Next(0, 101);

            switch (rndNum)
            {
                // 0–49  (50% chance) Attack missed
                case < 50:
                    attackDamage = 0; // 0 dmg
                    break;
                // 50–74 (25% chance) normal attack hit
                case < 75:
                    attackDamage = rnd.Next(10, 16); // 10–15 dmg
                    break;
                // 75–89 (15% chance) strong attack hit
                case < 90:
                    attackDamage = rnd.Next(15, 21); // 15–20 dmg
                    break;
                // 90–98 (9% chance) very strong attack hit
                case < 99:
                    attackDamage = rnd.Next(20, 31); // 20–30 dmg
                    break;
                // 100 (1% chance) critical attack hit
                case 100:
                    attackDamage = rnd.Next(50, 101); // 50–100 dmg
                    break;
            }
            if (attacker == "enemy")
            {
                attackDamage /= 2;
            }
            return Math.Abs(attackDamage);
        }
    }
}
