using Brogue.Enums;
using Brogue.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    public class Excecutioner : LegendaryMelee
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Excecutioner(int dLevel, int cLevel)
        {
            Name = "Excecutioner";
            FlavorText = "Witness true strength";
            UsedBy = new List<Classes> { Classes.Warrior, Classes.Brawler, Classes.Berserker };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
            TwoHanded = true;

            if (TwoHanded)
            {
                Damage += TwoHandedBonus;
            }
        }
    }
}
