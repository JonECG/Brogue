using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Brogue.Engine;

namespace Brogue.Enemies
{
    [Serializable]
    class MeleeEnemy : Enemy
    {
        public override bool TakeTurn(Level level)
        {
            if (Aggro(level))
            {
                Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target));
                int pathCost = AStar.getCost(path);

                if (path != null)
                {
                    if (pathCost <= 1)
                    {
                        Attack();
                    }
                    else
                    {
                        Move(path[0], level);
                    }
                }
                else
                {
                    //IntVec[] possible = AStar.getPossiblePositionsFrom(level, level.CharacterEntities.FindPosition(this), moveSpeed);
                    //IntVec targetPos = null;
                    //foreach (IntVec pos in possible)
                    //{
                    //    if (targetPos == null)
                    //    {
                    //        targetPos = pos;
                    //    }
                    //    else if (AStar.getCost(AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), targetPos)) > AStar.getCost(AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), pos)))
                    //    {
                    //        targetPos = pos;
                    //    }
                    //}
                    //level.Move(this, targetPos, true);
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
                if (AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(g)) != null)
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

                        if (tRange > aggroRange)
                        {
                            target = null;
                        }

                        if (gRange < tRange && gRange < aggroRange)
                        {
                            target = g;
                            targetFound = true;
                        }
                    }
                }
                else if (target != null && AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target)) != null)
                {
                    int tRange = AStar.getCost(AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target)));
                    if (tRange > aggroRange)
                    {
                        target = null;
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
            aggroRange = 7;
            defense = 10 + (5 * i);
            attack = 7 + (6 * i);
            health = 15 + (10 * i);
            exp = 5 + 10 * i;
            
        }

        public override DynamicTexture GetTexture()
        {
            if (IsAggro)
            {
                return Engine.Engine.GetTexture("Enemies/MeleeEnemy_aggressive");
            }
            else
            {
                return Engine.Engine.GetTexture("Enemies/MeleeEnemy_passive");
            }
        }
    }
}
