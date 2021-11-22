using Abilities.EndAbilities.MeleeAttack.Scripts;
using Abilities.VisitorPattern.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace SkillSystem.Nodes.RandomNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/RandomNodes/SetAnimatorBool", fileName = "SetAnimatorBool")]
    public class SetAnimatorBool : NodeObj
    {
        [SerializeField] private string animBool;
        [SerializeField] private bool state;
        public override void Execute(IVisitor visitor)
        {
            if (visitor is MeleeAttackAbility ability)
            {
                ability.Casted += transform =>
                    ability.Owner.GetComponent<Unit>().Animator.SetBool(animBool, state);
            }
        }
    }
}