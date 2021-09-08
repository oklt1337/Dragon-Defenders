using _Project.Scripts.Animation;
using _Project.Scripts.Gameplay.Faction;
using _Project.Scripts.Gameplay.Skillsystem;
using _Project.Scripts.Gameplay.Skillsystem.Ability.BaseAbilities;
using _Project.Scripts.Gameplay.Unit.UnitDatabases;
using _Project.Scripts.Sound;
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
        public byte level;
        public float experience;
        public int cost;
        public float cooldown;
        public SkillTree skillTree;
        public Ability ability;
        public AnimationHandler animationHandler;
        public SoundHandler soundHandler;

        
        private void Dismantle()
        {
            
        }

        private void Cast()
        {
            
        }
    }
}
