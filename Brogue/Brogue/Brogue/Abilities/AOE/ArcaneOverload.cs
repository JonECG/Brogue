using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable] public class ArcaneOverload : DOTAreaOfEffect
    {
         public ArcaneOverload()
        {
            name = "Arcane Overload";
            description = "The spellblade overloads with \narcane energy, sending out \npulses of arcane damage.";
            radius = 3;
            isActuallyFilled = false;
            castSquares = new IntVec[60];
            baseDamage = 10;
            dotUsed = false;
            wasJustCast = false;
            abilityCooldown = 10;
            numTicks = 4;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0,0);
            }
            abilityIndex = 12;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return heroDamage/2;
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Level mapLevel, HeroClasses.Hero hero)
        {
            int damage = calculateDamage(heroLevel, heroDamage);
            cooldown = abilityCooldown;
            wasJustCast = true;
            willBeCast = false;
            for (int i = 0; i < castSquares.Length; i++)
            {
                drawVisualEffect(castSquares[i]);
                GameCharacter enemy = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (enemy != null)
                {
                    enemy.DealElementalDamage(Enums.ElementAttributes.Arcane, 7, HeroClasses.Hero.level / 2);
                    enemy.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
            turnCount--;
            isActuallyFilled = false;
        }

        public override void drawVisualEffect(IntVec origin)
        {
            Engine.Engine.AddVisualAttack(origin, "Hero/Overload", .5f, 1.5f, .05f);
        }
    }
}
