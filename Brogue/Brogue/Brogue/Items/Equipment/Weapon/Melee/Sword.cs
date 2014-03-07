using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;


namespace Brogue.Items.Equipment.Weapon.Melee
{
    [Serializable] public class Sword : MeleeWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Sword(int dLevel, int cLevel)
        {
            UsedBy = new List<Classes> 
            { 
                Classes.Warrior, Classes.Brawler, Classes.Berserker, Classes.Sentinel, Classes.Juggernaut, 
                Classes.Rogue, Classes.Duelist, Classes.Assassin,
                Classes.Magus, Classes.SpellBlade
            };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
            Name = findName("Sword", LevelReq);

        }
    }
}
