using System;
using _Project.GamePlay.GameManager.Scripts;
using _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using UnityEngine;

namespace _Project.AI.Enemies.Scripts
{
    /// Please burn the content to the Ground after Friday 29.10.2021
    public class Flier : Mover
    {
        
        
        private Transform commander;

        /// <summary>
        /// Flies to the Base.
        /// </summary>
        public void FlyToBase()
        {
            agent.SetDestination(GameManager.Instance.Hq.transform.position);
        }

        /// <summary>
        /// Flies to the Base.
        /// </summary>
        public void FlyToCommander()
        {
            agent.SetDestination(commander.position);
        }

        public void Start()
        {
            commander = GameManager.Instance.PlayerModel.Commander.transform;

            transform.position =
                    new Vector3(
                        transform.position.x,
                        GameManager.Instance.PlayerModel.Commander.transform.position.y,
                        transform.position.z);

        }

        private void Update()
        {
            FlyToCommander();
        }

        private int counter = 0;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.transform == commander)
            {
                other.GetComponent<Commander>().TakeDamage(1);
                //Death();
                if (++counter >= 3)
                {
                    Death();
                }
            }
        }
    }
}
