using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.AOE
{
    [Serializable]
    public class Poison : DOTAreaOfEffect
    {
        public Poison()
        {
            name = "Poison";
            description = "The assassin covers the area \nwith a strong poison that \nemanates for four rounds.";
            radius = 3;
            isActuallyFilled = false;
            castSquares = new IntVec[24];
            baseDamage = 4;
            dotUsed = false;
            abilityCooldown = 10;
            numTicks = 4;
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            abilityIndex = 25;
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return heroDamage/2;
        }

        public override void drawVisualEffect(IntVec origin)
        {
            Audio.playSound("Poison");
            Engine.Engine.AddVisualAttack(origin, "Hero/Poison", .5f, 1.5f, .05f);
        }
    }
}
