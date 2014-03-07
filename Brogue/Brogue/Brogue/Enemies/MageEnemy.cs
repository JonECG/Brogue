using Brogue.Mapping;
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
            CheckElementDamage();
            if (Aggro(level) && !isFrozen)
            {
                Engine.Engine.AddVisualAttack(this, target, Engine.Engine.GetTexture("Enemies/Attacks/FireBall"));
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
                GameCharacter hero = g;

                bool canSee = false;
                Direction[] nextPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(hero), ref canSee);

                if (canSee && target == null && nextPath.Length < aggroRange)
                {
                    target = hero;
                    targetFound = true;
                }
                else if (canSee && target != null && nextPath.Length < AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(target)).Length)
                {
                    target = hero;
                    targetFound = true;
                }

                break;
            }



            return targetFound;
        }

        public override void BuildEnemy(int i)
        {
            range = 6;
            aggroRange = 3;
            defense = 2 + (2 * i);
            if (defense > 30)
                defense = 30;
            attack = 4 + (4 * i);
            health = 5 + (4 * i);
            moveSpeed = 0;
            exp = 10 + 10 * i;
            element = Enums.ElementAttributes.Lighting;
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
