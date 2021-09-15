using _Project.Scripts.Animation;
using _Project.Scripts.Gameplay.Faction;
using _Project.Scripts.Gameplay.Skillsystem;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using _Project.Scripts.Gameplay.Unit.UnitDatabases;
using _Project.Scripts.Sound;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Unit
{
    public abstract class Unit : MonoBehaviour
    {   
        public BaseUnitDataBase baseUnitDataBase;
        public string unitName; 
        public GameObject unitModel;
        public Factions.Faction faction;
        public Factions.Class unitClass;
        public byte rank;
        public int cost;
        
        public float cooldown;
        public SkillTree skillTree;
        public Ability ability;
        public AnimationHandler animationHandler;
        public SoundHandler soundHandler;

        public byte level;
        public float experience;
        
        protected virtual void LoadDataFromScriptableObject()
        {
            unitName = baseUnitDataBase.name;
            faction = baseUnitDataBase.faction;
            unitClass = baseUnitDataBase.unitClass;
            rank = baseUnitDataBase.rank;
            cost = baseUnitDataBase.cost;
        }

        protected virtual void SetSkilltree()
        {
            skillTree = GetComponentInChildren<SkillTree>();
            //ability = skillTree.allPossibleSkillTree[0];
            ability = Instantiate(skillTree.allPossibleSkillTree[0]);
            cooldown = ability.cooldown;
        }
        
        private void Dismantle()
        {
            //manageracces return cost;
            Destroy(this);
        }

        public virtual void Start()
        {
            LoadDataFromScriptableObject();
            SetSkilltree();
        }

        private void Cast()
        {
            ability.Cast();
        }
    }
}
