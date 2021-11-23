using System;
using System.Collections.Generic;
using Deck_Cards.Cards.UnitCard.Scripts;
using TMPro;
using UI.Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI.In_Game.Building.Scripts
{
    public class UnitDisplayPanel : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button button;
        [SerializeField] private Image unitImage;
        [SerializeField] private TextMeshProUGUI unitName;
        [SerializeField] private TextMeshProUGUI faction;
        [SerializeField] private TextMeshProUGUI @class;
        [SerializeField] private TextMeshProUGUI limit;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI spell;
        [SerializeField] private List<Image> skillImages;
        [SerializeField] private List<TextMeshProUGUI> skillText;

        private void Awake()
        {
            button.onClick.AddListener(Disable);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(UnitCard card)
        {
            gameObject.SetActive(true);

            unitImage.sprite = card.Icon;
            unitName.text = string.Concat("Name: ", card.CardName);
            faction.text = string.Concat("Faction: ", card.Faction);
            @class.text = string.Concat("Class: ", card.Class);
            limit.text = string.Concat("Limit: ", card.Limit);
            description.text = string.Concat("Description: ", card.Description);
            spell.text = string.Concat("Spell: ", card.AbilityDataBase.Abilities[0].Description);

            for (var i = 0; i < skillImages.Count; i++)
            {
                skillImages[i].sprite = card.SkillTreeObj.NodeObjs[i].Icon;
                skillText[i].text = string.Concat(card.SkillTreeObj.NodeObjs[i].NodeName,": ", card.SkillTreeObj.NodeObjs[i].Description);
            }
        }

        public void ChangeInteractableStatus(bool status)
        {
            button.interactable = status;
        }
    }
}