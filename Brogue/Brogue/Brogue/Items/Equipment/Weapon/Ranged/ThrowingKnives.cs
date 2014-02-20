﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    public class ThrowingKnives : RangedWeapon
    {
        public static Texture2D Texture { get; set; }

        public override Texture2D GetTexture()
        {
            return Texture;
        }

        public ThrowingKnives(int dLevel, int cLevel)
        {
            Name = "Throwing Knives";
            UsedBy = new List<Class> { Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
