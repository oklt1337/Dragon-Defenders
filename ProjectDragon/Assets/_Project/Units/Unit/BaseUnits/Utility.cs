namespace _Project.Units.Unit.BaseUnits
{   /// <summary>
    ///     Author: Peter Luu
    /// </summary>
    public class Utility : OldUnit
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

        protected override void LoadDataFromScriptableObject()
        {
            base.LoadDataFromScriptableObject();
        }
        
        protected override void ApplyModifiers()
        {
            ability.Cooldown = cooldown;
        }

        #endregion
    
        #region Public Methods

        public override void LevelUp()
        {
            level++;
            
            //skilltree new increase
            //UpgradeSkill();
            
            //cooldown gets smaller 
            //cooldown *= 0.90f;
            
            //apply new modifiers
            ApplyModifiers();
        }
        
        public override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            if (ability.IsCastable)
                ability.Cast();
        }

        #endregion
    
        #region CallBacks


        #endregion
        

        
    }
}
