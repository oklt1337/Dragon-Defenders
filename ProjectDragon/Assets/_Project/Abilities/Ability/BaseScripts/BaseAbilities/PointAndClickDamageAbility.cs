using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.AI.Enemies.Scripts;
using _Project.Scripts.Gameplay.Skillsystem.Ability.AbilityDataBases.BaseAbilityDataBase;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    public class PointAndClickDamageAbility : SingleTargetDamageAbility
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields
        protected Enemy targetEnemy;

    

        #endregion
    
        #region Public Fields

    

        #endregion
    
        #region Public Properties
        public Enemy TargetEnemy
        {
            get => targetEnemy;
            set => targetEnemy = value;
        }
    

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods
        public override void Start()
        {
            base.Start();
        }
        public override void Update()
        {
            base.Update();
        }
    

        #endregion
    
        #region Private Methods

    

        #endregion
    
        #region Protected Methods

    

        #endregion
    
        #region Public Methods
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
        }
    

        #endregion
    
        #region CallBacks


        #endregion
    }
}

