using System;
using Abilities.Ability.Scripts;
using Abilities.EndAbilities.AOE_Area.Scripts;
using Abilities.EndAbilities.DashInLookDirection.Scripts;
using Abilities.EndAbilities.DecreaseCooldownOfDamageAbility.Scripts;
using Abilities.EndAbilities.IncreaseDamageForSetTime.Scripts;
using Abilities.EndAbilities.MeleeAttack.Scripts;
using Abilities.EndAbilities.SingleShot.Scripts;
using Abilities.EndAbilities.SingleShotSetRange.Scripts;
using GamePlay.GameManager.Scripts;
using UnityEngine;
using Utility.SceneManager.Scripts;

namespace Utility.DeveloperConsole.Scripts.Commands.CommanderCommands.GetCommands
{
    [CreateAssetMenu(fileName = "New Get Commander Ability Data", menuName = "Utilities/DeveloperConsole/Commands/Comander/Get Commands/Get Commander Ability Data")]
    public class GetCommanderAbilityData : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            if (args.Length != 1)
                return false;
            if (!int.TryParse(args[0], out var value))
                return false;
            return SceneManager.Scripts.SceneManager.CurrentScene == Scene.GameScene && PrintAbilities(value);
        }
        
        private static bool PrintAbilities(int value)
        {
            var abilities = GameManager.Instance.PlayerModel.Commander.Abilities;
            if (abilities.Count <= value) 
                return false;

            var abilityType = abilities[value].GetType();
            var abilityTypeStrings = abilityType.ToString().Split('.');
            DeveloperConsoleManager.Instance.Print(string.IsNullOrEmpty(abilities[value].AbilityName)
                ? $"Auto Attack: {abilityTypeStrings[abilityTypeStrings.Length - 1]}"
                : $"{abilities[value].AbilityName}: {abilityTypeStrings[abilityTypeStrings.Length - 1]}");
            DeveloperConsoleManager.Instance.Print(string.Concat("Cooldown: ", abilities[value].CoolDown));
            DeveloperConsoleManager.Instance.Print(string.Concat("TimeLeft: ", abilities[value].TimeLeft));

            PrintAbilitySpecifics(abilityType, abilities[value]);
            
            if (abilities[value].AllowedAttackingTypes == null) 
                return true;
            DeveloperConsoleManager.Instance.Print("Allowed Attacking Types:");
            foreach (var type in abilities[value].AllowedAttackingTypes)
            {
                DeveloperConsoleManager.Instance.Print(type.ToString());
            }
            return true;
        }

        private static void PrintAbilitySpecifics(Type type, Ability newAbility)
        {
            if (type.IsSubclassOf(typeof(DamageAbility)))
            {
                DeveloperConsoleManager.Instance.Print(string.Concat("Damage: ", ((DamageAbility) newAbility).Damage));
                DeveloperConsoleManager.Instance.Print(string.Concat("Attack Range: ", ((DamageAbility) newAbility).AttackRange));
            }
            else if (type.IsSubclassOf(typeof(UtilityAbility)))
            {
                DeveloperConsoleManager.Instance.Print(string.Concat("Duration: ", ((UtilityAbility) newAbility).Duration));
                DeveloperConsoleManager.Instance.Print(string.Concat("Effect Range: ", ((UtilityAbility) newAbility).EffectRange));
            }
            
            if (type.IsSubclassOf(typeof(AoeAreaAbility)))
            {
                var ability = ((AoeAreaAbility) newAbility);
                DeveloperConsoleManager.Instance.Print(string.Concat("AOE Range: ", ability.AoeRange));
                DeveloperConsoleManager.Instance.Print(string.Concat("Duration: ", ability.Duration));
            }
            if (type.IsSubclassOf(typeof(DashInLookDirectionAbility)))
            {
                var ability = ((DashInLookDirectionAbility) newAbility);
                DeveloperConsoleManager.Instance.Print(string.Concat("Max Charges: ", ability.MaxCharges));
                DeveloperConsoleManager.Instance.Print(string.Concat("Charges: ", ability.Charges));
            }
            if (type.IsSubclassOf(typeof(DecreaseCooldownOfDamageAbility)))
            {
                var ability = ((DecreaseCooldownOfDamageAbility) newAbility);
                DeveloperConsoleManager.Instance.Print(string.Concat("Decrease Value: ", ability.DecreaseCooldownValueInPercentage));
                DeveloperConsoleManager.Instance.Print(string.Concat("Max Targets: ", ability.MaxTargets));
            }
            if (type.IsSubclassOf(typeof(IncreaseDamageForSetTimeAbility)))
            {
                var ability = ((IncreaseDamageForSetTimeAbility) newAbility);
                DeveloperConsoleManager.Instance.Print(string.Concat("Increase Value: ", ability.IncreaseAttackValueInPercentage));
                DeveloperConsoleManager.Instance.Print(string.Concat("Current Target: ", ability.CurrenTarget.name));
            }
            if (type.IsSubclassOf(typeof(MeleeAttackAbility)))
            {
                var ability = ((MeleeAttackAbility) newAbility);
                DeveloperConsoleManager.Instance.Print(string.Concat("Stun Time: ", ability.StunTime));
            }
            if (type.IsSubclassOf(typeof(SingleShotAbility)))
            {
                var ability = ((SingleShotAbility) newAbility);
                DeveloperConsoleManager.Instance.Print(string.Concat("Projectile Speed: ", ability.ProjectileSpeed));
            }

            if (!type.IsSubclassOf(typeof(SingleShotSetRangeAbility))) return;
            {
                var ability = ((SingleShotSetRangeAbility) newAbility);
                DeveloperConsoleManager.Instance.Print(string.Concat("Travel Range: ", ability.TravelRange));
            }
        }
    }
}