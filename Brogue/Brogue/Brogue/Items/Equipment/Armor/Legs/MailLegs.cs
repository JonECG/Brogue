using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Legs
{
    [Serializable] public class MailLegs : Legs
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public MailLegs(int dLevel, int cLevel)
        {
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Warrior, Classes.Brawler, Classes.Berserker, Classes.Sentinel, Classes.Juggernaut };
            TypeBonus = 5;
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
            Name = findName("Mail Legs", LevelReq);
        }
    }
}
