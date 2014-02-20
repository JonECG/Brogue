using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    public abstract class RangedWeapon : Weapon
    {
        Random rand = new Random();

        public RangedWeapon()
        {
            Range = 3;
            BaseDamage = rand.Next(2, 8);
        }
    }
}
