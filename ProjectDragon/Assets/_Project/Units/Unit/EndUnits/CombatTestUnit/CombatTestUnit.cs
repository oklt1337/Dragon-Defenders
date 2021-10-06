using _Project.Units.Unit.BaseUnits;

namespace _Project.Units.Unit.EndUnits.CombatTestUnit
{
    public class CombatTestUnit : Combat
    {
        protected override void Update()
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
                    ability.Cast(transform,currentTarget);
                }
            }
        }
    }
}
