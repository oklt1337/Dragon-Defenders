using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase
{
    //[CreateAssetMenu(menuName="Tools/Ability/DamageAbilityDataBase")]
    public class DamageAbilityDataBase : AbilityDataBase
    {
        [SerializeField] protected float baseDamage;
        [SerializeField] protected GameObject damageProjectile;

        public GameObject DamageProjectile
        {
            get => damageProjectile;
            set => damageProjectile = value;
        }

        public float BaseDamage
        {
            get => baseDamage;
            set => baseDamage = value;
        }
    }
}
