using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Helm
{
    [Serializable]
    public class AbyssalHelm : LegendaryHelm
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public AbyssalHelm(int dLevel, int cLevel)
        {
            Name = "Abyssal Helm";
            FlavorText = "Legendary Helm";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Sentinel, Classes.Juggernaut };
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
