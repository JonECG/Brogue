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
    public class Equipment
    {
        const int MAX_ARMOR_SLOTS = 7;
        const int MAX_WEAPON_SLOTS = 2;
        private Helm helmet;
        private Chest chestPlate;
        private Legs grieves;
        private Necklace necklace;
        private Ring[] rings = new Ring[2];
        private Weapon[] equippedWeapons =new Weapon[MAX_WEAPON_SLOTS];
        private int slotsOpen = 2;

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
            totalArmor += helmet.ArmorValue + chestPlate.ArmorValue + grieves.ArmorValue;
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
            int handsTaken = (weapon.EquipableIn.Contains(Enums.Slots.Hand_Both))? 2: 1;
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

        public Weapon removeWeapon(int index)
        {
            int handsTaken = (equippedWeapons[index].EquipableIn.Contains(Enums.Slots.Hand_Both))? 2: 1;
            Weapon removedWeapon = equippedWeapons[index];
            equippedWeapons[index] = null;
            slotsOpen += handsTaken;
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
