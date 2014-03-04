using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Chest
{
    public class ProwlerChest : LegendaryChest
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public ProwlerChest(int dLevel, int cLevel)
        {
            Name = "Prowler Chest";
            FlavorText = "Legendary Chest";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Rogue, Classes.Duelist, Classes.Assassin, Classes.Ranger, Classes.Marksman };
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
