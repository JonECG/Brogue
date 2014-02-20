using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    class GuardianEnemy : Enemy
    {
        public override void TakeTurn(Level level)
        {
            if (IsAggro)
            {
                Direction[] path = AStar.getPathBetween(level, this.position, target.position);

                if (path.Length - range <= moveSpeed)
                {
                    for (int i = 0; i < path.Length - range; i++)
                    {
                        Move(path[i]);
                    }
                    Attack();
                }
                else
                {
                    for (int i = 0; i < moveSpeed; i++)
                    {
                        Move(path[i]);
                    }
                }
            }
        }

        public override void BuildEnemy(int i)
        {
            if (i > 10)
            {
                i = 10;
            }
            if (i < 0)
            {
                i = 0;
            }

            range = 1;
            defense = 15 + (5 * i);
            attack = 5 + (5 * i);
            health = 15 + (15 * i);
            moveSpeed = 5 + i / 2;
        }

        protected override void Die()
        {
            //ItemDropper once created
        }
    }
}
