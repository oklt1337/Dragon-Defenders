using _Project.GamePlay.Player.Commander.BaseCommanderClass.Scripts;
using _Project.SkillSystem.SkillTree;
using _Project.Units.Unit.BaseUnits;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.In_Game.Scripts
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private Button[] skills;
        [SerializeField] private Image[] skillImages;
        [SerializeField] private Sprite a;

        private SkillTree _skillTree;
        
        public void UpdateSkillTree(SkillTree skillTree)
        {
            _skillTree = skillTree;
            InitializeInfos();
        }

        private void InitializeInfos()
        {
            for (int i = 0; i < skillImages.Length; i++)
            {
                string key = (i + 1).ToString();
                
                if (_skillTree.tree[key].Sprite == null)
                {
                    skillImages[i].sprite = a;
                    continue;
                }
                skillImages[i].sprite = _skillTree.tree[key].Sprite;
            }
        }
    }
}
