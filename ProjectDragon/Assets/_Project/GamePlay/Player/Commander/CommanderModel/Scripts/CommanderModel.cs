using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree;
using UnityEngine;

namespace _Project.GamePlay.Player.Commander.CommanderModel.Scripts
{
    [CreateAssetMenu(menuName = "Tools/CommanderModel", fileName = "CommanderModel")]
    public class CommanderModel : ScriptableObject
    {
        public string commanderName;
        public GameObject commanderObj;
        public Factions.Faction faction;
        public Factions.Class commanderClass;
        public float health;
        public float mana;
        public float attackDamageModifier;
        public float defense;
        public float speed;
        public byte rank;
        public byte level;
        public float experience;
        public SkillTree skillTree;
        public CommanderAbilityDataBase commanderAbilityDataBase;
    }
}
