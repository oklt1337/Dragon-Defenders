using Deck_Cards.Cards.UnitCard.Scripts;
using Deck_Cards.Decks.Scripts;
using GamePlay.GameManager.Scripts;
using Photon.Pun;
using TMPro;
using UI.Managers.Scripts;
using Units.Unit.BaseUnits;
using Unity.Mathematics;
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

        private GameObject previewUnit;
        private bool canPlace;

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
            // Spawns preview unit.
            previewUnit = (GameObject)Instantiate
                (Resources.Load(unit.PrefabPath),Input.mousePosition,quaternion.identity,GameManager.Instance.UnitManager.transform);

            // fml
            previewUnit.layer = 2;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Moves the preview.
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = buildCam.nearClipPlane;
            Ray ray = buildCam.ScreenPointToRay(screenPos);
            
            Physics.Raycast(ray, out RaycastHit hit);
            
            previewUnit.transform.position = hit.point;
            
            // Checks if position is placeable. 
            if (hit.collider != null && hit.collider.CompareTag("Ground"))
            {
                canPlace = true;
            }
            else
            {
                canPlace = false;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(previewUnit);
            
            if(!canPlace)
                return;
            
            if (GameManager.Instance.PlayerModel.Money < unit.GoldCost)
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
            var tower = PhotonNetwork.Instantiate(unit.PrefabPath, previewUnit.transform.position, Quaternion.identity).GetComponent<Unit>();
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