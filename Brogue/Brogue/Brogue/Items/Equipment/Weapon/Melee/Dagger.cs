﻿using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Melee
{
    public class Dagger : MeleeWeapon
    {
        public Dagger(int dLevel, int cLevel)
        {
            Name = "Dagger";
            UsedBy = new List<Class> { Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
