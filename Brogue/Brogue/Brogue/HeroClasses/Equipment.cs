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
using Brogue.Items.Equipment;
using Brogue.Items.Equipment.Offhand;

namespace Brogue.HeroClasses
{
    [Serializable]
    public class Equipment
    {
        public const int MAX_ARMOR_SLOTS = 6;
        public const int MAX_WEAPON_SLOTS = 2;
        public Armor helmet;
        public Armor chestPlate;
        public Armor grieves;
        public Accessory necklace;
        public Accessory[] rings = new Accessory[2];
        public Gear[] equippedWeapons = new Gear[MAX_WEAPON_SLOTS];
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
                if (equippedWeapons[i] != null && equippedWeapons[i].ItemType == ITypes.Weapon)
                {
                    Weapon weapon = (Weapon)equippedWeapons[i];
                    totalDamage += weapon.Damage;
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

        public bool isWeaponEquipable(Gear weapon, Classes heroRole, int level)
        {
            bool result = false;
            string logInfo = "You can't equip this weapon";
            if (weapon != null)
            {
                logInfo = "Your class cannot equip this item.";
                if (weapon.ItemType == ITypes.Weapon || weapon.ItemType == ITypes.Offhand)
                {
                    bool equipable = false;
                    for (int i = 0; i < weapon.UsedBy.Count && !equipable; i++)
                    {
                        equipable = (weapon.UsedBy[i] == heroRole);
                    }
                    result = (equipable && weapon.LevelReq <= level);
                    if (!result)
                    {
                        logInfo = "You aren't a high enough level to equip this weapon.";
                    }
                }
            }
            if (!result)
            {
                Engine.Engine.Log(logInfo);
            }
            return result;
        }

        public bool isArmorEquipable(Armor armor, Classes heroRole, int level)
        {
            bool result = false;
            string logInfo = "You can't equip this armor";
            if (armor != null)
            {
                logInfo = "Your class cannot equip this item.";
                bool equipable = false;
                for (int i = 0; i < armor.UsedBy.Count && !equipable; i++)
                {
                    equipable = (armor.UsedBy[i] == heroRole);
                }
                result = (equipable && armor.LevelReq <= level);
                if (!result && equipable)
                {
                    logInfo = "You aren't a high enough level to equip this piece of armor.";
                }
            }
            if (!result)
            {
                Engine.Engine.Log(logInfo);
            }
            return result;
        }

        public bool isAccessoryEquipable(Accessory accessory, Classes heroRole, int level)
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
                        helmet = (Armor)armor;
                        break;
                    case Slots.Chest:
                        chestPlate = (Armor)armor;
                        break;
                    case Slots.Legs:
                        grieves = (Armor)armor;
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
                        necklace = accessory;
                        break;
                    case Slots.Finger_One:
                        if (rings[0] != null && rings[1] == null)
                        {
                            rings[1] = accessory;
                        }
                        else
                        {
                            rings[0] = accessory;
                        }
                        break;
                }
            }
        }

        public void equipWeapon(Gear weapon, Hero hero)
        {
            int handsTaken = (weapon.EquipableIn.Contains(Enums.Slots.Hand_Both)) ? 2 : 1;
            bool equipped = false;
            for (int i = 0; i < weapon.EquipableIn.Count && !equipped; i++)
            {
                switch (weapon.EquipableIn[i])
                {
                    case Slots.Hand_Both:
                        if (equippedWeapons[0] == null && equippedWeapons[1] == null && slotsOpen == 2)
                        {
                            equippedWeapons[0] = weapon;
                            equipped = true;
                        }
                        break;
                    case Slots.Hand_Primary:
                        if (equippedWeapons[0] == null && slotsOpen >= 1)
                        {
                            equippedWeapons[0] = weapon;
                            equipped = true;
                        }
                        break;
                    case Slots.Hand_Auxillary:
                        if (equippedWeapons[1] == null && slotsOpen >= 1)
                        {
                            equippedWeapons[1] = weapon;
                            if (weapon.ItemType == ITypes.Offhand)
                            {
                                Offhand spellbook = (Offhand)weapon;
                                hero.Element = spellbook.Element;
                            }
                            equipped = true;
                        }
                        break;
                }
            }
            slotsOpen -= (equipped) ? handsTaken : 0;
            //if (handsTaken == 2 && equippedWeapons[0] == null && equippedWeapons[1] == null)
            //{
            //    equippedWeapons[0] = (Weapon)weapon;
            //    slotsOpen -= handsTaken;
            //}
            //else if (handsTaken == 1 && weapon.EquipableIn[0] == Slots.Hand_Auxillary && equippedWeapons[1] == null)
            //{
            //    if (weapon.ItemType == ITypes.Offhand)
            //    {
            //        equippedWeapons[1] = (Offhand)weapon;
            //    }
            //    else
            //    {
            //        equippedWeapons[1] = (Weapon)weapon;
            //    }
            //    slotsOpen -= handsTaken;
            //}
            //else if (handsTaken == 1 && equippedWeapons[0] == null)
            //{
            //    equippedWeapons[0] = (Weapon)weapon;
            //    slotsOpen -= handsTaken;
            //}
            //else if (handsTaken == 1 && equippedWeapons[1] == null)
            //{
            //    equippedWeapons[1] = (Weapon)weapon;
            //    slotsOpen -= handsTaken;
            //}

            //if (slotsOpen >= handsTaken)
            //{
            //    if (equippedWeapons[removedWeapon] == null)
            //    {
            //        if (weapon.ItemType == ITypes.Offhand)
            //        {
            //            equippedWeapons[1] = weapon;
            //            SpellBook elements = (SpellBook)weapon;
            //            hero.Element = elements.Element;
            //        }
            //        else if (weapon.EquipableIn[0] == Slots.Hand_Auxillary)
            //        {
            //            equippedWeapons[1] = weapon;
            //        }
            //        else if (handsTaken == 2)
            //        {
            //            equippedWeapons[0] = weapon;
            //        }
            //        else
            //        {
            //            equippedWeapons[removedWeapon] = weapon;
            //        }
            //        slotsOpen -= handsTaken;
            //    }
            //}
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

        public Gear removeWeapon(Gear newlyEquippedWeapon, int removedWeaponIndex = -1)
        {
            Gear removed = null;
            if (removedWeaponIndex != -1)
            {
                removed = equippedWeapons[removedWeaponIndex];
                equippedWeapons[removedWeaponIndex] = null;
                if (removed != null)
                {
                    slotsOpen += (removed.EquipableIn.Contains(Slots.Hand_Both)) ? 2 : 1;
                }
            }
            else
            {
                if (equippedWeapons[0] != null && equippedWeapons[0].EquipableIn.Contains(Slots.Hand_Both))
                {
                    removed = equippedWeapons[0];
                    equippedWeapons[0] = null;
                    slotsOpen += (removed != null) ? 2 : 0;
                }
                else if (newlyEquippedWeapon.EquipableIn.Contains(Slots.Hand_Primary) && newlyEquippedWeapon.EquipableIn.Contains(Slots.Hand_Auxillary))
                {
                    removed = equippedWeapons[1];
                    equippedWeapons[1] = null;
                    slotsOpen += (removed != null)?1:0;
                }
                else if (newlyEquippedWeapon.EquipableIn.Contains(Slots.Hand_Primary))
                {
                    removed = equippedWeapons[0];
                    equippedWeapons[0] = null;
                    slotsOpen += (removed != null) ? 1 : 0;
                }
                else if (newlyEquippedWeapon.EquipableIn.Contains(Slots.Hand_Auxillary))
                {
                    removed = equippedWeapons[1];
                    if (removed != null && removed.ItemType == ITypes.Offhand)
                    {
                        //remove element here
                    }
                    equippedWeapons[1] = null;
                    slotsOpen += (removed != null) ? 1 : 0;
                }
                
            }
            return removed;
        }

        public Accessory removeAccessory(Accessory type)
        {
            Accessory removedAccessory = null;
            bool found = false;
            if (type != null)
            {
                for (int i = 0; i < type.EquipableIn.Count && !found; i++)
                {
                    switch (type.EquipableIn[i])
                    {
                        case Slots.Neck:
                            removedAccessory = necklace;
                            necklace = null;
                            found = true;
                            break;
                        case Slots.Finger_One:
                            if (rings[0] != null && rings[0].Equals(type))
                            {
                                removedAccessory = rings[0];
                                found = true;
                                rings[0] = null;
                            }
                            if (rings[1] != null && rings[1].Equals(type))
                            {
                                removedAccessory = rings[1];
                                found = true;
                                rings[1] = null;
                            }
                            break;
                    }
                }
            }
            return removedAccessory;
        }

        public int getPrimaryWeaponRange()
        {
            Weapon primary = (equippedWeapons[0] != null) ? (Weapon)equippedWeapons[0] : null;
            int range = (primary != null) ? primary.Range : 0;
            return range;
        }

        public int getPrimaryWeaponDamage()
        {
            Weapon primary = (equippedWeapons[0] != null) ? (Weapon)equippedWeapons[0] : null;
            int damage = (primary != null) ? primary.Damage : 0;
            return damage;
        }

        public int getAuxilaryWeaponRange()
        {
            Weapon auxilary = (equippedWeapons[1] != null && equippedWeapons[1].ItemType == ITypes.Weapon) ? (Weapon)equippedWeapons[1] : null;
            int range = (auxilary != null) ? auxilary.Range : 0;
            return range;
        }

        public int getAuxilaryWeaponDamage()
        {
            Weapon auxilary = (equippedWeapons[1] != null && equippedWeapons[1].ItemType == ITypes.Weapon) ? (Weapon)equippedWeapons[1] : null;
            int damage = (auxilary != null) ? auxilary.Damage : 0;
            return damage;
        }
    }
}
