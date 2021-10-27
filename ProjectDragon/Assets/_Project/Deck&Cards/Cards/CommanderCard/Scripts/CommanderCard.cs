using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Faction;
using _Project.GamePlay.Player.Commander.CommanderModel.Scripts;
using _Project.SkillSystem.SkillTree;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Deck_Cards.Cards.CommanderCard.Scripts
{
    [CreateAssetMenu(menuName = "Tool/Cards/CommanderCard", fileName = "CommanderCard")]
    public class CommanderCard : BaseCards.Scripts.BaseCards
    {
        [SerializeField] private CommanderModel commander;
        public CommanderModel Commander => commander;

        public override void Save(int id, int cost, Rarity rarity, Sprite sprite, VideoClip clip)
        {
            base.Save(id, cost, rarity, sprite, clip);
        }

        public void Save(int id, int cost, Rarity rarity, Sprite sprite, VideoClip clip, GameObject obj,
            Factions.Faction faction, Factions.Class @class, float health, float mana,
            float atk, float defense, float speed, SkillTree skillTree, CommanderAbilityDataBase abilities)
        {
            Save(id, cost, rarity, sprite, clip);

            Commander.commanderObj = obj;
            Commander.faction = faction;
            Commander.commanderClass = @class;
            Commander.health = health;
            Commander.mana = mana;
            Commander.attackDamageModifier = atk;
            Commander.defense = defense;
            Commander.speed = speed;
            Commander.skillTree = skillTree;
            Commander.commanderAbilityDataBase = abilities;
        }
    }
}