using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit
{
    public abstract class Utility : Unit
    {
        
        public override void Start()
        {
            base.Start();
        }
        protected virtual void Update()
        {
        }

        protected override void LoadDataFromScriptableObject()
        {
            base.LoadDataFromScriptableObject();
        }
        
        protected override void LevelUp()
        {
            level++;
            
            //skilltree new increase
            UpgradeSkill();
            
            //cooldown gets smaller 
            cooldown *= 0.90f;
            
            //apply new modifiers
            ApplyModifiers();

        }
        
        protected override void ApplyModifiers()
        {
            ability.Cooldown = cooldown;
        }
    }
}
