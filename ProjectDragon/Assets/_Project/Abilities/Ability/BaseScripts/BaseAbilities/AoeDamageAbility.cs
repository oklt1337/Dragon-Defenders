using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Projectiles.AoeStaticSpawn;
using _Project.Units.Unit.BaseUnits;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEditor.Experimental;
using UnityEngine;

namespace _Project.Abilities.Ability.BaseScripts.BaseAbilities
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class AoeDamageAbility : DamageAbility
    {
        #region Singleton

        #endregion
    
        #region SerializeFields

    

        #endregion
    
        #region Private Fields

        
        private bool isScreamOftheWildActive;
        
        private int  screamOfTheWildCounter;
        [Min(1)]private int executeScreamWhenReached = 1;
        private float screamOfTheWildCounterScaler;
    

        #endregion
    
        #region protected Fields
        [ShowInInspector]protected bool isSpawnProjectileOnEnemyPosition;
        [ShowInInspector]protected float lifeTime;
        
    

        #endregion
    
        #region Public Fields
        
    

        #endregion
    
        #region Public Properties
        public float LifeTime
        {
            get => LifeTime;
            set => LifeTime = value;
        }
    

        #endregion
    
        #region Events

    

        #endregion
    
        #region Unity Methods
        public override void Start()
        {
            base.Start();
            //maxDistance = ((AoeDamageAbilityDataBase) abilityDatabase).MAXDistance;
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
            LifeTime = ((AoeDamageAbilityDataBase) dataBase).LifeTime;
            isSpawnProjectileOnEnemyPosition = ((AoeDamageAbilityDataBase) dataBase).IsSpawnProjectileOnEnemyPosition;
            
        }
    
        public override void Cast(Transform spawnPosition)
        {
            //check if cast can be casted
            if (!isCastable) return;

            GameObject rune;
            if (isSpawnProjectileOnEnemyPosition)
            {
                rune = SpawnProjectileOnEnemy(spawnPosition);
            }
            else
            {
                rune =  SpawnProjectileInFrontOfUnit();
            }

            if (isScreamOftheWildActive)
            {
                screamOfTheWildCounter %= executeScreamWhenReached;

                if (screamOfTheWildCounter == 0)
                {
                    Vector3 tempScale = rune.transform.localScale; 
                    rune.transform.localScale = new Vector3(
                        tempScale.x * screamOfTheWildCounterScaler,
                        tempScale.y,
                        tempScale.z * screamOfTheWildCounterScaler
                        );
                }
            }

            //at the end of cast the cooldown has to be reset
            ResetCoolDown();
        }

        protected virtual GameObject SpawnProjectileInFrontOfUnit()
        {
            GameObject rune = PhotonNetwork.Instantiate(
                string.Concat(projectilepath, damageProjectile.name),
                transform.position + transform.forward,
                Quaternion.identity
            );
            SetProjectileEssentials(rune);
            return rune;
        }
        
        protected virtual GameObject SpawnProjectileOnEnemy(Transform spawnPosition)
        {
            GameObject rune = PhotonNetwork.Instantiate(
                string.Concat(projectilepath, damageProjectile.name),
                spawnPosition.position,
                Quaternion.identity
            );
            SetProjectileEssentials(rune);
            return rune;
        }

        protected virtual void SetProjectileEssentials(GameObject rune)
        {
            AoeStaticSpawn damageZone = rune.GetComponent<AoeStaticSpawn>();
            damageZone.LifeTime = lifeTime;
            damageZone.Damage = baseDamage;
        }

        public void UnlockScreamOfTheWild(int executeScreamWhenReachedValue, float scale)
        {
            isScreamOftheWildActive = true;
            executeScreamWhenReached = executeScreamWhenReachedValue;
            screamOfTheWildCounterScaler = scale;
        }
        #endregion
    
        #region CallBacks


        #endregion
    }
}
