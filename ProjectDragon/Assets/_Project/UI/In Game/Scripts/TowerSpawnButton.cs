using _Project.GamePlay.GameManager.Scripts;
using _Project.Units.Unit.BaseUnits;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.UI.In_Game.Scripts
{
    public class TowerSpawnButton : MonoBehaviourPun, IPointerClickHandler, IBeginDragHandler, IDragHandler,
        IEndDragHandler
    {
        [SerializeField] private Unit unit;
        [SerializeField] private Camera buildCam;

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
            if (GameManager.Instance.PlayerModel.Money < unit.Cost)
                return;

            Vector3 screenPos = Input.mousePosition;
            screenPos.z = buildCam.nearClipPlane;
            Ray ray = buildCam.ScreenPointToRay(screenPos);


            Physics.Raycast(ray, out RaycastHit hit);

            if (!hit.collider.CompareTag("Ground")) 
                return;
            
            // Do the spawning when everything works out.
            PhotonNetwork.Instantiate(string.Concat(unit.UnitPathName, unit.name), hit.point, Quaternion.identity);
            GameManager.Instance.PlayerModel.AddMoney(-unit.Cost);
        }

        #endregion

        public void UpdateUnitName(Unit newUnit)
        {
            unit = newUnit;
        }
    }
}