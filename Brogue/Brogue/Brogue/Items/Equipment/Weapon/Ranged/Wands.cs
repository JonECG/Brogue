using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Ranged
{
    [Serializable] public class Wands : RangedWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Wands(int dLevel, int cLevel)
        {
            Name = "Wand";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Mage, Class.Sorcerer, Class.SpellWeaver };
            EquipableIn = new List<Slots> { Slots.Hand_Primary };
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
