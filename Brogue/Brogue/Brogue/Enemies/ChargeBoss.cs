using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    [Serializable]
    class ChargeBoss : BossEnemy
    {
        int turnCounter = 0;

        public override bool TakeTurn(Level level)
        {
            turnCounter++;
            CheckElementDamage();

            if (IsAggro && !isFrozen)
            {
                Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(targets[0]));
                if (path.Length > 1)
                {
                    level.Move(this, path[0]);
                }
                else if (path.Length > 2)
                {
                    level.Move(this, path[0]);
                    level.Move(this, path[1]);
                }

                if (path.Length < 4)
                {
                    targets[0].TakeDamage(attacks[0], this);
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
            health = 50 + 40 * i;
            maxHealth = health;
            defense = 20 + 5 * i;
            if (defense > 70)
                defense = 70;
            attacks.Add(10 + i * 2);
            attacks.Add(20 + i * 3);
            moveSpeed = 1;
            exp = 200 + 100 * i-1;
        }

        public override DynamicTexture GetTexture()
        {
            if (IsAggro)
            {
                return Engine.Engine.GetTexture("Enemies/ChargeBossAggressive");
            }
            else
            {
                return Engine.Engine.GetTexture("Enemies/ChargeBossPassive");
            }
        }
    }
}
