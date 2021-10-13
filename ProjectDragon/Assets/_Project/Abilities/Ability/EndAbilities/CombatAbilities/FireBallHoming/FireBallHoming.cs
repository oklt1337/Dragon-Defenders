using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Projectiles;
using _Project.Scripts.Gameplay.Skillsystem.Ability;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Abilities.Ability.EndAbilities.CombatAbilities.FireBallHoming
{
   /// <summary>
   /// Author: Peter Luu
   /// </summary>
   public class FireBallHoming : PointAndClickDamageAbility
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

         public override void Update()
         {
            base.Update();
         }

         public override void Start()
         {
            base.Start();
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
