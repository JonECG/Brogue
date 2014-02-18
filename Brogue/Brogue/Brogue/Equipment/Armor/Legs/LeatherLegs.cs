using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;


namespace Brogue.Equipment.Armor.Legs
{
    class LeatherLegs : Legs
    {
        public LeatherLegs(int dLevel)
        {
            Name = "Leather Legs";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Rogue };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);

        }
    }
}
