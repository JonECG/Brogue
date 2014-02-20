using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Legs
{
    public abstract class Legs : Armor
    {
        Random rand = new Random();

        public Legs()
        {
            BaseArmor = rand.Next(9, 14);
            EquipableIn = new List<Slots> { Slots.Legs };
        }
    }
}
