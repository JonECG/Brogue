using Brogue.Abilities.Damaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.SingleTargets
{
    [Serializable]
    public class Mug : SingleTarget
    {
        public Mug()
        {
            name = "Mug";
            description = "The rogue strikes the enemy chosen, and obtains a random item.";
            castSquares = new IntVec[1];
            for (int i = 0; i < castSquares.Length; i++)
            {
                castSquares[i] = new IntVec(0, 0);
            }
            baseDamage = 6;
            radius = 1;
            abilityCooldown = 6;
        }

        protected override void finishCast(int damage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            for (int i = 0; i < castSquares.Length; i++)
            {
                GameCharacter test = (GameCharacter)mapLevel.CharacterEntities.FindEntity(castSquares[i]);
                if (test != null)
                {
                    Audio.playSound("Mugging");
                    test.TakeDamage(damage, hero);
                }
                castSquares[i] = new IntVec(0, 0);
            }
            Items.Item heroItem = null;
            while(heroItem == null || heroItem.ItemType == Enums.ITypes.Consumable)
            {
                heroItem = Items.Item.randomItem(mapLevel.DungeonLevel, hero.level);
            }
            hero.GetInventory().addItem(heroItem);
        }

        public override int calculateDamage(int heroLevel, int heroDamage)
        {
            return (baseDamage * heroLevel) + heroDamage;
        }
    }
}
