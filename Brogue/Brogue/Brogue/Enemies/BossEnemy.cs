using Brogue.Engine;
using Brogue.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Enemies
{
    [Serializable]
    abstract class BossEnemy : GameCharacter
    {
        protected List<GameCharacter> targets = new List<GameCharacter>();
        protected List<int> attacks = new List<int>();
        protected List<int> attackranges = new List<int>();
        protected int defense;
        protected int moveSpeed;
        protected int exp;

        public bool IsAggro
        {
            get { return (targets.Count > 0); }
        }

        //Movement method, moves a single square
        public void Move(Direction d, Level level)
        {
            level.Move(this, d);
        }

        //This method will be called each turn to determine who (if anyone) to attack
        public abstract void Aggro(Level level);

        //This method accepts an int i (maybe change to an enum later, talk about it with you guys) which corresponds
        //to the level of difficulty the enemy should be. This will also affect drop table choice.
        public abstract void BuildBoss(int i);

        //Drops items and any other needed actions for death
        protected virtual void Die()
        {
            Level level = Engine.Engine.currentLevel;
            Engine.Engine.AddXP(exp, Engine.Engine.currentLevel.CharacterEntities.FindPosition(this));
            level.DroppedItems.Add(Items.Item.randomLegendary(Engine.Engine.currentLevel.DungeonLevel, 2), level.CharacterEntities.FindPosition(this));
            Engine.Engine.currentLevel.CharacterEntities.Remove(this);
            
        }

        //Converts damage based on armour and then removes from health.
        public override void TakeDamage(int damage, GameCharacter attacker)
        {
            if (targets.Count == 0)
            {
                targets.Add(attacker);
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

        //Boss specific method
        protected IntVec FindNearestTarget(IEnumerable<GameCharacter> chars, Level level)
        {
            IntVec target = null;

            foreach (GameCharacter g in chars)
            {
                if (target == null)
                {
                    target = level.CharacterEntities.FindPosition(g);
                }
                else 
                {
                    int gRange = AStar.getCost(AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), level.CharacterEntities.FindPosition(g)));
                    int targetRange = AStar.getCost(AStar.getPathBetween(level, level.CharacterEntities.FindPosition(this), target));
                    if (gRange < targetRange)
                    {
                        target = level.CharacterEntities.FindPosition(g);
                    }
                }
            }

            if (target == null)
            {
                throw new Exception("The level does not contain any friendly units or a Hero.");
            }

            return target;
        }

        public override DynamicTexture GetTexture()
        {
            return Engine.Engine.GetTexture("Enemies/BossEnemy");
        }
    }
}
