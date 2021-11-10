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
    }

    public abstract class Ability : IVisitor
    {
        public AbilityObj AbilityObj { get; }
        public float CoolDown { get; set; }
        public float TimeLeft { get; set; }
        public bool Casted { get; internal set; }

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
            if (Casted)
            {
                TimeLeft = CoolDown;
                Casted = false;
            }
            else
            {
                TimeLeft -= deltaTime;
            }
        }
    }
}
