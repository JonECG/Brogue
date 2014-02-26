using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Chest
{
    public class LeatherChest : Chest
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
            UsedBy = new List<Class> { Class.Rogue, Class.Duelist, Class.Assassin, Class.Ranger, Class.Marksman };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
