using _Project.SkillSystem.SkillTree;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.In_Game.Building.Scripts
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private Image[] skillImages;
        [SerializeField] private Sprite a;

        private SkillTree _skillTree;

        #region Public Methods

        /// <summary>
        /// Upgrades the skill tree.
        /// </summary>
        /// <param name="skillTree"></param>
        public void UpdateSkillTree(SkillTree skillTree)
        {
            _skillTree = skillTree;
            
            UpdateImages();
        }

        /// <summary>
        /// Upgrades the skill if possible.
        /// </summary>
        /// <param name="btn"> The Button that was pressed. </param>
        public void OnClick(Button btn)
        {
            string buttonName = btn.gameObject.name;
            
            if(!_skillTree.tree[buttonName].IsLearnable)
                return;
            
            //TODO: Money Check and using that money.
            
            _skillTree.tree[buttonName].EnableSkill();
            UpdateImages();
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Updates the all Images. 
        /// </summary>
        private void UpdateImages()
        {
            for (int i = 0; i < skillImages.Length; i++)
            {
                string key = (i + 1).ToString();

                // Fail check.
                if (_skillTree.tree[key].Sprite == null)
                {
                    skillImages[i].sprite = a;
                    continue;
                }

                // Update the sprite.
                skillImages[i].sprite = _skillTree.tree[key].Sprite;

                // Make the image grey when the skill was neither learned nor is learnable.
                if (_skillTree.tree[key].IsLearnable || _skillTree.tree[key].IsSkillActive)
                    skillImages[i].color = Color.white;
                else
                    skillImages[i].color = Color.gray;
            }
        }

        #endregion
    }
}