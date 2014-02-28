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

        public override bool TakeTurn(Level level)
        {
            turnCounter++;

            Aggro(level);

            if (AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(targets[0])) != null)
            {
                if (turnCounter % 2 == 0)
                {
                    targets[0].TakeDamage(attacks[0], this);
                }

                IntVec[] possible = AStar.getPossiblePositionsFrom(level, level.CharacterEntities.FindPosition(this), 1);
                IntVec targetPos = null;
                foreach (IntVec i in possible)
                {
                    if (targetPos == null)
                    {
                        targetPos = i;
                    }
                    else
                    {
                        if (AStar.getCost(AStar.getPathBetween(level, targetPos, level.CharacterEntities.FindPosition(targets[0]))) < AStar.getCost(AStar.getPathBetween(level, i, level.CharacterEntities.FindPosition(targets[0]))))
                        {
                            targetPos = i;
                        }
                    }
                }
                Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), targetPos);
                foreach (Direction d in path)
                {
                    Move(d, level);
                }
            }
            return true;
        }

        public override void Aggro(Level level)
        {
            if (targets.Count == 0)
            {
                targets.Add(level.CharacterEntities.FindEntity(FindNearestTarget(level.GetCharactersIsFriendly(true), level)));
            }
            else
            {
                targets[0] = level.CharacterEntities.FindEntity(FindNearestTarget(level.GetCharactersIsFriendly(true), level));
            }
        }

        public override void BuildBoss(int i)
        {
            health = 30 + 20 * i;
            maxHealth = health;
            defense = 10 + 3 * i;
            if (defense > 30)
                defense = 30;
            attacks.Add(30 + i * 5);
            exp = 30 + 30 * i;
        }
    }
}
