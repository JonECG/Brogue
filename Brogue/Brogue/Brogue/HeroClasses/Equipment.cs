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
using Brogue.Enums;

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

        public int getAccessoryHealthModifier()
        {
            int increasedHealth = 0;
            if (necklace != null)
            {
                for (int i = 0; i < necklace.StatIncreased.Count; i++)
                {
                    if (necklace.StatIncreased[i] == Modifiers.Health)
                    {
                        increasedHealth += necklace.StatIncrease;
                    }
                }
            }

            for (int i = 0; i < rings.Length; i++)
            {
                if (rings[i] != null)
                {
                    for (int j = 0; j < rings[i].StatIncreased.Count; j++)
                    {
                        if (rings[i].StatIncreased[j] == Modifiers.Health)
                        {
                            increasedHealth += rings[i].StatIncrease;
                        }
                    }
                }
            }
            return increasedHealth;
        }

        public int getTotalDamageIncrease()
        {
            int totalDamage = 0;
            for (int i = 0; i < equippedWeapons.Length; i++)
            {
                if (equippedWeapons[i] != null)
                {
                    totalDamage += equippedWeapons[i].Damage;
                }
            }

            if (necklace != null)
            {
                for (int i = 0; i < necklace.StatIncreased.Count; i++)
                {
                    if (necklace.StatIncreased[i] == Modifiers.Damage)
                    {
                        totalDamage += necklace.StatIncrease;
                    }
                }
            }

            for (int i = 0; i < rings.Length; i++)
            {
                if (rings[i] != null)
                {
                    for (int j = 0; j < rings[0].StatIncreased.Count; j++)
                    {
                        if (rings[i].StatIncreased[j] == Modifiers.Damage)
                        {
                            totalDamage += rings[i].StatIncrease;
                        }
                    }
                }
            }
            return totalDamage;
        }

        public bool isWeaponEquipable(Weapon weapon, Class heroRole, int level)
        {
            
            bool result = false;
            if (weapon != null)
            {
                bool equipable = false;
                for (int i = 0; i < weapon.UsedBy.Count && !equipable; i++)
                {
                    equipable = (weapon.UsedBy[i] == heroRole);
                }
                if (equipable && weapon.LevelReq <= level)
                {
                    int handsTaken = (weapon.EquipableIn.Contains(Enums.Slots.Hand_Both)) ? 2 : 1;
                    result = (slotsOpen >= handsTaken);
                }
            }
            if (!result)
            {
                Engine.Engine.Log("You can't equip this weapon.");
            }
            return result;
        }

        public bool isArmorEquipable(Armor armor, Class heroRole, int level)
        {
            bool result = false;
            if (armor != null)
            {
                bool equipable = false;
                for (int i = 0; i < armor.UsedBy.Count && !equipable; i++)
                {
                    equipable = (armor.UsedBy[i] == heroRole);
                }
                result = (equipable && armor.LevelReq <= level);
            }
            if (!result)
            {
                Engine.Engine.Log("You can't equip this armor.");
            }
            return result;
        }

        public bool isAccessoryEquipable(Accessory accessory, Class heroRole, int level)
        {
            bool result = false;
            if (accessory != null)
            {
                bool equipable = false;
                for (int i = 0; i < accessory.UsedBy.Count && !equipable; i++)
                {
                    equipable = (accessory.UsedBy[i] == heroRole);
                }
                result = (equipable && accessory.LevelReq <= level);
            }
            if (!result)
            {
                Engine.Engine.Log("You can't equip this accessory.");
            }
            return result;
        }

        public void equipArmor(Armor armor)
        {
            for (int i = 0; i < armor.EquipableIn.Count; i++)
            {
                switch (armor.EquipableIn[i])
                {
                    case Slots.Head:
                        helmet = (Helm)armor;
                        break;
                    case Slots.Chest:
                        chestPlate = (Chest)armor;
                        break;
                    case Slots.Legs:
                        grieves = (Legs)armor;
                        break;
                }
            }
        }

        public void equipAccessory(Accessory accessory)
        {
            for (int i = 0; i < accessory.EquipableIn.Count; i++)
            {
                switch (accessory.EquipableIn[i])
                {
                    case Slots.Neck:
                        necklace = (Necklace)accessory;
                        break;
                    case Slots.Finger_One:
                        if (rings[0] != null && rings[1] == null)
                        {
                            rings[1] = (Ring)accessory;
                        }
                        else
                        {
                            rings[0] = (Ring)accessory;
                        }
                        break;
                }
            }
        }

        public void equipWeapon(Weapon weapon, int index)
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

        public Armor removeArmor(Armor type)
        {
            Armor removedArmor = null;
            for (int i = 0; i < type.EquipableIn.Count; i++)
            {
                switch (type.EquipableIn[i])
                {
                    case Slots.Head:
                        removedArmor = helmet;
                        helmet = null;
                        break;
                    case Slots.Legs:
                        removedArmor = grieves;
                        grieves = null;
                        break;
                    case Slots.Chest:
                        removedArmor = chestPlate;
                        chestPlate = null;
                        break;
                }
            }
            return removedArmor;
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

        public Accessory removeAccessory(Accessory type)
        {
            Accessory removedAccesory = null;
            bool found = false;
            for (int i = 0; i < type.EquipableIn.Count && !found; i++)
            {
                switch (type.EquipableIn[i])
                {
                    case Slots.Neck:
                        removedAccesory = necklace;
                        necklace = null;
                        found = true;
                        break;
                    case Slots.Finger_One:
                        if (rings[0] != null && rings[1] != null)
                        {
                            removedAccesory = rings[1];
                            found = true;
                            rings[1] = null;
                        }
                        break;
                }
            }
            return removedAccesory;
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
