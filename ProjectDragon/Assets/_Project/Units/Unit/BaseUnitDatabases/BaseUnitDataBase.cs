using _Project.GamePlay.Player.AnimationHandler.Scripts;
using _Project.GamePlay.Player.SoundHandler.Scripts;
using _Project.Scripts.Gameplay.Faction;
using _Project.SkillSystem.SkillTree;
using UnityEngine;

namespace _Project.Units.Unit.BaseUnitDatabases
{
    //[CreateAssetMenu(menuName="Tools/Units/BaseUnitDataBase")]
    public abstract class BaseUnitDataBase : ScriptableObject
    {
        [SerializeField] protected string unitName;
        [SerializeField][TextArea] protected string description;
        [SerializeField] protected GameObject unitModel;
        [SerializeField] protected Factions.Faction faction;
        [SerializeField] protected Factions.Class unitClass;
        [SerializeField] protected byte rank;
        [SerializeField] protected int cost;
        [SerializeField] public SkillTree skillTree; 
        [SerializeField] protected GameObject abilityGameObjectGameObject;
        [SerializeField] protected AnimationHandler animationHandler;
        [SerializeField] protected SoundHandler soundHandler;

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

        public Factions.Faction Faction
        {
            get => faction;
            set => faction = value;
        }

        public Factions.Class UnitClass
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

        public GameObject AbilityGameObject
        {
            get => abilityGameObjectGameObject;
            set => abilityGameObjectGameObject = value;
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
    }
}
