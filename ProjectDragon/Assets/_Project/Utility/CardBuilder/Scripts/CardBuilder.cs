using System;
using System.Linq;
using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Deck_Cards.Cards.CommanderCard.Scripts;
using _Project.Deck_Cards.Cards.UnitCard.Scripts;
using _Project.Faction;
using _Project.GamePlay.Player.Commander.CommanderModel.Scripts;
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
        private static int ToolBarIndex;
        private Vector2 scrollPos;
        private CreateCardWindow newCardWindow;
        private const string CommanderPath = "Assets/Resources/Cards/CommanderCards";

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
            Instance = GetWindow<CardBuilder>("Card Builder");
            Instance.Show();
        }

        private void OnGUI()
        {
            DrawToolBar();

            ToolBarIndex = GUILayout.Toolbar(ToolBarIndex, new[] {new GUIContent("Commander"), new GUIContent("Unit")});
            EditorGUILayout.Space(1);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            switch (ToolBarIndex)
            {
                case 0:
                    DrawCommanderStats();
                    break;
                case 1:
                    DrawUnitStats();
                    break;
            }

            DrawSave();
            EditorGUILayout.EndScrollView();
        }

        #region Draw

        private void DrawToolBar()
        {
            EditorGUILayout.BeginHorizontal("Toolbar");
            if (GUILayout.Button(new GUIContent("+", "Create a new Card."), EditorStyles.toolbarButton,
                GUILayout.Width(20)))
            {
                newCardWindow = CreateCardWindow.Init(position);
                newCardWindow.OnCreate += CreateCard;
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
            EditorGUILayout.LabelField("Card Stats");

            //CardId
            id = EditorGUILayout.IntField("ID", id);
            //CardCost
            cost = EditorGUILayout.IntField("Cost", cost);
            //CardRarity
            var rarities = Enum.GetValues(typeof(Rarity)).Cast<Rarity>().Select(v => v.ToString()).ToArray();
            rarity = (Rarity) EditorGUILayout.Popup("Rarity", (int) rarity, rarities);
            //CardIcon
            icon = (Sprite) EditorGUILayout.ObjectField("Icon", icon, typeof(Sprite), false);
            //CardDemo
            demo = (VideoClip) EditorGUILayout.ObjectField("Demo", demo, typeof(VideoClip), false);
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

            if (commanderCards.Length > 0)
            {
                EditorGUI.BeginChangeCheck();
                selectedCommander =
                    EditorGUILayout.Popup("Selected Card", selectedCommander,
                        commanderCards.Select(c => c.name).ToArray());
                if (EditorGUI.EndChangeCheck())
                {
                    SetStats(commanderCards[selectedCommander]);
                }

                EditorGUILayout.Space(5);

                //Drawing
                DrawBaseStats();

                GUILayout.Space(15);
                EditorGUILayout.LabelField("Commander Stats");
                //CommanderName
                cName = EditorGUILayout.TextField("Name", cName);
                //CommanderObj
                commanderObj =
                    (GameObject) EditorGUILayout.ObjectField("GameObject", commanderObj, typeof(GameObject), false);
                //CommanderFaction
                var factions = Enum.GetValues(typeof(ClassAndFaction.Faction)).Cast<ClassAndFaction.Faction>()
                    .Select(v => v.ToString())
                    .ToArray();
                faction = (ClassAndFaction.Faction) EditorGUILayout.Popup("Faction", (int) faction, factions);
                //CommanderClass
                var classes = Enum.GetValues(typeof(ClassAndFaction.Class)).Cast<ClassAndFaction.Class>()
                    .Select(v => v.ToString())
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
                skillTree = (SkillTree) EditorGUILayout.ObjectField("SkillTree", skillTree, typeof(SkillTree), false);
                //CommanderAbilities
                commanderAbilityDataBase = (CommanderAbilityDataBase) EditorGUILayout.ObjectField("Abilities",
                    commanderAbilityDataBase, typeof(CommanderAbilityDataBase), false);
            }
            else
            {
                selectedCommander = EditorGUILayout.Popup("Selected Card", selectedCommander, new[] {"none"});
                selectedCommander = 0;
            }
        }

        #endregion

        #region Card Related

        private void SetStats(BaseCards baseCard)
        {
            switch (ToolBarIndex)
            {
                case 0:
                    if (baseCard != null)
                    {
                        var commanderCard = (CommanderCard) baseCard;
                        //BaseStats
                        id = commanderCard.CardID;
                        cost = commanderCard.Cost;
                        rarity = commanderCard.Rarity;
                        icon = commanderCard.Icon;
                        demo = commanderCard.Demo;

                        //Commander Stats
                        cName = commanderCard.Commander.commanderName;
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

        private void CreateCard(string cardName)
        {
            newCardWindow.OnCreate -= CreateCard;

            // Create new Card Obj
            string path;
            switch (ToolBarIndex)
            {
                case 0:
                    // Create Commander
                    var guid = AssetDatabase.CreateFolder(CommanderPath, cardName);
                    path = string.Concat(AssetDatabase.GUIDToAssetPath(guid), "/", cardName);

                    var card = CreateInstance<CommanderCard>();
                    card.Commander = CreateInstance<CommanderModel>();
                    card.Commander.skillTree = CreateInstance<SkillTree>();
                    card.Commander.commanderAbilityDataBase = CreateInstance<CommanderAbilityDataBase>();
                    card.Commander.commanderName = cardName;

                    AssetDatabase.CreateAsset(card, string.Concat(path, "-Card", ".asset"));
                    AssetDatabase.CreateAsset(card.Commander, string.Concat(path, "-Commander", ".asset"));
                    AssetDatabase.CreateAsset(card.Commander.skillTree,
                        string.Concat(path, "-CommanderSkillTree", ".asset"));
                    AssetDatabase.CreateAsset(card.Commander.commanderAbilityDataBase,
                        string.Concat(path, "-CommanderAbilityDataBase", ".asset"));

                    AssetDatabase.SaveAssets();

                    // Set new Card as selected
                    var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);
                    var index = commanderCards.ToList().FindIndex(c => c.Commander.commanderName == cardName);

                    if (index != -1)
                    {
                        selectedCommander = index;
                        SetStats(commanderCards[index]);
                    }

                    break;
                case 1:
                    // Create Unit

                    break;
            }
        }


        private void Save()
        {
            switch (ToolBarIndex)
            {
                case 0:
                    // Commander Stats
                    var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);
                    if (commanderCards.Length == 0)
                        return;

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