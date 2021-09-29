using _Project.Scripts.Gameplay.Projectiles;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability.EndAbilities.CombatAbilities
{
    public class DamageRuneCall : AoeDamageAbility
    {
        [SerializeField] private GameObject castObject;
        public override void Cast(Transform spawnPosition)
        {
            //check if cast can be casted
            if (!isCastable) return;
            
            
            GameObject rune = Instantiate(castObject, 
                spawnPosition.transform.position, 
                Quaternion.identity,
                spawnPosition.transform);
            rune.transform.localScale *= maxDistance;
            
            /*
            GameObject rune = PhotonNetwork.Instantiate(
                "Projectiles/" + castObject.name,
                spawnPosition.position,
                Quaternion.identity
            );
            */
            
            
            
            
            AoeStaticSpawn damageZone = rune.GetComponent<AoeStaticSpawn>();
            //damageZone.LifeTime = 
            damageZone.Damage = baseDamage;

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
