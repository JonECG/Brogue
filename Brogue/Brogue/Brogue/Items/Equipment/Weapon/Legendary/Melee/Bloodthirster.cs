using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    [Serializable] public class Bloodthirster : LegendaryMelee
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Bloodthirster(int dLevel, int cLevel)
        {
            Name = "Bloodthirster";
            FlavorText = "I NEED MY STACKS";
            UsedBy = new List<Classes> { Classes.Warrior, Classes.Brawler, Classes.Berserker };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, cLevel);
            TwoHanded = true;

            if (TwoHanded)
            {
                Damage += TwoHandedBonus;
            }
        }
    }
}
