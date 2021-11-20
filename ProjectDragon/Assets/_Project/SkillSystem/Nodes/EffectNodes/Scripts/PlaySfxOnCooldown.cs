using System;
using Abilities.Ability.Scripts;
using Abilities.Effects.SFXEffect.Scripts;
using Abilities.VisitorPattern.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.EffectNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/EffectNodes/PlaySfxOnCooldown", fileName = "PlaySfxOnCooldown")]
    public class PlaySfxOnCooldown : BaseNodes.Scripts.EffectNodes
    {
        [SerializeField] private AudioClip audioClip;
        public event Action OnPlaySound;

        public override void Execute(IVisitor visitor)
        {
            if (!(visitor is Ability ability)) 
                return;
            var effect = ability.Owner.gameObject.AddComponent<SfxEffect>();
            effect.Init(value, audioClip);
            effect.OnPlaySound += () => OnPlaySound?.Invoke();
        }
    }
}