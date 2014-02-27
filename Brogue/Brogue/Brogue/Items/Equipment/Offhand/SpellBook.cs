using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;
namespace Brogue.Items.Equipment.Offhand
{
    public class SpellBook : Offhand
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }
        public SpellBook(int dLevel, int cLevel)
        {
            Name = "Spellbook";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Mage, Class.Sorcerer, Class.SpellWeaver };
            Element = new List<ElementAttributes> { findElement() };
        }
    }
}
