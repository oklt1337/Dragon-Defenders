using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using Unity.VisualScripting;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability
{
    public class SkillshotDamageAbility : SingleTargetDamageAbility
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
            maxDistance = ((SkillShotDamageAbilityDataBase)abilityDatabase).MAXDistance;
        }
        protected override void Update()
        {
            base.Update();
        }
    }
}
