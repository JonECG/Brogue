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
            Name = "Necklace";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Class> 
            {
                Class.Mage, Class.Sorcerer, Class.SpellWeaver, Class.Magus, Class.SpellBlade,
                Class.Rogue, Class.Duelist, Class.Assassin, Class.Ranger, Class.Marksman,
                Class.Warrior, Class.Brawler, Class.Berserker, Class.Sentinel, Class.Juggernaut
            };
            EquipableIn = new List<Slots> { Slots.Neck };
            StatIncreased = new List<Modifiers> { findStatIncreased() };
            StatIncrease = findStatIncrease(BaseIncrease, dLevel, LevelReq);
        }
    }
}
