using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase
{
    //[CreateAssetMenu(menuName="Tools/Ability/AbilityDataBase")]
    public class AbilityDataBase : ScriptableObject
    {
        [SerializeField] protected float manaCost;
        [SerializeField] protected float cooldown;
        [SerializeField] protected AnimationClip animationClip;
        [SerializeField] protected AudioClip audioClip;
        public float ManaCost
        {
            get => manaCost;
            private set => manaCost = value;
        }

        public AnimationClip AnimationClip
        {
            get => animationClip;
            private set => animationClip = value;
        }

        public AudioClip AudioClip
        {
            get => audioClip;
            private set => audioClip = value;
        }

        public float Cooldown
        {
            get => cooldown;
            private set => cooldown = value;
        }
    }
}
