using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
namespace Brogue.Equipment.Armor.Helm
{
    abstract class Helm : Armor
    {
        Random rand = new Random();

        public Helm()
        {
            BaseArmor = rand.Next(3, 6);
            EquipableIn = new List<Slots> { Slots.Head };
        }
    }
}
