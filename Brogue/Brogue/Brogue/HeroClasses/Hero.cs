using Brogue.Inventories;
using Brogue.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.HeroClasses
{

    public enum direction { LEFT, RIGHT, UP, DOWN };

    public abstract class Hero : GameCharacter
    {
        public static int maxHealth { get; protected set; }
        public static int currentHealth { get; protected set; }
        protected int numAbilities;
        protected int spacesPerTurn;
        protected float directionFacing;
        protected Ability[] abilities;
        protected Gear[] currentlyEquippedItems;
        protected Inventory inventory;

        public void move(direction dir, bool canMove)
        {
            if (dir.Equals(direction.RIGHT))
            {
                directionFacing = 0;
                position.ints[0] = (canMove) ? 1 : 0;
            }
            if (dir.Equals(direction.DOWN))
            {
                directionFacing = (float)(Math.PI / 2);
                position.ints[1] = (canMove) ? 1 : 0;
            }
            if (dir.Equals(direction.RIGHT))
            {
                directionFacing = (float)(Math.PI);
                position.ints[0] += (canMove) ? -1 : 0;
            }
            if (dir.Equals(direction.UP))
            {
                directionFacing = (float)(3 * Math.PI / 2);
                position.ints[1] += (canMove) ? -1 : 0;
            }
        }

        public override void TakeDamage(int damage, GameCharacter attacker)
        {

        }

        public override void TakeTurn(Mapping.Level level)
        {
            throw new NotImplementedException();
        }

        //public void castAbility(int ability)
        //public void attack();
        //public void useItem();
        public void equipItem(int itemToEquip, int currentItemIndex = 0)
        {
            Item temp = currentlyEquippedItems[currentItemIndex];
            currentlyEquippedItems[currentItemIndex] = inventory.stored[itemToEquip].item;
        }

        public void swapItems(int itemOne, int itemTwo)
        {
            inventory.swapItem(itemOne, itemTwo);
        }

        public void pickupItem(Item item)
        {
            inventory.addItem(item);
        }

        public void dropItem(int whichItem, int count)
        {
            inventory.removeItem(whichItem, count);
        }
    }
}
