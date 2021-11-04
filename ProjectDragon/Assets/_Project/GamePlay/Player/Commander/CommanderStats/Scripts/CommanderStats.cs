using Abilities.VisitorPattern.Scripts;
using Faction;
using SkillSystem.SkillTree.Scripts;

namespace GamePlay.Player.Commander.CommanderStats.Scripts
{
    public class CommanderStats : IVisitor
    {
        public CommanderStats(ClassAndFaction.Faction faction, ClassAndFaction.Class @class, float health, float mana,
            float attackDamageModifier, float defense, float speed)
        {
            Faction = faction;
            CommanderClass = @class;
            Health = health;
            MAXHealth = health;
            Mana = mana;
            MaxMana = mana;
            AttackDamageModifier = attackDamageModifier;
            Defense = defense;
            Speed = speed;
        }

        public ClassAndFaction.Faction Faction { get; internal set; }
        public ClassAndFaction.Class CommanderClass { get; internal set; }
        public float Health { get; internal set; }
        public float MAXHealth { get; internal set; }
        public float Mana { get; internal set; }
        public float MaxMana { get; internal set; }
        public float AttackDamageModifier { get; internal set; }
        public float Defense { get; internal set; }
        public float Speed { get; internal set; }

        public void Visit(Node node)
        {
            node.Accept(this);
        }
    }
}