using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    [Serializable] public class Chakrams : RangedWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Chakrams(int dLevel, int cLevel)
        {
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Magus, Classes.SpellBlade };
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
            Name = findName("Chakram", LevelReq);
        }
    }
}
