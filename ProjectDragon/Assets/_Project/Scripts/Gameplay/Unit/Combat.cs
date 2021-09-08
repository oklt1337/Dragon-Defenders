using System;
using _Project.Scripts.Gameplay.Unit.UnitDatabases;
using ExitGames.Client.Photon.Encryption;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit
{
    public abstract class Combat : Unit
    {
        public float attackDamageModifier;
        public float attackRange;
        public Transform target;
    
    
        private void SelectTarget()
        {
        
        }
    }
}
