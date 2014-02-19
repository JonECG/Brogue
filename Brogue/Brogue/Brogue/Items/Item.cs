using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Brogue.Items.Equipment.Weapon.Melee;
using Brogue.Items.Equipment.Weapon.Ranged;
using Brogue.Items.Equipment.Armor.Helm;
using Brogue.Items.Equipment.Armor.Chest;
using Brogue.Items.Equipment.Armor.Legs;
using Brogue.Items.Equipment.Armor.Shields;
using Brogue.Items.Equipment.Accessory;
using Brogue.Items.Consumables;
using Brogue.Enums;

namespace Brogue.Items
{
    abstract class Item
    {
        public string Name { get; protected set; }
        public string Image { get; protected set; }
        public Texture2D Texture { get; protected set; }

        public static Texture2D LoadContent(ContentManager content, string imgLocation)
        {
            return content.Load <Texture2D>(imgLocation);
        }

        public static Item randomItem(int dLevel)
        {
            int findItem;
            Random rand = new Random();
            #region randomItem generator
            findItem = rand.Next(Enum.GetNames(typeof(ITypes)).Length);
            #region random Consumable
            if ((ITypes)findItem == ITypes.Consumable)
            {
                findItem = rand.Next(Enum.GetNames(typeof(CoTypes)).Length);
                if ((CoTypes)findItem == CoTypes.Potion)
                {
                    return new Potion(dLevel);
                }
            }
            #endregion
            #region random Accessory
            else if ((ITypes)findItem == ITypes.Accessory)
            {
                findItem = rand.Next(Enum.GetNames(typeof(AcTypes)).Length);
                if ((AcTypes)findItem == AcTypes.Necklace)
                {
                    return new Necklace(dLevel);
                }
                else if ((AcTypes)findItem == AcTypes.Ring)
                {
                    return new Ring(dLevel);
                }
            }
            #endregion
            #region random Weapon
            else if ((ITypes)findItem == ITypes.Weapon)
            {
                findItem = rand.Next(Enum.GetNames(typeof(WTypes)).Length);
                if ((WTypes)findItem == WTypes.MeleeWeapon)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(MWTypes)).Length);
                    if ((MWTypes)findItem == MWTypes.Axe)
                    {
                        return new Axe(dLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.BastardSword)
                    {
                        return new BastardSword(dLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.Claws)
                    {
                        return new Claws(dLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.Dagger)
                    {
                        return new Dagger(dLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.GreatAxe)
                    {
                        return new GreatAxe(dLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.Sword)
                    {
                        return new Sword(dLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.WarHammer)
                    {
                        return new WarHammer(dLevel);
                    }
                }
                else if ((WTypes)findItem == WTypes.RangedWeapon)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(RWTypes)).Length);
                    if ((RWTypes)findItem == RWTypes.Boomerang)
                    {
                        return new Boomerang(dLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.Kunai)
                    {
                        return new Kunai(dLevel);
                    }
                    else  if ((RWTypes)findItem == RWTypes.Staff)
                    {
                        return new Staff(dLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.ThrowingKnives)
                    {
                        return new ThrowingKnives(dLevel);
                    }
                }
            }
            #endregion
            #region random Armor
            else if ((ITypes)findItem == ITypes.Armor)
            {
                findItem = rand.Next(Enum.GetNames(typeof(ArTypes)).Length);
                if ((ArTypes)findItem == ArTypes.Chest)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(ChTypes)).Length);
                    if ((ChTypes)findItem == ChTypes.ClothChest)
                    {
                        return new ClothChest(dLevel);
                    }
                    else if ((ChTypes)findItem == ChTypes.LeatherChest)
                    {
                        return new LeatherChest(dLevel);
                    }
                    else if ((ChTypes)findItem == ChTypes.MailChest)
                    {
                        return new MailChest(dLevel);
                    }
                }
                else if ((ArTypes)findItem == ArTypes.Helm)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(HTypes)).Length);
                    if ((HTypes)findItem == HTypes.ClothHelm)
                    {
                        return new ClothHelm(dLevel);
                    }
                    else if ((HTypes)findItem == HTypes.LeatherHelm)
                    {
                        return new LeatherHelm(dLevel);
                    }
                    else if ((HTypes)findItem == HTypes.MailHelm)
                    {
                        return new MailHelm(dLevel);
                    }
                }
                else if ((ArTypes)findItem == ArTypes.Legs)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(LTypes)).Length);
                    if ((LTypes)findItem == LTypes.ClothLegs)
                    {
                        return new ClothLegs(dLevel);
                    }
                    else if ((LTypes)findItem == LTypes.LeatherLegs)
                    {
                        return new LeatherLegs(dLevel);
                    }
                    else if ((LTypes)findItem == LTypes.MailLegs)
                    {
                        return new MailLegs(dLevel);
                    }
                }
                else if ((ArTypes)findItem == ArTypes.Shields)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(STypes)).Length);
                    if ((STypes)findItem == STypes.PlateShield)
                    {
                        return new PlateShield(dLevel);
                    }
                    else if ((STypes)findItem == STypes.WoodenShield)
                    {
                        return new WoodenShield(dLevel);
                    }
                }
            }

            #endregion
            #endregion
            return new Potion(dLevel);
        }
    }
}
