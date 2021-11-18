using System;
using Deck_Cards.Cards.UnitCard.Scripts;
using Deck_Cards.Decks.Scripts;
using GamePlay.GameManager.Scripts;
using Photon.Pun;
using UI.Managers.Scripts;
using Units.Unit.BaseUnits;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.In_Game.Building.Scripts
{
    public class TowerSpawnButton : MonoBehaviourPun, IPointerClickHandler, IBeginDragHandler, IDragHandler,
        IEndDragHandler, ICanvas
    {
        [SerializeField] private UnitCard unit;
        [SerializeField] private Camera buildCam;

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

            if (!hit.collider.CompareTag("Ground")) 
                return;
            
            // Do the spawning when everything works out.
            var tower = PhotonNetwork.Instantiate(unit.PrefabPath, hit.point, Quaternion.identity).GetComponent<Unit>();
            tower.Initialize(unit);
            
            if (!GameManager.Instance.PlayerModel.ModifyMoney(-unit.GoldCost))
            {
                Destroy(tower.gameObject);
            }
        }

        #endregion

        public void ChangeInteractableStatus(bool status)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUnit(Deck deck)
        {
            unit = deck.UnitCards[(int.Parse(name))];
        }
    }
}