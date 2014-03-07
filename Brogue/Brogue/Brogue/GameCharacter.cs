using Brogue.Engine;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brogue.Enums;

namespace Brogue
{
    [Serializable] public abstract class GameCharacter : IRenderable
    {
        public int health;
        public int maxHealth;
        public bool isFriendly;
        abstract public bool TakeTurn(Level level);
        public abstract void TakeDamage(int damage, GameCharacter attacker);
        public static DynamicTexture texture;
        private int elementDamageTime = 0;
        private int elementDamage = 0;
        public bool isFrozen{ get; set; }

        public GameCharacter()
        {
            HeroClasses.Hero.texture = Engine.Engine.GetTexture("Hero/Hero");
            HeroClasses.Hero.abilitySprite = Engine.Engine.GetTexture("abilityOverlay");
            HeroClasses.Hero.castingSquareSprite = Engine.Engine.GetTexture("CastingSquareOverlay");
            HeroClasses.Hero.loadSprite();
            Enemies.Enemy.texture = Engine.Engine.GetTexture("Enemies/Enemy");
            Enemies.MeleeEnemy.texture = Engine.Engine.GetTexture("Enemies/Enemy");
            Enemies.RangedEnemy.texture = Engine.Engine.GetTexture("Enemies/Enemy");
            Enemies.MageEnemy.texture = Engine.Engine.GetTexture("Enemies/Enemy");
            Enemies.BossEnemy.texture = Engine.Engine.GetTexture("Enemies/BossEnemy");
        }

        public void DealElementalDamage(ElementAttributes e, int duration)
        {
            int power = Engine.Engine.currentLevel.DungeonLevel;
            elementDamageTime = duration;

            if (e == ElementAttributes.Fire)
            {
                elementDamage = (int)(1.5d * (double)power);
            }
            else if (e == ElementAttributes.Ice)
            {
                elementDamage = power;
                isFrozen = true;
            }
            else if (e == ElementAttributes.Lighting)
            {
                elementDamage = 2 * power;
            }
        }

        private void checkElementDamage()
        {
            if (elementDamageTime > 0)
            {
                health -= elementDamage;
                elementDamage--;
            }
            else
            {
                elementDamage = 0;
                elementDamageTime = 0;
                isFrozen = false;
            }
        }

        public void Heal(int heal)
        {
            health = (health + heal > maxHealth) ? maxHealth : health + heal;
        }

        public Sprite GetSprite()
        {
            return new Sprite(GetTexture());
        }
        public virtual DynamicTexture GetTexture()
        {
            return Engine.Engine.GetTexture("GAME_CHAR");
        }
    }
}
