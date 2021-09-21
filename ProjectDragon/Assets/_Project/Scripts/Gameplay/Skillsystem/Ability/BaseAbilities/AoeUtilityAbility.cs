using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities
{
    public class AoeUtilityAbility : UtilityAbility
    {
        [SerializeField] protected float maxDistance;

        public float MAXDistance
        {
            get => maxDistance;
            set => maxDistance = value;
        }

        protected override void Start()
        {
            base.Start();
            maxDistance = ((AoeUtilityAbilityDataBase) abilityDatabase).MAXDistance;
        }
        
        protected override void Update()
        {
            base.Update();
        }
        
    }
}
