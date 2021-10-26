using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Projectiles.LinearProjectiles;
using _Project.Scripts.Gameplay.Skillsystem.Ability;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Abilities.Ability.EndAbilities.CombatAbilities.LinearTriShot
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class LinearTriShot : SkillShotDamageAbility
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

        private static int bulletsPerCast = 3;
        private int angleOffset = 45;

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
            for(int i = 0; i < bulletsPerCast ;i++)
            {
                
                GameObject tempTriShot = PhotonNetwork.Instantiate(
                    string.Concat(projectilepath, damageProjectile.name),
                    spawnPosition.position,
                    Quaternion.identity
                );
                tempTriShot.transform.rotation = Quaternion.LookRotation(enemy.position - tempTriShot.transform.position);
                
                tempTriShot.transform.Rotate(0,-angleOffset + (i * angleOffset),0);
                
                //bulletsPerCast 
                
                tempTriShot.transform.Rotate(0,-angleOffset + (i * angleOffset),0);
                
                
                
                
                /**/
                
                LinearProjectiles projectile = tempTriShot.GetComponent<LinearProjectiles>();
                projectile.Speed = Speed;
                projectile.Damage = baseDamage;
                //calculate how far it will go with that speed;
                projectile.SetLifeTime(maxProjectileRange);
            }
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
