using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Equipment.Weapon.Melee
{
    abstract class MeleeWeapon : Weapon
    {
        public MeleeWeapon()
        {
            Range = 1;
        }
    }
}
