using System;
using System.Linq;
using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using _Project.Deck_Cards.Cards.BaseCards.Scripts;
using _Project.Deck_Cards.Cards.CommanderCard.Scripts;
using _Project.Deck_Cards.Cards.UnitCard.Scripts;
using _Project.Faction;
using _Project.SkillSystem.SkillTree;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace _Project.Utility.CardBuilder.Scripts
{
    public class CardBuilder : EditorWindow
    {
        private static CardBuilder Instance;

        [Header("General")] 
        private Vector2 scrollPos;
        private CreateCardWindow newCardWindow;

        [Header("Base Stats")] 
        private int cardID;
        private string cardName;
        private string description;
        private int cost;
        private GameObject model;
        private Rarity rarity;
        private ClassAndFaction.Faction faction;
        private ClassAndFaction.Class @class;
        private SkillTree skillTree;
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
        private CommanderAbilityDataBase commanderAbilityDataBase;

        [Header("Unit Stats")] 
        private const string UnitPath = "Assets/Resources/Cards/UnitCards";
        private int selectedUnit;
        private UnitType unitType;
        private int goldCost;
        private UnitAbilityDataBase unitAbilityDatabase;

        //Combat
        private float unitAttackDamageModifier;
        private float attackRange;

        //Utility
        private float effectRange;

        public static int ToolBarIndex { get; private set; }

        private static event Action<string> OnRecompile;

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
            ToolBarIndex = GUILayout.Toolbar(ToolBarIndex, new[] {new GUIContent("Commander"), new GUIContent("Unit")});
            if (EditorGUI.EndChangeCheck())
            {
                if (ToolBarIndex == 0)
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
            cardID = EditorGUILayout.IntField("ID", cardID);
            //CardName
            cardName = EditorGUILayout.TextField("Card Name", cardName);
            //CardDescription
            description = EditorGUILayout.TextField("Description", description);
            //CardCost
            cost = EditorGUILayout.IntField("Cost", cost);

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
            skillTree = (SkillTree) EditorGUILayout.ObjectField("SkillTree", skillTree, typeof(SkillTree), false);

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
                //UnitType
                var types = Enum.GetValues(typeof(UnitType)).Cast<UnitType>().Select(v => v.ToString()).ToArray();
                unitType = (UnitType) EditorGUILayout.Popup("Unit Type", (int) unitType, types);
                //UnitGoldCost
                goldCost = EditorGUILayout.IntField("Gold Cost", goldCost);
                //UnitAbilities
                unitAbilityDatabase = (UnitAbilityDataBase) EditorGUILayout.ObjectField("Abilities",
                    unitAbilityDatabase, typeof(UnitAbilityDataBase), false);

                EditorGUILayout.Space(5);
                switch (unitType)
                {
                    case UnitType.Combat:
                        unitAttackDamageModifier = EditorGUILayout.FloatField("Attack", unitAttackDamageModifier);
                        attackRange = EditorGUILayout.FloatField("Attack Range", attackRange);
                        break;
                    case UnitType.Utility:
                        effectRange = EditorGUILayout.FloatField("Effect Range", effectRange);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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

        private void SetStats(BaseCard baseCard)
        {
            switch (ToolBarIndex)
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
                        commanderAbilityDataBase = commanderCard.CommanderAbilityDataBase;
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

                        //UnitStats
                        unitType = unitCard.UnitType;
                        goldCost = unitCard.GoldCost;
                        unitAbilityDatabase = unitCard.UnitAbilityDataBase;

                        switch (unitType)
                        {
                            case UnitType.Combat:
                                var combatUnitCard = (CombatUnitCard) unitCard;

                                unitAttackDamageModifier = combatUnitCard.AttackDamageModifier;
                                attackRange = combatUnitCard.AttackRange;
                                break;
                            case UnitType.Utility:
                                var utilityUnitCard = (UtilityUnitCard) unitCard;

                                effectRange = utilityUnitCard.EffectRange;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
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
            cost = baseCard.Cost;
            model = baseCard.Model;
            rarity = baseCard.Rarity;
            faction = baseCard.Faction;
            @class = baseCard.Class;
            skillTree = baseCard.SkillTree;
            demo = baseCard.Demo;
            icon = baseCard.Icon;
        }

        private void SetEmpty()
        {
            cardID = 0;
            cardName = string.Empty;
            description = string.Empty;
            cost = 0;
            model = null;
            rarity = 0;
            faction = 0;
            @class = 0;
            skillTree = null;
            demo = null;
            icon = null;

            health = 0;
            mana = 0;
            commanderAttackDamageModifier = 0;
            defense = 0;
            speed = 0;
            commanderAbilityDataBase = null;

            unitType = 0;
            goldCost = 0;
            unitAbilityDatabase = null;

            unitAttackDamageModifier = 0;
            attackRange = 0;

            effectRange = 0;
        }

        private void CreateCard(string newCardName, bool isCombatUnit)
        {
            newCardWindow.OnCreate -= CreateCard;

            // Create new Card Obj
            string path;
            string guid;
            int index;
            switch (ToolBarIndex)
            {
                case 0:
                    // Create Commander
                    guid = AssetDatabase.CreateFolder(CommanderPath, newCardName);
                    path = string.Concat(AssetDatabase.GUIDToAssetPath(guid), "/", newCardName);

                    //Create Instance
                    var commanderCard = CreateInstance<CommanderCard>();
                    commanderCard.SkillTree = CreateInstance<SkillTree>();
                    commanderCard.CommanderAbilityDataBase = CreateInstance<CommanderAbilityDataBase>();
                    commanderCard.CardName = newCardName;

                    //Create Assets
                    AssetDatabase.CreateAsset(commanderCard, string.Concat(path, "-Card", ".asset"));
                    AssetDatabase.CreateAsset(commanderCard.SkillTree,
                        string.Concat(path, "-CommanderSkillTree", ".asset"));
                    AssetDatabase.CreateAsset(commanderCard.CommanderAbilityDataBase,
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
                    // Create Commander
                    guid = AssetDatabase.CreateFolder(UnitPath, newCardName);
                    path = string.Concat(AssetDatabase.GUIDToAssetPath(guid), "/", newCardName);

                    //Create Instance
                    // Create Unit
                    switch (isCombatUnit)
                    {
                        case true:

                            var combatUnitCard = CreateInstance<CombatUnitCard>();
                            combatUnitCard.SkillTree = CreateInstance<SkillTree>();
                            combatUnitCard.UnitAbilityDataBase = CreateInstance<UnitAbilityDataBase>();
                            combatUnitCard.CardName = newCardName;
                            combatUnitCard.UnitType = UnitType.Combat;

                            //Create Assets
                            AssetDatabase.CreateAsset(combatUnitCard, string.Concat(path, "-Card", ".asset"));
                            AssetDatabase.CreateAsset(combatUnitCard.SkillTree,
                                string.Concat(path, "-CommanderSkillTree", ".asset"));
                            AssetDatabase.CreateAsset(combatUnitCard.UnitAbilityDataBase,
                                string.Concat(path, "-CommanderAbilityDataBase", ".asset"));
                            AssetDatabase.SaveAssets();
                            break;
                        case false:

                            var utilityUnitCard = CreateInstance<UtilityUnitCard>();
                            utilityUnitCard.SkillTree = CreateInstance<SkillTree>();
                            utilityUnitCard.UnitAbilityDataBase = CreateInstance<UnitAbilityDataBase>();
                            utilityUnitCard.CardName = newCardName;
                            utilityUnitCard.UnitType = UnitType.Utility;

                            //Create Assets
                            AssetDatabase.CreateAsset(utilityUnitCard, string.Concat(path, "-Card", ".asset"));
                            AssetDatabase.CreateAsset(utilityUnitCard.SkillTree,
                                string.Concat(path, "-CommanderSkillTree", ".asset"));
                            AssetDatabase.CreateAsset(utilityUnitCard.UnitAbilityDataBase,
                                string.Concat(path, "-CommanderAbilityDataBase", ".asset"));
                            AssetDatabase.SaveAssets();
                            break;
                    }

                    // Set new Card as selected
                    var unitCards = Resources.LoadAll<UnitCard>(string.Empty);
                    index = unitCards.ToList().FindIndex(c => c.CardName == newCardName);

                    if (index != -1)
                    {
                        selectedCommander = index;
                        SetStats(unitCards[index]);
                    }

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

                    //save
                    commanderCards[selectedCommander].Save(cardID, cardName, description, cost, model, rarity, faction,
                        @class, skillTree, demo, icon, health, mana, commanderAttackDamageModifier, defense, speed,
                        commanderAbilityDataBase);
                    break;
                case 1:
                    var unitCards = Resources.LoadAll<UnitCard>(string.Empty);
                    if (unitCards.Length == 0)
                        return;

                    switch (unitCards[selectedUnit].unitType)
                    {
                        case UnitType.Combat:
                            //save
                            var combatUnitCard = (CombatUnitCard) unitCards[selectedUnit];

                            combatUnitCard.Save(cardID, cardName, description, cost, model, rarity, faction,
                                @class, skillTree, demo, icon, unitType, goldCost, unitAbilityDatabase,
                                unitAttackDamageModifier, attackRange);
                            break;
                        case UnitType.Utility:
                            //save
                            var utilityUnitCard = (UtilityUnitCard) unitCards[selectedUnit];

                            utilityUnitCard.Save(cardID, cardName, description, cost, model, rarity, faction,
                                @class, skillTree, demo, icon, unitType, goldCost, unitAbilityDatabase, effectRange);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
            }
            
            GUI.FocusControl(null);
        }

        #endregion

        private void Reload()
        {
            if (ToolBarIndex == 0)
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