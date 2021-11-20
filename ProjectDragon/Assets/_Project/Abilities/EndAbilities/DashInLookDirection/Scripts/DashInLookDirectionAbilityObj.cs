using Abilities.Ability.Scripts;
using UnityEngine;

namespace Abilities.EndAbilities.DashInLookDirection.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/Abilities/UtilityAbilities/DashInLookDirection", fileName = "DashInLookDirection")]
    public class DashInLookDirectionAbilityObj : UtilityAbilityObj
    {
        [SerializeField] private int charges;
        public int Charges => charges;
        public override T CreateInstance<T>()
        {
            return new DashInLookDirectionAbility(this) as T;
        }

        public static void Cast(Transform target, float abilityDashDistance)
        {
            target.position += target.forward * abilityDashDistance;
        }
    }

    public class DashInLookDirectionAbility : UtilityAbility
    {
        public int MaxCharges { get; set; }
        public int Charges { get; set; }
        
        public DashInLookDirectionAbility(DashInLookDirectionAbilityObj abilityObj) : base(abilityObj)
        {
            MaxCharges = abilityObj.Charges;
            Charges = MaxCharges;
        }

        public override void OnEnter(Transform target)
        {
        }

        public override void OnStay(Transform target)
        {
            if (TimeLeft > 0) 
                return;
            if (Charges < 1)
                return;
            if (!CanDash(Owner.forward, EffectRange))
                return;
            StartCooldown = true;
            Casted?.Invoke(target);
            Charges--;
            
            DashInLookDirectionAbilityObj.Cast(Owner, EffectRange);
        }

        public override void OnExit(Transform target)
        {
        }

        public override void Tick(float deltaTime)
        {
            if (StartCooldown)
            {
                TimeLeft = CoolDown;
                StartCooldown = false;
            }
            else
            {
                TimeLeft -= deltaTime;
                if (!(TimeLeft <= 0)) 
                    return;
                if (Charges >= MaxCharges) 
                    return;
                Charges++;
                TimeLeft = CoolDown;
            }
        }

        private bool CanDash(Vector3 dir, float distance)
        {
            var ray = new Ray(Owner.position, dir);
            Physics.Raycast(ray, out var hit, distance);
            return hit.collider == null;
        }
    }
}