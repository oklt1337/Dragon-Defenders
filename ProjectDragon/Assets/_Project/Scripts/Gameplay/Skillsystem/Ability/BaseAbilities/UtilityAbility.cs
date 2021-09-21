using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities
{
    public abstract class UtilityAbility: Ability
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

        protected override void Start()
        {
            base.Start();
            duration = ((UtilityAbilityDatabase) abilityDatabase).Duration;
            buffValue = ((UtilityAbilityDatabase) abilityDatabase).BuffValue;
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
