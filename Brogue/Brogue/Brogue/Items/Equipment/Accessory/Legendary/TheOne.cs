using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Accessory.Legendary
{
    [Serializable]
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
            UsedBy = new List<Classes>
            {
                Classes.Warrior, Classes.Brawler, Classes.Brawler, Classes.Sentinel, Classes.Juggernaut,
                Classes.Mage, Classes.Sorcerer, Classes.SpellWeaver, Classes.Magus, Classes.SpellBlade,
                Classes.Rogue, Classes.Duelist, Classes.Assassin, Classes.Ranger, Classes.Marksman
            };
            EquipableIn = new List<Slots> { Slots.Finger_One, Slots.Finger_Two };
            StatIncreased = new List<Modifiers> { Modifiers.Damage, Modifiers.Health };
            StatIncrease = findStatIncrease(BaseIncrease, dLevel, LevelReq);
        }
    }
}
