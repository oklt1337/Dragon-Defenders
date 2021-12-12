using Sirenix.Serialization;
using UnityEngine;

namespace AbilitySystem.Ability.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/Ability", fileName = "Ability")]
    public class Ability : ScriptableObject
    {
        [OdinSerialize] public Modifier.Scripts.Modifier modifier;
    }
}