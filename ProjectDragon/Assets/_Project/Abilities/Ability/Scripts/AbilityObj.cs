using System;
using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.Scripts;
using SkillSystem.SkillTree.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities.Ability.Scripts
{
    public enum AbilityType
    {
        Damage,
        Utility
    }
    
    public abstract class AbilityObj : ScriptableObject
    {
        [SerializeField] private AbilityType abilityType;
        [SerializeField] private float coolDown;
        public AbilityType AbilityType => abilityType;
        public float CoolDown => coolDown;

        public new abstract T CreateInstance<T>() where T : Ability;
    }

    public abstract class Ability : IVisitor
    {
        public AbilityObj AbilityAbilityObj { get; }
        public float CoolDown { get; set; }

        public float TimeLeft { get; set; }

        public bool StartCooldown { get; internal set; }
        public Action Casted;

        protected Ability(AbilityObj abilityAbilityObj)
        {
            AbilityAbilityObj = abilityAbilityObj;
            CoolDown = abilityAbilityObj.CoolDown;
        }

        public void Visit(Node node)
        {
            node.Accept(this);
        }

        public virtual void Tick(float deltaTime)
        {
            if (StartCooldown)
            {
                TimeLeft = CoolDown;
                StartCooldown = false;
            }
            else
            {
                TimeLeft -= deltaTime;
            }
        }
    }
}
