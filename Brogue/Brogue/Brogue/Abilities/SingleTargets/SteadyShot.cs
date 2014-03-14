using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable]
    public class SteadyShot : SingleTarget
    {
        public SteadyShot()
        {
            name = "Steady Shot";
            description = "The ranger lines up the perfect \nshot on the selected target.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 6;
            radius = 4;
            abilityCooldown = 5;
            abilityIndex = 26;
        }

        protected override void finishCast(int damage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    Audio.playSound("Gunshot", .3f);
                    test.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage * heroLevel) + heroDamage;
        }
    }
}
