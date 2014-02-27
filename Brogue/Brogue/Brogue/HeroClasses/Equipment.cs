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
        public int slotsOpen = 2;

        public Equipment()
        {
            for (int i = 0; i < MAX_WEAPON_SLOTS; i++)
            {
                equippedWeapons[i] = null;
            }
        }

        public int getTotalArmorRating()
        {
            int totalArmor = 0;
            for (int i = 0; i < equippedArmor.Length; i++)
            {
                if (equippedArmor[i] != null)
                {
                    totalArmor += equippedArmor[i].ArmorValue;
                }
            }
            return totalArmor;
        }

        public int getTotalWeaponDamage()
        {
            int totalDamage = 0;
            for (int i = 0; i < equippedWeapons.Length; i++)
            {
                if (equippedWeapons[i] != null)
                {
                    totalDamage += equippedWeapons[i].Damage;
                }
            }
            return totalDamage;
        }

        public void equipWeapon(Weapon weapon)
        {
            int handsTaken = (weapon.EquipableIn.Contains(Enums.Slots.Hand_Both))? 2: 1;
            if (slotsOpen >= handsTaken)
            {
                bool found = false;
                for (int i = 0; i < equippedWeapons.Length && !found; i++)
                {
                    if (equippedWeapons[i] == null)
                    {
                        found = true;
                        equippedWeapons[i] = weapon;
                        slotsOpen -= handsTaken;
                    }
                }
            }
        }

        public void removeWeapon(int index)
        {
            int handsTaken = (equippedWeapons[index].EquipableIn.Contains(Enums.Slots.Hand_Both))? 2: 1;
            equippedWeapons[index] = null;
            slotsOpen += handsTaken;
        }
    }
}
