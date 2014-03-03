using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Offhand.Legendary
{
    public class Necronomicon : LegendaryOffhand
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Necronomicon(int dLevel, int cLevel)
        {
            Name = "Necronomicon";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> { Class.Mage, Class.Sorcerer, Class.SpellWeaver };
            Element = new List<ElementAttributes> { ElementAttributes.Fire, ElementAttributes.Ice, ElementAttributes.Lighting };
            FlavorText = "This didn't show me how to summon the dead.";
        }
    }
}
