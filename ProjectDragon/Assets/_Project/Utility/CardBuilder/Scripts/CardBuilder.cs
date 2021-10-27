using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Deck_Cards.Cards.CommanderCard.Scripts;
using _Project.Deck_Cards.Cards.UnitCard.Scripts;
using _Project.Faction;
using _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using _Project.SkillSystem.SkillTree;
using _Project.Units.Unit.BaseUnits;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Utility.CardBuilder.Scripts
{
    public class CardBuilder : EditorWindow
    {
        private static CardBuilder Instance;

        [Header("General")] 
        private int toolBarIndex;

        [Header("Base Stats")] 
        private int id;
        private int cost;
        private string cName;
        private Rarity rarity;
        private Sprite icon;
        private VideoClip demo;

        [Header("Unit Stats")] 
        private Unit unit;

        [Header("Commander Stats")] 
        private int selectedCommander;
        private GameObject commanderObj;
        private ClassAndFaction.Faction faction;
        private ClassAndFaction.Class commanderClass;
        private float health;
        private float mana;
        private float attackDamageModifier;
        private float defense;
        private float speed;
        private SkillTree skillTree;
        private CommanderAbilityDataBase commanderAbilityDataBase;

        [MenuItem("Window/Card Builder")]
        public static void Init()
        {
            if (Instance != null)
                return;

            Instance = GetWindow<CardBuilder>("Card Builder");
            Instance.Show();
        }

        private void OnGUI()
        {
            DrawToolBar();

            toolBarIndex = GUILayout.Toolbar(toolBarIndex, new[] {new GUIContent("Commander"), new GUIContent("Unit")});
            EditorGUILayout.Space(1);

            switch (toolBarIndex)
            {
                case 0:
                    DrawCommanderStats();
                    break;
                case 1:
                    DrawUnitStats();
                    break;
            }

            DrawSave();
        }

        #region Draw

        private void DrawToolBar()
        {
            EditorGUILayout.BeginHorizontal("Toolbar");
            if (GUILayout.Button(new GUIContent("Create"), EditorStyles.toolbarButton, GUILayout.Width(60)))
            {
                CreateCard();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(1);
        }

        private void DrawSave()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(new GUIContent("Save"), GUILayout.Width(60)))
            {
                Save();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawBaseStats()
        {
            //CardId
            id = EditorGUILayout.IntField("ID", id);
            //CardCost
            cost = EditorGUILayout.IntField("Cost", cost);
            //CardName
            cName = EditorGUILayout.TextField("Card Name:", cName);
            //CardRarity
            var rarities = Enum.GetValues(typeof(Rarity)).Cast<Rarity>().Select(v => v.ToString()).ToArray();
            rarity = (Rarity) EditorGUILayout.Popup("Rarity", (int) rarity, rarities);
            //CardIcon
            //CardDemo
        }

        private void DrawUnitStats()
        {
            // Unit Stats
            GUILayout.Label("Unit Stats", EditorStyles.boldLabel);
        }

        private void DrawCommanderStats()
        {
            // Commander Stats
            var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);

            selectedCommander =
                EditorGUILayout.Popup("Selected Card", selectedCommander, commanderCards.Select(c => c.name).ToArray());
            SetStats(commanderCards[selectedCommander]);
            EditorGUILayout.Space(5);

            //Drawing
            DrawBaseStats();

            //CommanderObj
            //CommanderFaction
            var factions = Enum.GetValues(typeof(ClassAndFaction.Faction)).Cast<ClassAndFaction.Faction>().Select(v => v.ToString())
                .ToArray();
            faction = (ClassAndFaction.Faction) EditorGUILayout.Popup("Faction", (int) faction, factions);
            //CommanderClass
            var classes = Enum.GetValues(typeof(ClassAndFaction.Class)).Cast<ClassAndFaction.Class>().Select(v => v.ToString())
                .ToArray();
            commanderClass = (ClassAndFaction.Class) EditorGUILayout.Popup("Class", (int) commanderClass, classes);
            //CommanderHealth
            health = EditorGUILayout.FloatField("Health", health);
            //CommanderMana
            mana = EditorGUILayout.FloatField("Mana", mana);
            //CommanderAttackDamageModifier
            attackDamageModifier = EditorGUILayout.FloatField("Attack", attackDamageModifier);
            //CommanderDefense
            defense = EditorGUILayout.FloatField("Defense", defense);
            //CommanderSpeed
            speed = EditorGUILayout.FloatField("Speed", speed);
            //CommanderSkillTree
            //CommanderAbilities
        }

        #endregion

        #region Card Related

        private void SetStats(BaseCards baseCard)
        {
            switch (toolBarIndex)
            {
                case 0:
                    var commanderCard = (CommanderCard) baseCard;
                    if (commanderCard.Commander != null)
                    {
                        id = commanderCard.CardID;
                        cost = commanderCard.Cost;
                        cName = commanderCard.Commander.commanderName;
                        rarity = commanderCard.Rarity;
                        icon = commanderCard.Icon;
                        demo = commanderCard.Demo;
                        commanderObj = commanderCard.Commander.commanderObj;
                        faction = commanderCard.Commander.faction;
                        commanderClass = commanderCard.Commander.commanderClass;
                        health = commanderCard.Commander.health;
                        mana = commanderCard.Commander.mana;
                        attackDamageModifier = commanderCard.Commander.attackDamageModifier;
                        defense = commanderCard.Commander.defense;
                        speed = commanderCard.Commander.speed;
                        skillTree = commanderCard.Commander.skillTree;
                        commanderAbilityDataBase = commanderCard.Commander.commanderAbilityDataBase;
                    }
                    else
                    {
                        cName = string.Empty;
                        faction = 0;
                        commanderClass = 0;
                        health = 0;
                        mana = 0;
                        attackDamageModifier = 0;
                        defense = 0;
                        speed = 0;
                    }

                    break;
                case 1:
                    var unitCard = (UnitCard) baseCard;
                    break;
            }
        }

        private void CreateCard()
        {
        }

        private void Save()
        {
            switch (toolBarIndex)
            {
                case 0:
                    // Commander Stats
                    var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);

                    commanderCards[selectedCommander].Save(id, cost, rarity, icon, demo, commanderObj, faction,
                        commanderClass, health, mana, attackDamageModifier, defense, speed, skillTree,
                        commanderAbilityDataBase);
                    break;
                case 1:
                    break;
            }
        }

        #endregion
    }
}