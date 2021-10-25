using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class SingleTargetDamageAbilityDataBase : DamageAbilityDataBase
    {
        [SerializeField] protected float speed;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }
    }
}
