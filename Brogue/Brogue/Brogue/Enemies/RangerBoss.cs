﻿using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    [Serializable]
    class RangerBoss : BossEnemy
    {
        int turnCounter = 0;
        int range;

        public override bool TakeTurn(Level level)
        {
            turnCounter++;
            CheckElementDamage();

            if (IsAggro && !isFrozen)
            {
                if (AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(targets[0])) != null)
                {
                    Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(targets[0]));
                    if (path.Length > range)
                    {
                        level.Move(this, path[0]);
                    }
                    else if (turnCounter % 3 == 0)
                    {
                        Engine.Engine.AddVisualAttack(this, targets[0], Engine.Engine.GetTexture("Enemies/Attacks/TwoArrow"));
                        targets[0].TakeDamage(attacks[0]*2, this);
                    }
                    else
                    {
                        Engine.Engine.AddVisualAttack(this, targets[0], Engine.Engine.GetTexture("Enemies/Attacks/Arrow"));
                        targets[0].TakeDamage(attacks[0], this);
                    }
                }
            }
            return true;
        }

        public override void Aggro(Level level)
        {
            throw new NotImplementedException();
        }

        public override void BuildBoss(int i)
        {   
            range = 7;
            health = 30 + 20 * i;
            maxHealth = health;
            defense = 10 + 3 * i;
            if (defense > 30)
                defense = 30;
            attacks.Add(30 + i * 5);
            exp = 200 + 100 * i-1;
        }

        public override void TakeDamage(int damage, GameCharacter attacker)
        {
            if (!IsAggro)
            {
                Level level = Engine.Engine.currentLevel;
                targets.Add(attacker);
                IntVec[] possible = AStar.getPossiblePositionsFrom(level, level.CharacterEntities.FindPosition(this), 5, false);
                IntVec targetPos = new IntVec(-1, -1);

                foreach (IntVec i in possible)
                {
                    if (targetPos.X == -1)
                    {
                        targetPos = i;
                    }
                    else
                    {
                        if (AStar.calculateHeuristic(i, level.CharacterEntities.FindPosition(attacker)) > AStar.calculateHeuristic(targetPos, level.CharacterEntities.FindPosition(attacker)))
                        {
                            targetPos = i;
                        }
                    }
                }

                level.Move(this, targetPos, true);
            }
            else
            {
                base.TakeDamage(damage, attacker);
            }
        }

        public override DynamicTexture GetTexture()
        {
            if (IsAggro)
            {
                return Engine.Engine.GetTexture("Enemies/RangerBossAggressive");
            }
            else
            {
                return Engine.Engine.GetTexture("Enemies/RangerBossPassive");
            }
        }
    }
}
