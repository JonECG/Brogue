using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable] public class Assassinate : SingleTarget
    {
        public Assassinate()
        {
            name = "Assassinate";
            description = "The assassin strikes his opponent \nwith great precision";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 7;
            radius = 2;
            abilityCooldown = 12;
            abilityIndex = 24;
        }

        protected override void finishCast(int damage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    Audio.playSound("DaggerStab");
                    Engine.Engine.AddVisualAttack(test, "Hero/DaggerSlash", .5f, 1.5f, .05f);
                    test.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage + heroDamage/10) * heroLevel/3;
        }
    }
}
