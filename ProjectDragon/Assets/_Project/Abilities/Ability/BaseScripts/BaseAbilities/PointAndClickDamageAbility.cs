using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.AI.Enemies.Scripts;
using _Project.Projectiles.HominProjectile;
using Photon.Pun;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
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
        
        public override void Cast(Transform spawnPosition, Transform enemy)
        {
            //check if cast can be casted
            if (!isCastable) return;
         
            GameObject tmpFireBall = PhotonNetwork.Instantiate(
                string.Concat(projectilepath, damageProjectile.name),
                spawnPosition.position,
                Quaternion.identity
            );
         
         
            HomingProjectile projectile = tmpFireBall.GetComponent<HomingProjectile>();
            projectile.Target = enemy;
            projectile.Speed = Speed;
            projectile.Damage = baseDamage;
         
            //at the end of cast the cooldown has to be reset
            ResetCoolDown();
        }
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
        }
    

        #endregion
    
        #region CallBacks


        #endregion
    }
}

