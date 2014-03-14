using Brogue.Abilities.AOE;
using Brogue.Abilities.SingleTargets;
using Brogue.Abilities.Togglable;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.HeroClasses
{
    [Serializable]
    public class SpellBlade : Hero
    {
        public SpellBlade()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/SpellBlade");
            Hero.loadSprite();
            heroRole = Classes.SpellBlade;
            baseHealth = 400;
            healthPerLevel = 40;
            requiredBranchLevel = int.MaxValue;
            resetLevel();
            resetHealth();
            abilities[0] = new Fireball();
            abilities[1] = new Blink();
            abilities[2] = new ArcaneWeapon();
            abilities[3] = new SoulSiphon();
            abilities[4] = new ArcaneBarrage();
            abilities[5] = new ArcaneOverload();
            Enemies.Enemy.UpdateTargets(this);
            Enemies.BossEnemy.UpdateBossTargets(this);
        }
    }
}
