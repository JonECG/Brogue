using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable] public class WhirlwindSlash : AreaOfEffect
    {
        private int baseDamage = 3;

        public WhirlwindSlash()
        {
            radius = 2;
            isActuallyFilled = false;
            castSquares = new IntVec[12];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
        }

        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            radiusSquares = AStar.getPossiblePositionsFrom(level, start, radius, true);
            return radiusSquares;
        }

        public override void addCastingSquares(IntVec cursorPosition)
        {
            if (castSquares[0].X == 0 && castSquares[0].Y == 0)
            {
                castSquares = radiusSquares;
            }
            else
            {
                isActuallyFilled = true;
            }
        }

        public override void removeCastingSquares(IntVec cursorPosition)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
        }

        public override bool filledSquares()
        {
            return isActuallyFilled;
        }

        public override IntVec[] getCastingSquares()
        {
            return castSquares;
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Level mapLevel, HeroClasses.Hero hero)
        {
            int damage = baseDamage * (heroLevel+heroDamage/2);
            cooldown = 5;
            wasJustCast = true;
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter enemy = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (enemy != null)
                {
                    enemy.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
            isActuallyFilled = false;
        }


    }
}
