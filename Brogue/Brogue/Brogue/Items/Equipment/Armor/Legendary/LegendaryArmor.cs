using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary
{
    [Serializable] public abstract class LegendaryArmor : Armor
    {
        public string FlavorText { get; protected set; }

        public LegendaryArmor()
        {
            TypeBonus = 15;
        }
    }
}
