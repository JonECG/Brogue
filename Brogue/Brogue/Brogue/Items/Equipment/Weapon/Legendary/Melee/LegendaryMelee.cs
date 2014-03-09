using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    [Serializable] public abstract class LegendaryMelee : LegendaryWeapon
    {
        private static Random rand = new Random();
        public bool TwoHanded { get; protected set; }
        public int TwoHandedBonus { get; protected set; }

        public LegendaryMelee()
        {
            Range = 1;
            BaseDamage = rand.Next(15, 31);
            TwoHandedBonus = BaseDamage;
        }
    }
}
