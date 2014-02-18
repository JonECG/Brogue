using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Weapon.Melee
{
    class Claws : MeleeWeapon
    {
        public Claws(int dLevel)
        {
            Name = "Claws";
            UsedBy = new List<Class> { Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
