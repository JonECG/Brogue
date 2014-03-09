using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Melee
{
    [Serializable] public abstract class MeleeWeapon : Weapon
    {
        private static Random rand = new Random();
        public bool TwoHanded { get; protected set; }
        public int TwoHandedBonus { get; protected set; }

        public MeleeWeapon()
        {
            Range = 1;
            BaseDamage = rand.Next(5, 14);
            TwoHandedBonus = BaseDamage;
        }
    }
}
