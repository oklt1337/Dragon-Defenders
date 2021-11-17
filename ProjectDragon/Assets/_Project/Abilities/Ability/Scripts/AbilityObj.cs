using System;
using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.Projectiles.Scripts;
using Abilities.Projectiles.Scripts.BaseProjectiles;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.SkillTree.Scripts;
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
        public AbilityObj AbilityObj { get; }
        public float CoolDown { get; set; }
        public float TimeLeft { get; set; }
        public bool StartCooldown { get; internal set; }
        public Action Casted;

        protected Ability(AbilityObj abilityObj)
        {
            AbilityObj = abilityObj;
            CoolDown = abilityObj.CoolDown;
        }

        public void Visit(Node node)
        {
            node.Accept(this);
        }

        public void Tick(float deltaTime)
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

        public abstract void Cast(Transform spawnPoint, Transform target, Caster caster);
    }
}
