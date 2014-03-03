using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Accessory.Legendary
{
    public class TheOne : LegendaryAccessory
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public TheOne(int dLevel, int cLevel)
        {
            Name = "The One";
            FlavorText = "Does this really rule them all.";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class>
            {
                Class.Warrior, Class.Brawler, Class.Brawler, Class.Sentinel, Class.Juggernaut,
                Class.Mage, Class.Sorcerer, Class.SpellWeaver, Class.Magus, Class.SpellBlade,
                Class.Rogue, Class.Duelist, Class.Assassin, Class.Ranger, Class.Marksman
            };
            EquipableIn = new List<Slots> { Slots.Finger_One, Slots.Finger_Two };
            StatIncreased = new List<Modifiers> { Modifiers.Damage, Modifiers.Health };
            StatIncrease = findStatIncrease(BaseIncrease, dLevel, LevelReq);
        }
    }
}
