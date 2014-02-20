using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Helm
{
    public class MailHelm : Helm
    {
        public MailHelm(int dLevel, int cLevel)
        {
            Name = "Mail Helm";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 5;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
