using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Brogue.Enums;


namespace Brogue.Items.Equipment.Armor.Legs
{
    public class LeatherLegs : Legs
    {
        public static Texture2D Texture { get; set; }

        public override Texture2D GetTexture()
        {
            return Texture;
        }

        public LeatherLegs(int dLevel, int cLevel)
        {
            Name = "Leather Legs";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Rogue };
            TypeBonus = 3;
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);

        }
    }
}
