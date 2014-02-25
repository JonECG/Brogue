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

        public override void TakeTurn(Level level)
        {
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
        }

        public override void Aggro(Level level)
        {
            targets[0] = level.CharacterEntities.FindEntity(FindNearestTarget(level.GetCharactersIsFriendly(true), level));
            foreach (GameCharacter g in level.GetCharactersIsFriendly(true))
            {
                if (g.position != targets[0].position)
                {
                    targets.Add(g);
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
        }

        protected override void Die()
        {
            throw new NotImplementedException();
        }
    }
}
