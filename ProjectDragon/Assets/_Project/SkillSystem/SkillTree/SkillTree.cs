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
        public int maxLayers = 3;
        public int MAXLayers => maxLayers;
    }
}
