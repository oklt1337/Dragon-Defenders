using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Projectiles;
using _Project.Scripts.Gameplay.Unit;
using Photon.Pun;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability
{
   public class FireBallHoming : PointAndClickDamageAbility
   {
      [SerializeField] private GameObject castObject;

      public override void Cast(Transform spawnPosition, Transform enemy)
      {
         //check if cast can be casted
         if (!isCastable) return;
         
         
         GameObject tmpFireBall = Instantiate(castObject, 
            spawnPosition.transform.position, 
            quaternion.identity,
           spawnPosition.transform);
         
         /*
         GameObject tmpFireBall = PhotonNetwork.Instantiate(
            "Projectiles/" + castObject.name,
            spawnPosition.position,
            Quaternion.identity
         );
         */
         
         HomingProjectile projectile = tmpFireBall.GetComponent<HomingProjectile>();
         projectile.Target = enemy;
         projectile.Speed = Speed;
         projectile.Damage = baseDamage;
         
         //at the end of cast the cooldown has to be reset
         ResetCoolDown();
      }
      
      protected override void Update()
      {
         base.Update();
      }

      protected override void Start()
      {
         base.Start();
      }
   }
}
