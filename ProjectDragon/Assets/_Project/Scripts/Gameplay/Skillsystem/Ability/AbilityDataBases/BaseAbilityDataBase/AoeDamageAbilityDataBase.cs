using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase
{
    //[CreateAssetMenu(menuName="Tools/Ability/AoeDamageAbilityDataBase")]
    public class AoeDamageAbilityDataBase : DamageAbilityDataBase
    {
        [SerializeField] protected float maxDistance;

        public float MAXDistance
        {
            get => maxDistance;
            set => maxDistance = value;
        }
    }
}