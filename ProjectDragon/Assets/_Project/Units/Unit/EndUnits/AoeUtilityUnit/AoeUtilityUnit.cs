namespace _Project.Units.Unit.EndUnits.AoeUtilityUnit
{
    /// <summary>
    ///     Author: Peter Luu
    /// </summary>
    public class AoeUtilityUnit : Units.Unit.BaseUnits.Utility
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
            if (ability.IsCastable)
                ability.Cast();
        }

        #endregion
    
        #region Public Methods

    

        #endregion
    
        #region CallBacks


        #endregion
        
    }
}
