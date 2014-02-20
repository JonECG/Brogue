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
    public abstract class Item : IRenderable
    {
        public string Name { get; protected set; }
        static public Texture2D Texture { get; protected set; }

        public static void LoadContent(ContentManager content)
        {
            Items.Consumables.Potion.Texture = content.Load<Texture2D>("Items/Sword");

            Items.Equipment.Weapon.Melee.Axe.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Weapon.Melee.Sword.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Weapon.Melee.GreatAxe.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Weapon.Melee.BastardSword.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Weapon.Melee.Dagger.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Weapon.Melee.WarHammer.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Weapon.Melee.Claws.Texture = content.Load<Texture2D>("Items/Sword");

            Items.Equipment.Weapon.Ranged.Boomerang.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Weapon.Ranged.Kunai.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Weapon.Ranged.Staff.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Weapon.Ranged.ThrowingKnives.Texture = content.Load<Texture2D>("Items/Sword");

            Items.Equipment.Armor.Chest.ClothChest.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Armor.Chest.LeatherChest.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Armor.Chest.MailChest.Texture = content.Load<Texture2D>("Items/Sword");

            Items.Equipment.Armor.Legs.ClothLegs.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Armor.Legs.LeatherLegs.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Armor.Legs.MailLegs.Texture = content.Load<Texture2D>("Items/Sword");

            Items.Equipment.Armor.Helm.ClothHelm.Texture = content.Load<Texture2D>("Itms/Sword");
            Items.Equipment.Armor.Helm.LeatherHelm.Texture = content.Load<Texture2D>("Itms/Sword");
            Items.Equipment.Armor.Helm.MailHelm.Texture = content.Load<Texture2D>("Itms/Sword");

            Items.Equipment.Armor.Shields.PlateShield.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Armor.Shields.WoodenShield.Texture = content.Load<Texture2D>("Items/Sword");

            Items.Equipment.Accessory.Necklace.Texture = content.Load<Texture2D>("Items/Sword");
            Items.Equipment.Accessory.Ring.Texture = content.Load<Texture2D>("Items/Sword");
        }

        public static Item randomItem(int dLevel, int cLevel)
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
                    return new Potion(dLevel, cLevel);
                }
            }
            #endregion
            #region random Accessory
            else if ((ITypes)findItem == ITypes.Accessory)
            {
                findItem = rand.Next(Enum.GetNames(typeof(AcTypes)).Length);
                if ((AcTypes)findItem == AcTypes.Necklace)
                {
                    return new Necklace(dLevel, cLevel);
                }
                else if ((AcTypes)findItem == AcTypes.Ring)
                {
                    return new Ring(dLevel, cLevel);
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
                        return new Axe(dLevel, cLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.BastardSword)
                    {
                        return new BastardSword(dLevel, cLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.Claws)
                    {
                        return new Claws(dLevel, cLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.Dagger)
                    {
                        return new Dagger(dLevel, cLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.GreatAxe)
                    {
                        return new GreatAxe(dLevel, cLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.Sword)
                    {
                        return new Sword(dLevel, cLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.WarHammer)
                    {
                        return new WarHammer(dLevel, cLevel);
                    }
                }
                else if ((WTypes)findItem == WTypes.RangedWeapon)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(RWTypes)).Length);
                    if ((RWTypes)findItem == RWTypes.Boomerang)
                    {
                        return new Boomerang(dLevel, cLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.Kunai)
                    {
                        return new Kunai(dLevel, cLevel);
                    }
                    else  if ((RWTypes)findItem == RWTypes.Staff)
                    {
                        return new Staff(dLevel, cLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.ThrowingKnives)
                    {
                        return new ThrowingKnives(dLevel, cLevel);
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
                        return new ClothChest(dLevel,cLevel);
                    }
                    else if ((ChTypes)findItem == ChTypes.LeatherChest)
                    {
                        return new LeatherChest(dLevel, cLevel);
                    }
                    else if ((ChTypes)findItem == ChTypes.MailChest)
                    {
                        return new MailChest(dLevel, cLevel);
                    }
                }
                else if ((ArTypes)findItem == ArTypes.Helm)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(HTypes)).Length);
                    if ((HTypes)findItem == HTypes.ClothHelm)
                    {
                        return new ClothHelm(dLevel, cLevel);
                    }
                    else if ((HTypes)findItem == HTypes.LeatherHelm)
                    {
                        return new LeatherHelm(dLevel, cLevel);
                    }
                    else if ((HTypes)findItem == HTypes.MailHelm)
                    {
                        return new MailHelm(dLevel, cLevel);
                    }
                }
                else if ((ArTypes)findItem == ArTypes.Legs)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(LTypes)).Length);
                    if ((LTypes)findItem == LTypes.ClothLegs)
                    {
                        return new ClothLegs(dLevel, cLevel);
                    }
                    else if ((LTypes)findItem == LTypes.LeatherLegs)
                    {
                        return new LeatherLegs(dLevel, cLevel);
                    }
                    else if ((LTypes)findItem == LTypes.MailLegs)
                    {
                        return new MailLegs(dLevel, cLevel);
                    }
                }
                else if ((ArTypes)findItem == ArTypes.Shields)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(STypes)).Length);
                    if ((STypes)findItem == STypes.PlateShield)
                    {
                        return new PlateShield(dLevel, cLevel);
                    }
                    else if ((STypes)findItem == STypes.WoodenShield)
                    {
                        return new WoodenShield(dLevel, cLevel);
                    }
                }
            }

            #endregion
            #endregion
            return new Potion(dLevel, cLevel);
        }

        public Sprite GetSprite()
        {
            throw new NotImplementedException();
        }
    }
}
