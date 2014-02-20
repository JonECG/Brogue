using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Chest
{
    public class MailChest : Chest
    {
        public MailChest(int dLevel, int cLevel)
        {
            Name = "Mail Chest";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 5;
            ArmorValue = findArmorValue(BaseArmor, dLevel, LevelReq, TypeBonus);
        }
    }
}
