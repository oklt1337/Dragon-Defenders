using System.Collections.Generic;
using GamePlay.GameManager.Scripts;
using SkillSystem.Nodes.Scripts;
using SkillSystem.SkillTree.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.In_Game.Building.Scripts
{
    public class UpgradePanel : MonoBehaviour, ICanvas
    {
        #region Serialized Fields

        [Header("Base Stiff")] 
        [SerializeField] private List<Button> skillButtons = new List<Button>();
        [SerializeField] private Sprite missingSprite;

        [Header("Commander Stuff")]
        [SerializeField] private GameObject commanderPanel;
        [SerializeField] private List<Image> commanderSkillImages = new List<Image>();

        [Header("Unit Stuff")] 
        [SerializeField] private List<Image> unitSkillImages = new List<Image>();
        [SerializeField] private GameObject unitPanel;

        #endregion

        #region Private Fields
        
        private SkillTree skillTree;

        #endregion

        #region Unity Methods

        private void Update()
        {
            CheckClosing();
        }

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
            // Turning them of for fail save reasons.
            commanderPanel.SetActive(false);
            unitPanel.SetActive(false);

            skillTree = newSkillTree;

            if (skillTree.Nodes.Count > 6)
            {
                commanderPanel.gameObject.SetActive(true);
            }
            else
            {
                unitPanel.gameObject.SetActive(true);
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

            if (skillTree.Nodes.Count > 6)
            {
                // Checks if player has reached the appropriate wave for the level up. 
                if (skillTree.Nodes[index].NodeObj.Cost > GameManager.Instance.WaveManager.CurrentWaveIndex)
                    return;
            }
            else
            {
                // Checks if player has enough money for the Level up and takes that money.
                if (skillTree.Nodes[index].NodeObj.Cost > GameManager.Instance.PlayerModel.Money)
                    return;

                GameManager.Instance.PlayerModel.ModifyMoney(-skillTree.Nodes[index].NodeObj.Cost);
            }

            skillTree.SetNodeActive(index);
            UpdateImages();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the all Images. 
        /// </summary>
        private void UpdateImages()
        {
            var activeSkillImages = skillTree.Nodes.Count > 6 ? commanderSkillImages : unitSkillImages;

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
                if (skillTree.Nodes[key].NodeState == NodeState.Learnable ||
                    skillTree.Nodes[key].NodeState == NodeState.Activated)
                    activeSkillImages[i].color = Color.white;
                else
                    activeSkillImages[i].color = Color.gray;
            }
        }

        /// <summary>
        /// Checks if the player wants to close the panel.
        /// </summary>
        private void CheckClosing()
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
                return;

            commanderPanel.SetActive(false);
            unitPanel.SetActive(false);
            gameObject.SetActive(false);
        }

        #endregion
    }
}