using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Shields
{
    public class WoodenShield : Shields
    {
        public WoodenShield(int dLevel, int cLevel)
        {
            Name = "Wooden Shield";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 2;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
