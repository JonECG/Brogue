using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue
{
    public abstract class GameCharacter : IRenderable
    {
        public int health;
        public int maxHealth;
		public IntVec position;
        public bool isFriendly;
        abstract public bool TakeTurn(Level level);
        public abstract void TakeDamage(int damage, GameCharacter attacker);
        public static Texture2D texture;
        public void Heal(int heal)
        {
            health += heal;
        }
        public virtual Sprite GetSprite()
        {
            return new Sprite(GetTexture());
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public static void LoadContent(ContentManager content)
        {
            HeroClasses.Hero.texture = content.Load<Texture2D>("Hero/Hero");
            HeroClasses.Hero.abilitySprite = content.Load<Texture2D>("abilityOverlay");
            HeroClasses.Hero.loadSprite();
            Enemies.Enemy.texture = content.Load<Texture2D>("Enemies/Enemy");
            Enemies.MeleeEnemy.texture = content.Load<Texture2D>("Enemies/Enemy");
            Enemies.RangedEnemy.texture = content.Load<Texture2D>("Enemies/Enemy");
            Enemies.MageEnemy.texture = content.Load<Texture2D>("Enemies/Enemy");

            Enemies.BossEnemy.texture = content.Load<Texture2D>("Enemies/BossEnemy");
        }
    }
}
