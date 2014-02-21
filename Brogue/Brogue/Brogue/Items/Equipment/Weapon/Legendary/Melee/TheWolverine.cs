﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    public class TheWolverine : LegendaryMelee
    {
        public static Texture2D Texture { get; set; }

        public override Texture2D GetTexture()
        {
            return Texture;
        }

        public TheWolverine(int dLevel, int cLevel)
        {
            Name = "The Wolverine";
            FlavorText = "I'm the best there is at what I do. But what I do best isn't very nice.";
            UsedBy = new List<Class> { Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
