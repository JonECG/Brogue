using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;


namespace Brogue.Items.Equipment.Armor.Legs
{
    public class LeatherLegs : Legs
    {
        public LeatherLegs(int dLevel, int cLevel)
        {
            Name = "Leather Legs";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Rogue };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);

        }
    }
}
