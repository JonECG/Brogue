using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    [Serializable] public class SniperRifle : RangedWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public SniperRifle(int dlevel, int clevel)
        {
            Name = "Sniper Rifle";
            LevelReq = findLevelReq(dlevel, clevel);
            UsedBy = new List<Classes> { Classes.Marksman };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            Damage = findDamage(BaseDamage, dlevel, LevelReq);
        }
    }
}
