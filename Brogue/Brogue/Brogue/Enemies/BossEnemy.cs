using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    abstract class BossEnemy : GameCharacter
    {
        protected List<GameCharacter> targets;
        protected List<int> attacks;
        protected int defense;
        protected int moveSpeed;
        protected int attackRange1, attackRange2, attackRange3;

        public bool IsAggro
        {
            get { return (targets.Count > 0); }
        }

        //Movement method, moves a single square
        public void Move(Direction d)
        {
            position.X += d.X;
            position.Y += d.Y;
        }

        //This method will be called each turn to determine who (if anyone) to attack
        public abstract bool Aggro(Level level);

        //This method accepts an int i (maybe change to an enum later, talk about it with you guys) which corresponds
        //to the level of difficulty the enemy should be. This will also affect drop table choice.
        public abstract void BuildBoss(int i);

        //Drops items and any other needed actions for death
        protected abstract void Die();

        //Attacks the current target
        protected void Attack(int attackType)
        {
            int attack = attacks[attackType];

            foreach (GameCharacter g in targets)
            {
                g.TakeDamage(attack, this);
            }
        }

        //Converts damage based on armour and then removes from health.
        public override void TakeDamage(int damage, GameCharacter attacker)
        {
            if (targets.Count == 0)
            {
                targets[0] = attacker;
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
    }
}
