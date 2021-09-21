using System;
using _Project.Scripts.Gameplay.Enemies;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem.Ability
{
    public class PointAndClickDamageAbility : SingleTargetDamageAbility
    {
        protected Enemy _targetEnemy;

        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
        }

        public Enemy TargetEnemy
        {
            get => _targetEnemy;
            set => _targetEnemy = value;
        }
    }
}

