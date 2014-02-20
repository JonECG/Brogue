using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    class RangedEnemy : Enemy
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

            IEnumerable<GameCharacter> chars = level.GetCharactersIsFriendly(true);

            foreach (GameCharacter g in chars)
            {
                int gRange = AStar.getCost(AStar.getPathBetween(level, this.position, g.position));

                if (gRange < aggroRange && gRange < AStar.getCost(AStar.getPathBetween(level, this.position, target.position)))
                {
                    target = level.CharacterEntities.FindEntity(g.position);
                    targetFound = true;
                }
            }

            return targetFound;
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

            range = 5 + i / 2;
            aggroRange = 7 + i;
            defense = 5 + (3 * i);
            attack = 5 + (4 * i);
            health = 10 + (7 * i);
            moveSpeed = 5 + (2 * i);
        }

        protected override void Die()
        {
            //ItemDropper once created
        }
    }
}
