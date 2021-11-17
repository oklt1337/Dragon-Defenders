using System;
using System.Collections.Generic;
using SkillSystem.SkillTree.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.In_Game.Building.Scripts
{
    public class UpgradePanel : MonoBehaviour, ICanvas
    {
        [SerializeField] private List<Button> skillButtons = new List<Button>();
        [SerializeField] private List<Image> commanderSkillImages = new List<Image>();
        [SerializeField] private List<Image> unitSkillImages = new List<Image>();
        [SerializeField] private Sprite missingSprite;
        [SerializeField] private GameObject commanderPanel;
        [SerializeField] private GameObject unitPanel;

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
            foreach (var button in skillButtons)
            {
                button.interactable = status;
            }
        }

        /// <summary>
        /// Upgrades the skill tree.
        /// </summary>
        /// <param name="newSkillTree">SkillTree</param>
        public void UpdateSkillTree(SkillTree newSkillTree)
        {
            skillTree = newSkillTree;
            
            if (skillTree.Nodes.Keys.Count > 6)
            {
                commanderPanel.gameObject.SetActive(true);
            }
            else
            {
                unitPanel.gameObject.SetActive(false);
            }
            
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
            var activeSkillImages = skillTree.Nodes.Keys.Count > 6 ? commanderSkillImages : unitSkillImages;

            for (int i = 0; i < activeSkillImages.Count; i++)
            {
                var key = (i + 1);

                // Fail check.
                if (skillTree.Nodes[key].NodeObj.Icon == null)
                {
                    activeSkillImages[i].sprite = missingSprite;
                    continue;
                }

                // Update the sprite.
                activeSkillImages[i].sprite = skillTree.Nodes[key].NodeObj.Icon;

                // Make the image grey when the skill was neither learned nor is learnable.
                if (skillTree.Nodes[key].NodeState == NodeState.Learnable || skillTree.Nodes[key].NodeState == NodeState.Activated)
                    activeSkillImages[i].color = Color.white;
                else
                    activeSkillImages[i].color = Color.gray;
            }
        }

        #endregion

    }
}