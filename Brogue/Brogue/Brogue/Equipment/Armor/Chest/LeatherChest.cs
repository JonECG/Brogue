using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Armor.Chest
{
    class LeatherChest : Chest
    {
        public LeatherChest(int dLevel)
        {
            Name = "Leather Chest";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Rogue };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
