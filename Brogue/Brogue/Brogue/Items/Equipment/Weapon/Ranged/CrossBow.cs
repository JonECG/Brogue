using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    public class CrossBow : RangedWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public CrossBow(int dlevel, int clevel)
        {
            Name = "Cross Bow";
            LevelReq = findLevelReq(dlevel, clevel);
            UsedBy = new List<Class> { Class.Ranger, Class.Marksman };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            Damage = findDamage(BaseDamage, dlevel, LevelReq);
        }
    }
}
