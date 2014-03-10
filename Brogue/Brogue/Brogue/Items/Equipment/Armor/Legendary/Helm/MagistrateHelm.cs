using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Helm
{
    [Serializable]
    public class MagistrateHelm : LegendaryHelm
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public MagistrateHelm(int dLevel, int cLevel)
        {
            Name = "Magistrate Helm";
            FlavorText = "Legendary Helm";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Mage, Classes.Sorcerer, Classes.SpellWeaver, Classes.Magus, Classes.SpellBlade };
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
