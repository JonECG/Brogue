using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Armor.Shields
{
    class WoodenShield : Shields
    {
        public WoodenShield(int dLevel)
        {
            Name = "Wooden Shield";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 2;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
