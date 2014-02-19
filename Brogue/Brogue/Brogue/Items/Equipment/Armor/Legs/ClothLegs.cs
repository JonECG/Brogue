using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Legs
{
    class ClothLegs : Legs
    {
        public ClothLegs(int dLevel)
        {
            Name = "Cloth Legs";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Mage };
            TypeBonus = 1;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
