using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Legs
{
    public class ProwlerLegs : LegendaryLegs
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public ProwlerLegs(int dLevel, int cLevel)
        {
            Name = "Prowler Legs";
            FlavorText = "Legendary Legs";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Rogue, Class.Duelist, Class.Assassin, Class.Ranger, Class.Marksman };
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
