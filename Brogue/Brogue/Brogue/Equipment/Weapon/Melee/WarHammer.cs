using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Weapon.Melee
{
    class WarHammer : MeleeWeapon
    {
        public WarHammer(int dLevel)
        {
            Name = "WarHammer";
            UsedBy = new List<Class> { Class.Warrior };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
