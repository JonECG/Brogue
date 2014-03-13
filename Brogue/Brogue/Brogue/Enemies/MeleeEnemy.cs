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
            CheckElementDamage();
            if (health <= 0)
            {
                Die();
            }
            else
            {
                if (Aggro(level) && !isFrozen)
                {
                    Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target));
                    int pathCost = AStar.getCost(path);

                    if (path != null)
                    {
                        if (pathCost <= 1)
                        {
                            Audio.playSound("Slash");
                            Engine.Engine.AddVisualAttack(target, "Hero/HammerSmash", .25f, 2.0f, .15f);
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
                Direction[] nextPath;

                if (AStar.calculateHeuristic(level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(g)) < aggroRange)
                {
                    nextPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(hero), ref canSee);
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
            range = 1;
            aggroRange = 5;
            deAggroRange = 10;
            defense = 1 + (5 * i);
            if (defense > 60)
                defense = 60;
            attack = 2 + (4 * i);
            health = 30 + (10 * i);
            exp = 30 + 15 * i-1;
            
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
