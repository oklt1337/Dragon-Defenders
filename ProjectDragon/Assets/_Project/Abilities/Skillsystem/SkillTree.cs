using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Skillsystem
{
    public class SkillTree : MonoBehaviour
    {
        [SerializeField] private List<Ability.BaseAbilities.Ability> allPossibleSkillTree;

        public List<Ability.BaseAbilities.Ability> AllPossibleSkillTree
        {
            get => allPossibleSkillTree;
            set => allPossibleSkillTree = value;
        }

        public void UpgradeCommanderAbilities()
        {
        
        }
        
        public void UpgradeUnitAbilities()
        {
            //change when skilltree is decided how to
            //allPossibleSkillTree.IndexOf()
        }
        
    }
}
