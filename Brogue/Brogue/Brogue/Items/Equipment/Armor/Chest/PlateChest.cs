﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Armor.Chest
{
    public class PlateChest : Chest
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public PlateChest(int dLevel, int cLevel)
        {
            Name = "Plate Chest";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Sentinel, Class.Juggernaut };
            TypeBonus = 7;
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}