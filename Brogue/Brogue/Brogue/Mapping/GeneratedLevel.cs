using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Brogue.Mapping
{
    class GeneratedLevel
    {
        private Level level;

        private int seed, levels, dungeonLevel, heroLevel;

        public GeneratedLevel(int seed, int levels, int dungeonLevel = 1, int heroLevel = 1)
        {
            this.seed = seed;
            this.levels = levels;
            this.dungeonLevel = dungeonLevel;
            this.heroLevel = heroLevel;
            level = null;

            Thread thread = new Thread( run );
            thread.Start();
        }

        private void run()
        {
            Level temp = LevelGenerator.generate(seed, levels, dungeonLevel, heroLevel);
            level = temp;
        }

        public Level RetrieveLevel()
        {
            while (level == null)
            {
                Thread.Yield();
            }
            return level;
        }
    }
}
