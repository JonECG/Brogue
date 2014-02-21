using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.Items.Equipment.Weapon.Legendary.Ranged
{
    public class KunaiWithChain : LegendaryRanged
    {
        public static Texture2D Texture { get; set; }

        public override Texture2D GetTexture()
        {
            return Texture;
        }

        public KunaiWithChain(int dLevel, int cLevel)
        {
            Name = "Kunai With Chain";
            FlavorText = "KUNAI WIT CHAIN!";
            UsedBy = new List<Class> { Class.Rogue };
            EquipableIn = new List<Slots> { Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
