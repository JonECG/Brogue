using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Armor.Legs
{
    class MailLegs : Legs
    {
        public MailLegs(int dLevel)
        {
            Name = "Mail Legs";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 5;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
