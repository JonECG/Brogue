using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    [Serializable] public class BladeWaltz : LegendaryMelee
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public BladeWaltz(int dLevel, int cLevel)
        {
            Name = "Blade Waltz";
            FlavorText = "I long for a worthy opponent.";
            UsedBy = new List<Class> { Class.Duelist, Class.Assassin };
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary, Slots.Hand_Primary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
