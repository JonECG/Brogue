using Brogue.Engine;
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

            if (IsAggro)
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
                        targets[0].TakeDamage(attacks[0]*2, this);
                    }
                    else
                    {
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
            exp = 30 + 30 * i;
        }
    }
}
