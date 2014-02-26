using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Legendary.Ranged
{
    public class RodOfExtending : LegendaryRanged
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public RodOfExtending(int dLevel, int cLevel)
        {
            Name = "The Rod of Extending";
            FlavorText = "Natural Mage Enhancement";
            UsedBy = new List<Class> { Class.Mage, Class.Sorcerer, Class.SpellWeaver };
            EquipableIn = new List<Slots> { Slots.Hand_Primary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
