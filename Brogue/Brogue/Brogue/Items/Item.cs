using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Engine;
using Microsoft.Xna.Framework.Content;
using Brogue.Enums;
using Brogue.Items.Consumables;
using Brogue.Items.Equipment.Weapon.Melee;
using Brogue.Items.Equipment.Weapon.Ranged;
using Brogue.Items.Equipment.Armor.Helm;
using Brogue.Items.Equipment.Armor.Chest;
using Brogue.Items.Equipment.Armor.Legs;
using Brogue.Items.Equipment.Armor.Shields;
using Brogue.Items.Equipment.Accessory;
using Brogue.Items.Equipment.Weapon.Legendary.Melee;
using Brogue.Items.Equipment.Weapon.Legendary.Ranged;
using Brogue.Items.Equipment.Armor.Legendary.Shields;
using Brogue.Items.Equipment.Offhand;


namespace Brogue.Items
{
    public abstract class Item : IRenderable
    {
        public string Name { get; protected set; }
        public ITypes ItemType { get; protected set; }

        private static Random rand = new Random();

        public abstract DynamicTexture GetTexture();

        static Item()
        {
            Items.Consumables.Potion.Texture = Engine.Engine.GetTexture("Items/Potion");

            Items.Equipment.Weapon.Melee.Axe.Texture = Engine.Engine.GetTexture("Items/Axe");
            Items.Equipment.Weapon.Melee.Sword.Texture = Engine.Engine.GetTexture("Items/Sword");
            Items.Equipment.Weapon.Melee.GreatAxe.Texture = Engine.Engine.GetTexture("Items/GreatAxe");
            Items.Equipment.Weapon.Melee.BastardSword.Texture = Engine.Engine.GetTexture("Items/BastardSword");
            Items.Equipment.Weapon.Melee.Dagger.Texture = Engine.Engine.GetTexture("Items/Dagger");
            Items.Equipment.Weapon.Melee.WarHammer.Texture = Engine.Engine.GetTexture("Items/Warhammer");
            Items.Equipment.Weapon.Melee.Claws.Texture = Engine.Engine.GetTexture("Items/Claws");
            Items.Equipment.Weapon.Melee.Rapier.Texture = Engine.Engine.GetTexture("Items/Rapier");
            Items.Equipment.Weapon.Melee.Scythe.Texture = Engine.Engine.GetTexture("Items/Scythe");
            Items.Equipment.Weapon.Legendary.Melee.TheWolverine.Texture = Engine.Engine.GetTexture("Items/Claws");
            Items.Equipment.Weapon.Legendary.Melee._40k.Texture = Engine.Engine.GetTexture("Items/Warhammer");

            Items.Equipment.Weapon.Ranged.Kunai.Texture = Engine.Engine.GetTexture("Items/Kunai");
            Items.Equipment.Weapon.Ranged.Staff.Texture = Engine.Engine.GetTexture("Items/Staff");
            Items.Equipment.Weapon.Ranged.Bow.Texture = Engine.Engine.GetTexture("Items/Bow");
            Items.Equipment.Weapon.Ranged.CrossBow.Texture = Engine.Engine.GetTexture("Items/Crossbow");
            Items.Equipment.Weapon.Ranged.SniperRifle.Texture = Engine.Engine.GetTexture("Items/SniperRifle");
            Items.Equipment.Weapon.Ranged.Pistol.Texture = Engine.Engine.GetTexture("Items/Pistol");
            Items.Equipment.Weapon.Ranged.Wands.Texture = Engine.Engine.GetTexture("Items/Wand");
            Items.Equipment.Weapon.Ranged.Chakrams.Texture = Engine.Engine.GetTexture("Items/Chakram");
            Items.Equipment.Weapon.Legendary.Ranged.RodOfExtending.Texture = Engine.Engine.GetTexture("Items/Staff");
            Items.Equipment.Weapon.Legendary.Ranged.KunaiWithChain.Texture = Engine.Engine.GetTexture("Items/Kunai");

            Items.Equipment.Armor.Chest.ClothChest.Texture = Engine.Engine.GetTexture("Items/ClothChest");
            Items.Equipment.Armor.Chest.LeatherChest.Texture = Engine.Engine.GetTexture("Items/LeatherChest");
            Items.Equipment.Armor.Chest.MailChest.Texture = Engine.Engine.GetTexture("Items/MailChest");
            Items.Equipment.Armor.Chest.PlateChest.Texture = Engine.Engine.GetTexture("Items/PlateChest");

            Items.Equipment.Armor.Legs.ClothLegs.Texture = Engine.Engine.GetTexture("Items/ClothLegs");
            Items.Equipment.Armor.Legs.LeatherLegs.Texture = Engine.Engine.GetTexture("Items/LeatherLegs");
            Items.Equipment.Armor.Legs.MailLegs.Texture = Engine.Engine.GetTexture("Items/MailLegs");
            Items.Equipment.Armor.Legs.PlateLegs.Texture = Engine.Engine.GetTexture("Items/PlateLegs");

            Items.Equipment.Armor.Helm.ClothHelm.Texture = Engine.Engine.GetTexture("Items/ClothHelm");
            Items.Equipment.Armor.Helm.LeatherHelm.Texture = Engine.Engine.GetTexture("Items/LeatherHelm");
            Items.Equipment.Armor.Helm.MailHelm.Texture = Engine.Engine.GetTexture("Items/MailHelm");
            Items.Equipment.Armor.Helm.PlateHelm.Texture = Engine.Engine.GetTexture("Items/PlateHelm");

            Items.Equipment.Armor.Shields.PlateShield.Texture = Engine.Engine.GetTexture("Items/PlateShield");
            Items.Equipment.Armor.Shields.WoodenShield.Texture = Engine.Engine.GetTexture("Items/WoodenShield");
            Items.Equipment.Armor.Legendary.Shields.FirstAvenger.Texture = Engine.Engine.GetTexture("Items/PlateShield");

            Items.Equipment.Accessory.Necklace.Texture = Engine.Engine.GetTexture("Items/Necklace");
            Items.Equipment.Accessory.Ring.Texture = Engine.Engine.GetTexture("Items/Ring");

            Items.Equipment.Offhand.SpellBook.Texture = Engine.Engine.GetTexture("Items/Spellbook");
        }

