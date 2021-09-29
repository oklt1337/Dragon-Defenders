using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities
{
    public class SingleTargetDamageAbility : DamageAbility
    {
        [SerializeField] protected float speed;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        protected override void Start()
        {
            base.Start();
            speed = ((SingleTargetDamageAbilityDataBase)abilityDatabase).Speed;
        }
        protected override void Update()
        {
            base.Update();
        }
    }
}
