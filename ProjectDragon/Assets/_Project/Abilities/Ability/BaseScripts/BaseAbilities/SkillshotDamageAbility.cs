using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Projectiles.LinearProjectiles;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class SkillShotDamageAbility : SingleTargetDamageAbility
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

    

        #endregion
    
        #region protected Fields
        
        [ShowInInspector] protected float maxProjectileRange;
        [ShowInInspector] protected int bulletsPerCast;

        

        [ShowInInspector] protected float angleOffset;
        

        #endregion
    
        #region Public Fields

        

        #endregion
    
        #region Public Properties
        public float MaxProjectileRange
        {
            get => maxProjectileRange;
            set => maxProjectileRange = value;
        }
    
        public int BulletsPerCast
        {
            get => bulletsPerCast;
            set => bulletsPerCast = value;
        }

        public float AngleOffset
        {
            get => angleOffset;
            set => angleOffset = value;
        }
        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods
        public override void Start()
        {
            base.Start();
            //maxDistance = ((SkillShotDamageAbilityDataBase)abilityDatabase).MAXDistance;
        }
        public override void Update()
        {
            base.Update();
        }
    

        #endregion
    
        #region Private Methods

    

        #endregion
    
        #region Protected Methods
        
        public override void Cast(Transform spawnPosition, Transform enemy)
        {
            //check if cast can be casted
            if (!isCastable) return;
            
            //
            if (bulletsPerCast <= 1)
            {
                SingleShot( spawnPosition, enemy);
            }
            else
            {
                MultiShot( spawnPosition, enemy);
            }
            //at the end of cast the cooldown has to be reset
            ResetCoolDown();
        }

        protected void SingleShot(Transform spawnPosition, Transform enemy)
        {
            GameObject shot = PhotonNetwork.Instantiate(
                string.Concat(projectilepath, damageProjectile.name),
                spawnPosition.position,
                Quaternion.identity
            );
            shot.transform.rotation = Quaternion.LookRotation(enemy.position - shot.transform.position);
            SetProjectileEssentials(shot);
        }

        protected void MultiShot(Transform spawnPosition, Transform enemy)
        {
            float anglePerBullet = (bulletsPerCast != 1) ? angleOffset / (bulletsPerCast - 1) : 0;
            float startAngle = angleOffset * -0.5f;
            
            for(int i = 0; i < bulletsPerCast ;i++)
            {
                
                GameObject tempMultiShot = PhotonNetwork.Instantiate(
                    string.Concat(projectilepath, damageProjectile.name),
                    spawnPosition.position,
                    Quaternion.identity
                );
                tempMultiShot.transform.rotation = Quaternion.LookRotation(enemy.position - tempMultiShot.transform.position);
                tempMultiShot.transform.Rotate(0,startAngle + (i * anglePerBullet),0);

                SetProjectileEssentials(tempMultiShot);
            }
        }

        protected void SetProjectileEssentials(GameObject projectile)
        {
            LinearProjectiles projectileScript = projectile.GetComponent<LinearProjectiles>();
            projectileScript.Speed = Speed;
            projectileScript.Damage = baseDamage;
            //calculate how far it will go with that speed;
            projectileScript.SetLifeTime(maxProjectileRange);
        }
    

        #endregion
    
        #region Public Methods
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
            maxProjectileRange = ((SkillShotDamageAbilityDataBase) dataBase).MaxProjectileRange;
            bulletsPerCast = ((SkillShotDamageAbilityDataBase) dataBase).BulletsPerCast;
            angleOffset = ((SkillShotDamageAbilityDataBase) dataBase).AngleOffset;
        }
    

        #endregion
    
        #region CallBacks


        #endregion
    }
}
