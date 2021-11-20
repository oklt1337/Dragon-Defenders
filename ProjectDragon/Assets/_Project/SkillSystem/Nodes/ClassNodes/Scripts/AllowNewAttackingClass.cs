using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.ClassNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/ClassNodes/AllowNewAttackingType", fileName = "AllowNewAttackingType")]
    public class AllowNewAttackingClass : BaseNodes.Scripts.ClassNodes
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is Ability ability)
            {
                ability.AllowedAttackingTypes.Add(@class);
            }
        }
    }
}