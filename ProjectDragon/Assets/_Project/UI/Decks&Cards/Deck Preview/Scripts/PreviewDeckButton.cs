using System;
using Deck_Cards.Cards.BaseCards.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Deck_Preview.Scripts
{
    public class PreviewDeckButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;
        
        public BaseCard Card { get; private set; }
        public Button Button => button;
        public TextMeshProUGUI Text => text;

        /// <summary>
        /// Sets the Card of the Preview Deck Button.
        /// </summary>
        /// <param name="newCard"></param>
        public void SetCard(BaseCard newCard)
        {
            Card = newCard;

            if (Card != null)
            {
                image.sprite = Card.icon;
                text.text = Card.CardName;
            }
            else
            {
                image.sprite = null;
                text.text = "(Empty)";
            }
        }
    }
}
