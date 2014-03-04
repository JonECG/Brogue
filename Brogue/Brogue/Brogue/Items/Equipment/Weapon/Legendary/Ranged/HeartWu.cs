using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Ranged
{
    public class HeartWu : LegendaryRanged
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public HeartWu(int dLevel, int cLevel)
        {
            Name = "Heart of Wu";
            FlavorText = "NO, NOT LUBU!";
            UsedBy = new List<Classes> { Classes.Mage, Classes.SpellBlade };
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
