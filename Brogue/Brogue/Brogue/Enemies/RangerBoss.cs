using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    class RangerBoss : BossEnemy
    {
        int turnCounter = 0;

        public override void TakeTurn(Level level)
        {
            turnCounter++;

            Aggro(level);

            if (AStar.getPathBetween(level, position, targets[0].position) == null)
            {
                if (turnCounter % 2 == 0)
                {
                    targets[0].TakeDamage(attacks[0], this);
                }

                IntVec[] possible = AStar.getPossiblePositionsFrom(level, position, 5);
                IntVec targetPos = null;
                foreach (IntVec i in possible)
                {
                    if (targetPos == null)
                    {
                        targetPos = i;
                    }
                    else
                    {
                        if (AStar.getCost(AStar.getPathBetween(level, targetPos, targets[0].position)) < AStar.getCost(AStar.getPathBetween(level, i, targets[0].position)))
                        {
                            targetPos = i;
                        }
                    }
                }
                Direction[] path = AStar.getPathBetween(level, position, targetPos);
                foreach (Direction d in path)
                {
                    Move(d, level);
                }
            }
        }

        public override void Aggro(Level level)
        {
            targets[0] = level.CharacterEntities.FindEntity(FindNearestTarget(level.GetCharactersIsFriendly(true), level));
        }

        public override void BuildBoss(int i)
        {
            health = 30 + 20 * i;
            maxHealth = health;
            defense = 10 + 3 * i;
            if (defense > 30)
                defense = 30;
            attacks.Add(30 + i * 5);
        }

        protected override void Die()
        {
            throw new NotImplementedException();
        }
    }
}
