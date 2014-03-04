using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    [Serializable] public class Bow : RangedWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Bow(int dLevel, int cLevel)
        {
            Name = "Bow";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Ranger, Classes.Marksman };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
