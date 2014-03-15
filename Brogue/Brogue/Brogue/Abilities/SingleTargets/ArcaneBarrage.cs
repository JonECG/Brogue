using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable]
    public class ArcaneBarrage : SingleTarget
    {

        public ArcaneBarrage()
        {
            name = "Arcane Barrage";
            description = "The spellblade fires a barrage \nof arcane energy at all of the \nselected enemies.";
            castSquares = new IntVec[5];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 5;
            radius = 5;
            abilityCooldown = 12;
            abilityIndex = 19;
        }

        protected override void finishCast(int damage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    Engine.Engine.AddVisualAttack(hero, test, "Hero/Overload", 1.0f, 1.0f, 0);
                    test.DealElementalDamage(Enums.ElementAttributes.Arcane, 7, HeroClasses.Hero.level/2);
                    test.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage +  heroLevel/3)+heroDamage;
        }
    }
}
