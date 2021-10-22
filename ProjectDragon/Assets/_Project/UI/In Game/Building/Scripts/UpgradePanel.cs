using System.Collections.Generic;
using _Project.SkillSystem.SkillTree;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.In_Game.Building.Scripts
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private List<Image> skillImages = new List<Image>();
        [SerializeField] private Sprite missingSprite;

        private SkillTree skillTree;

        #region Public Methods

        /// <summary>
        /// Upgrades the skill tree.
        /// </summary>
        /// <param name="skillTree"></param>
        public void UpdateSkillTree(SkillTree skillTree)
        {
            this.skillTree = skillTree;
            
            UpdateImages();
        }

        /// <summary>
        /// Upgrades the skill if possible.
        /// </summary>
        /// <param name="btn"> The Button that was pressed. </param>
        public void OnClick(Button btn)
        {
            string buttonName = btn.gameObject.name;
            
            if(!skillTree.tree[buttonName].IsLearnable)
                return;
            
            //TODO: Money Check and using that money.
            
            skillTree.tree[buttonName].EnableSkill();
            UpdateImages();
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Updates the all Images. 
        /// </summary>
        private void UpdateImages()
        {
            for (int i = 0; i < skillImages.Count; i++)
            {
                string key = (i + 1).ToString();

                // Fail check.
                if (skillTree.tree[key].Sprite == null)
                {
                    skillImages[i].sprite = missingSprite;
                    continue;
                }

                // Update the sprite.
                skillImages[i].sprite = skillTree.tree[key].Sprite;

                // Make the image grey when the skill was neither learned nor is learnable.
                if (skillTree.tree[key].IsLearnable || skillTree.tree[key].IsSkillActive)
                    skillImages[i].color = Color.white;
                else
                    skillImages[i].color = Color.gray;
            }
        }

        #endregion
    }
}