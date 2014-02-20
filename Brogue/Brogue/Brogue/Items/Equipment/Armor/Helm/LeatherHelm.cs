using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Helm
{
    public class LeatherHelm : Helm
    {
        public LeatherHelm(int dLevel, int cLevel)
        {
            Name = "Leather Helm";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Rogue };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
