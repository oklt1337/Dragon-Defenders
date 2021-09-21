using _Project.Scripts.Gameplay.Enemies;
using _Project.Scripts.Gameplay.Projectiles;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.EndAbilities.CombatAbilities
{
    public class LinearTriShot : SkillshotDamageAbility
    {
        [SerializeField] private GameObject castObject;

        public override void Cast(Transform spawnPosition, Transform enemy)
        {
            //check if cast can be casted
            if (!isCastable) return;
            for(int i = 0; i < 3 ;i++){
                
                /*
                GameObject tempTriShot = Instantiate(castObject, 
                    spawnPosition.transform.position, 
                    quaternion.identity,
                    spawnPosition.transform);
                tempTriShot.transform.rotation = Quaternion.LookRotation(enemy.position - tempTriShot.transform.position);
                tempTriShot.transform.Rotate(0,-45 + (i * 45),0);
                */
                
                GameObject tempTriShot = PhotonNetwork.Instantiate(
                    "Projectiles/" + castObject.name,
                    spawnPosition.position,
                    Quaternion.identity
                );
                
                LinearProjectiles projectile = tempTriShot.GetComponent<LinearProjectiles>();
                projectile.Speed = Speed;
                projectile.Damage = baseDamage;
                //calculate how far it will go with that speed;
                projectile.SetLifeTime(maxDistance);
            }
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
