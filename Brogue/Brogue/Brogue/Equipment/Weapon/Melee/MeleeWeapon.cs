using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Equipment.Weapon.Melee
{
    abstract class MeleeWeapon : Weapon
    {
        Random rand = new Random();

        public MeleeWeapon()
        {
            Range = 1;
            BaseDamage = rand.Next(5, 14);
        }
    }
}
