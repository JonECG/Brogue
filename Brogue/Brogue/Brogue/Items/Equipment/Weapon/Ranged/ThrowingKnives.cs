using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    class ThrowingKnives : RangedWeapon
    {
        public ThrowingKnives(int dLevel)
        {
            Name = "Throwing Knives";
            UsedBy = new List<Class> { Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
