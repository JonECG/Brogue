using Brogue;
using Brogue.Engine;
using Brogue.HeroClasses;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.Abilities.Damaging.SingleTargets
{
    [Serializable]
    public class Cleave : SingleTarget
    {

        public Cleave()
        {
            name = "Cleave";
            description = "The warrior strikes two immideately \nadjacent squares.";
            castSquares = new IntVec[2];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(-25, -25);
            }
            baseDamage = 5;
            radius = 1;
            abilityCooldown = 5;
            abilitySprite = new Sprite(abilityLine, new IntVec(0, 0));
        }

        protected override void finishCast(int damage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    Engine.Engine.AddVisualAttack(test, "Hero/sword-slash", .25f, 2.0f, .15f);
                    Audio.playSound("Slash", .25f);
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
