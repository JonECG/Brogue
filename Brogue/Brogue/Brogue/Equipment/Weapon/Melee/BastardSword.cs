using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Equipment.Weapon.Melee
{
    class BastardSword : MeleeWeapon
    {
        public BastardSword(int dLevel)
        {
            Name = "Bastard Sword";
            UsedBy = new List<Class> { Class.Warrior };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
