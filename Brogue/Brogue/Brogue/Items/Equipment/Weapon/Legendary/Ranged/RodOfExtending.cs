﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.Items.Equipment.Weapon.Legendary.Ranged
{
    public class RodOfExtending : LegendaryRanged
    {
        public static Texture2D Texture { get; set; }

        public override Texture2D GetTexture()
        {
            return Texture;
        }

        public RodOfExtending(int dLevel, int cLevel)
        {
            Name = "The Rod of Extending";
            FlavorText = "Natural Mage Enhancement";
            UsedBy = new List<Class> { Class.Mage };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
