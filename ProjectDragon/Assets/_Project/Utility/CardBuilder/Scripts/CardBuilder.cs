using System;
using System.Linq;
using Abilities.AbilityDataBase.Scripts;
using Deck_Cards.Cards.BaseCards.Scripts;
using Deck_Cards.Cards.CommanderCard.Scripts;
using Deck_Cards.Cards.UnitCard.Scripts;
using Faction;
using SkillSystem.SkillTree.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace Utility.CardBuilder.Scripts
{
    public class CardBuilder : EditorWindow
    {
        private static CardBuilder Instance;

        [Header("General")] 
        private int toolBarIndex;
        private Vector2 scrollPos;
        private CreateCardWindow newCardWindow;
        private DeleteCardWindow deleteCardWindow;

        [Header("Base Stats")] 
        private int cardID;
        private string cardName;
        private string description;
        private GameObject model;
        private Rarity rarity;
        private ClassAndFaction.Faction faction;
        private ClassAndFaction.Class @class;
        private SkillTreeObj skillTreeObj;
        private AbilityDataBase abilityDataBase;
        private VideoClip demo;
        private Sprite icon;

        [Header("Commander Stats")] 
        private const string CommanderPath = "Assets/Resources/Cards/CommanderCards";
        private int selectedCommander;
        private float health;
        private float mana;
        private float commanderAttackDamageModifier;
        private float defense;
        private float speed;

        [Header("Unit Stats")] 
        private const string UnitPath = "Assets/Resources/Cards/UnitCards";
        private int selectedUnit;
        private int limit;
        private int goldCost;

        [MenuItem("Window/Card Builder")]
        public static void Init()
        {
            Instance = GetWindow<CardBuilder>("Card Builder");
            Instance.Show();
        }

        private void OnValidate()
        {
            Reload();
        }

        private void OnGUI()
        {
            DrawToolBar();

            EditorGUI.BeginChangeCheck();
            toolBarIndex = GUILayout.Toolbar(toolBarIndex, new[] {new GUIContent("Commander"), new GUIContent("Unit")});
            if (EditorGUI.EndChangeCheck())
            {
                if (toolBarIndex == 0)
                {
                    var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);

                    if (commanderCards.Length > 0)
                    {
                        SetStats(commanderCards[selectedCommander]);
                    }
                }
                else
                {
                    // Commander Stats
                    var unitCards = Resources.LoadAll<UnitCard>(string.Empty);
                    if (unitCards.Length > 0)
                    {
                        SetStats(unitCards[selectedUnit]);
                    }
                }
            }

            EditorGUILayout.Space(1);

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);


            switch (toolBarIndex)
            {
                case 0:
                    DrawCommanderStats();
                    break;
                case 1:
                    DrawUnitStats();
                    break;
            }
            DrawButtons();
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
            if (GUILayout.Button(new GUIContent("Save"), GUILayout.Width(60)))
            {
                Save();
            }
        }

        private void DrawDelete()
        {
            if (GUILayout.Button(new GUIContent("Delete"), GUILayout.Width(60)))
            {
                DeleteDisplay();
            }
        }

        private void DrawButtons()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            DrawDelete();
            DrawSave();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
        }

        private void DrawBaseStats()
        {
            EditorGUILayout.LabelField("Card Stats");

            //CardId
            cardID = EditorGUILayout.IntField("ID", cardID);
            //CardName
            cardName = EditorGUILayout.TextField("Card Name", cardName);
            //CardDescription
            description = EditorGUILayout.TextField("Description", description);
            //CardModel
            model =
                (GameObject) EditorGUILayout.ObjectField("GameObject", model, typeof(GameObject), false);
            //CardRarity
            var rarities = Enum.GetValues(typeof(Rarity)).Cast<Rarity>().Select(v => v.ToString()).ToArray();
            rarity = (Rarity) EditorGUILayout.Popup("Rarity", (int) rarity, rarities);
            //CardFaction
            var factions = Enum.GetValues(typeof(ClassAndFaction.Faction)).Cast<ClassAndFaction.Faction>()
                .Select(v => v.ToString())
                .ToArray();
            faction = (ClassAndFaction.Faction) EditorGUILayout.Popup("Faction", (int) faction, factions);
            //CommanderClass
            var classes = Enum.GetValues(typeof(ClassAndFaction.Class)).Cast<ClassAndFaction.Class>()
                .Select(v => v.ToString())
                .ToArray();
            @class = (ClassAndFaction.Class) EditorGUILayout.Popup("Class", (int) @class, classes);
            //CardSkillTree
            skillTreeObj = (SkillTreeObj) EditorGUILayout.ObjectField("SkillTree", skillTreeObj, typeof(SkillTreeObj), false);
            //CardAbilities
            abilityDataBase = (AbilityDataBase) EditorGUILayout.ObjectField("Abilities",
                abilityDataBase, typeof(AbilityDataBase), false);
            //CardDemo
            demo = (VideoClip) EditorGUILayout.ObjectField("Demo", demo, typeof(VideoClip), false);
            //CardIcon
            icon = (Sprite) EditorGUILayout.ObjectField("Icon", icon, typeof(Sprite), false);
        }

        private void DrawUnitStats()
        {
            var unitCards = Resources.LoadAll<UnitCard>(string.Empty);

            if (unitCards.Length > 0)
            {
                EditorGUI.BeginChangeCheck();
                selectedUnit =
                    EditorGUILayout.Popup("Selected Card", selectedUnit,
                        unitCards.Select(c => c.name).ToArray());
                if (EditorGUI.EndChangeCheck())
                {
                    SetStats(unitCards[selectedUnit]);
                }

                EditorGUILayout.Space(5);

                //Drawing
                DrawBaseStats();
                GUILayout.Space(10);

                EditorGUILayout.LabelField("Unit Stats");
                //UnitLimit
                limit = EditorGUILayout.IntField("Limit", limit);
                //UnitGoldCost
                goldCost = EditorGUILayout.IntField("Gold Cost", goldCost);
            }
            else
            {
                selectedUnit = EditorGUILayout.Popup("Selected Card", selectedUnit, new[] {"none"});
                selectedUnit = 0;
            }
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
                GUILayout.Space(10);

                EditorGUILayout.LabelField("Commander Stats");
                //CommanderHealth
                health = EditorGUILayout.FloatField("Health", health);
                //CommanderMana
                mana = EditorGUILayout.FloatField("Mana", mana);
                //CommanderAttackDamageModifier
                commanderAttackDamageModifier = EditorGUILayout.FloatField("Attack", commanderAttackDamageModifier);
                //CommanderDefense
                defense = EditorGUILayout.FloatField("Defense", defense);
                //CommanderSpeed
                speed = EditorGUILayout.FloatField("Speed", speed);
            }
            else
            {
                selectedCommander = EditorGUILayout.Popup("Selected Card", selectedCommander, new[] {"none"});
                selectedCommander = 0;
            }
        }

        #endregion

        #region Card Related

        private void SetStats(BaseCard baseCard)
        {
            switch (toolBarIndex)
            {
                case 0:
                    if (baseCard != null)
                    {
                        var commanderCard = (CommanderCard) baseCard;
                        //BaseStats
                        SetBaseStats(commanderCard);

                        //Commander Stats
                        health = commanderCard.Health;
                        mana = commanderCard.Mana;
                        commanderAttackDamageModifier = commanderCard.AttackDamageModifier;
                        defense = commanderCard.Defense;
                        speed = commanderCard.Speed;
                    }
                    else
                    {
                        SetEmpty();
                    }

                    break;
                case 1:
                    if (baseCard != null)
                    {
                        var unitCard = (UnitCard) baseCard;
                        SetBaseStats(unitCard);
                        
                        goldCost = unitCard.GoldCost;
                        limit = unitCard.Limit;
                    }
                    else
                    {
                        SetEmpty();
                    }

                    break;
            }
        }

        private void SetBaseStats(BaseCard baseCard)
        {
            cardID = baseCard.CardID;
            cardName = baseCard.CardName;
            description = baseCard.Description;
            model = baseCard.Model;
            rarity = baseCard.Rarity;
            faction = baseCard.Faction;
            @class = baseCard.Class;
            skillTreeObj = baseCard.SkillTreeObj;
            abilityDataBase = baseCard.AbilityDataBase;
            demo = baseCard.Demo;
            icon = baseCard.Icon;
        }

        private void SetEmpty()
        {
            cardID = 0;
            cardName = string.Empty;
            description = string.Empty;
            model = null;
            rarity = 0;
            faction = 0;
            @class = 0;
            skillTreeObj = null;
            abilityDataBase = null;
            demo = null;
            icon = null;

            health = 0;
            mana = 0;
            commanderAttackDamageModifier = 0;
            defense = 0;
            speed = 0;
            
            goldCost = 0;
        }

        private void CreateCard(string newCardName)
        {
            newCardWindow.OnCreate -= CreateCard;

            // Create new Card Obj
            string path;
            string guid;
            int index;
            switch (toolBarIndex)
            {
                case 0:
                    // Create Commander
                    guid = AssetDatabase.CreateFolder(CommanderPath, newCardName);
                    path = string.Concat(AssetDatabase.GUIDToAssetPath(guid), "/", newCardName);

                    //Create Instance
                    var commanderCard = CreateInstance<CommanderCard>();
                    commanderCard.SkillTreeObj = CreateInstance<SkillTreeObj>();
                    commanderCard.AbilityDataBase = CreateInstance<AbilityDataBase>();
                    commanderCard.CardName = newCardName;

                    //Create Assets
                    AssetDatabase.CreateAsset(commanderCard, string.Concat(path, "-Card", ".asset"));
                    AssetDatabase.CreateAsset(commanderCard.SkillTreeObj,
                        string.Concat(path, "-CommanderSkillTree", ".asset"));
                    AssetDatabase.CreateAsset(commanderCard.AbilityDataBase,
                        string.Concat(path, "-CommanderAbilityDataBase", ".asset"));
                    AssetDatabase.SaveAssets();

                    // Set new Card as selected
                    var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);
                    index = commanderCards.ToList().FindIndex(c => c.CardName == newCardName);

                    if (index != -1)
                    {
                        selectedCommander = index;
                        SetStats(commanderCards[index]);
                    }

                    break;
                case 1:
                    // Create Unit
                    guid = AssetDatabase.CreateFolder(UnitPath, newCardName);
                    path = string.Concat(AssetDatabase.GUIDToAssetPath(guid), "/", newCardName);
                    
                    var unitCard = CreateInstance<UnitCard>();
                    unitCard.SkillTreeObj = CreateInstance<SkillTreeObj>();
                    unitCard.AbilityDataBase = CreateInstance<AbilityDataBase>();
                    unitCard.CardName = newCardName;

                    //Create Assets
                    AssetDatabase.CreateAsset(unitCard, string.Concat(path, "-Card", ".asset"));
                    AssetDatabase.CreateAsset(unitCard.SkillTreeObj,string.Concat(path, "-SkillTree", ".asset"));
                    AssetDatabase.CreateAsset(unitCard.AbilityDataBase, string.Concat(path, "-AbilityDataBase", ".asset"));
                    AssetDatabase.SaveAssets();

                    // Set new Card as selected
                    var unitCards = Resources.LoadAll<UnitCard>(string.Empty);
                    index = unitCards.ToList().FindIndex(c => c.CardName == newCardName);

                    if (index != -1)
                    {
                        selectedUnit = index;
                        SetStats(unitCards[index]);
                    }
                    break;
            }
        }

        private void Save()
        {
            switch (toolBarIndex)
            {
                case 0:
                    // Commander Stats
                    var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);
                    if (commanderCards.Length == 0)
                        return;

                    //save
                    commanderCards[selectedCommander].Save(cardID, cardName, description, model, rarity, faction,
                        @class, skillTreeObj, abilityDataBase,demo, icon, health, mana, commanderAttackDamageModifier, defense, speed);
                    break;
                case 1:
                    var unitCards = Resources.LoadAll<UnitCard>(string.Empty);
                    if (unitCards.Length == 0)
                        return;
                    
                    unitCards[selectedUnit].Save(cardID, cardName, description, model, rarity, faction,
                        @class, skillTreeObj, abilityDataBase, demo, icon, goldCost, limit);
                    break;
            }
            GUI.FocusControl(null);
        }

        private void DeleteDisplay()
        {
            switch (toolBarIndex)
            {
                case 0:
                    // Commander Stats
                    var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);
                    if (commanderCards.Length == 0)
                        return;

                    deleteCardWindow = DeleteCardWindow.Init(position);
                    deleteCardWindow.OnDelete += Delete;
                    break;
                case 1:
                    var unitCards = Resources.LoadAll<UnitCard>(string.Empty);
                    if (unitCards.Length == 0)
                        return;
                    
                    deleteCardWindow = DeleteCardWindow.Init(position);
                    deleteCardWindow.OnDelete += Delete;
                    break;
            }
            GUI.FocusControl(null);
        }

        private void Delete()
        {
            string path;
            bool success;
            switch (toolBarIndex)
            {
                case 0:
                    // Commander Stats
                    var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);
                    if (commanderCards.Length == 0)
                        return;

                    //delete
                    path = string.Concat(CommanderPath, "/", commanderCards[selectedCommander].cardName);
                    success = AssetDatabase.DeleteAsset(path);
                    if (success)
                    {
                        Reload();
                    }
                    break;
                case 1:
                    var unitCards = Resources.LoadAll<UnitCard>(string.Empty);
                    if (unitCards.Length == 0)
                        return;

                    //delete
                    path = string.Concat(UnitPath, "/", unitCards[selectedUnit].cardName);
                    success = AssetDatabase.DeleteAsset(path);
                    if (success)
                    {
                        Reload();
                    }
                    break;
            }
            GUI.FocusControl(null);
        }

        #endregion

        private void Reload()
        {
            if (toolBarIndex == 0)
            {
                var commanderCards = Resources.LoadAll<CommanderCard>(string.Empty);
                if (commanderCards.Length > 0)
                {
                    SetStats(commanderCards[selectedCommander]);
                }
            }
            else
            {
                // Commander Stats
                var unitCards = Resources.LoadAll<UnitCard>(string.Empty);
                if (unitCards.Length > 0)
                {
                    SetStats(unitCards[selectedUnit]);
                }
            }
        }
    }
}