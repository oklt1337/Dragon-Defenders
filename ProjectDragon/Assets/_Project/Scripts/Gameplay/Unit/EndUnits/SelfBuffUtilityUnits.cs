using System;
using Unity.VisualScripting;

namespace _Project.Scripts.Gameplay.Unit.EndUnits
{
    public class SelfBuffUtilityUnits : Utility
    {
        protected override void Update()
        {
            if (ability.IsCastable)
                ability.Cast();
        }
    }
}
