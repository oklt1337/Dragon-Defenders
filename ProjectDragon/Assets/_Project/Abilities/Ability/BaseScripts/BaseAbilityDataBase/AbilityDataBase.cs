using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase
{
    //[CreateAssetMenu(menuName="Tools/Ability/AbilityDataBase")]
    public class AbilityDataBase : ScriptableObject
    {
        [SerializeField] protected float manaCost;
        [SerializeField] protected float cooldown;

        public float ManaCost
        {
            get => manaCost;
            set => manaCost = value;
        }

        public float Cooldown
        {
            get => cooldown;
            set => cooldown = value;
        }
    }
}
