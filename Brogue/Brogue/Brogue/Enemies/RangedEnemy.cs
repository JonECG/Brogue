using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    [Serializable]
    class RangedEnemy : Enemy
    {
        public override bool TakeTurn(Level level)
        {
            if (Aggro(level))
            {
                Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target));
                int pathCost = AStar.getCost(path);

                if (path != null)
                {
                    if (pathCost <= range)
                    {
                        Attack();
                        Engine.Engine.Log("Was in range: " + level.CharacterEntities.FindPosition(this) + " to " + level.CharacterEntities.FindPosition(target) + " " + pathCost + " " + String.Join<Direction>(", ", path));
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

            if (target != null)
            {
                bool tIsPossible = false;
                Direction[] tPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target), ref tIsPossible);
                if (tIsPossible)
                {
                    if (tPath.Length > aggroRange)
                    {
                        target = null;
                    }
                    else
                    {
                        targetFound = true;
                    }
                }
                else
                {
                    target = null;
                }
            }

            foreach (GameCharacter g in chars)
            {
                bool isPossible = false;
                Direction[] gPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(g), ref isPossible);
                if (isPossible)
                {
                    if (target != null)
                    {
                        Direction[] tPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target));
                        if (gPath.Length < tPath.Length)
                        {
                            target = g;
                            targetFound = true;
                        }
                    }
                    else if (gPath.Length < aggroRange)
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
            if (i > 10)
            {
                i = 10;
            }
            if (i < 0)
            {
                i = 0;
            }

            range = 5;
            aggroRange = 7;
            defense = 5 + (3 * i);
            attack = 5 + (4 * i);
            health = 10 + (7 * i);
            exp = 5 + 10 * i;
        }

        public override DynamicTexture GetTexture()
        {
            if (IsAggro)
            {
                return Engine.Engine.GetTexture("Enemies/RangedEnemy_aggressive");
            }
            else
            {
                return Engine.Engine.GetTexture("Enemies/RangedEnemy_passive");
            }
        }
    }
}
