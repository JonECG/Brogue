﻿using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;

namespace Brogue.Enemies
{
    [Serializable]
    class MageEnemy : Enemy
    {
        public override bool TakeTurn(Level level)
        {
            if (Aggro(level))
            {
                Attack();
            }
            return true;
        }

        public override bool Aggro(Level level)
        {
            bool targetFound = false;

            IEnumerable<GameCharacter> chars = level.GetCharactersIsFriendly(true);

            if (target != null)
            {
                bool tIsPossible = false;
                Direction[] tPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target), ref tIsPossible);
                if (tIsPossible)
                {
                    if (tPath.Length > aggroRange)
                    {
                        target = null;
                    }
                    else
                    {
                        targetFound = true;
                    }
                }
                else
                {
                    target = null;
                }
            }

            foreach (GameCharacter g in chars)
            {
                bool isPossible = false;
                Direction[] gPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(g), ref isPossible);
                if (isPossible)
                {
                    if (target != null)
                    {
                        Direction[] tPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target));
                        if (gPath.Length < tPath.Length)
                        {
                            target = g;
                            targetFound = true;
                        }
                    }
                    else if (gPath.Length < aggroRange)
                    {
                        target = g;
                        targetFound = true;
                    }
                }
            }

            return targetFound;
        }

        public override void BuildEnemy(int i)
        {
            if (i > 10)
            {
                i = 10;
            }
            if (i < 0)
            {
                i = 0;
            }

            range = 4+i/2;
            aggroRange = 3 + i / 2;
            defense = 2 + (2 * i);
            attack = 8 + (8 * i);
            health = 5 + (5 * i);
            moveSpeed = 0;
            exp = 10 + 10 * i;
        }

        public override DynamicTexture GetTexture()
        {
            if (IsAggro)
            {
                return Engine.Engine.GetTexture("Enemies/MageEnemy_aggressive");
            }
            else
            {
                return Engine.Engine.GetTexture("Enemies/MageEnemy_passive");
            }
        }
    }
}
