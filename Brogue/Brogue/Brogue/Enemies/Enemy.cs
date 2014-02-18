using Brogue.Mapping;
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
        protected int aggroRange;

        public bool IsAggro
        {
            get { return (target != null); }
        }

        //Movement method, moves a single square
        public void Move(Direction d)
        {
            if (d == Direction.UP)
            {
                position.Y = position.Y + 1;
            }
            if (d == Direction.DOWN)
            {
                position.Y = position.Y - 1;
            }
            if (d == Direction.LEFT)
            {
                position.X = position.X - 1;
            }
            if (d == Direction.RIGHT)
            {
                position.X = position.X + 1;
            }
        }

        //This method will be called each turn to determine who (if anyone) to attack
        public abstract bool Aggro(Level level);

        //This method accepts an int i (maybe change to an enum later, talk about it with you guys) which corresponds
        //to the level of difficulty the enemy should be. This will also affect drop table choice.
        public abstract void BuildEnemy(int i);

        //Drops items and any other needed actions for death
        protected abstract void Die();
        
        //Attacks the current target
        protected void Attack()
        {
            target.takeDamage(attack);
        }

        //Converts damage based on armour and then removes from health.
        public override void TakeDamage(int damage)
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
