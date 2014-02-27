using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Mapping;

namespace Brogue.Enemies
{
    class VampBoss : BossEnemy
    {
        int turnCounter = 0;

        public override bool TakeTurn(Level level)
        {
            position = level.CharacterEntities.FindPosition(this);
            turnCounter++;

            Aggro(level);

            if(AStar.getPathBetween(level, position, targets[0].position) == null)
            {
                if (turnCounter % 9 == 0)
                {
                    foreach (GameCharacter g in targets)
                    {
                        if (g.maxHealth / g.health > 4)
                        {
                            g.TakeDamage(9001, this);
                        }
                        else
                        {
                            g.Heal(g.maxHealth / 5);
                        }
                    }
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
                IntVec[] possible = AStar.getPossiblePositionsFrom(level, position, 5);
                Random gen = new Random();
                IntVec choice = possible[gen.Next(0, possible.Length)];

                level.Move(this, choice, true);
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
            if (targets.Count > 0)
            {
                foreach (GameCharacter g in level.GetCharactersIsFriendly(true))
                {
                    if (level.CharacterEntities.FindPosition(g) != level.CharacterEntities.FindPosition(targets[0]))
                    {
                        targets.Add(g);
                    }
                }
            }
        }

        public override void BuildBoss(int i)
        {
            health = 50 + 20 * i;
            maxHealth = health;
            defense = 10 + 5 * i;
            if (defense > 50)
                defense = 50;
            attacks.Add(10 + i * 2);
            attacks.Add(10 + i * 3);
            exp = 10 + 10 * i;
        }
    }
}
