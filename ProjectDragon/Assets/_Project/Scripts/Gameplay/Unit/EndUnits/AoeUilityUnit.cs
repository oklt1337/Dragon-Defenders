namespace _Project.Scripts.Gameplay.Unit.EndUnits
{
    public class AoeUilityUnit : Utility
    {
        protected override void Update()
        {
            if (ability.IsCastable)
                ability.Cast();
        }
    }
}
