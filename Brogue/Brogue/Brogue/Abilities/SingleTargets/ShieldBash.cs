using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable] public class ShieldBash : SingleTarget
    {
        public ShieldBash()
        {
            name = "Shield Bash";
            description = "The sentinel strikes enemies with \nhis shield.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 5;
            radius = 1;
            abilityCooldown = 3;
            abilityIndex = 6;
        }

        protected override void finishCast(int damage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    Audio.playSound("HammerSmash");
                    Engine.Engine.AddVisualAttack(test, "Hero/ShieldBash", .25f, 1.0f, .03f);
                    test.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage * (heroLevel/2)) + (heroDamage);
        }
    }
}
