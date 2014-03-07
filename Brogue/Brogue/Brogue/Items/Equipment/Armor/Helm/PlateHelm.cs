using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Armor.Helm
{
    [Serializable] public class PlateHelm : Helm
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }
        public PlateHelm(int dLevel, int cLevel)
        {
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Sentinel, Classes.Juggernaut };
            TypeBonus = 7;
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
            Name = findName("Plate Helm", LevelReq);
        }
    }
}
