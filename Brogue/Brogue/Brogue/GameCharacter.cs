using Brogue.Engine;
using Brogue.Mapping;
using Microsoft.Xna.Framework.Content;
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
		public IntVec position = new IntVec(0, 0);
        public bool isFriendly;
        abstract public bool TakeTurn(Level level);
        public abstract void TakeDamage(int damage, GameCharacter attacker);
        public static DynamicTexture texture;

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

        public void Heal(int heal)
        {
            health = (health + maxHealth > maxHealth) ? maxHealth : health + heal;
        }

        public Sprite GetSprite()
        {
            return new Sprite(GetTexture());
        }
        public virtual DynamicTexture GetTexture()
        {
            return Engine.Engine.GetTexture("GameCharacter");
        }
    }
}
