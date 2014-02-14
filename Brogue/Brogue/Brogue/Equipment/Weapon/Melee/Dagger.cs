using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Equipment.Weapon.Melee
{
    class Dagger : MeleeWeapon
    {
        Dagger(int dungeonLevel)
        {
            Name = "Dagger";
            UsedBy = new List<Class> { Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dungeonLevel);
            Damage = findDamage(BaseDamage, dungeonLevel, LevelReq);
        }
    }
}
