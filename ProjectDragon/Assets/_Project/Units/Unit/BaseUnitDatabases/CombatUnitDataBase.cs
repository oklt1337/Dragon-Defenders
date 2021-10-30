using UnityEngine;

namespace _Project.Units.Unit.BaseUnitDatabases
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    [CreateAssetMenu(menuName="Tools/Units/CombatUnitDataBase")]
    public class CombatUnitDataBase : BaseUnitDataBase
    {
        [SerializeField] protected float attackDamageModifier;
        [SerializeField] protected float attackRange;
        public float AttackDamageModifier
        {
            get => attackDamageModifier;
            set => attackDamageModifier = value;
        }
                                 
        public float AttackRange
        {
            get => attackRange;
            set => attackRange = value;
        }
    }
}
