using System.Collections.Generic;
using _Project.Scripts.Gameplay.Faction;
using _Project.Scripts.Gameplay.Skillsystem;
using _Project.Scripts.Gameplay.Skillsystem.Ability;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using UnityEditor.Animations;
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
        public PointAndClickDamageAbility primaryAttack;
        public List<Ability> abilities;
        public AnimatorController animatorController;
    }
}
