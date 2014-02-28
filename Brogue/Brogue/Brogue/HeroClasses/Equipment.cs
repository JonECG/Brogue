using Brogue.Items.Equipment.Armor;
using Brogue.Items.Equipment.Armor.Helm;
using Brogue.Items.Equipment.Armor.Chest;
using Brogue.Items.Equipment.Armor.Legs;
using Brogue.Items.Equipment.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Items.Equipment.Accessory;

namespace Brogue.HeroClasses
{
    [Serializable] public class Equipment
    {
        public const int MAX_ARMOR_SLOTS = 6;
        public const int MAX_WEAPON_SLOTS = 2;
        public Helm helmet;
        public Chest chestPlate;
        public Legs grieves;
        public Necklace necklace;
        public Ring[] rings = new Ring[2];
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
            totalArmor += (helmet != null) ? helmet.ArmorValue : 0;
            totalArmor += (chestPlate != null) ? chestPlate.ArmorValue : 0;
            totalArmor += (grieves != null) ? grieves.ArmorValue : 0;
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

        public void equipWeapon(Weapon weapon, int index)
        {
            if (weapon != null)
            {
                int handsTaken = (weapon.EquipableIn.Contains(Enums.Slots.Hand_Both)) ? 2 : 1;
                if (slotsOpen >= handsTaken)
                {
                    if (equippedWeapons[index] == null)
                    {
                        if (handsTaken == 2)
                        {
                            equippedWeapons[0] = weapon;
                        }
                        else
                        {
                            equippedWeapons[index] = weapon;
                        }
                        slotsOpen -= handsTaken;
                    }
                }
            }
        }

        public Weapon removeWeapon(int index)
        {
            Weapon removedWeapon = null;
            if (equippedWeapons[index] != null)
            {
                int handsTaken = (equippedWeapons[index].EquipableIn.Contains(Enums.Slots.Hand_Both)) ? 2 : 1;
                removedWeapon = equippedWeapons[index];
                equippedWeapons[index] = null;
                slotsOpen += handsTaken;
            }
            return removedWeapon;
        }

        public Weapon getPrimaryWeapon()
        {
            return equippedWeapons[0];
        }

        public Weapon getAuxilaryWeapon()
        {
            return equippedWeapons[1];
        }
    }
}
