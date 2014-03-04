﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;


namespace Brogue.Items.Equipment.Weapon.Melee
{
    [Serializable] public class BastardSword : MeleeWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public BastardSword(int dLevel, int cLevel)
        {
            Name = "Bastard Sword";
            UsedBy = new List<Classes> { Classes.Warrior, Classes.Brawler, Classes.Berserker };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
