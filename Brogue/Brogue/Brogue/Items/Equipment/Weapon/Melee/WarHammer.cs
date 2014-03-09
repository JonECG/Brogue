using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Brogue.Enums;


namespace Brogue.Items.Equipment.Weapon.Melee
{
    [Serializable] public class WarHammer : MeleeWeapon
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public WarHammer(int dLevel, int cLevel)
        {
            UsedBy = new List<Classes> { Classes.Warrior, Classes.Brawler, Classes.Berserker };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
            Name = findName("War Hammer", LevelReq);
            TwoHanded = true;

            if (TwoHanded)
            {
                Damage += TwoHandedBonus;
            }
        }
    }
}
