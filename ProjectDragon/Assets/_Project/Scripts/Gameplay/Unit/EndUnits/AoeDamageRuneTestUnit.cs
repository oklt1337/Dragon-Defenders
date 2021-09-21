using _Project.Scripts.Gameplay.Unit.BaseUnits;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit.EndUnits
{
    [CreateAssetMenu(menuName="Tools/Units/EndUnitDataBases/AoeDamageRuneTestUnit")]
    public class AoeDamageRuneTestUnit : Combat
    {
        public override void Update()
        {
            base.Update();
            if (ability.IsCastable)
            {
                //cast Check is inside the ability
                if (!currentTarget.gameObject.activeSelf)
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
