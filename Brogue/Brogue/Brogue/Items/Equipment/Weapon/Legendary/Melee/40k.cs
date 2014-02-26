﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    public class _40k : LegendaryMelee
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public _40k(int dLevel, int cLevel)
        {
            Name = "40k";
            FlavorText = "They are my Space Marines...and they shall know no fear.";
            UsedBy = new List<Class> { Class.Warrior, Class.Brawler, Class.Berserker };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
