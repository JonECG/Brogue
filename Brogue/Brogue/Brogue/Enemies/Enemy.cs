using Brogue.Engine;
using Brogue.Mapping;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Brogue.Enums;
using Brogue.HeroClasses;

//owned by Jake
namespace Brogue.Enemies
{
    [Serializable]
    abstract class Enemy : GameCharacter
    {
        protected GameCharacter target;
        protected int attack;
        protected int defense;
        protected int moveSpeed = 1;
        protected int range;
        protected int aggroRange;
        protected int exp;
        protected ElementAttributes element;

        public bool IsAggro
        {
            get { return (target != null); }
        }

        /// <summary>
        /// Moves a single square based on a Direction
        /// </summary>
        /// <param name="d">Direction</param>
        /// <param name="level">The current level</param>
        public void Move(Direction d, Level level)
        {
            level.Move(this, d);
        }

        /// <summary>
        /// Determines whether or not this enemy is able to aggro any targets
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>whether or not this enemy has found a target</returns>
        public abstract bool Aggro(Level level);

        /// <summary>
        /// Essentially a constructor that is seperate from the actual constructor to allow for 0 argument construction for testing
        /// </summary>
        /// <param name="i">The dungeon level or the character level</param>
        public abstract void BuildEnemy(int i);

        /// <summary>
        /// Actions to perform on death (EXP rewarding, item dropping, enemy removal)
        /// </summary>
        protected virtual void Die()
        {
            Level level = Engine.Engine.currentLevel;
            Engine.Engine.AddXP(exp, Engine.Engine.currentLevel.CharacterEntities.FindPosition(this));
            IntVec itemPos = level.CharacterEntities.FindPosition(this);
            level.CharacterEntities.Remove(this);

            //CURRENTLY USING DUNGEON LEVEL OF 1 AND CHARACTER LEVEL OF 2. NEED ACCESS.
            level.DroppedItems.Add(Items.Item.randomItem(1, 2), itemPos);
        }
        
        /// <summary>
        /// Attacks the current target based on its attack
        /// </summary>
        protected void Attack()
        {
            target.TakeDamage(attack, this);
            if (element != null)
            {
                target.DealElementalDamage(element, (1 + Engine.Engine.currentLevel.DungeonLevel / 3));
            }
        }

        public GameCharacter Target
        {
            get { return target; }
            set { target = value; }
        }

        /// <summary>
        /// Converts damage input into the enemy to a lower damage based on this enemy's defense,  and then deals it and calls Die() if health is less than 0
        /// </summary>
        /// <param name="damage">The damage the attacker dealt</param>
        /// <param name="attacker">The GameCharacter that attacked this enemy (used for aggro)</param>
        public override void TakeDamage(int damage, GameCharacter attacker)
        {
            Level level = Engine.Engine.currentLevel;
            if (target == null)
            {
                target = attacker;
            }

            foreach (GameCharacter g in level.GetCharactersIsFriendly(false))
            {
                //if (AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(g)).Length <= 2)
                {
                    if (g is Enemy)
                    {
                        //(Enemy)g.Target = attacker;
                    }
                }
            }

            float tempArmor = (float)defense / 100f;
            if (attacker is Mage)
            {
                tempArmor /= 3;
            }
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
