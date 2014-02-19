using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Chest
{
    class ClothChest : Chest
    {
        public ClothChest(int dLevel)
        {
            Name = "Cloth Chest";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Mage };
            TypeBonus = 1;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
