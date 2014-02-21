using Brogue.Items.Equipment.Armor;
using Brogue.Items.Equipment.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.HeroClasses
{
    public class Equipment
    {
        const int MAX_ARMOR_SLOTS = 7;
        const int MAX_WEAPON_SLOTS = 2;
        public Armor[] equippedArmor = new Armor[MAX_ARMOR_SLOTS];
        public Weapon[] equippedWeapons =new Weapon[MAX_WEAPON_SLOTS];

        public int getTotalArmorRating()
        {
            int totalArmor = 0;
            for (int i = 0; i < equippedArmor.Length; i++)
            {
                totalArmor += equippedArmor[i].ArmorValue;
            }
            return totalArmor;
        }

        public int getTotalWeaponDamage()
        {
            int totalDamage = 0;
            for (int i = 0; i < equippedWeapons.Length; i++)
            {
                totalDamage += equippedWeapons[i].Damage;
            }
            return totalDamage;
        }
    }
}
