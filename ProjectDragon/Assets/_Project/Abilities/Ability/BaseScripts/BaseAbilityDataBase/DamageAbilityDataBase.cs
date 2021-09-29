using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase
{
    //[CreateAssetMenu(menuName="Tools/Ability/DamageAbilityDataBase")]
    public class DamageAbilityDataBase : AbilityDataBase
    {
        [SerializeField] protected float baseDamage;

        public float BaseDamage
        {
            get => baseDamage;
            set => baseDamage = value;
        }
    }
}
