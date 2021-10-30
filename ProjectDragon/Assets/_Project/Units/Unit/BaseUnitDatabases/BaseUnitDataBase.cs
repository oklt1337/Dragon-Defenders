using _Project.Abilities.Ability.CommanderAbilityDataBase.Scripts;
using _Project.Faction;
using _Project.GamePlay.Player.AnimationHandler.Scripts;
using _Project.GamePlay.Player.SoundHandler.Scripts;
using _Project.SkillSystem.SkillTree;
using UnityEngine;

namespace _Project.Units.Unit.BaseUnitDatabases
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    public abstract class BaseUnitDataBase : ScriptableObject
    {
        #region SerializeFields

        [SerializeField] protected string unitName;
        [SerializeField][TextArea] protected string description;
        [SerializeField] protected GameObject unitModel;
        [SerializeField] protected ClassAndFaction.Faction faction;
        [SerializeField] protected ClassAndFaction.Class unitClass;
        [SerializeField] protected byte rank;
        [SerializeField] protected int cost;
        [SerializeField] protected UnitAbilityDataBase unitAbilityDataBase;
        [SerializeField] public SkillTree skillTree;
        [SerializeField] protected AnimationHandler animationHandler;
        [SerializeField] protected SoundHandler soundHandler;
        
        #endregion
        
        #region Public Properties

        public UnitAbilityDataBase UnitAbilityDataBase
        {
            get => unitAbilityDataBase;
            set => unitAbilityDataBase = value;
        }
        public string UnitName
        {
            get => unitName;
            set => unitName = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public GameObject UnitModel
        {
            get => unitModel;
            set => unitModel = value;
        }

        public ClassAndFaction.Faction Faction
        {
            get => faction;
            set => faction = value;
        }

        public ClassAndFaction.Class UnitClass
        {
            get => unitClass;
            set => unitClass = value;
        }

        public byte Rank
        {
            get => rank;
            set => rank = value;
        }

        public int Cost
        {
            get => cost;
            set => cost = value;
        }

        public SkillTree SkillTree
        {
            get => skillTree;
            set => skillTree = value;
        }

        public AnimationHandler AnimationHandler
        {
            get => animationHandler;
            set => animationHandler = value;
        }

        public SoundHandler SoundHandler
        {
            get => soundHandler;
            set => soundHandler = value;
        }

        #endregion
    }
}
