using Abilities.Ability.Scripts;
using Abilities.VisitorPattern.Scripts;
using GamePlay.Player.Commander.CommanderStats.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;

namespace SkillSystem.Nodes.ClassNodes.Scripts
{
    [CreateAssetMenu(menuName = "AbilitySystem/SkillTree/Nodes/ClassNodes/ChangeClass", fileName = "ChangeClass")]
    public class ChangeClass : BaseNodes.Scripts.ClassNodes 
    {
        public override void Execute(IVisitor visitor)
        {
            switch (visitor)
            {
                case Ability ability:
                {
                    var unit = ability.Owner.GetComponent<Unit>();
                    if (unit != null)
                    {
                        unit.UnitClass = @class;
                    }

                    break;
                }
                case CommanderStats commanderStats:
                    commanderStats.CommanderClass = @class;
                    break;
            }
        }
    }
}