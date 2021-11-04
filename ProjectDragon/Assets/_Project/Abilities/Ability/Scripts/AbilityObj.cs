using Abilities.VisitorPattern.Scripts;
using SkillSystem.SkillTree.Scripts;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

namespace Abilities.Ability.Scripts
{
    public abstract class AbilityObj : ScriptableObject
    {
        public Ability CreateInstance()
        {
            return new Ability(this);
        }

        public virtual void Cast(Transform spawnPoint, Transform target)
        {
            //Spawn projectile or Cast Buff
        }
    }

    public class Ability : IVisitor
    {
        public AbilityObj AbilityObj { get; }

        public Ability(AbilityObj abilityObj)
        {
            AbilityObj = abilityObj;
        }

        public void Visit(Node node)
        {
            node.Accept(this);
        }
    }
}
