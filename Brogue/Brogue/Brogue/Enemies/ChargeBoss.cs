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

            Aggro(level);

            if (AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(targets[0])) != null)
            {
                if (turnCounter % 3 == 0)
                {
                    Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(targets[0]));
                    if (AStar.getCost(path) <= moveSpeed + 1)
                    {
                        foreach (Direction d in path)
                        {
                            if (!(level.CharacterEntities.FindPosition(this).X + d.X == level.CharacterEntities.FindPosition(targets[0]).X
                                && level.CharacterEntities.FindPosition(this).Y + d.Y == level.CharacterEntities.FindPosition(targets[0]).Y))
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
                    Direction[] path = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(targets[0]));
                    if (AStar.getCost(path) <= moveSpeed + 1)
                    {
                        foreach (Direction d in path)
                        {
                            if (!(level.CharacterEntities.FindPosition(this).X + d.X == level.CharacterEntities.FindPosition(targets[0]).X
                                && level.CharacterEntities.FindPosition(this).Y + d.Y == level.CharacterEntities.FindPosition(targets[0]).Y))
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
            exp = 30 + 30 * i;
        }
    }
}
