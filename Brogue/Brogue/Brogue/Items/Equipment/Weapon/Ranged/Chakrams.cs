using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    public class Chakrams : RangedWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Chakrams(int dLevel, int cLevel)
        {
            Name = "Chakrams";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Magus, Class.SpellBlade };
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
