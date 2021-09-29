using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase
{
    //[CreateAssetMenu(menuName="Tools/Ability/SingleTargetDamageAbilityDataBase")]
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
