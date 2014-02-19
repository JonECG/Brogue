using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon
{
    abstract class Weapon : Gear
    {
        public int BaseDamage { get; protected set; }
        public int Damage { get; protected set; }
        public int Range { get; protected set; }

        public static int findDamage(int bDmg, int dLevel, int lReq)
        {
            return bDmg * dLevel + lReq;
        }
    }
}
