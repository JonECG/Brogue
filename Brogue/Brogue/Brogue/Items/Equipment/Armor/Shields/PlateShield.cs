using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Shields
{
    public class PlateShield : Shields
    {
        public PlateShield(int dLevel, int cLevel)
        {
            Name = "Plate Shield";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 5;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
