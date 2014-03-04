using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Chest
{
    [Serializable] public class LeatherChest : Chest
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public LeatherChest(int dLevel, int cLevel)
        {
            Name = "Leather Chest";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Rogue, Classes.Duelist, Classes.Assassin, Classes.Ranger, Classes.Marksman };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
