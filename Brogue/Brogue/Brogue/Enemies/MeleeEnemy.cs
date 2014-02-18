using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;

namespace Brogue.Enemies
{
    class MeleeEnemy : Enemy
    {
        public override void TakeTurn(Level level)
        {
            if (Aggro(level))
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
            else
            {
                //unimplemented
            }
        }

        public override bool Aggro(Level level)
        {
            bool targetFound = false;

            if (target != null)
            {
                if (AStar.getCost(AStar.getPathBetween(level, this.position, level.getPlayer().position)) < aggroRange+3)
                {
                    target = level.getPlayer();
                }
            }
            else
            {
                if (AStar.getCost(AStar.getPathBetween(level, this.position, level.getPlayer().position)) < aggroRange)
                {
                    target = level.getPlayer();
                }
            }

            return targetFound;
        }

        public override void buildEnemy(int i)
        {
            if(i > 10)
            {
                i = 10;
            }
            if(i < 0)
            {
                i = 0;
            }

            range = 1;
            aggroRange = 5 + i;
            defense = 10 + (5 * i);
            attack = 7 + (6 * i);
            health = 15 + (10 * i);
            moveSpeed = 3 + (2 * i);
        }

        protected override void die()
        {
            //ItemDropper once created
        }
    }
}
