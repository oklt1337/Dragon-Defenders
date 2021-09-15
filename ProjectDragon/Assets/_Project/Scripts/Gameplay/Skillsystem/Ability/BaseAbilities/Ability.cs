using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities
{
    public abstract class Ability : MonoBehaviour
    {
        public float manaCost;
        public float cooldown;
        public AnimationClip animationClip;
        public AudioClip audioClip;

        public virtual void Cast()
        {
            
        }
        
        public virtual void Cast(Transform enemy)
        {
            
        }
        public virtual void Cast(Transform owner, Transform enemy)
        {
            
        }
    }
}
