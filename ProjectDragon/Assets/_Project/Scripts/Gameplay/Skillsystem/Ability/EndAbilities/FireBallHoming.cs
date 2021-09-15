using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Unit;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability
{
   public class FireBallHoming : PointAndClickDamageAbility
   {
      [SerializeField] private GameObject castObject;
      private List<GameObject> objects;
      private Transform currentEnemy;

      private float tmpCooldown;
      //colliderList?

      public override void Cast(Transform owner, Transform enemy)
      {
         if (tmpCooldown > 0) return;
         tmpCooldown = cooldown;
         
         Debug.Log("CurrentEnemy was set.");
         GameObject tmpFireBall = Instantiate(castObject,
            owner.transform.position, 
            quaternion.identity,
           owner.transform); 
         
        currentEnemy = enemy;
        Debug.Log("CurrentEnemy was set.");
        
        //Getcomponent and setting values better values 
        tmpFireBall.GetComponent<FireBallHomingObject>().Damage = baseDamage;
        //tmpFireBall.GetComponent<FireBallHomingObject>().Speed = speed;
        objects.Add(tmpFireBall);
         
      }

      //at the time this is a prefab and does not exist in te script to update its projectiles
      //so he can't handle the projectiles
      public void Update()
      {
         coolDownCounter();
         if (!(currentEnemy == null))
         {
            foreach (GameObject projectile in objects)
            {
               //this check only if needed
               if (projectile)
               {
                  //LerP = nicht langsamer werden.
                  projectile.transform.position = Vector3.Lerp(projectile.transform.position,currentEnemy.transform.position, 1 * Time.deltaTime );
                  
                  
                  if ((projectile.transform.position - currentEnemy.transform.position).sqrMagnitude <= float.Epsilon )
                  {
                     Destroy(projectile);
                  }
                  Debug.Log("Lerping in progress");
                  
                  //colliderList
                  
               }
            }
         }
         
      }

      private void coolDownCounter()
      {
         tmpCooldown -= Time.deltaTime;
      }

      public void Start()
      {
         objects = new List<GameObject>();
      }
   }
}
