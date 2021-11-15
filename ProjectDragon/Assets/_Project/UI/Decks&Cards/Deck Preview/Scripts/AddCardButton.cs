using Deck_Cards.Cards.BaseCards.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Deck_Preview.Scripts
{
    public class AddCardButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;
        
        [SerializeField] private BaseCard card;
        
        public Button Button => button;
        public BaseCard Card => card;

        private void Start()
        {
            if(Card == null)
                return;
                
            image.sprite = Card.Icon;
            text.text = Card.CardName;
        }

        /// <summary>
        /// Will be of use when we actually unlock cards and stuff.
        /// </summary>
        /// <param name="newCard"></param>
        public void SetCard(BaseCard newCard)
        {
            card = newCard;
            
            if (Card != null)
            {
                image.sprite = Card.icon;
            }
            else
            {
                
            }
        }
    }
}
