using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Helm
{
    [Serializable]
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
            UsedBy = new List<Classes> { Classes.Rogue, Classes.Duelist, Classes.Assassin, Classes.Ranger, Classes.Marksman };
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
