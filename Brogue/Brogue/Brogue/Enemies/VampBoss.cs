using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;
using Brogue.Engine;

namespace Brogue.Enemies
{
    [Serializable]
    class VampBoss : BossEnemy
    {
        int turnCounter = 0;

        public override bool TakeTurn(Level level)
        {
            turnCounter++;

            if (IsAggro)
            {
                if (AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(targets[0])) != null)
                {
                    Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(targets[0]));
                    if (path.Length > 1)
                    {
                        level.Move(this, path[0]);
                    }

                    else if (turnCounter % 3 == 0)
                    {
                        foreach (GameCharacter g in targets)
                        {
                            g.TakeDamage(attacks[1], this);
                            health = (health * 4) / 5;
                        }
                    }
                    else
                    {
                        targets[0].TakeDamage(attacks[0], this);
                        Heal(attacks[0] / 10);
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
            Engine.Engine.Log("VAMP_BOSS_CREATED");
            health = 50 + 20 * i;
            maxHealth = health;
            defense = 10 + 5 * i;
            if (defense > 50)
                defense = 50;
            attacks.Add(10 + i * 2);
            attacks.Add(10 + i * 3);
            exp = 30 + 30 * i;
        }
    }
}
