using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit.UnitDatabases
{
    [CreateAssetMenu(menuName="Tools/Units/CombatUnitDataBase")]
    public class CombatUnitDataBase : BaseUnitDataBase
    {
        public float attackDamageModifier;
        public float attackRange;
    }
}
