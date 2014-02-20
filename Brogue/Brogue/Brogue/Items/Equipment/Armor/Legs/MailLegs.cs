using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Legs
{
    public class MailLegs : Legs
    {
        public MailLegs(int dLevel, int cLevel)
        {
            Name = "Mail Legs";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 5;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
