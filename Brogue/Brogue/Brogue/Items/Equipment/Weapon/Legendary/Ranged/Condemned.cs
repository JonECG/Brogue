using Brogue.Engine;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary.Ranged
{
    [Serializable]
    public class Condemned : LegendaryRanged
    {
        public static DynamicTexture Texture { get; set; }

        public override DynamicTexture GetTexture()
        {
            return Texture;
        }

        public Condemned(int dLevel, int cLevel)
        {
            Name = "Condemned";
            FlavorText = "The dark should fear me.";
            UsedBy = new List<Classes> { Classes.Ranger, Classes.Marksman };
            EquipableIn = new List<Slots> { Slots.Hand_Both };
            LevelReq = findLevelReq(dLevel, cLevel);
            Damage = findDamage(BaseDamage, dLevel, LevelReq);
        }
    }
}
