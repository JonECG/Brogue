using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Ranged
{
    public class BloodWing : LegendaryRanged
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public BloodWing(int dLevel, int cLevel)
        {
            Name = "Bloodwing";
            FlavorText = "What's a \"Vault Hunter\".";
            UsedBy = new List<Class> { Class.Marksman };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
