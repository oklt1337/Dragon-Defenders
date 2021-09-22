using System;
using Unity.VisualScripting;

namespace _Project.Scripts.Gameplay.Unit.EndUnits
{
    public class SelfBuffUtilityUnits : BaseUnits.Utility
    {
        protected override void Update()
        {
            if (ability.IsCastable)
                ability.Cast();
        }
    }
}
