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
            CheckElementDamage();
            if (Aggro(level)  && !isFrozen)
            {
                Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target));
                int pathCost = AStar.getCost(path);

                if (path != null)
                {
                    if (pathCost <= range)
                    {
                        Engine.Engine.AddVisualAttack(this, target, Engine.Engine.GetTexture("Enemies/Attacks/Arrow"));
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
                    if (tPath.Length > deAggroRange)
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
                GameCharacter hero = g;

                bool canSee = false;
                Direction[] nextPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(hero), ref canSee);

                if (canSee && target == null && nextPath.Length < aggroRange)
                {
                    target = hero;
                    targetFound = true;
                }
                else if (canSee && target != null && nextPath.Length < AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target)).Length)
                {
                    target = hero;
                    targetFound = true;
                }

                break;
            }

            if (targetFound)
            {
                foreach (Enemy e in GetAllEnemies())
                {
                    if (AStar.calculateHeuristic(level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(e)) < 3)
                    {
                        e.ForceAggro(target);
                    }
                }
            }

            return targetFound;
        }

        public override void BuildEnemy(int i)
        {
            range = 5;
            aggroRange = 5;
            deAggroRange = 7;
            defense = 0 + (3 * i);
            if (defense > 50)
                defense = 50;
            attack = 3 + (4 * i);
            health = 10 + (3 * i);
            exp = 2 + 10 * i-1;
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
