using System.Collections.Generic;
using _Project.SkillSystem.BaseSkills;
using UnityEngine;

namespace _Project.SkillSystem.SkillDataBases
{
    [CreateAssetMenu(menuName="Tools/Skills/SkillMultipleStatChangeDataBase")]
    public class SkillMultipleStatChangeDataBase : SkillDataBase
    {
        [SerializeField] protected List<SkillHolder> skillHolderList;

        public List<SkillHolder> SkillHolderList => skillHolderList;
    }
}
