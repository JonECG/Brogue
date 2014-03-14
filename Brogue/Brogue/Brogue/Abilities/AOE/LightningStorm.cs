using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    class LightningStorm : AreaOfEffect
    {
        public LightningStorm()
        {
            name = "Lightning Storm";
            description = "The sorcerer conjurs a lightning \nstorm for ultimate destruction.";
            radius = 5;
            isActuallyFilled = false;
            castSquares = new IntVec[50];
            baseDamage = 10;
            abilityCooldown = 10;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0,0);
            }
            abilityIndex = 12;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return baseDamage+heroLevel+heroDamage*2;
        }

        public override void drawVisualEffect(GameCharacter hero, GameCharacter enemy)
        {
            Engine.Engine.AddVisualAttack(enemy, "Hero/Bolt", .5f, 1.5f, .05f);
            Audio.playSound("lightning", .5f);
        }
    }
}
