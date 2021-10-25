using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Projectiles;
using Photon.Pun;
using UnityEngine;

namespace _Project.Abilities.Ability.EndAbilities.CombatAbilities.DamageRuneCall
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public class DamageRuneCall : AoeDamageAbility
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
        
        public override void Init(AbilityDataBase dataBase)
        {
            base.Init(dataBase);
        }

        public override void Cast(Transform spawnPosition)
        {
            //check if cast can be casted
            if (!isCastable) return;

            GameObject rune = PhotonNetwork.Instantiate(
                string.Concat(projectilepath, damageProjectile.name),
                spawnPosition.position,
                Quaternion.identity
            );

            AoeStaticSpawn damageZone = rune.GetComponent<AoeStaticSpawn>();
            
            //damageZone.LifeTime = 
            damageZone.Damage = baseDamage;

            //at the end of cast the cooldown has to be reset
            ResetCoolDown();
        }

        #endregion
    
        #region CallBacks


        #endregion
        
      
        
    }
}
