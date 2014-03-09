using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    [Serializable] public class PaleRider : LegendaryMelee
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public PaleRider(int dLevel, int cLevel)
        {
            Name = "PaleRider";
            FlavorText = "Wreaks of Death";
            UsedBy = new List<Classes> { Classes.Assassin };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
            TwoHanded = true;

            if (TwoHanded)
            {
                Damage += TwoHandedBonus;
            }
        }
    }
}
