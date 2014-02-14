
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brogue.HeroClasses
{

    public enum direction { LEFT, RIGHT, UP, DOWN };

    public abstract class Hero
    {
        public GridLocation location;
        public int numAbilities;
        public int spacesPerTurn;
        public float directionFacing;
        //Ability[] abilities;
        //Item currentlyEquippedItem;
        //Inventory inventory;

        public void move(direction dir, bool canMove)
        {
            if (dir.Equals(direction.RIGHT))
            {
                directionFacing = 0;
                location.ints[0] = (canMove) ? 1 : 0;
            }
            if (dir.Equals(direction.DOWN))
            {
                directionFacing = (float)(Math.PI / 2);
                location.ints[1] = (canMove) ? 1 : 0;
            }
            if (dir.Equals(direction.RIGHT))
            {
                directionFacing = (float)(Math.PI);
                location.ints[0] += (canMove) ? -1 : 0;
            }
            if (dir.Equals(direction.UP))
            {
                directionFacing = (float)(3 * Math.PI / 2);
                location.ints[1] += (canMove) ? -1 : 0;
            }
        }

        //public void castAbility(int ability)
        //public void attack();
        //public void useItem();
        //public void equipItem();
        //public void pickupItem();
        //public void dropItem();
    }
}
