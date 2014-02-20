﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    public class Staff : RangedWeapon
    {
        override Texture2D Texture { get; protected set; }

        public Staff(int dLevel, int cLevel)
        {
            Name = "Staff";
            UsedBy = new List<Class> { Class.Mage };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
