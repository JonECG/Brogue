using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Brogue.Engine;

namespace Brogue.Enemies
{
    class MeleeEnemy : Enemy
    {
        public override bool TakeTurn(Level level)
        {
            position = Engine.Engine.currentLevel.CharacterEntities.FindPosition(this);
            if (Aggro(level))
            {
                Direction[] path = AStar.getPathBetween(level, this.position, level.CharacterEntities.FindPosition(target));

                if (path != null)
                {
                    if (path.Length - range <= moveSpeed)
                    {
                        for (int i = 0; i < path.Length - range; i++)
                        {
                            Move(path[i], level);
                        }
                        Attack();
                    }
                    else
                    {
                        for (int i = 0; i < moveSpeed; i++)
                        {
                            Move(path[i], level);
                        }
                    }
                }
                else
                {
                    IntVec[] possible = AStar.getPossiblePositionsFrom(level, position, moveSpeed);
                    IntVec targetPos = null;
                    foreach (IntVec pos in possible)
                    {
                        if (targetPos == null)
                        {
                            targetPos = pos;
                        }
                        else if (AStar.getCost(AStar.getPathBetween(level, position, targetPos)) > AStar.getCost(AStar.getPathBetween(level, position, pos)))
                        {
                            targetPos = pos;
                        }
                    }
                    level.Move(this, targetPos, true);
                }
            }
            else
            {
                //unimplemented
            }
            return true;
        }

        public override bool Aggro(Level level)
        {
            bool targetFound = false;

            IEnumerable<GameCharacter> chars = level.GetCharactersIsFriendly(true);

            foreach (GameCharacter g in chars)
            {
                int gRange = AStar.getCost(AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(g)));

                if (target == null)
                {
                    if (gRange <= aggroRange)
                    {
                        target = g;
                        targetFound = true;
                    }
                }
                else
                {
                    int tRange = AStar.getCost(AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target)));

                    if (g.Equals(target) && tRange <= aggroRange)
                    {
                        targetFound = true;
                    }
                    else if (gRange < tRange)
                    {
                        target = g;
                        targetFound = true;
                    }
                }
            }

            return targetFound;
        }

        public override void BuildEnemy(int i)
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
            exp = 5 + 3 * i;
            
        }

        public override DynamicTexture GetTexture()
        {
            return Engine.Engine.GetTexture("Enemies/MeleeEnemy");
        }
    }
}
