using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.Engine;

namespace Brogue.Items.Equipment.Weapon.Legendary.Melee
{
    [Serializable] public class TheWolverine : LegendaryMelee
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public TheWolverine(int dLevel, int cLevel)
        {
            Name = "The Wolverine";
            FlavorText = "I'm the best there is at what I do.";
            UsedBy = new List<Classes> { Classes.Rogue, Classes.Assassin, Classes.Berserker };
            EquipableIn = new List<Slots> { Slots.Hand_Primary, Slots.Hand_Auxillary };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
