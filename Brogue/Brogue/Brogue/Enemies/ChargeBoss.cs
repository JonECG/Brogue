using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    class ChargeBoss : BossEnemy
    {
        int turnCounter = 0;

        public override void TakeTurn(Level level)
        {
            turnCounter++;

            Aggro(level);

            if (AStar.getPathBetween(level, position, targets[0].position) == null)
            {
                if (turnCounter % 3 == 0)
                {
                    Direction[] path = AStar.getPathBetween(level, this.position, targets[0].position);
                    if (AStar.getCost(path) <= moveSpeed + 1)
                    {
                        foreach (Direction d in path)
                        {
                            if (!(position.X + d.X == targets[0].position.X && position.Y + d.Y == targets[0].position.Y))
                            {
                                Move(d, level);
                            }
                        }
                        targets[0].TakeDamage(attacks[1], this);
                    }
                    else
                    {
                        for (int i = 0; i < moveSpeed; i++)
                        {
                            Move(path[i], level);
                        }
                    }
                }
                else
                {
                    Direction[] path = AStar.getPathBetween(level, this.position, targets[0].position);
                    if (AStar.getCost(path) <= moveSpeed + 1)
                    {
                        foreach (Direction d in path)
                        {
                            if (!(position.X + d.X == targets[0].position.X && position.Y + d.Y == targets[0].position.Y))
                            {
                                Move(d, level);
                            }
                        }
                        targets[0].TakeDamage(attacks[0], this);
                    }
                    else
                    {
                        for (int i = 0; i < moveSpeed; i++)
                        {
                            Move(path[i], level);
                        }
                    }
                }
            }
        }

        public override void Aggro(Level level)
        {
            targets[0] = level.CharacterEntities.FindEntity(FindNearestTarget(level.GetCharactersIsFriendly(true), level));
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
            moveSpeed = 3 + i;
        }

        protected override void Die()
        {
            throw new NotImplementedException();
        }
    }
}
