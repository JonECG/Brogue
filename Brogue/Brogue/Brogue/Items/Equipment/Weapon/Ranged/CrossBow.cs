using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    [Serializable] public class CrossBow : RangedWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public CrossBow(int dlevel, int clevel)
        {
            LevelReq = findLevelReq(dlevel, clevel);
            UsedBy = new List<Classes> { Classes.Ranger, Classes.Marksman };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            Damage = findDamage(BaseDamage, dlevel, LevelReq);
            Name = findName("Crossbow", LevelReq);
            TwoHanded = true;

            if (TwoHanded)
            {
                Damage += TwoHandedBonus;
            }
        }
    }
}
