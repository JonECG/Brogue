using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Legs
{
    public abstract class LegendaryLegs : LegendaryArmor
    {
        Random rand = new Random();

        public LegendaryLegs()
        {
            BaseArmor = rand.Next(18, 27);
            EquipableIn = new List<Slots> { Slots.Legs };
        }
    }
}
