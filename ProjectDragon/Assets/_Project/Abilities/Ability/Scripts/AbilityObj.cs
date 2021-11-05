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
        public AbilityType AbilityType => abilityType;
    }

    public abstract class Ability : IVisitor
    {
        public AbilityObj AbilityObj { get; }

        protected Ability(AbilityObj abilityObj)
        {
            AbilityObj = abilityObj;
        }

        public void Visit(Node node)
        {
            node.Accept(this);
        }
    }
}
