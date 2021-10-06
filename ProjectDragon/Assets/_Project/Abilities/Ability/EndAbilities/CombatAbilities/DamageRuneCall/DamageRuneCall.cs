using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.Scripts.Gameplay.Projectiles;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using Photon.Pun;
using UnityEngine;

namespace _Project.Abilities.Ability.EndAbilities.CombatAbilities.DamageRuneCall
{
    public class DamageRuneCall : AoeDamageAbility
    {
        public override void Cast(Transform spawnPosition)
        {
            //check if cast can be casted
            if (!isCastable) return;
            /*GameObject rune = Instantiate(castObject, 
                spawnPosition.transform.position, 
                Quaternion.identity,
                spawnPosition.transform);
            rune.transform.localScale *= maxDistance;
            */
            
            GameObject rune = PhotonNetwork.Instantiate(
                string.Concat(projectilepath, damageProjectile.name),
                spawnPosition.position,
                Quaternion.identity
            );
            /**/
            
            
            
            
            AoeStaticSpawn damageZone = rune.GetComponent<AoeStaticSpawn>();
            //damageZone.LifeTime = 
            damageZone.Damage = baseDamage;

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
    }
}
