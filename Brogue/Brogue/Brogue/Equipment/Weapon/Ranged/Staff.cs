using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Weapon.Ranged
{
    class Staff : RangedWeapon
    {
        public Staff(int dLevel)
        {
            Name = "Staff";
            UsedBy = new List<Class> { Class.Mage };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
