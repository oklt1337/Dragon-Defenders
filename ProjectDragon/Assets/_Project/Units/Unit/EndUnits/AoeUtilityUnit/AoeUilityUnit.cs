namespace _Project.Scripts.Gameplay.Unit.EndUnits
{
    public class AoeUilityUnit : Units.Unit.BaseUnits.Utility
    {
        protected override void Update()
        {
            if (ability.IsCastable)
                ability.Cast();
        }
    }
}
