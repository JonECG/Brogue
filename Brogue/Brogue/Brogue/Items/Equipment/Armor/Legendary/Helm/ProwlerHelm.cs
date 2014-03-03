using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Helm
{
    public class ProwlerHelm : LegendaryHelm
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public ProwlerHelm(int dLevel, int cLevel)
        {
            Name = "Prowler Helm";
            FlavorText = "Legendary Helm";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Rogue, Class.Duelist, Class.Assassin, Class.Ranger, Class.Marksman };
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
