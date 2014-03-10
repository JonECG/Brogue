using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    [Serializable]
    public class Judgement : LegendaryMelee
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Judgement(int dLevel, int cLevel)
        {
            Name = "Judgement";
            FlavorText = "To the fields of Justice.";
            UsedBy = new List<Classes> 
            { 
                Classes.Warrior, Classes.Brawler, Classes.Berserker, Classes.Sentinel, Classes.Juggernaut, 
                Classes.Rogue, Classes.Duelist, Classes.Assassin,
                Classes.Magus, Classes.SpellBlade
            };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
