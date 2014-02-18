using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Armor.Helm
{
    class LeatherHelm : Helm
    {
        public LeatherHelm(int dLevel)
        {
            Name = "Leather Helm";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Rogue };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
