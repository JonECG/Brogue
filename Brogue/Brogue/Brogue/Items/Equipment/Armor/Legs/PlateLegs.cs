using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Armor.Legs
{
    [Serializable] public class PlateLegs : Legs
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public PlateLegs(int dlevel, int clevel)
        {
            LevelReq = findLevelReq(dlevel, clevel);
            UsedBy = new List<Classes> { Classes.Sentinel, Classes.Juggernaut };
            TypeBonus = 7;
            ArmorValue = findArmorValue(BaseArmor, dlevel, TypeBonus);
            Name = findName("Plate Legs", LevelReq);
        }
    }
}
