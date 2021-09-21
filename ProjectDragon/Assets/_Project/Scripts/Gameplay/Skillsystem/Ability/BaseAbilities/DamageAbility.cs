using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities
{
    public abstract class DamageAbility : Ability
    {
        [SerializeField] protected float baseDamage;

        public float BaseDamage
        {
            get => baseDamage;
            set => baseDamage = value;
        }

        protected override void Start()
        {
            base.Start();
            baseDamage = ((DamageAbilityDataBase)abilityDatabase).BaseDamage;
        }
        protected override void Update()
        {
            base.Update();
        }
    }
    
    
}
