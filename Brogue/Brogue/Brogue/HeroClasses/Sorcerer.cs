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
    [Serializable] public class Sorcerer : Hero
    {
        public Sorcerer()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/Sorcerer");
            Hero.loadSprite();
            heroRole = Classes.Sorcerer;
            baseHealth = 200;
            healthPerLevel = 35;
            requiredBranchLevel = 700;
            level = 10;
            resetLevel();
            resetHealth();
            Enemies.Enemy.UpdateTargets(this);
            Enemies.BossEnemy.UpdateBossTargets(this);
            abilities[0] = new Fireball();
            abilities[1] = new Blink();
            abilities[2] = new LightningStorm();
            abilities[3] = new IceArmor();
            Engine.Engine.Log(health.ToString());
        }
    }
}
