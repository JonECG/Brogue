﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Armor.Shields
{
    [Serializable] public class PlateShield : Shields
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public PlateShield(int dLevel, int cLevel)
        {
            Name = "Plate Shield";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Sentinel, Classes.Juggernaut };
            TypeBonus = 5;
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
