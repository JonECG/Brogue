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
            description = "Arcane something rather.";
            radius = 4;
            isActuallyFilled = false;
            castSquares = new IntVec[60];
            baseDamage = 10;
            dotUsed = false;
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
            return 0;
        }

        public override void drawVisualEffect(IntVec origin)
        {
            Engine.Engine.AddVisualAttack(origin, "Hero/Bolt", .5f, 1.5f, .05f);
        }
    }
}
