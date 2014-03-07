using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Helm
{
    [Serializable] public class LeatherHelm : Helm
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public LeatherHelm(int dLevel, int cLevel)
        {
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Rogue, Classes.Duelist, Classes.Assassin, Classes.Ranger, Classes.Marksman };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
            Name = findName("Leather Helm", LevelReq);
        }
    }
}
