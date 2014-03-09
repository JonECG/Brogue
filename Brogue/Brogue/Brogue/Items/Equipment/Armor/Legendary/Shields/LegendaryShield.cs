using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Legendary.Shields
{
    [Serializable] public abstract class LegendaryShield : LegendaryArmor
    {
        private static Random rand = new Random();

        public LegendaryShield()
        {
            BaseArmor = rand.Next(16, 35);
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
        }
    }
}
