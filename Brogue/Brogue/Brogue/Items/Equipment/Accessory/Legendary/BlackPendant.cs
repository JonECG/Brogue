using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Accessory.Legendary
{
    public class BlackPendant : LegendaryAccessory
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public BlackPendant(int dLevel, int cLevel)
        {
            Name = "The Black Pendant";
            FlavorText = "...";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes>
            {
                Classes.Warrior, Classes.Brawler, Classes.Brawler, Classes.Sentinel, Classes.Juggernaut,
                Classes.Mage, Classes.Sorcerer, Classes.SpellWeaver, Classes.Magus, Classes.SpellBlade,
                Classes.Rogue, Classes.Duelist, Classes.Assassin, Classes.Ranger, Classes.Marksman
            };
            EquipableIn = new List<Slots> { Slots.Neck };
            StatIncreased = new List<Modifiers> { Modifiers.Damage, Modifiers.Health };
            StatIncrease = findStatIncrease(BaseIncrease, dLevel, LevelReq);
        }
    }
}
