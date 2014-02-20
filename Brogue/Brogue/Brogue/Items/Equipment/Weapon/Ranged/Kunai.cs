using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    public class Kunai : RangedWeapon
    {
        public Kunai(int dLevel, int cLevel)
        {
            Name = "Kunai";
            UsedBy = new List<Class> { Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
