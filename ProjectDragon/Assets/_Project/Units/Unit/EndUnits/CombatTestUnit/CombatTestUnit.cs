using _Project.Units.Unit.BaseUnits;

namespace _Project.Units.Unit.EndUnits.CombatTestUnit
{
    public class CombatTestUnit : Combat
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields

    

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties

    

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods

    

        #endregion
    
        #region Private Methods

    

        #endregion
    
        #region Protected Methods

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
                    ability.Cast(transform,currentTarget);
                }
            }
        }

        #endregion
    
        #region Public Methods

    

        #endregion
    
        #region CallBacks


        #endregion
    }
}
