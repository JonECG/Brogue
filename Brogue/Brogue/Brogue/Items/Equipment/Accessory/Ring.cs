using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;

namespace Brogue.Items.Equipment.Accessory
{
    [Serializable] public class Ring : Accessory
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Ring(int dLevel, int cLevel)
        {
            Name = "Ring";
            LevelReq = findLevelReq(dLevel, cLevel);
            UsedBy = new List<Classes> 
            { 
                Classes.Mage, Classes.Sorcerer, Classes.SpellWeaver, Classes.Magus, Classes.SpellBlade,
                Classes.Rogue, Classes.Duelist, Classes.Assassin, Classes.Ranger, Classes.Marksman,
                Classes.Warrior, Classes.Brawler, Classes.Berserker, Classes.Sentinel, Classes.Juggernaut
            };
            EquipableIn = new List<Slots> { Slots.Finger_One, Slots.Finger_Two };
            StatIncreased = new List<Modifiers> { findStatIncreased() };
            StatIncrease = findStatIncrease(BaseIncrease, dLevel, LevelReq);
        }
    }
}
