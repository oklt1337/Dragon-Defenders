using UnityEngine;

namespace Abilities.Ability.Scripts
{
    public abstract class AbilityObj : ScriptableObject
    {
        public Ability CreateInstance()
        {
            return new Ability(this);
        }

        public virtual void Cast(Transform spawnPoint, Transform target)
        {
            
        }
    }

    public class Ability
    {
        public AbilityObj AbilityObj { get; }
        
        public Ability(AbilityObj abilityObj)
        {
            AbilityObj = abilityObj;
        }
    }
}
