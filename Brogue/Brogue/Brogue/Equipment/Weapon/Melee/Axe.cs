using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Weapon.Melee
{
    class Axe : MeleeWeapon
    {
        public Axe(int dLevel)
        {
            Name = "Axe";
            UsedBy = new List<Class> { Class.Warrior };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
