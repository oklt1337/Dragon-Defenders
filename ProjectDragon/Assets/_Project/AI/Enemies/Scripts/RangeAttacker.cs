using _Project.Enemies.Scripts;
using _Project.GamePlay.GameManager.Scripts;
using _Project.Projectiles.LinearProjectiles;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.AI.Enemies.Scripts
{
    //also burn this one and replace with a real AI system;
    public class RangeAttacker : Attacker
    {
        [SerializeField] private float cooldown = 8;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float maxDistance;
        private float tempCoolDown;
        [SerializeField] private GameObject projectileObject;
        private string projectilePath =  "Projectiles/";
        
        // Start is called before the first frame update
        void Start()
        {
            if (!projectileObject)
            {
                projectileObject = new GameObject();
            }
            tempCoolDown = cooldown;
            target = GameManager.Instance.PlayerModel.Commander;
        }

        // Update is called once per frame
        void Update()
        {
            if ((target.transform.position - transform.position).sqrMagnitude >= 900 )
            {
                agent.SetDestination(target.transform.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }
            tempCoolDown -= Time.deltaTime;
            if (tempCoolDown <= 0 )
            {
                
                
                tempCoolDown = cooldown;
                GameObject tempTriShot = PhotonNetwork.Instantiate(
                    string.Concat(projectilePath,projectileObject.name),
                    transform.position,
                    Quaternion.identity
                );
                tempTriShot.transform.rotation = Quaternion.LookRotation(target.transform.position - tempTriShot.transform.position);

                LinearProjectileEnemy projectile = tempTriShot.GetComponent<LinearProjectileEnemy>();
                projectile.Speed = speed;
                projectile.Damage = damage;
                //calculate how far it will go with that speed;
                projectile.SetLifeTime(maxDistance);
            }
        }
    }
}
