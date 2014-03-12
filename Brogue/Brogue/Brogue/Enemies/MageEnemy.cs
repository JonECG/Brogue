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
                Audio.playSound("enimeFireball");
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
                    if (tPath.Length > deAggroRange)
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
                Direction[] nextPath;

                if (AStar.calculateHeuristic(level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(g)) < aggroRange)
                {
                    nextPath = AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(hero), ref canSee);
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
                }



                break;
            }

            if (targetFound)
            {
                foreach (Enemy e in GetAllEnemies())
                {
                    if (AStar.calculateHeuristic(level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(e)) < 3)
                    {
                        e.ForceAggro(target);
                    }
                }
            }

            return targetFound;
        }

        public override void BuildEnemy(int i)
        {
            range = 13;
            aggroRange = 3;
            deAggroRange = 6;
            defense = 2 + (2 * i);
            if (defense > 30)
                defense = 30;
            attack = 3 + (4 * i-3);
            health = 30 + (5 * i);
            moveSpeed = 0;
            exp = 40 + 20 * i-1;
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
