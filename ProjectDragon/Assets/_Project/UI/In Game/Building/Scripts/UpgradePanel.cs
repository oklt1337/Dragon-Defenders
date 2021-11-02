using System.Collections.Generic;
using _Project.SkillSystem.SkillTree.Scripts;
using _Project.UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.In_Game.Building.Scripts
{
    public class UpgradePanel : MonoBehaviour, ICanvas
    {
        [SerializeField] private List<Image> skillImages = new List<Image>();
        [SerializeField] private Sprite missingSprite;

        private SkillTree skillTree;

        #region Unity Methods

        private void OnEnable()
        {
            CanvasManager.Instance.Subscribe(this);
        }

        private void OnDisable()
        {
            CanvasManager.Instance.Unsubscribe(this);
        }

        #endregion
        
        #region Public Methods
        
        public void ChangeInteractableStatus(bool status)
        {
            throw new System.NotImplementedException();
        }

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
            int.TryParse(buttonName, out var index);

            if (skillTree.Nodes[index].NodeState != NodeState.Learnable) 
                return;
            
            skillTree.SetNodeActive(index);
            UpdateImages();

            //TODO: Money Check and using that money.

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
                var key = (i + 1);

                // Fail check.
                if (skillTree.Nodes[key].Icon == null)
                {
                    skillImages[i].sprite = missingSprite;
                    continue;
                }

                // Update the sprite.
                skillImages[i].sprite = skillTree.Nodes[key].Icon;

                // Make the image grey when the skill was neither learned nor is learnable.
                if (skillTree.Nodes[key].NodeState == NodeState.Learnable || skillTree.Nodes[key].NodeState == NodeState.Activated)
                    skillImages[i].color = Color.white;
                else
                    skillImages[i].color = Color.gray;
            }
        }

        #endregion

    }
}