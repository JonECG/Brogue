using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    [Serializable]
    class GuardianEnemy : Enemy
    {
        public override bool TakeTurn(Level level)
        {
            CheckElementDamage();

            if (IsAggro && !isFrozen)
            {
                Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target));
                int pathCost = AStar.getCost(path);

                if (path != null)
                {
                    if (pathCost == 1)
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
            return true;
        }

        public override bool Aggro(Level level)
        {
            throw new NotImplementedException();
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
            exp = 10 + 15 * i;
        }

        public override DynamicTexture GetTexture()
        {
            if (IsAggro)
            {
                return Engine.Engine.GetTexture("Enemies/GuardianAggressive");
            }
            else
            {
                return Engine.Engine.GetTexture("Enemies/GuardianPassive");
            }
        }
    }
}
