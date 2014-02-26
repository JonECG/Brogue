using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Armor.Legs
{
    public class PlateLegs : Legs
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public PlateLegs(int dlevel, int clevel)
        {
            Name = "Plate Legs";
            LevelReq = findLevelReq(dlevel, clevel);
            UsedBy = new List<Class> { Class.Sentinel, Class.Juggernaut };
            TypeBonus = 7;
            ArmorValue = findArmorValue(BaseArmor, dlevel, TypeBonus);
        }
    }
}
