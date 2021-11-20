using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.RandomNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/RandomNodes/DestroyItSelf", fileName = "DestroyItSelf")]
    public class DestroyItSelf : NodeObj
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is Ability ability)
            {
                Destroy(ability.Owner.gameObject);
            }
        }
    }
}