using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    public abstract class LegendaryMelee : LegendaryWeapon
    {
        Random rand = new Random();

        public LegendaryMelee()
        {
            Range = 1;
            BaseDamage = rand.Next(15, 31);
        }
    }
}
