namespace _Project.Units.Unit.BaseUnits
{
    public abstract class Utility : Unit
    {
        
        public override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
        }

        protected override void LoadDataFromScriptableObject()
        {
            base.LoadDataFromScriptableObject();
        }
        
        public override void LevelUp()
        {
            level++;
            
            //skilltree new increase
            //UpgradeSkill();
            
            //cooldown gets smaller 
            //cooldown *= 0.90f;
            
            //apply new modifiers
            ApplyModifiers();

        }
        
        protected override void ApplyModifiers()
        {
            ability.Cooldown = cooldown;
        }
    }
}
