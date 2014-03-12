using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable] public class DoubleSlash : SingleTarget
    {

        public DoubleSlash()
        {
            name = "Double Slash";
            description = "The warrior strikes a single target \nto deal double damage.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 0;
            radius = 1;
            abilityCooldown = 6;
            abilitySprite = new Sprite(abilityLine, new IntVec(3, 0));
        }

        protected override void finishCast(int damage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    Engine.Engine.AddVisualAttack(test, "Hero/DoubleSlash", .25f, 2.0f, .15f);
                    Audio.playSound("Slash", .25f);
                    test.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return heroDamage * 2;
        }
    }
}
