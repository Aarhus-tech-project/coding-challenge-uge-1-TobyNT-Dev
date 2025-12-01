namespace DungeonCrawler
{
    class DungeonLayout
    {
        public List<Loot> loot;
        public List<Enemy> enemies;

        public DungeonLayout(List<Loot> _loot, List<Enemy> _enemies)
        {
            loot = _loot;
            enemies = _enemies;
        }
    }
}
