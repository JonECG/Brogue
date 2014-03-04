using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Melee
{
    [Serializable] public class Scythe : MeleeWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Scythe(int dLevel, int cLevel)
        {
            Name = "Scythe";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> { Classes.Assassin };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
