using Abilities.VisitorPattern.Scripts;
using GamePlay.GameManager.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace SkillSystem.Nodes.StatNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/StatNodes/ReduceCooldownForEveryEnemyDeath", fileName = "ReduceCooldownForEveryEnemyDeath")]
    public class ReduceCooldownForEveryEnemyDeath : StatNode
    {
        public override void Execute(IVisitor visitor)
        {
            if (visitor is Unit unit)
            {
                GameManager.Instance.EnemySpawner.OnEnemyDeath += enemy =>
                {
                    DecreaseCoolDown(unit);
                };
            }
        }

        private void DecreaseCoolDown(Unit unit)
        {
            unit.Ability.CoolDown -= value;
            if (unit.Ability.CoolDown <= 0)
                unit.Ability.CoolDown = 0;
        }
    }
}