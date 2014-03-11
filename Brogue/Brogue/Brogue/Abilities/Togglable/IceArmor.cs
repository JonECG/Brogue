using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brogue.Abilities.Togglable
{
    [Serializable] public class IceArmor : ToggleAbility
    {
        const int armorBoost = 10;

        public IceArmor()
        {
            name = "Ice Armor";
            description = "The mage summons armor to protect him \nduring battle.";
            abilitiySprite = new Sprite(abilityLine, new IntVec(13, 0));
        }

        public override void updateToggle(int heroLevel, HeroClasses.Hero hero) 
        {
            if (createdLevel < heroLevel && isActive)
            {
                hero.setArmorBoost(armorBoost + (int)(heroLevel * 1.5));
                createdLevel = heroLevel;
            }
        }

        public override void finishCastandDealDamage(int heroLevel, int heroDamage, Mapping.Level mapLevel, HeroClasses.Hero hero)
        {
            if (!isActive)
            {
                createdLevel = heroLevel;
                Engine.Engine.Log(hero.getArmorBoost().ToString());
                hero.ApplyArmorBoost(armorBoost + (int)(heroLevel * 1.5), int.MaxValue);
                Engine.Engine.Log(hero.getArmorBoost().ToString());
                isActive = true;
            }
            else if (isActive)
            {
                hero.ApplyArmorBoost(0, 0);
                isActive = false;
            }
            cooldown = 0;
        }
    }
}
