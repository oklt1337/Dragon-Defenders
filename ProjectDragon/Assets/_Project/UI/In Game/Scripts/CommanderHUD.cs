using System;
using _Project.UI.Managers.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.In_Game.Scripts
{
    public class CommanderHUD : MonoBehaviour,ICanvas
    {
        [SerializeField] private Button[] abilities;
        [SerializeField] private Slider commanderHealth;
        [SerializeField] private Slider commanderMana;

        public void ChangeInteractableStatus(bool status)
        {
            foreach (var t in abilities)
            {
                t.interactable = status;
            }

            commanderHealth.interactable = status;
            commanderMana.interactable = status;
        }
    }
}
