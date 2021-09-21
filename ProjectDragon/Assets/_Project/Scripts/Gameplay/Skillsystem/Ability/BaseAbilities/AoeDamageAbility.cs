using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities
{
    public class AoeDamageAbility : DamageAbility
    {
        public float maxDistance;

        public float MAXDistance
        {
            get => maxDistance;
            set => maxDistance = value;
        }

        protected override void Start()
        {
            base.Start();
            maxDistance = ((AoeDamageAbilityDataBase) abilityDatabase).MAXDistance;
        }
        protected override void Update()
        {
            base.Update();
        }
    }
}
