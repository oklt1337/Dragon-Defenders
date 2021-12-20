using System;
using Abilities.VisitorPattern.Scripts;
using Faction;
using SkillSystem.Nodes.BaseNodes.Scripts;

namespace GamePlay.Player.Commander.CommanderStats.Scripts
{
    [Serializable]
    public class CommanderStats : IVisitor
    {
        public CommanderStats(ClassAndFaction.Faction faction, ClassAndFaction.Class @class, float health, float mana,
            float attackDamageModifier, float defense, float speed)
        {
            Faction = faction;
            CommanderClass = @class;
            MaxHealth = health;
            MaxMana = mana;
            AttackDamageModifier = attackDamageModifier;
            Defense = defense;
            Speed = speed;
        }

        private float health;
        private float maxHealth;
        private float maxMana;
        private float mana;
        private float speed;

        public ClassAndFaction.Faction Faction { get; internal set; }
        public ClassAndFaction.Class CommanderClass { get; internal set; }
        public float AttackDamageModifier { get; internal set; }
        public float Defense { get; internal set; }

        public float Health
        {
            get => health;
            internal set
            {
                health = value;
                if (health <= 0)
                {
                    OnCommanderDeath?.Invoke();
                    health = 0;
                }
                OnCommanderHealthChanged?.Invoke(health);
            }
        }

        public float MaxHealth
        {
            get => maxHealth;
            internal set
            {
                if (value > maxHealth)
                    Health += value - maxHealth;
                
                maxHealth = value;
                OnCommanderMaxHealthChanged?.Invoke(maxHealth);
            }
        }

        public float Mana
        {
            get => mana;
            internal set
            {
                mana = value;
                OnCommanderManaChanged?.Invoke(mana);
            } 
        }
        public float MaxMana
        {
            get => maxMana;
            internal set
            {
                if (value > maxMana)
                    Mana += value - maxMana;
                
                maxMana = value;
                OnCommanderMaxManaChanged?.Invoke(maxMana);
            } 
        }
        
        public float Speed { 
            get => speed;
            internal set
            {
                speed = value;
                OnCommanderSpeedChanged?.Invoke(speed);
            } 
        }
        
        public event Action OnCommanderDeath;
        public event Action<float> OnCommanderHealthChanged;
        public event Action<float> OnCommanderMaxHealthChanged;
        public event Action<float> OnCommanderManaChanged; 
        public event Action<float> OnCommanderMaxManaChanged;
        public event Action<float> OnCommanderSpeedChanged;

        public void Visit(Node node)
        {
            node.Accept(this);
        }
    }
}