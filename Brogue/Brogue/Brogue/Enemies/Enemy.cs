using Brogue.Engine;
using Brogue.Mapping;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//owned by Jake
namespace Brogue.Enemies
{
    abstract class Enemy : GameCharacter
    {
        protected GameCharacter target;
        protected int attack;
        protected int defense;
        protected int moveSpeed;
        protected int range;
        protected int aggroRange;
        protected int exp;

        public bool IsAggro
        {
            get { return (target != null); }
        }

        //Movement method, moves a single square
        public void Move(Direction d, Level level)
        {
            level.Move(this, d);
            position.X += d.X;
            position.Y += d.Y;
        }

        //This method will be called each turn to determine who (if anyone) to attack
        public abstract bool Aggro(Level level);

        //This method accepts an int i (maybe change to an enum later, talk about it with you guys) which corresponds
        //to the level of difficulty the enemy should be. This will also affect drop table choice.
        public abstract void BuildEnemy(int i);

        //Drops items and any other needed actions for death
        protected virtual void Die()
        {
            Engine.Engine.AddXP(exp, Engine.Engine.currentLevel.CharacterEntities.FindPosition(this));
            Engine.Engine.currentLevel.CharacterEntities.Remove(this);
        }
        
        //Attacks the current target
        protected void Attack()
        {
            target.TakeDamage(attack, this);
        }

        //Converts damage based on armour and then removes from health.
        public override void TakeDamage(int damage, GameCharacter attacker)
        {
            if (target == null)
            {
                target = attacker;
            }
            float tempArmor = (float)defense / 100f;
            damage -= (int)((float)damage * tempArmor);
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }

        public override DynamicTexture GetTexture()
        {
            return Engine.Engine.GetTexture("Enemies/Enemy");
        }
    }
}
