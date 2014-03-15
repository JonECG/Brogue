using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable] public class WhirlwindSlash : AreaOfEffect
    {

        public WhirlwindSlash()
        {
            name = "Whirlwind Slash";
            description = "The warrior swings his blade in a \ncircle sending tornadoes \nspiraling towards his foes.";
            radius = 1;
            isActuallyFilled = false;
            castSquares = new IntVec[12];
            baseDamage = 3;
            abilityCooldown = 7;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            abilityIndex = 1;
        }
        public override IntVec[] viewCastRange(Level level, IntVec start)
        {
            radiusSquares = AStar.getPossiblePositionsInBox(level, start, radius, radius, AStar.CharacterTargeting.PASS_THROUGH, true);
            return radiusSquares;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (heroDamage/4) * baseDamage;
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Level mapLevel, HeroClasses.Hero hero)
        {
            Audio.playSound("WhirlwindSlash");
            base.finishCastandDealDamage(heroLevel, heroDamage, mapLevel, hero);
        }

        public override void drawVisualEffect(GameCharacter hero, GameCharacter enemy)
        {
            Engine.Engine.AddVisualAttack(hero, enemy, "Hero/Whirlwind", .5f, 1.0f, .03f);
        }
    }
}
