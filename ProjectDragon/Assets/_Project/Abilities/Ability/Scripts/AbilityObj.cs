using System;
using System.Collections.Generic;
using Abilities.VisitorPattern.Scripts;
using Faction;
using SkillSystem.Nodes.BaseNodes.Scripts;
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
        [SerializeField] private Sprite icon;
        public AbilityType AbilityType => abilityType;
        public float CoolDown => coolDown;
        public Sprite Icon => icon;

        public new abstract T CreateInstance<T>() where T : Ability;
    }

    public abstract class Ability : IVisitor
    {
        public AbilityObj AbilityAbilityObj { get; }
        public float CoolDown { get; set; }
        public float TimeLeft { get; set; }
        public Transform Owner { get; set; }
        public Sprite Icon { get; set; }
        public List<ClassAndFaction.Class> AllowedAttackingTypes { get; set; }

        public bool StartCooldown { get; internal set; }
        public Action<Transform> Casted;

        protected Ability(AbilityObj abilityAbilityObj)
        {
            AbilityAbilityObj = abilityAbilityObj;
            CoolDown = abilityAbilityObj.CoolDown;
            Icon = abilityAbilityObj.Icon;
        }

        public void Init(Transform owner)
        {
            Owner = owner;
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
