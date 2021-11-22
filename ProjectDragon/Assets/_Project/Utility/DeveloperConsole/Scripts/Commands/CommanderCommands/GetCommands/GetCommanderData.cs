using GamePlay.GameManager.Scripts;
using UnityEngine;
using Utility.SceneManager.Scripts;

namespace Utility.DeveloperConsole.Scripts.Commands.CommanderCommands.GetCommands
{
    [CreateAssetMenu(fileName = "New Get Commander Data", menuName = "Utilities/DeveloperConsole/Commands/Comander/Get Commands/Get Commander Data")]
    public class GetCommanderData : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            if (args.Length > 0)
                return false;
            if (SceneManager.Scripts.SceneManager.CurrentScene != Scene.GameScene)
                return false;
            
            PrintBase();
            PrintStats();
            PrintAbilities();
            PrintSkills();
            return true;
        }

        private static void PrintBase()
        {
            var commander = GameManager.Instance.PlayerModel.Commander;
            
            DeveloperConsoleManager.Instance.Print("Commander Data");
            DeveloperConsoleManager.Instance.Print(string.Concat("Name: ", commander.CommanderName));
            DeveloperConsoleManager.Instance.Print(string.Concat("Level: ", commander.Level));
            DeveloperConsoleManager.Instance.Print(string.Concat("Experience: ", commander.Experience));
        }

        private static void PrintStats()
        {
            var stats = GameManager.Instance.PlayerModel.Commander.CommanderStats;
            
            DeveloperConsoleManager.Instance.Print("Stats:");
            DeveloperConsoleManager.Instance.Print(string.Concat("Faction: ", stats.Faction));
            DeveloperConsoleManager.Instance.Print(string.Concat("Class: ", stats.CommanderClass));
            DeveloperConsoleManager.Instance.Print(string.Concat("Speed: ", stats.Speed));
            DeveloperConsoleManager.Instance.Print(string.Concat("Max Health: ", stats.MAXHealth));
            DeveloperConsoleManager.Instance.Print(string.Concat("Health: ", stats.Health));
            DeveloperConsoleManager.Instance.Print(string.Concat("Max Mana: ", stats.MAXMana));
            DeveloperConsoleManager.Instance.Print(string.Concat("Mana: ", stats.Mana));
            DeveloperConsoleManager.Instance.Print(string.Concat("Attack: ", stats.AttackDamageModifier));
            DeveloperConsoleManager.Instance.Print(string.Concat("Defense: ", stats.Defense));
        }

        private static void PrintAbilities()
        {
            var abilities = GameManager.Instance.PlayerModel.Commander.Abilities;
            
            DeveloperConsoleManager.Instance.Print("Abilities:");
            for (var i = 0; i < abilities.Count; i++)
            {
                DeveloperConsoleManager.Instance.Print($"Ability{i}: {abilities[i].AbilityName}");
            }
        }
        
        private static void PrintSkills()
        {
            var skillTree = GameManager.Instance.PlayerModel.Commander.SkillTree;
            
            DeveloperConsoleManager.Instance.Print("SkillTree:");
            for (var i = 0; i < skillTree.Nodes.Count; i++)
            {
                DeveloperConsoleManager.Instance.Print($"Skill{i}: {skillTree.Nodes[i].NodeObj.NodeName}");
            }
        }
    }
}
