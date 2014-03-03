﻿using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Armor.Legendary.Legs
{
    public class AbyssalLegs : LegendaryLegs
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public AbyssalLegs(int dLevel, int cLevel)
        {
            Name = "Abyssal Legs";
            FlavorText = "Legendary Legs";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Sentinel, Class.Juggernaut };
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}