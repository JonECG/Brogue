using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Helm
{
    [Serializable]
    public abstract class LegendaryHelm : LegendaryArmor
    {
        private static Random rand = new Random();

        public LegendaryHelm()
        {
            BaseArmor = rand.Next(10, 18);
            EquipableIn = new List<Slots> { Slots.Head };
        }
    }
}
