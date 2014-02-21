using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    public class _40k : LegendaryMelee
    {
        public static Texture2D Texture { get; set; }

        public override Texture2D GetTexture()
        {
            return Texture;
        }

        public _40k(int dLevel, int cLevel)
        {
            Name = "40k";
            FlavorText = "They are my Space Marines...and they shall know no fear.";
            UsedBy = new List<Class> { Class.Warrior };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
