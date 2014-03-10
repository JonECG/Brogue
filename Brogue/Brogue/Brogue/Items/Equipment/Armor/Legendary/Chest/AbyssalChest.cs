using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Chest
{
    [Serializable]
    public class AbyssalChest : LegendaryChest
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public AbyssalChest(int dLevel, int cLevel)
        {
            Name = "Abyssal Chest";
            FlavorText = "Legendary Chest";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Sentinel, Classes.Juggernaut };
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
