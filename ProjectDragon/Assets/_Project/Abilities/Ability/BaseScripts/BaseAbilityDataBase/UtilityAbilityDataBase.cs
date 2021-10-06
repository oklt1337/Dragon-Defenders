using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase
{
    //[CreateAssetMenu(menuName="Tools/Ability/UtilityAbilityDataBase")]
    public class UtilityAbilityDatabase : AbilityDataBase
    {
        [SerializeField] protected float duration;
        [SerializeField] protected float buffValue;

        public float Duration
        {
            get => duration;
            set => duration = value;
        }

        public float BuffValue
        {
            get => buffValue;
            set => buffValue = value;
        }
    }
}
