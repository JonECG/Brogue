using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    [Serializable]
    static class EnemyCreator
    {
        private static Random gen = new Random();

        public static Enemy[] GetRandomEnemy(int amount, int powerLevel)
        {
            Enemy enemy = null;
            List<Enemy> enemies = new List<Enemy>();
            int rand = gen.Next(0, 4);
            switch(rand)
            {
                case(0):
                    for (int i = 0; i < amount; i++)
                    {
                        enemy = new MeleeEnemy();
                        enemy.BuildEnemy(powerLevel);
                        enemies.Add(enemy);
                    }
                    break;
                case (1):
                    for (int i = 0; i < amount; i++)
                    {
                        enemy = new RangedEnemy();
                        enemy.BuildEnemy(powerLevel);
                        enemies.Add(enemy);
                    }
                    break;
                case (2):
                    for (int i = 0; i < amount; i++)
                    {
                        enemy = new MageEnemy();
                        enemy.BuildEnemy(powerLevel);
                        enemies.Add(enemy);
                    }
                    break;
                case (3):
                    for (int i = 0; i < amount; i++)
                    {
                        enemy = new GuardianEnemy();
                        enemy.BuildEnemy(powerLevel);
                        enemies.Add(enemy);
                    }
                    break;
            }

            return enemies.ToArray();
        }



        public static BossEnemy GetRandomBoss(int powerLevel)
        {
            BossEnemy enemy = null;
            Random gen = new Random();
            int rand = gen.Next(0, 3);

            switch (rand)
            {
                case (0):
                    enemy = new VampBoss();
                    enemy.BuildBoss(powerLevel);
                    break;
                case (1):
                    enemy = new ChargeBoss();
                    enemy.BuildBoss(powerLevel);
                    break;
                case (2):
                    enemy = new RangerBoss();
                    enemy.BuildBoss(powerLevel);
                    break;
            }

            return enemy;
        }
    }
}
