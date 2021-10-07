using _Project.Units.Unit.BaseUnits;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit.EndUnits
{ 
    public class AoeDamageRuneTestUnit : Combat
    {
        protected override void Update()
        {
            base.Update();
            if (ability.IsCastable)
            {
                //cast Check is inside the ability
                if (!currentTarget || !currentTarget.gameObject.activeSelf)
                {
                    SelectTarget();
                }
                else
                {
                    ability.Cast(currentTarget);
                }
            }
        }
    }
}
