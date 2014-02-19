using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Weapon.Melee
{
    class GreatAxe : MeleeWeapon
    {
        public GreatAxe(int dLevel)
        {
            Name = "Great Axe";
            UsedBy = new List<Class> { Class.Warrior };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
