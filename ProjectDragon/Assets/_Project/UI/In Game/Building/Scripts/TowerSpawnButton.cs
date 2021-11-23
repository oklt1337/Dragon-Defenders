using Deck_Cards.Cards.UnitCard.Scripts;
using Deck_Cards.Decks.Scripts;
using GamePlay.GameManager.Scripts;
using Photon.Pun;
using TMPro;
using UI.Managers.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.In_Game.Building.Scripts
{
    public class TowerSpawnButton : MonoBehaviourPun, IPointerClickHandler, IBeginDragHandler, IDragHandler,
        IEndDragHandler, ICanvas
    {
        [SerializeField] private UnitCard unit;
        [SerializeField] private Camera buildCam;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI prototypeText;

        #region Unity Methods

        private void Start()
        {
            UpdateUnit(GameManager.DefaultDeck);
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

        #region Interface Methods

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //TODO: Instantiate transparent unit. (Not Photon Instantiate!)
        }

        public void OnDrag(PointerEventData eventData)
        {
            //TODO: Move transparent unit with courser.
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (GameManager.Instance.PlayerModel.Money < unit.GoldCost)
                return;

            Vector3 screenPos = Input.mousePosition;
            screenPos.z = buildCam.nearClipPlane;
            Ray ray = buildCam.ScreenPointToRay(screenPos);


            Physics.Raycast(ray, out RaycastHit hit);

            if(hit.collider == null)
                return;

            if (!hit.collider.CompareTag("Ground"))
                return;

            if (!GameManager.Instance.PlayerModel.ModifyMoney(-unit.GoldCost))
                return;

            if (!GameManager.Instance.UnitManager.AddPlacedUnit(unit))
            {
                // Give the player his money back.
                GameManager.Instance.PlayerModel.ModifyMoney(unit.GoldCost);
                return;
            }
                

            // Do the spawning when everything works out.
            var tower = PhotonNetwork.Instantiate(unit.PrefabPath, hit.point, Quaternion.identity).GetComponent<Unit>();
            tower.Initialize(unit);
            tower.transform.parent = GameManager.Instance.UnitManager.transform;
        }

        #endregion

        public void ChangeInteractableStatus(bool status)
        {
            
        }

        public void OnClick()
        {
            GameManager.Instance.BuildHUD.UnitDisplayPanel.Initialize(unit);
        }

        /// <summary>
        /// Updates the unit and the displayed info.
        /// </summary>
        /// <param name="deck"></param>
        private void UpdateUnit(Deck deck)
        {
            unit = deck.UnitCards[(int.Parse(name))];
            image.sprite = unit.Icon;

            // Only for prototype.
            prototypeText.text = unit.CardName;
        }
    }
}