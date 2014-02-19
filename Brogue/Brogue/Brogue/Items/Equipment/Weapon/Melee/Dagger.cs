using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Melee
{
    class Dagger : MeleeWeapon
    {
        public Dagger(int dLevel)
        {
            Name = "Dagger";
            UsedBy = new List<Class> { Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
