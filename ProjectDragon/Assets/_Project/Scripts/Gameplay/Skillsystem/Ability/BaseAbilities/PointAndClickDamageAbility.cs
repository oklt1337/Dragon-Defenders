using _Project.Scripts.Gameplay.Enemies;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability
{
    public class PointAndClickDamageAbility : SingleTargetDamageAbility
    {
        public Enemy targetEnemy;
        [SerializeField] private float speed;
    }
}

