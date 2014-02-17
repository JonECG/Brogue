using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//owned by Jake
namespace Brogue
{
    abstract class Enemy : GameCharacter
    {
        protected GameCharacter target;
        protected int attack;
        protected int defense;
        protected int health;
        protected int moveSpeed;
        protected int range;

        public Boolean IsAggro
        {
            get { return (target != null); }
        }

        //This method will be called each turn to determine who (if anyone) to attack
        public abstract bool aggro();

        //This method accepts an int i (maybe change to an enum later, talk about it with you guys) which corresponds
        //to the level of difficulty the enemy should be. This will also affect drop table choice.
        public abstract void buildEnemy(int i);

        //Drops items and any other needed actions for death
        protected abstract void die();
        
        //Attacks the current target
        protected abstract void attack();

        //Converts damage based on armour and then removes from health.
        public override void takeDamage(int damage)
        {
            float tempArmor = (float)defense / 100f;
            damage -= (int)((float)damage * tempArmor);
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                die();
            }
        }
    }
}
