using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace Brogue.Items.Equipment.Armor.Legendary.Shields
{
    public class FirstAvenger : LegendaryShield
    {
        public static Texture2D Texture { get; set; }

        public override Texture2D GetTexture()
        {
            return Texture;
        }

        public FirstAvenger(int dLevel, int cLevel)
        {
            Name = "The First Avenger";
            FlavorText = "I had a date.";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Warrior };
            TypeBonus = 15;
            ArmorValue = findArmorValue(BaseArmor, dLevel, TypeBonus);
        }
    }
}
