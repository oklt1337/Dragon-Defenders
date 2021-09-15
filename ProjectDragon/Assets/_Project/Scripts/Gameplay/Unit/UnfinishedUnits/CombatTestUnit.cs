using _Project.Scripts.Gameplay.Enemies;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit.UnfinishedUnits
{
    public class CombatTestUnit : Combat
    {
        public override void Update()
        {
            if (!currentTarget || !currentTarget.gameObject.activeSelf)
            {
                Debug.Log("Combat not Cast");
                SelectTarget();
            }
            else
            {
                Debug.Log("Combat Cast");
                ability.Cast(transform,currentTarget);
            }
        }
    }
}
