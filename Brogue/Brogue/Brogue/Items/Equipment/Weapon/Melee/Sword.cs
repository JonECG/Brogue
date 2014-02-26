using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;


namespace Brogue.Items.Equipment.Weapon.Melee
{
    public class Sword : MeleeWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Sword(int dLevel, int cLevel)
        {
            Name = "Sword";
            UsedBy = new List<Class> 
            { 
                Class.Warrior, Class.Brawler, Class.Berserker, Class.Sentinel, Class.Juggernaut, 
                Class.Rogue, Class.Duelist, Class.Assassin,
                Class.Magus, Class.SpellBlade
            };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
