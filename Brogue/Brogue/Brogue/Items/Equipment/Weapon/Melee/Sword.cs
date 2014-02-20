using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Weapon.Melee
{
    public class Sword : MeleeWeapon
    {
        public Sword(int dLevel, int cLevel)
        {
            Name = "Sword";
            UsedBy = new List<Class> { Class.Warrior, Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
