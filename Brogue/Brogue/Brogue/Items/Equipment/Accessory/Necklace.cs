using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Accessory
{
    [Serializable] public class Necklace : Accessory
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Necklace(int dLevel, int cLevel)
        {
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> 
            {
                Classes.Mage, Classes.Sorcerer, Classes.SpellWeaver, Classes.Magus, Classes.SpellBlade,
                Classes.Rogue, Classes.Duelist, Classes.Assassin, Classes.Ranger, Classes.Marksman,
                Classes.Warrior, Classes.Brawler, Classes.Berserker, Classes.Sentinel, Classes.Juggernaut
            };
            EquipableIn = new List<Slots> { Slots.Neck };
            StatIncreased = new List<Modifiers> { findStatIncreased() };
            StatIncrease = findStatIncrease(BaseIncrease, dLevel, LevelReq);
            Name = findName("Necklace", LevelReq);
        }
    }
}
