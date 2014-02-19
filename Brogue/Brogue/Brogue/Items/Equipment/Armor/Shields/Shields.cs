using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Shields
{
    abstract class Shields : Armor
    {
        Random rand = new Random();

        public Shields()
        {
            BaseArmor = rand.Next(8, 18);
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
        }

    }
}
