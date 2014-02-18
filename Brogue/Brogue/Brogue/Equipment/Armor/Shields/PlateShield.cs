using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Armor.Shields
{
    class PlateShield : Shields
    {
        public PlateShield(int dLevel)
        {
            Name = "Plate Shield";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 5;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
