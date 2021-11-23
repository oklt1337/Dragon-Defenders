using System.Collections.Generic;
using GamePlay.GameManager.Scripts;
using SkillSystem.Nodes.BaseNodes.Scripts;
using SkillSystem.SkillTree.Scripts;
using UI.Managers.Scripts;
using UnityEngine;
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
        [SerializeField] private Image unitIcon;
        [SerializeField] private List<Image> unitSkillImages = new List<Image>();
        [SerializeField] private GameObject unitPanel;

        #endregion

        #region Private Fields
        
        private SkillTree skillTree;

        #endregion

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
        public void UpdateSkillTree(SkillTree newSkillTree, Sprite img)
        {
            // Closing the settings to make sure both aren't open at the same time.
            InGameCanvasManager.Scripts.InGameCanvasManager.Instance.InGameSettingsScreen.OnCloseSettingsClick();
            
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
                unitIcon.sprite = img;
            }

            UpdateImages();
        }

        /// <summary>
        /// Upgrades the skill if possible.
        /// </summary>
        /// <param name="btn"> The Button that was pressed. </param>
        public void OnClick(Button btn)
        {
            var buttonName = btn.gameObject.name;
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
            
            AudioManager.Scripts.AudioManager.Instance.PlayAudioClip(AudioManager.Scripts.AudioManager.Instance.UiSound[2]);
            skillTree.SetNodeActive(index);
            UpdateImages();
        }
        
        /// <summary>
        /// Checks if the player wants to close the panel.
        /// </summary>
        public void Close()
        {
            commanderPanel.SetActive(false);
            unitPanel.SetActive(false);
            gameObject.SetActive(false);
            skillTree = null;
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
                // Fail check.
                activeSkillImages[i].sprite = skillTree.Nodes[i].NodeObj.Icon == null ? missingSprite : skillTree.Nodes[i].NodeObj.Icon;

                // Make the image grey when the skill was neither learned nor is learnable.
                if (skillTree.Nodes[i].NodeState == NodeState.Learnable ||
                    skillTree.Nodes[i].NodeState == NodeState.Activated)
                    activeSkillImages[i].color = Color.white;
                else
                    activeSkillImages[i].color = Color.black;
            }
        }

        #endregion
    }
}