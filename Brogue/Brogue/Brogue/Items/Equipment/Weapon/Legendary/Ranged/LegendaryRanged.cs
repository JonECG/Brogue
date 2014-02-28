using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Ranged
{
    [Serializable] public abstract class LegendaryRanged : LegendaryWeapon
    {
        Random rand = new Random();

        public LegendaryRanged()
        {
            Range = 3;
            BaseDamage = rand.Next(10, 18);
        }
    }
}
