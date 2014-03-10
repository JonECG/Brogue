using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Items.Equipment.Weapon.Legendary
{
    [Serializable] public abstract class LegendaryWeapon : Weapon
    {
        public string FlavorText { get; protected set; }

        public LegendaryWeapon()
        {
            IsLegendary = true;
            ItemType = ITypes.Weapon;
        }
    }
}
