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
    [Serializable]
    public class Spellweaver : Hero
    {
        public Spellweaver()
        {
            heroTexture = Engine.Engine.GetTexture("Hero/SpellWeaver");
            Hero.loadSprite();
            heroRole = Classes.SpellWeaver;
            baseHealth = 350;
            healthPerLevel = 30;
            requiredBranchLevel = int.MaxValue;
            resetLevel();
            resetHealth();
            abilities[0] = new Fireball();
            abilities[1] = new Blink();
            abilities[2] = new LightningStorm();
            abilities[3] = new IceArmor();
            abilities[4] = new FlameStrike();
            abilities[5] = new TimeStop();
            Enemies.Enemy.UpdateTargets(this);
            Enemies.BossEnemy.UpdateBossTargets(this);
        }
    }
}