        public static Item randomItem(int dLevel, int cLevel)
        {
            int findItem;
            int lChance = 101;
            int lBound = 37;
            int hBound = 49;

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
                    else if ((MWTypes)findItem == MWTypes.Rapier)
                    {
                        return new Rapier(dLevel, cLevel);
                    }
                    else if ((MWTypes)findItem == MWTypes.Scythe)
                    {
                        return new Scythe(dLevel, cLevel);
                    }
                }
                else if ((WTypes)findItem == WTypes.RangedWeapon)
                {
                    findItem = rand.Next(Enum.GetNames(typeof(RWTypes)).Length);
                    if ((RWTypes)findItem == RWTypes.Kunai)
                    {
                        return new Kunai(dLevel, cLevel);
                    }
                    else  if ((RWTypes)findItem == RWTypes.Staff)
                    {
                        return new Staff(dLevel, cLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.Bow)
                    {
                        return new Bow(dLevel, cLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.CrossBow)
                    {
                        return new CrossBow(dLevel, cLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.Pistol)
                    {
                        return new Pistol(dLevel, cLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.SniperRifle)
                    {
                        return new SniperRifle(dLevel, cLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.Wands)
                    {
                        return new Wands(dLevel, cLevel);
                    }
                    else if ((RWTypes)findItem == RWTypes.Chakram)
                    {
                        return new Chakrams(dLevel, cLevel);
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
                    else if ((ChTypes)findItem == ChTypes.PlateChest)
                    {
                        return new PlateChest(dLevel, cLevel);
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
                    else if ((HTypes)findItem == HTypes.PlateHelm)
                    {
                        return new PlateHelm(dLevel, cLevel);
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
                    else if ((LTypes)findItem == LTypes.PlateLegs)
                    {
                        return new PlateLegs(dLevel, cLevel);
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
            #region random Offhand
            else if ((ITypes)findItem == ITypes.Offhand)
            {
                findItem = rand.Next(Enum.GetNames(typeof(OTypes)).Length);
                if ((OTypes)findItem == OTypes.Spellbook)
                {
                    return new SpellBook(dLevel, cLevel);
                }
            }
            #endregion
            #region Random Legendary
            else if ((ITypes)findItem == ITypes.Legendary)
            {
                findItem = rand.Next(lChance);
                if (findItem < ((lChance - 1) / 2))
                {
                    findItem = rand.Next(lChance);
                    if (findItem > ((lChance - 1) / 2))
                    {
                        findItem = rand.Next(lChance);
                        if(findItem > lBound && findItem < hBound)
                        {
                            randomLegendary(dLevel, cLevel);
                        }
                    }
                }
            }
            #endregion
            #endregion
            return new Potion(dLevel, cLevel);
        }

        public Sprite GetSprite()
        {
            return new Sprite(GetTexture());
        }

        public static Item randomLegendary(int dLevel, int cLevel)
        {
            int findItem;

            findItem = rand.Next(Enum.GetNames(typeof(Legends)).Length);
            if ((Legends)findItem == Legends.Weapon)
            {
                findItem = rand.Next(Enum.GetNames(typeof(LegendaryWeapon)).Length);
                if ((LegendaryWeapon)findItem == LegendaryWeapon._40k)
                {
                    return new _40k(dLevel, cLevel);
                }
                if ((LegendaryWeapon)findItem == LegendaryWeapon.KunaiWithChain)
                {
                    return new KunaiWithChain(dLevel, cLevel);
                }
                if ((LegendaryWeapon)findItem == LegendaryWeapon.RodOfExtending)
                {
                    return new RodOfExtending(dLevel, cLevel);
                }
                if ((LegendaryWeapon)findItem == LegendaryWeapon.TheWolverine)
                {
                    return new TheWolverine(dLevel, cLevel);
                }
            }
            else if ((Legends)findItem == Legends.Armor)
            {
                findItem = rand.Next(Enum.GetNames(typeof(LegendaryArmor)).Length);
                if ((LegendaryArmor)findItem == LegendaryArmor.TheFirstAvenger)
                {
                    return new FirstAvenger(dLevel, cLevel);
                }
            }

            return new Potion(dLevel, cLevel);
        }

        public static Item getDesiredDitem(int dLevel, int cLevel)
        {
            return new KunaiWithChain(dLevel, cLevel);
        }
    }
}
