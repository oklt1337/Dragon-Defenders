using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Abilities.Ability.BaseScripts.BaseAbilityDataBase;
using _Project.Scripts.Gameplay.Projectiles;
using _Project.Scripts.Gameplay.Skillsystem.Ability;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Abilities.Ability.EndAbilities.CombatAbilities.FireBallHoming
{
   public class FireBallHoming : PointAndClickDamageAbility
   {
      public override void Cast(Transform spawnPosition, Transform enemy)
      {
         //check if cast can be casted
         if (!isCastable) return;

         //if (!IsInitiated) return;
          /*GameObject tmpFireBall = Instantiate(damageProjectile, 
            spawnPosition.transform.position, 
            Quaternion.identity,
           spawnPosition.transform);
        */
         Debug.Log(damageProjectile.name);
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
      
      public override void Update()
      {
         base.Update();
      }

      public override void Start()
      {
         base.Start();
      }
      public override void Init(AbilityDataBase dataBase)
      {
         base.Init(dataBase);
      }
   }
}
