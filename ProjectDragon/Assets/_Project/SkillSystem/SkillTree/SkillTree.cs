using System;
using System.Collections.Generic;
using System.IO;
using _Project.Abilities.Ability.BaseScripts.BaseAbilities;
using _Project.SkillSystem.BaseSkills;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Project.SkillSystem.SkillTree
{
    /// <summary>
    /// Author: Peter Luu
    /// </summary>
    [CreateAssetMenu(menuName="Tools/SkillTree")]
    public class SkillTree : SerializedScriptableObject
    {
        public Dictionary<string, Skill> tree;
        public int maxLayers = 2;
        public int MAXLayers => maxLayers;

        public bool InitSkillTreeSkills()
        {
            foreach (var skill in tree)
            {
                skill.Value.Init();
                if (skill.Key == "0"|| skill.Key == "1")
                {
                    skill.Value.IsLearnable = true;
                }
            }

            return true;
        }
        
        //leave it for now because it could  change anytime
        public bool EnableSkillThroughSkillTree(String skillTreeKey)
        {
            if (!tree[skillTreeKey].EnableSkill()) return false;

            if (skillTreeKey == "0")
            {
                tree["2"].IsLearnable = true;
                tree["3"].IsLearnable = true;
            }
            if (skillTreeKey == "1")
            {
                tree["3"].IsLearnable = true;   
                tree["4"].IsLearnable = true;  
            }
            return true;
        }
    }
}
