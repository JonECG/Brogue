using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Helm
{
    public class ClothHelm : Helm
    {
        public ClothHelm(int dLevel, int cLevel)
        {
            Name = "Cloth Helm";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Mage };
            TypeBonus = 1;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
