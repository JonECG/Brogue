using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable] public abstract class DOTAreaOfEffect : Ability
    {
        protected IntVec[] radiusSquares;
        protected bool isActuallyFilled;
        public bool dotUsed;
        protected int baseDamage, turnCount, numTicks;
        protected int abilityCooldown;

        public DOTAreaOfEffect()
        {
            type = Enums.AbilityTypes.DOTAOE;
        }

        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            radiusSquares = AStar.getPossiblePositionsFrom(level, start, radius, AStar.CharacterTargeting.PASS_THROUGH, false);
            return radiusSquares;
        }

        public override void addCastingSquares(IntVec cursorPosition)
        {
            if (turnCount == 0)
            {
                turnCount = numTicks;
            }
            if ((castSquares[0].X == 0 && castSquares[0].Y == 0))
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

        public void updateAOEPosition(Level mapLevel, IntVec start, HeroClasses.Hero hero)
        {
            if (turnCount > 0)
            {
                viewCastRange(mapLevel, start);
                addCastingSquares(radiusSquares[0]);
                finishCastandDealDamage(HeroClasses.Hero.level, hero.GetEquipment().getTotalDamageIncrease(), mapLevel, hero);
            }
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Level mapLevel, HeroClasses.Hero hero)
        {
            int damage = calculateDamage(heroLevel, heroDamage);
            cooldown = abilityCooldown;
            wasJustCast = true;
            for (int i = 0; i < castSquares.Length; i++)
            {
                drawVisualEffect(castSquares[i]);
                GameCharacter enemy = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (enemy != null)
                {
                    enemy.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
            turnCount--;
            isActuallyFilled = false;
        }

        public abstract void drawVisualEffect(IntVec castEnemy);
    }
}
