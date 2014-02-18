using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Armor.Helm
{
    class MailHelm : Helm
    {
        public MailHelm(int dLevel)
        {
            Name = "Mail Helm";
            LevelReq = findLevelReq(dLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 5;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
