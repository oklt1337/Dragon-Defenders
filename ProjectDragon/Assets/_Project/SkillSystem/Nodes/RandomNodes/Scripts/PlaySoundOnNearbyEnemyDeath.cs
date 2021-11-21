using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using AI.Enemies.Base_Enemy.Scripts;
using GamePlay.GameManager.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using UnityEngine;

namespace SkillSystem.Nodes.RandomNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/RandomNodes/PlaySoundOnNearbyEnemyDeath", fileName = "PlaySoundOnNearbyEnemyDeath")]
    public class PlaySoundOnNearbyEnemyDeath : NodeObj
    {
        [SerializeField] private AudioClip audioClip;
        private DamageAbility damageAbility;
        public override void Execute(IVisitor visitor)
        {
            if (visitor is DamageAbility)
            {
                GameManager.Instance.EnemySpawner.OnEnemyDeath += PlaySound;
            }
        }

        private void PlaySound(Enemy enemy)
        {
            if (Vector3.Distance(enemy.transform.position, damageAbility.Owner.transform.position) < damageAbility.AttackRange)
            {
                AudioManager.Scripts.AudioManager.Instance.PlayAudioClipOneShot(audioClip);
            }
        }
    }
}