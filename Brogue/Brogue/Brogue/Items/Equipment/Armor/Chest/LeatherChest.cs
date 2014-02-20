using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Chest
{
    public class LeatherChest : Chest
    {
        public LeatherChest(int dLevel, int cLevel)
        {
            Name = "Leather Chest";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Rogue };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
