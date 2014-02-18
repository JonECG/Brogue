using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Armor.Helm
{
    class ClothHelm : Helm
    {
        public ClothHelm(int dLevel)
        {
            Name = "Cloth Helm";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Mage };
            TypeBonus = 1;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
