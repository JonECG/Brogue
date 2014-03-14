using Brogue.Abilities.AOE;
using Brogue.Abilities.Damaging.SingleTargets;
using Brogue.Abilities.SingleTargets;
using Brogue.Abilities.Togglable;
using Brogue.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.HeroClasses
{
    [Serializable] public class Juggernaut : Hero
    {
        public Juggernaut()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/Juggernaut");
            Hero.loadSprite();
            heroRole = Classes.Juggernaut;
            baseHealth = 500;
            healthPerLevel = 75;
            requiredBranchLevel = int.MaxValue;
            resetLevel();
            resetHealth();
            abilities[0] = new Cleave();
            abilities[1] = new WhirlwindSlash();
            abilities[2] = new ShieldBlock();
            abilities[3] = new ShieldBash();
            abilities[4] = new Thornmail();
            abilities[5] = new DivineShield();
            Enemies.Enemy.UpdateTargets(this);
            Enemies.BossEnemy.UpdateBossTargets(this);
        }
    }
}
